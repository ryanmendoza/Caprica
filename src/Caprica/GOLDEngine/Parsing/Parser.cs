using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Caprica.Extensions;
using Caprica.GOLDEngine.IO;
using Caprica.GOLDEngine.Parsing.Exceptions;
using Caprica.GOLDEngine.Parsing.Groups;

namespace Caprica.GOLDEngine.Parsing
{
    /// <inheritdoc />
    /// <summary>
    ///     This is the main class in the GOLD Parser Engine and is used to perform
    ///     all duties required to the parsing of a source text string. This class
    ///     contains the LALR(1) State Machine code, the DFA State Machine code,
    ///     character table (used by the DFA algorithm) and all other structures and
    ///     methods needed to interact with the developer.
    /// </summary>
    public class Parser : IDisposable
    {
        #region Fields

        private readonly Stack<Token> _groupStack = new Stack<Token>();

        private readonly Stack<Token> _inputTokens = new Stack<Token>(); // Tokens to be analyzed

        private readonly Stack<Token> _stack = new Stack<Token>();

        private readonly Position _sysPosition = new Position(); // Internal - so user cannot mess with values

        private List<CharacterSet> _characterSetTable = new List<CharacterSet>();

        private int _currentLalr;

        private FAStateList _dfa = new FAStateList();

        private List<Group> _groupTable = new List<Group>();

        private bool _hasReduction;

        /// <summary>
        ///     Flag that holds whether this <see cref="T:Caprica.GOLDEngine.Parsing.Parser" /> was called to dispose.
        /// </summary>
        private bool _isDisposed;

        private string _lookaheadBuffer;

        private LRStateList _lrStates = new LRStateList();

