﻿using Leak.Core.Common;
using Leak.Core.Core;
using Leak.Core.Omnibus.Events;
using System;
using System.Linq;

namespace Leak.Core.Omnibus.Tasks
{
    public class SchedulePeerTask : LeakTask<OmnibusContext>
    {
        private readonly OmnibusStrategy strategy;
        private readonly PeerHash peer;
        private readonly int count;

        public SchedulePeerTask(OmnibusStrategy strategy, PeerHash peer, int count)
        {
            this.strategy = strategy;
            this.peer = peer;
            this.count = count;
        }

        public void Execute(OmnibusContext context)
        {
            OmnibusBlock[] blocks = null;
            DateTime now = default(DateTime);
            FileHash hash = context.Metainfo.Hash;

            lock (context.Synchronized)
            {
                if (context.Bitfields.Contains(peer))
                {
                    blocks = strategy.Next(context, peer, count).ToArray();

                    if (blocks.Length > 0)
                    {
                        now = DateTime.Now;
                    }

                    foreach (OmnibusBlock block in blocks)
                    {
                        PeerHash previous = context.Reservations.Add(peer, block, now);

                        if (previous != null)
                        {
                            context.Callback.OnBlockExpired(hash, previous, block);
                        }
                    }
                }
            }

            if (blocks != null)
            {
                context.Callback.OnBlockReserved(hash, new OmnibusReservationEvent(peer, blocks));
            }
        }
    }
}