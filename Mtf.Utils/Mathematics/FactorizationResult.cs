using System.Collections.Generic;

namespace Mtf.Utils.Mathematics
{
    public class FactorizationResult
    {
        public bool IsSemiPrime { get; set; }

        public List<ulong> Factors { get; set; }
    }
}