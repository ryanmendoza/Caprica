using System;

namespace Caprica.Abstractions
{
    /// <inheritdoc />
    /// <summary>
    ///     Defines a base class which holds reference of one or more resourceful object.
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        /// <summary>
        ///     Flag that holds whether this <see cref="T:Caprica.Abstractions.Disposable">instance</see> was called to dispose.
        /// </summary>
        private bool isDisposed;

        #region IDisposable Members

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
        ///     Finalizes an instance of the <see cref="T:Caprica.Abstractions.Disposable" /> class. Releases unmanaged 
        ///     resources and performs other cleanup operations before the <see cref="T:Caprica.Abstractions.Disposable" /> 
        ///     is reclaimed by garbage collection.
        /// </summary>
        ~Disposable()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Disposes the resources of this <see cref="T:Caprica.Abstractions.Disposable">instance</see>.
        /// </summary>
        protected virtual void DisposeCore()
        {
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources of this <see cref="T:Caprica.Abstractions.Disposable">instance</see>.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }

            isDisposed = true;
        }
    }
}