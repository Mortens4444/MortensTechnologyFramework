using System;
using System.Collections.Generic;
using Mtf.Utils.StringExtensions;
using NUnit.Framework;

namespace Mtf.Utils.Test
{
    [TestFixture]
    public class Tests
    {
        private static readonly Dictionary<string, TimeSpan?> testCases = new Dictionary<string, TimeSpan?>
        {
            { String.Empty, null },
            { "12:00am", new TimeSpan(12, 0, 0) },
            { "12:30", new TimeSpan(12, 30, 0) },
            { "2:30pm", new TimeSpan(14, 30, 0) },
            { "24 hr(s)", new TimeSpan(24, 0, 0) }
        };

        [TestCase("Test ConvertToTimeSpan function")]
        public void ConvertToTimeSpanTest()
        {
            foreach (var testCase in testCases)
            {
                var result = testCase.Key.ConvertToTimeSpan();
                if (result.HasValue)
                {
                    Assert.AreEqual(testCase.Value, result.Value);
                }
                else
                {
                    Assert.IsNull(testCase.Value);
                }
            }
        }
    }
}