        private TextReader _source;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.Parser" /> class.
        /// </summary>
        public Parser()
        {
            CurrentPosition = new Position();
            ExpectedSymbols = new List<Symbol>();
            Grammar = new Grammar();
            SymbolTable = new List<Symbol>();
            ProductionTable = new List<Production>();

            Restart();

            TablesLoaded = false;
            TrimReductions = true;
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="T:Caprica.GOLDEngine.Parsing.Parser" /> class. Releases unmanaged
        ///     resources and performs other cleanup operations before the <see cref="T:Caprica.GOLDEngine.Parsing.Parser" />
        ///     is reclaimed by garbage collection.
        /// </summary>
        ~Parser()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Currents the position (column and line) being read from the source.
        /// </summary>
        /// <returns>
        ///     The current position.
        /// </returns>
        public Position CurrentPosition
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets or sets the current reduction. When the Parse() method returns a Reduce, this method will contain the current Reduction.
        /// </summary>
        /// <value>
        ///     The current reduction.
        /// </value>
        public object CurrentReduction
        {
            get => _hasReduction ? _stack.Peek().Data : null;

            set
            {
                if (_hasReduction)
                {
                    _stack.Peek().Data = value;
                }
            }
        }

        /// <summary>
        ///     Gets the current input token. If the Parse() function returns TokenRead, this method will return that last read token.
        /// </summary>
        public Token CurrentInputToken => _inputTokens.Peek();

        /// <summary>
        ///     Gets the expected symbols. If the Parse() method returns a SyntaxError, this method will contain a list of the symbols the grammar expected to see.
        /// </summary>
        public List<Symbol> ExpectedSymbols
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the information about the current grammar.
        /// </summary>
        public Grammar Grammar
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the list of Productions recognized by the grammar.
        /// </summary>
        public List<Production> ProductionTable
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the rule text.
        /// </summary>
        /// <value>
        ///     The rule text.
        /// </value>
        public string RuleText
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the list of Symbols recognized by the grammar.
        /// </summary>
        public List<Symbol> SymbolTable
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets a value indicating whether the grammar tables have been loaded.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the grammar tables are loaded; otherwise, <c>false</c>.
        /// </value>
        public bool TablesLoaded
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether reductions will be trimmed in cases where a production contains a single element.
        /// </summary>
        /// <value>
        ///     <c>true</c> if reductions will be trimmed in cases where a production contains a single element; otherwise, <c>false</c>.
        /// </value>
        public bool TrimReductions
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Loads the tables.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>
        ///     TODO
        /// </returns>
        public bool LoadTables(Type type, string resourceName)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Restart();

            using (var tableReader = new TableReader())
            {
                tableReader.Open(type, resourceName);

                while (!tableReader.EndOfFile())
                {
                    tableReader.GetNextRecord();

                    int index;

                    string name;

                    var recordType = tableReader.RetrieveByte<RecordType>();

                    int symbolIndex;

                    switch (recordType)
                    {
                        case RecordType.CharRanges: // #, Total Sets, RESERVED, (Start#, End#  ...)
                            index = tableReader.RetrieveInt16<int>();

                            tableReader.RetrieveInt16<int>(); // Codepage

                            tableReader.RetrieveInt16<int>(); // Total

                            tableReader.RetrieveEntry(); // Reserved

                            var characterSet = _characterSetTable[index] = new CharacterSet();

                            while (!tableReader.RecordComplete())
                            {
                                var start = tableReader.RetrieveInt16<int>();

                                var end = tableReader.RetrieveInt16<int>();

                                characterSet.Add(new CharacterRange(start, end));
                            }

                            break;

                        case RecordType.DFAState: // #, Accept?, Accept#, Reserved (CharSet#, Target#, Reserved)...
                            index = tableReader.RetrieveInt16<int>();

                            var accept = tableReader.RetrieveBoolean();

                            var acceptIndex = tableReader.RetrieveInt16<int>();

                            tableReader.RetrieveEntry(); // Reserved

                            _dfa[index] = accept ? new FAState(SymbolTable[acceptIndex]) : new FAState();

                            while (!tableReader.RecordComplete()) // (Edge chars, Target#, Reserved)...
                            {
                                var setIndex = tableReader.RetrieveInt16<int>(); // Char table index

                                var target = tableReader.RetrieveInt16<int>(); // Target

                                tableReader.RetrieveEntry(); // Reserved

                                _dfa[index].Edges.Add(new FAEdge(_characterSetTable[setIndex], target));
                            }

                            break;

                        case RecordType.Group: // #, Name, Container#, Start#, End#, Tokenized, Open Ended, Reserved, Count, (Nested Group #...) 
                            var group = new Group();

                            index = tableReader.RetrieveInt16<int>(); // #

                            group.Name = tableReader.RetrieveString();
                            group.Container = SymbolTable[tableReader.RetrieveInt16<int>()];
                            group.Start = SymbolTable[tableReader.RetrieveInt16<int>()];
                            group.End = SymbolTable[tableReader.RetrieveInt16<int>()];

                            group.Advance = tableReader.RetrieveInt16<AdvanceModeType>();
                            group.Ending = tableReader.RetrieveInt16<EndingModeType>();
                            tableReader.RetrieveEntry(); // Reserved

                            for (var n = 1; n <= tableReader.RetrieveInt16<int>(); n++)
                            {
                                group.Nesting.Add(tableReader.RetrieveInt16<int>());
                            }

                            // === Link back
                            group.Container.Group = group;
                            group.Start.Group = group;
                            group.End.Group = group;

                            _groupTable[index] = group;

                            break;

                        case RecordType.InitialStates: // DFA, LALR
                            _dfa.InitialState = tableReader.RetrieveInt16<int>();

                            _lrStates.InitialState = tableReader.RetrieveInt16<int>();

                            break;

                        case RecordType.LRState: // #, Reserved (Symbol#, Action, Target#, Reserved)...
                            index = tableReader.RetrieveInt16<int>();

                            tableReader.RetrieveEntry(); // Reserved

                            var lrState = _lrStates[index] = new LRState();

                            // (Symbol#, Action, Target#, Reserved)...
                            while (!tableReader.RecordComplete())
                            {
                                symbolIndex = tableReader.RetrieveInt16<int>();

                                var action = tableReader.RetrieveInt16<LRActionType>();

                                var target = tableReader.RetrieveInt16<int>();

                                tableReader.RetrieveEntry(); // Reserved

                                lrState.Add(new LRAction(SymbolTable[symbolIndex], action, target));
                            }

                            break;

                        case RecordType.Production: // #, ID#, Reserved, (Symbol#,  ...)
                            index = tableReader.RetrieveInt16<int>();

                            var headIndex = tableReader.RetrieveInt16<int>();

                            tableReader.RetrieveEntry(); // Reserved

                            var production = ProductionTable[index] = new Production(SymbolTable[headIndex], index);

                            while (!tableReader.RecordComplete())
                            {
                                symbolIndex = tableReader.RetrieveInt16<int>();

                                production.Handle.Add(SymbolTable[symbolIndex]);
                            }

                            break;

                        case RecordType.Property: // Index, Name, Value
                            index = tableReader.RetrieveInt16<int>();

                            // ReSharper disable once RedundantAssignment
                            name = tableReader.RetrieveString(); // Not Used.

                            var value = tableReader.RetrieveString();

                            Grammar.SetProperty(index, value);

                            break;

                        case RecordType.Symbol: // #, Name, Kind
                            index = tableReader.RetrieveInt16<int>();

                            name = tableReader.RetrieveString();

                            var symbolType = tableReader.RetrieveInt16<SymbolTypes>();

                            SymbolTable[index] = new Symbol(name, symbolType, index);

                            break;

                        case RecordType.TableCounts: // CharacterSet, DFA, Group, LALR, Rule, & Symbol Counts
                            SymbolTable = new List<Symbol>().Initialize(default(Symbol), tableReader.RetrieveInt16<int>());

                            _characterSetTable = new List<CharacterSet>().Initialize(default(CharacterSet), tableReader.RetrieveInt16<int>());

                            ProductionTable = new List<Production>().Initialize(default(Production), tableReader.RetrieveInt16<int>());

                            _dfa = new FAStateList(tableReader.RetrieveInt16<int>());

                            _lrStates = new LRStateList(tableReader.RetrieveInt16<int>());

                            _groupTable = new List<Group>().Initialize(default(Group), tableReader.RetrieveInt16<int>());

                            break;

                        default: // RecordIDComment
                            throw new ParserException("File Error. A record of type '" + (char) recordType + "' was read. This is not a valid code.");
                    }
                }
            }

            TablesLoaded = true;

            return true;
        }

