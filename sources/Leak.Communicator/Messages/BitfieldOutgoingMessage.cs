﻿using System;
using Leak.Common;

namespace Leak.Communicator.Messages
{
    public class BitfieldOutgoingMessage : NetworkOutgoingMessage
    {
        private readonly byte[] data;

        public BitfieldOutgoingMessage(Bitfield bitfield)
        {
            this.data = new byte[(bitfield.Length - 1) / 8 + 1];

            for (int i = 0; i < bitfield.Length; i++)
            {
                if (bitfield[i])
                {
                    data[i / 8] += (byte)(1 << (byte)(7 - i % 8));
                }
            }
        }

        public int Length
        {
            get { return data.Length + 5; }
        }

        public DataBlock ToBytes(DataBlockFactory factory)
        {
            return factory.Pooled(Length, (buffer, offset, count) =>
            {
                buffer[offset + 4] = 0x05;

                Bytes.Write(data.Length + 1, buffer, offset);
                Array.Copy(data, 0, buffer, offset + 5, data.Length);
            });
        }
    }
}