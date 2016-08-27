﻿using Leak.Core.Cando.Metadata;
using Leak.Core.Common;
using Leak.Core.Messages;

namespace Leak.Core.Client
{
    public abstract class PeerClientCallbackBase : PeerClientCallback
    {
        public virtual void OnFileScheduled(FileHash hash)
        {
        }

        public virtual void OnFileDiscovered(FileHash hash)
        {
        }

        public virtual void OnFileInitialized(FileHash hash, PeerClientMetainfo summary)
        {
        }

        public virtual void OnFileStarted(FileHash hash)
        {
        }

        public virtual void OnFileCompleted(FileHash hash)
        {
        }

        public virtual void OnPeerConnecting(FileHash hash, PeerAddress peer)
        {
        }

        public virtual void OnPeerConnected(FileHash hash, PeerClientConnected connected)
        {
        }

        public virtual void OnPeerRejected(FileHash hash, PeerAddress peer)
        {
        }

        public virtual void OnPeerDisconnected(FileHash hash, PeerHash peer)
        {
        }

        public virtual void OnPeerHandshake(FileHash hash, PeerEndpoint endpoint)
        {
        }

        public virtual void OnPeerIncomingMessage(FileHash hash, PeerHash peer, PeerClientMessage message)
        {
        }

        public virtual void OnPeerOutgoingMessage(FileHash hash, PeerHash peer, PeerClientMessage message)
        {
        }

        public virtual void OnPeerBitfield(FileHash hash, PeerHash peer, Bitfield bitfield)
        {
        }

        public virtual void OnPeerChoked(FileHash hash, PeerHash peer)
        {
        }

        public virtual void OnPeerUnchoked(FileHash hash, PeerHash peer)
        {
        }

        public virtual void OnBlockReceived(FileHash hash, PeerHash peer, Piece piece)
        {
        }

        public virtual void OnPieceVerified(FileHash hash, PeerClientPieceVerification verification)
        {
        }

        public virtual void OnMetadataReceived(FileHash hash, PeerHash peer, MetadataData data)
        {
        }
    }
}