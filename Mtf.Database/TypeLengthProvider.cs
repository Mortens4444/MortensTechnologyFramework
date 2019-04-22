using System;
using System.Collections.Generic;

namespace Mtf.Database
{
    public class TypeLengthProvider
    {
        private static readonly Dictionary<string, int> TypesLengths = new Dictionary<string, int>
        {
            { "BIT", sizeof(byte) },
            { "CHAR", sizeof(byte) },
            { "NCHAR", sizeof(byte) },
            { "NVARCHAR", sizeof(byte) },
            { "TINYINT", sizeof(byte) },
            { "VARCHAR", sizeof(byte) },
            { "SMALLDATETIME", sizeof(short) },
            { "SMALLINT", sizeof(short) },
            { "INT", sizeof(int) },
            { "SMALLMONEY", sizeof(int) },
            { "BIGINT", sizeof(long) },
            { "DATETIME", 16 },
            { "UNIQUEIDENTIFIER", 16 },
            { "DATETIME2", 8 },
            { "DECIMAL", 17 },
            { "NUMERIC", 17 },
            { "DOUBLE", sizeof(float) },
            { "FLOAT", sizeof(float) },
            { "MONEY", sizeof(float) },
            { "REAL", sizeof(double) },
            { "DATETIMEOFFSET", 68 },
            { "SYSNAME", 256 },
            { "IMAGE", Int32.MaxValue },
            { "NTEXT", Int32.MaxValue }
        };

        public int GetLength(string type)
        {
            var upperCaseType = type.ToUpper();
            if (TypesLengths.ContainsKey(upperCaseType))
            {
                return TypesLengths[upperCaseType];
            }
            return 0;
        }
    }
}