        /// <summary>
        ///     Opens the specified reader for parsing.
        /// </summary>
        /// <param name="ruleText">The rule text.</param>
        /// <returns>
        ///     TODO
        /// </returns>
        public bool Open(string ruleText)
        {
            Restart();

            RuleText = ruleText;

            _source = new StringReader(ruleText);

            _stack.Push(new Token
                            {
                                State = _lrStates.InitialState // Create stack top item. Only needs state.
                            });

            return true;
        }

        /// <summary>
        ///     Performs a parse action on the input. This method is typically used in a loop until either
        ///     grammar is accepted or an error occurs.
        /// </summary>
        /// <returns>
        ///     TODO
        /// </returns>
        public ParseMessageType Parse()
        {
            var message = default(ParseMessageType);

            if (!TablesLoaded)
            {
                return ParseMessageType.NotLoadedError;
            }

            // ===================================
            // Loop until breakable event
            // ===================================

            var done = false;

            while (!done)
            {
                Token token;

                if (_inputTokens.Count == 0)
                {
                    done = true;

                    message = ParseMessageType.TokenRead;

                    token = ProduceToken();

                    _inputTokens.Push(token);
                }
                else
                {
                    token = _inputTokens.Peek();

                    CurrentPosition.Copy(token.Position); // Update current position.

                    if (_groupStack.Count == 0)
                    {
                        switch (token.Parent.Type)
                        {
                            case SymbolTypes.Error: // Oh noes!
                                done = true;

                                message = ParseMessageType.LexicalError;

                                break;

                            case SymbolTypes.Noise: // Ignore.
                                _inputTokens.Pop();

                                break;

                            default:
                                var action = ParseLALR(token);

                                switch (action)
                                {
                                    case ParseResultType.Accept:
                                        done = true;

                                        message = ParseMessageType.Accept;

                                        break;

                                    case ParseResultType.InternalError:
                                        done = true;

                                        message = ParseMessageType.InternalError;

                                        break;

                                    case ParseResultType.ReduceNormal:
                                        done = true;

                                        message = ParseMessageType.Reduction;

                                        break;

                                    case ParseResultType.Shift:
                                        // ParseToken() shifted the token to the front of the Token-Queue. 
                                        // It now exists on the Token-Stack and must be eliminated from the queue.
                                        _inputTokens.Pop();

                                        break;

                                    case ParseResultType.SyntaxError:
                                        done = true;

                                        message = ParseMessageType.SyntaxError;

                                        break;
                                }

                                break;
                        }
                    }
                    else
                    {
                        done = true;

                        message = ParseMessageType.GroupError;
                    }
                }
            }

            return message;
        }

