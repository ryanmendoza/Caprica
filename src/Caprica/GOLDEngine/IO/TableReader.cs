using System;
using System.Globalization;
using System.IO;
using System.Resources;
using Caprica.GOLDEngine.Parsing.Exceptions;

namespace Caprica.GOLDEngine.IO
{
    /// <inheritdoc />
    /// <summary>
    ///     TODO
    /// </summary>
    internal class TableReader : IDisposable
    {
        private const byte KRecordContentMulti = 77;

        private BinaryReader _binaryReader;

        /// <summary>
        ///     Flag that holds whether this <see cref="T:Caprica.GOLDEngine.IO.TableReader" /> was called to dispose.
        /// </summary>
        private bool _isDisposed;

        public int EntryCount
        {
            get;
            private set;
        }

        public int EntriesRead
        {
            get;
            private set;
        }

        public string Header
        {
            get;
            private set;
        }

        #region IDisposable Members

        /// <inheritdoc />
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        ///     Finalizes an instance of the <see cref="T:Caprica.GOLDEngine.IO.TableReader" /> class. Releases unmanaged
        ///     resources and performs other cleanup operations before the <see cref="T:Caprica.GOLDEngine.IO.TableReader" />
        ///     is reclaimed by garbage collection.
        /// </summary>
        ~TableReader()
        {
            Dispose(false);
        }

        public bool EndOfFile()
        {
            var baseStream = _binaryReader.BaseStream;

            return baseStream.Position == baseStream.Length;
        }

        public bool GetNextRecord()
        {
            bool success;

            // Finish current record.
            while (EntriesRead < EntryCount)
            {
                RetrieveEntry();
            }

            // Start next record.
            var id = _binaryReader.ReadByte();

            if (id == KRecordContentMulti)
            {
                EntryCount = RawReadUInt16();

                EntriesRead = 0;

                success = true;
            }
            else
            {
                success = false;
            }

            return success;
        }

        public void Open(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            Open(new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read)));
        }

        public void Open(Type type, string name)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var stream = type.Assembly.GetManifestResourceStream(name);

            if (stream == null)
            {
                throw new MissingManifestResourceException(string.Format(CultureInfo.InvariantCulture, "Cannot open the manifest resource stream named '{0}' for type '{1}'.", name, type));
            }

            Open(new BinaryReader(stream));
        }

        public void Open(BinaryReader binaryReader)
        {
            _binaryReader = binaryReader ?? throw new ArgumentNullException(nameof(binaryReader));

            EntryCount = 0;

            EntriesRead = 0;

            Header = RawReadCString();
        }

        public bool RecordComplete()
        {
            return EntriesRead >= EntryCount;
        }

        public bool RetrieveBoolean()
        {
            return Retrieve<bool>(EntryType.Boolean);
        }

        public T RetrieveByte<T>()
        {
            return Retrieve<T>(EntryType.Byte);
        }

        public Entry RetrieveEntry()
        {
            var result = new Entry();

            if (RecordComplete())
            {
                result.Type = EntryType.Empty;

                result.Value = string.Empty;
            }
            else
            {
                EntriesRead++;

                var type = result.Type = (EntryType) _binaryReader.ReadByte();

                switch (type)
                {
                    case EntryType.Boolean:
                        result.Value = _binaryReader.ReadByte() == 1;

                        break;

                    case EntryType.Byte:
                        result.Value = _binaryReader.ReadByte();

                        break;

                    case EntryType.Empty:
                        result.Value = string.Empty;

                        break;

                    case EntryType.String:
                        result.Value = RawReadCString();

                        break;

                    case EntryType.UInt16:
                        result.Value = RawReadUInt16();

                        break;

                    default:
                        result.Type = EntryType.Error;

                        result.Value = string.Empty;

                        break;
                }
            }

            return result;
        }

        public T RetrieveInt16<T>()
        {
            return Retrieve<T>(EntryType.UInt16);
        }

        public string RetrieveString()
        {
            return Retrieve<string>(EntryType.String);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources of this <see cref="T:Caprica.GOLDEngine.IO.TableReader" /> instance.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                if (_binaryReader == null)
                {
                    return;
                }

                _binaryReader.Close();

                _binaryReader.Dispose();

                _binaryReader = null;
            }

            _isDisposed = true;
        }

        private string RawReadCString()
        {
            var done = false;

            var text = string.Empty;

            while (!done)
            {
                var char16 = RawReadUInt16();

                if (char16 == 0)
                {
                    done = true;
                }
                else
                {
                    text += (char) char16;
                }
            }

            return text;
        }

        private int RawReadUInt16()
        {
            // Read a uint in little endian. This is the format already used
            // by the .NET BinaryReader. However, it is good to specificially
            // define this given byte order can change depending on platform.

            int b0 = _binaryReader.ReadByte(); // Least significant byte first.

            int b1 = _binaryReader.ReadByte();

            var result = (b1 << 8) + b0;

            return result;
        }

        private T Retrieve<T>(EntryType type)
        {
            var entry = RetrieveEntry();

            if (entry.Type == type)
            {
                return (T) entry.Value;
            }

            throw new ParserException(entry.Type, _binaryReader);
        }
    }
}