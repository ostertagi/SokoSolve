using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using VectorInt;

namespace SokoSolve.Core.Primitives
{

    public class BitmapMaskedIndex : IBitmap
    {
        public class Master
        {
            public Master(IBitmap reference)
            {
                Reference = reference;
                
                var m = new int[reference.Width * reference.Height];
                m.Fill(-1);

                var l = new List<VectorInt2>();
                var cc = 0;
                foreach (var position in reference.TruePositions())
                {
                    l.Add(position);
                    m[reference.ToLinearSpace(position)] = cc++;
                }
                MapOut = l.ToImmutableList();
                MapIn = m.ToImmutableArray();
            }

            public IBitmap                   Reference { get; }
            public ImmutableArray<int>       MapIn     { get; }
            public ImmutableList<VectorInt2> MapOut    { get; }
            
            public int SizeInBytes => IndexedSize / 8 + 1; // TODO: +1 not needed is no rounding
            public int LinearSize  => Reference.Width * Reference.Height;
            public int IndexedSize => MapOut.Count;

            public override string ToString() =>
                $"LinearSize:{LinearSize}, IndexedSize:{IndexedSize}, {IndexedSize*100/LinearSize}% of LinearSize, SizeInBytes:{SizeInBytes}";
        }

        private readonly byte[] buffer;
        
        public BitmapMaskedIndex(Master mask)
        {
            Mask = mask;
            buffer = new byte[mask.SizeInBytes];
        }

        public Master Mask { get; }
        
        public int Width => Mask.Reference.Width;
        public int Height => Mask.Reference.Height;
        public VectorInt2 Size => Mask.Reference.Size;

        public bool this[int x, int y]
        {
            get => this[new VectorInt2(x, y)];
            set => this[new VectorInt2(x, y)] = value;
        }

        public bool this[VectorInt2 p]
        {
            get
            {
                var index = Mask.MapIn[p.ToLinearSpace(Mask.Reference)];
                return BitIndexedByteArray.GetBit(buffer, index);
            }
            set
            { 
                var index= Mask.MapIn[p.ToLinearSpace(Mask.Reference)];
                BitIndexedByteArray.SetBit(buffer, index, value);
            }
        }

        public bool Equals(IBitmap other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(IBitmap other)
        {
            throw new NotImplementedException();
        }

        public int Count { get; }
        public int SizeInBytes()
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<bool> ForEachValue()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<(VectorInt2 Position, bool Value)> ForEach()
        {
            throw new NotImplementedException();
        }
    }
}