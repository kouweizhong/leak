﻿using System;
using System.Security.Cryptography;
using Leak.Common;
using Leak.Files;
using Leak.Metadata;

namespace Leak.Metafile
{
    public class MetafileDestination : IDisposable
    {
        private readonly MetafileContext context;
        private readonly File file;

        public MetafileDestination(MetafileContext context)
        {
            string path = context.Parameters.Destination;

            this.context = context;
            this.file = context.Dependencies.Files.OpenOrCreate(path);
        }

        public void Write(int piece, byte[] data)
        {
            int offset = piece * 16384;
            FileHash hash = context.Parameters.Hash;

            file.Write(offset, data, result =>
            {
                context.Queue.Add(new MetafileTaskWritten(hash, piece, data.Length));
            });
        }

        public void Verify()
        {
            HashAlgorithm algorithm = SHA1.Create();
            byte[] buffer = new byte[16384];

            file.Read(0, buffer, result =>
            {
                context.Queue.Add(new MetafileTaskVerified(algorithm, result));
            });
        }

        public void Dispose()
        {
            file.Dispose();
        }
    }
}