        /// <summary>
        ///     TODO
        /// </summary>
        public void Restart()
        {
            _currentLalr = _lrStates.InitialState;

            //=== Lexer
            CurrentPosition.Column = 0;
            CurrentPosition.Line = 0;
            _sysPosition.Column = 0;
            _sysPosition.Line = 0;

            _hasReduction = false;

            ExpectedSymbols.Clear();
            _inputTokens.Clear();
            _lookaheadBuffer = string.Empty;
            _stack.Clear();

            //==== V4
            _groupStack.Clear();
        }

        internal void Clear()
        {
            Grammar.Clear();
            ProductionTable.Clear();
            SymbolTable.Clear();

            _characterSetTable.Clear();
            _dfa.Clear();
            _groupStack.Clear();
            _groupTable.Clear();
            _inputTokens.Clear();
            _lrStates.Clear();
            _stack.Clear();

            Restart();
        }

        private void ConsumeBuffer(int charCount)
        {
            // Consume/Remove the characters from the front of the buffer. 

            if (charCount > _lookaheadBuffer.Length)
            {
                return;
            }

            // Count Carriage Returns and increment the internal column and line
            // numbers. This is done for the Developer and is not necessary for the
            // DFA algorithm.
            for (var n = 0; n <= charCount - 1; n++)
            {
                switch (_lookaheadBuffer[n])
                {
                    case '\n':
                        _sysPosition.Column = 0;

                        _sysPosition.Line += 1;

                        break;

                    case '\r':
                        break; // Ignore, LF is used to inc line to be UNIX friendly.

                    default:
                        _sysPosition.Column += 1;

                        break;
                }
            }

            _lookaheadBuffer = _lookaheadBuffer.Remove(0, charCount);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources of this <see cref="T:Caprica.GOLDEngine.Parsing.Parser" /> instance.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                if (_source != null)
                {
                    _source.Close();

                    _source.Dispose();

                    _source = null;
                }
            }

            _isDisposed = true;
        }

        private string Lookahead(int charIndex)
        {
            // Return single char at the index. This function will also increase 
            // buffer if the specified character is not present. It is used 
            // by the DFA algorithm.

            // Check if we must read characters from the Stream
            if (charIndex > _lookaheadBuffer.Length)
            {
                var readCount = charIndex - _lookaheadBuffer.Length;

                for (var i = 1; i <= readCount; i++)
                {
                    _lookaheadBuffer += (char) _source.Read();
                }
            }

            // If the buffer is still smaller than the index, we have reached
            // the end of the text. In this case, return a null string - the DFA
            // code will understand.
            return charIndex <= _lookaheadBuffer.Length ? _lookaheadBuffer[charIndex - 1].ToString(CultureInfo.InvariantCulture) : string.Empty;
        }

        private string LookaheadBuffer(int count)
        {
            // Return Count characters from the lookahead buffer. DO NOT CONSUME
            // This is used to create the text stored in a token. It is disgarded
            // separately. Because of the design of the DFA algorithm, count should
            // never exceed the buffer length. The If-Statement below is fault-tolerate
            // programming, but not necessary.

            if (count > _lookaheadBuffer.Length)
            {
                count = _lookaheadBuffer.Length;
            }

            return _lookaheadBuffer.Substring(0, count);
        }

