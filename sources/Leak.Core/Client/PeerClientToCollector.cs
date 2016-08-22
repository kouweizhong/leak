﻿using Leak.Core.Cando.Metadata;
using Leak.Core.Collector;
using Leak.Core.Common;
using Leak.Core.Messages;

namespace Leak.Core.Client
{
    public class PeerClientToCollector : PeerCollectorCallbackBase
    {
        private readonly PeerClientContext context;

        public PeerClientToCollector(PeerClientContext context)
        {
            this.context = context;
        }

        public override void OnConnecting(FileHash hash, PeerAddress peer)
        {
            if (hash != null)
            {
                context.Callback.OnPeerConnecting(hash, peer);
            }
        }

        public override void OnConnected(PeerCollectorConnected connected)
        {
            if (connected.Hash != null)
            {
                context.Callback.OnPeerConnected(connected.Hash, new PeerClientConnected(connected));
            }
        }

        public override void OnRejected(PeerAddress peer)
        {
        }

        public override void OnDisconnected(PeerSession session)
        {
            context.Callback.OnPeerDisconnected(session.Hash, session.Peer);
        }

        public override void OnHandshake(PeerEndpoint endpoint)
        {
            context.Callback.OnPeerHandshake(endpoint.Session.Hash, endpoint);
        }

        public override void OnIncoming(PeerEndpoint endpoint, PeerCollectorMessage message)
        {
        }

        public override void OnOutgoing(PeerEndpoint endpoint, PeerCollectorMessage message)
        {
        }

        public override void OnBitfield(PeerEndpoint endpoint, BitfieldMessage message)
        {
            context.Callback.OnPeerBitfield(endpoint.Session.Hash, endpoint.Session.Peer, message.ToBitfield());

            context.Scheduler.Handle(with =>
            {
                with.OnPeerBitfield(endpoint.Session.Peer, message.ToBitfield());
            });
        }

        public override void OnChoke(PeerEndpoint endpoint, ChokeMessage message)
        {
            context.Callback.OnPeerChoked(endpoint.Session.Hash, endpoint.Session.Peer);
        }

        public override void OnUnchoke(PeerEndpoint endpoint, UnchokeMessage message)
        {
            context.Callback.OnPeerUnchoked(endpoint.Session.Hash, endpoint.Session.Peer);
        }

        public override void OnPiece(PeerEndpoint endpoint, PieceMessage message)
        {
            context.Scheduler.Handle(with =>
            {
                with.OnPeerPiece(endpoint.Session.Peer, message.ToPiece());
            });
        }

        public override void OnMetadataSize(PeerSession session, MetadataSize size)
        {
            context.Scheduler.Handle(with =>
            {
                with.OnMetadataSize(session.Peer, size);
            });
        }

        public override void OnMetadataReceived(PeerSession session, MetadataData metadata)
        {
            context.Callback.OnMetadataReceived(session.Hash, session.Peer, metadata);

            context.Scheduler.Handle(with =>
            {
                with.OnMetadataData(session.Peer, metadata);
            });
        }
    }
}