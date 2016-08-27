﻿using Leak.Core.Common;
using Leak.Core.Network;
using System;

namespace Leak.Core.Connector
{
    public class PeerConnectorContext
    {
        private readonly PeerConnectorConfiguration configuration;
        private readonly PeerConnectorQueue queue;
        private readonly PeerConnectorTimer timer;

        public PeerConnectorContext(Action<PeerConnectorConfiguration> configurer)
        {
            configuration = configurer.Configure(with =>
            {
                with.Callback = new PeerConnectorCallbackNothing();
                with.Peer = new PeerHash(Bytes.Random(20));
                with.Pool = new NetworkPool();
            });

            queue = new PeerConnectorQueue();
            timer = new PeerConnectorTimer(TimeSpan.FromSeconds(1));
        }

        public PeerConnectorConfiguration Configuration
        {
            get { return configuration; }
        }

        public NetworkPool Pool
        {
            get { return configuration.Pool; }
        }

        public PeerConnectorQueue Queue
        {
            get { return queue; }
        }

        public PeerConnectorTimer Timer
        {
            get { return timer; }
        }
    }
}