        // ReSharper disable once InconsistentNaming
        private Token LookaheadDFA()
        {
            // This function implements the DFA for th parser's lexer.
            // It generates a token which is used by the LALR state
            // machine.

            var target = 0;

            var token = new Token();

            // ===================================================
            // Match DFA token
            // ===================================================

            var done = false;
            var currentDfa = _dfa.InitialState;
            var acceptPosition = 1; // Next byte in the input Stream.
            var lastAcceptState = -1; // We have not yet accepted a character string.
            var lastAcceptPosition = -1;

            var ch = Lookahead(1);

            if (!(string.IsNullOrEmpty(ch) | Convert.ToInt32(ch == null ? 0 : ch[0]) == 65535)) // NO MORE DATA
            {
                while (!done)
                {
                    // This code searches all the branches of the current DFA state
                    // for the next character in the input Stream. If found the
                    // target state is returned.

                    ch = Lookahead(acceptPosition);

                    var found = false;

                    if (!string.IsNullOrEmpty(ch))
                    {
                        var n = 0;

                        while (n < _dfa[currentDfa].Edges.Count & !found)
                        {
                            var edge = _dfa[currentDfa].Edges[n];

                            //==== Look for character in the Character Set Table
                            if (edge.Characters.Contains(Convert.ToInt32(ch[0])))
                            {
                                found = true;

                                target = edge.Target; // .TableIndex.
                            }

                            n += 1;
                        }
                    }

                    // This block-if statement checks whether an edge was found from the current state. If so, the state and current
                    // position advance. Otherwise it is time to exit the main loop and report the token found (if there was one). 
                    // If the LastAcceptState is -1, then we never found a match and the Error Token is created. Otherwise, a new 
                    // token is created using the Symbol in the Accept State and all the characters that comprise it.

                    if (found)
                    {
                        // This code checks whether the target state accepts a token.
                        // If so, it sets the appropiate variables so when the
                        // algorithm in done, it can return the proper token and
                        // number of characters.

                        // NOT is very important!
                        if (_dfa[target].Symbol != null)
                        {
                            lastAcceptPosition = acceptPosition;

                            lastAcceptState = target;
                        }

                        currentDfa = target;

                        acceptPosition += 1;
                    }
                    else // No edge found.
                    {
                        done = true;

                        if (lastAcceptState == -1) // Lexer cannot recognize symbol.
                        {
                            token.Data = LookaheadBuffer(1);

                            token.Parent = SymbolTable.FirstOrDefault(symbol => symbol.Type == SymbolTypes.Error);
                        }
                        else // Create Token, read characters.
                        {
                            token.Data = LookaheadBuffer(lastAcceptPosition); // Data contains the total number of accept characters.

                            token.Parent = _dfa[target].Symbol;
                        }
                    } // DoEvents.
                }
            }
            else
            {
                // End of file reached, create End Token.
                token.Data = string.Empty;

                token.Parent = SymbolTable.FirstOrDefault(symbol => symbol.Type == SymbolTypes.End);
            }

            // ===================================================
            // Set the new token's position information
            // ===================================================
            // Notice, this is a copy, not a linking of an instance. We don't want the user 
            // to be able to alter the main value indirectly.
            token.Position.Copy(_sysPosition);

            return token;
        }

