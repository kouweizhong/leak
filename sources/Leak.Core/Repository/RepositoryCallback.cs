﻿using Leak.Core.Common;
using Leak.Core.Messages;

namespace Leak.Core.Repository
{
    public interface RepositoryCallback
    {
        /// <summary>
        /// Called when the resource was allocated in the file system.
        /// </summary>
        /// <param name="hash">The hash of the affected resource.</param>
        void OnAllocated(FileHash hash);

        /// <summary>
        /// Called when the resource was verified against metadata.
        /// </summary>
        /// <param name="hash">The hash of the affected resource.</param>
        /// <param name="bitfield">The bitfield indicating the progress.</param>
        void OnVerified(FileHash hash, Bitfield bitfield);

        /// <summary>
        /// Called when the piece was verified against metadata and accepted.
        /// </summary>
        /// <param name="hash">The hash of the affected resource.</param>
        /// <param name="piece">The accepted piece.</param>
        void OnAccepted(FileHash hash, RepositoryPiece piece);

        /// <summary>
        /// Called when the piece was verified against metadata and rejected.
        /// </summary>
        /// <param name="hash">The hash of the affected resource.</param>
        /// <param name="piece">The accepted piece.</param>
        void OnRejected(FileHash hash, RepositoryPiece piece);

        /// <summary>
        /// Called when the block was written onto the file system.
        /// </summary>
        /// <param name="hash">The hash of the affected resource.</param>
        /// <param name="block">The written block.</param>
        void OnWritten(FileHash hash, RepositoryBlock block);
    }
}