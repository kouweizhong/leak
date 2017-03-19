﻿using Leak.Data.Store;
using Leak.Memory;

namespace Leak.Client
{
    public class MemoryToRepository : RepositoryMemory
    {
        private readonly MemoryService service;

        public MemoryToRepository(MemoryService service)
        {
            this.service = service;
        }

        public RepositoryMemoryBlock Allocate(int size)
        {
            return new Block(service.Allocate(size));
        }

        private class Block : RepositoryMemoryBlock
        {
            private readonly MemoryBlock inner;

            public Block(MemoryBlock inner)
            {
                this.inner = inner;
            }

            public byte[] Data
            {
                get { return inner.Data; }
            }

            public int Length
            {
                get { return inner.Data.Length; }
            }

            public void Release()
            {
                inner.Release();
            }
        }
    }
}