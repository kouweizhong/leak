﻿using Leak.Common;
using Leak.Completion;
using Leak.Files;
using Leak.Networking;
using Leak.Tasks;

namespace Leak.Client.Swarm
{
    public interface SwarmRuntime
    {
        PipelineService Pipeline { get; }

        FileFactory Files { get; }

        CompletionWorker Worker { get; }

        void Start();

        void Stop();
    }
}