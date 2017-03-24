using System.Collections.Generic;
using Leak.Common;
using Leak.Extensions.Metadata;
using Leak.Glue;
using Leak.Meta.Get;

namespace Leak.Client.Adapters
{
    internal class MetaGetToGlueAdapter : MetagetGlue
    {
        private readonly GlueService service;

        public MetaGetToGlueAdapter(GlueService service)
        {
            this.service = service;
        }

        public IEnumerable<PeerHash> Peers
        {
            get
            {
                List<PeerHash> peers = new List<PeerHash>();

                service.ForEachPeer(peer =>
                {
                    if (service.IsSupported(peer, MetadataPlugin.Name))
                    {
                        peers.Add(peer);
                    }
                });

                return peers;
            }
        }

        public void SendMetadataRequest(PeerHash peer, int piece)
        {
            service.SendMetadataRequest(peer, piece);
        }
    }
}