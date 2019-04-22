using System;

namespace Mtf.Utils
{
    public static class RandomUtils
    {
        // TODO: Random seed should be better?
        public static int GetSeed()
        {
            return Environment.TickCount;
        }
    }
}