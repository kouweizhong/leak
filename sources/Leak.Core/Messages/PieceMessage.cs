﻿namespace Leak.Core.Messages
{
    public class PieceMessage
    {
        private readonly DataBlock block;
        private readonly DataBlock data;

        private readonly int piece;
        private readonly int offset;

        public PieceMessage(DataBlock block)
        {
            this.block = block;
            this.data = block.Scope(8);

            this.piece = block[3] + block[2] * 256 + block[1] * 256 * 256;
            this.offset = block[7] + block[6] * 256 + block[5] * 256 * 256;
        }

        public int Piece
        {
            get { return piece; }
        }

        public int Offset
        {
            get { return offset; }
        }

        public int Size
        {
            get { return data.Size; }
        }

        public Piece ToPiece()
        {
            return new Piece(piece, offset, data);
        }
    }
}