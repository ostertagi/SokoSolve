﻿using SokoSolve.Core.Primitives;
using VectorInt;

namespace SokoSolve.Core.Analytics
{
    public static class FloodFill
    {
        public static Bitmap Fill(IBitmap contraints, VectorInt2 p)
        {
            var result = new Bitmap(contraints.Size);
            FillCell(contraints, result, p);
            return result;
        }

        private static void FillCell(IBitmap contraints, IBitmap result, VectorInt2 p)
        {
            if (p.X < 0 || p.Y < 0) return;
            if (p.X > contraints.Size.X || p.Y > contraints.Size.Y) return;

            if (contraints[p]) return;
            if (result[p]) return;

            result[p] = true;

            FillCell(contraints, result, p + VectorInt2.Up);
            FillCell(contraints, result, p + VectorInt2.Down);
            FillCell(contraints, result, p + VectorInt2.Left);
            FillCell(contraints, result, p + VectorInt2.Right);
        }

        public static Bitmap Fill(BitmapSpan contraints, VectorInt2 p)
        {
            var result = new Bitmap(contraints.Size);
            Fill(contraints, p, result);
            return result;
        }

        public static void Fill(BitmapSpan contraints, VectorInt2 p, IBitmap output)
        {
            FillCell(contraints, p, output);
        }

        private static void FillCell(BitmapSpan contraints, VectorInt2 p, IBitmap output)
        {
            if (p.X < 0 || p.Y < 0) return;
            if (p.X > contraints.Size.X || p.Y > contraints.Size.Y) return;

            if (contraints[p]) return;
            if (output[p]) return;

            output[p] = true;

            FillCell(contraints, p + VectorInt2.Up, output);
            FillCell(contraints, p + VectorInt2.Down, output);
            FillCell(contraints, p + VectorInt2.Left, output);
            FillCell(contraints, p + VectorInt2.Right, output);
        }
    }
}