        // ReSharper disable once InconsistentNaming
        private ParseResultType ParseLALR(Token nextToken)
        {
            // This function analyzes a token and either:
            //   1. Makes a SINGLE reduction and pushes a complete Reduction object on the stack
            //   2. Accepts the token and shifts
            //   3. Errors and places the expected symbol indexes in the Tokens list
            // The Token is assumed to be valid and WILL be checked
            // If an action is performed that requires controlt to be returned to the user, the function returns true.
            // The Message parameter is then set to the type of action.

            var parseAction = _lrStates[_currentLalr][nextToken.Parent];

            var result = default(ParseResultType);

            // Work - shift or reduce.
            if (parseAction == null)
            {
                //=== Syntax Error! Fill Expected Tokens
                ExpectedSymbols.Clear();

                foreach (var action in _lrStates[_currentLalr]) // .Count - 1
                {
                    switch (action.Symbol.Type)
                    {
                        case SymbolTypes.End:
                        case SymbolTypes.GroupEnd:
                        case SymbolTypes.GroupStart:
                        case SymbolTypes.Terminal:
                            ExpectedSymbols.Add(action.Symbol);

                            break;
                        case SymbolTypes.Error:
                            break;
                        case SymbolTypes.Noise:
                            break;
                        case SymbolTypes.Nonterminal:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                result = ParseResultType.SyntaxError;
            }
            else
            {
                _hasReduction = false; // Will be set true if a reduction is made.

                switch (parseAction.Type)
                {
                    case LRActionType.Accept:
                        _hasReduction = true;

                        result = ParseResultType.Accept;

                        break;

                    case LRActionType.Reduce:
                        Token head;

                        // Produce a reduction - remove as many tokens as members in the rule & push a nonterminal token
                        var production = ProductionTable[parseAction.Value];

                        //======== Create Reduction
                        if (TrimReductions & production.ContainsOneNonTerminal())
                        {
                            //The current rule only consists of a single nonterminal and can be trimmed from the
                            //parse tree. Usually we create a new Reduction, assign it to the Data property
                            //of Head and push it on the stack. However, in this case, the Data property of the
                            //Head will be assigned the Data property of the reduced token (i.e. the only one
                            //on the stack).
                            //In this case, to save code, the value popped of the stack is changed into the head.

                            head = _stack.Pop();

                            head.Parent = production.Head;

                            result = ParseResultType.ReduceEliminated;
                        }
                        else // Build a Reduction.
                        {
                            _hasReduction = true;

                            var newReduction = new ReductionList(production.Handle.Count)
                                               {
                                                   Parent = production
                                               };

                            for (var c = production.Handle.Count - 1; c >= 0; c += -1)
                            {
                                newReduction[c] = _stack.Pop();
                            }

                            head = new Token(production.Head, newReduction);

                            result = ParseResultType.ReduceNormal;
                        }

                        //========== Goto
                        var index = _stack.Peek().State;

                        //========= If n is -1 here, then we have an Internal Table Error!!!!
                        var n = _lrStates[index].IndexOf(production.Head);

                        if (n == -1)
                        {
                            result = ParseResultType.InternalError;
                        }
                        else
                        {
                            _currentLalr = _lrStates[index][n].Value;

                            head.State = _currentLalr;

                            _stack.Push(head);
                        }

                        break;

                    case LRActionType.Shift:
                        _currentLalr = parseAction.Value;

                        nextToken.State = _currentLalr;

                        _stack.Push(nextToken);

                        result = ParseResultType.Shift;

                        break;
                    case LRActionType.Error:
                        break;
                    case LRActionType.Goto:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return result; // Very important.
        }

        private Token ProduceToken()
        {
            // ** VERSION 5.0 **
            // This function creates a token and also takes into account the current
            // lexing mode of the parser. In particular, it contains the group logic. 
            //
            // A stack is used to track the current "group". This replaces the comment
            // level counter. Also, text is appended to the token on the top of the 
            // stack. This allows the group text to returned in one chunk.

            var done = false;

            Token result = null;

            while (!done)
            {
                var read = LookaheadDFA();

                // The logic - to determine if a group should be nested - requires that the top of the stack 
                // and the symbol's linked group need to be looked at. Both of these can be unset. So, this section
                // sets a Boolean and avoids errors. We will use this boolean in the logic chain below. 
                bool nestGroup;

                if (read.Parent.Type == SymbolTypes.GroupStart)
                {
                    nestGroup = _groupStack.Count == 0 || _groupStack.Peek().Parent.Group.Nesting.Contains(read.Parent.Group.TableIndex);
                }
                else
                {
                    nestGroup = false;
                }

                //=================================
                // Logic chain
                //=================================

                if (nestGroup)
                {
                    ConsumeBuffer(read.Data.ToString().Length);

                    _groupStack.Push(read);
                }
                else if (_groupStack.Count == 0)
                {
                    // The token is ready to be analyzed.             
                    ConsumeBuffer(read.Data.ToString().Length);

                    done = true;

                    result = read;
                }
                else if (ReferenceEquals(_groupStack.Peek().Parent.Group.End, read.Parent))
                {
                    // End the current group.
                    var pop = _groupStack.Pop();

                    //=== Ending logic.
                    if (pop.Parent.Group.Ending == EndingModeType.Closed)
                    {
                        pop.Data += read.Data.ToString(); // Append text.

                        ConsumeBuffer(read.Data.ToString().Length); // Consume token.
                    }

                    // We are out of the group. Return pop'd token (which contains all the group text)
                    if (_groupStack.Count == 0)
                    {
                        done = true;

                        pop.Parent = pop.Parent.Group.Container; // Change symbol to parent.

                        result = pop;
                    }
                    else
                    {
                        _groupStack.Peek().Data += pop.Data.ToString(); // Append group text to parent.
                    }
                }
                else if (read.Parent.Type == SymbolTypes.End) // EOF always stops the loop. The caller function (Parse) can flag a runaway group error.
                {
                    done = true;

                    result = read;
                }
                else
                {
                    // We are in a group, Append to the Token on the top of the stack.
                    // Take into account the Token group mode  
                    var top = _groupStack.Peek();

                    if (top.Parent.Group.Advance == AdvanceModeType.Token)
                    {
                        top.Data += read.Data.ToString(); // Append all text.

                        ConsumeBuffer(read.Data.ToString().Length);
                    }
                    else
                    {
                        top.Data += read.Data.ToString()[0].ToString(CultureInfo.InvariantCulture); // Append one character.

                        ConsumeBuffer(1);
                    }
                }
            }

            return result;
        }

        #endregion
    }
}