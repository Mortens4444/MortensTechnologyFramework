using System;

namespace Mtf.Database
{
    public class MockSqlReaderResultProvider
    {
        public static SqlReaderResult GetMockSqlReaderResult_Fix()
        {
            var result = new SqlReaderResult("name,value,type");
            result.AddRow(new object[] { "Név", "0", "Típus" }); // 0
            result.AddRow(new object[] { "Név", "1", "Típus" }); // 1
            result.AddRow(new object[] { "Név", "0", "Típus" }); // 2
            result.AddRow(new object[] { "Név", "0", "Típus" }); // 3
            result.AddRow(new object[] { "Név", "1", "Típus" }); // 4
            result.AddRow(new object[] { "Név", "1", "Típus" }); // 5
            result.AddRow(new object[] { "Név", "1", "Típus" }); // 6
            result.AddRow(new object[] { "Név", "0", "Típus" }); // 7
            result.AddRow(new object[] { "Név", "0", "Típus" }); // 8
            result.AddRow(new object[] { "Név", "1", "Típus" }); // 9
            result.AddRow(new object[] { "Név", "1", "Típus" }); // 10
            result.AddRow(new object[] { "Név", "1", "Típus" }); // 11
            result.AddRow(new object[] { "Név", "0", "Típus" }); // 12
            result.AddRow(new object[] { "Név", "0", "Típus" }); // 13
            result.AddRow(new object[] { "Név", "1", "Típus" }); // 14
            result.AddRow(new object[] { "Név", "1", "Típus" }); // 15
            result.AddRow(new object[] { "Név", "0", "Típus" }); // 16
            result.AddRow(new object[] { "Név", "0", "Típus" }); // 17
            result.AddRow(new object[] { "Név", "1", "Típus" }); // 18
            result.AddRow(new object[] { "Név", "0", "Típus" }); // 19
            result.AddRow(new object[] { "Név", "1", "Típus" }); // 20
            return result;
        }

        public static SqlReaderResult GetMockSqlReaderResult()
        {
            var result = new SqlReaderResult("name,value,type");

            var r = new Random();
            for (var i = 0; i < 100000; i++)
            {
                var values = new object[3];
                values[0] = "Név";
                values[1] = r.Next(0, 2).ToString();
                values[2] = "Típus";
                result.AddRow(values);
            }

            return result;
        }

        public static SqlReaderResult GetMockSqlReaderResult_Test()
        {
            var result = new SqlReaderResult("name,value,type");

            for (var i = 0; i < 10; i++)
            {
                var values = new object[3];
                values[0] = "Név";
                values[1] = "Érték";
                values[2] = "Típus";
                result.AddRow(values);
            }

            for (var i = 0; i < 10; i++)
            {
                var values = new object[3];
                values[0] = $"Név {i + 1}";
                values[1] = $"Érték {i}";
                values[2] = "Típus";
                result.AddRow(values);
            }

            for (var i = 0; i < 10; i++)
            {
                var values = new object[3];
                if (i % 2 == 0)
                {
                    values[0] = "Név";
                    values[1] = "Érték";
                    values[2] = "Típus";
                }
                else
                {
                    values[0] = "Név_2";
                    values[1] = "Érték_2";
                    values[2] = "Típus_2";
                }
                result.AddRow(values);
            }

            return result;
        }

        public static SqlReaderResult GetMockSqlReaderResult_AllSame()
        {
            var result = new SqlReaderResult("name,value,type");

            for (var i = 0; i < 100; i++)
            {
                var values = new object[3];
                values[0] = "Név";
                values[1] = "Érték";
                values[2] = "Típus";
                result.AddRow(values);
            }
            return result;
        }

        public static SqlReaderResult GetMockSqlReaderResult_AllDifferent()
        {
            var result = new SqlReaderResult("name,value,type");

            for (var i = 0; i < 100; i++)
            {
                var values = new object[3];
                values[0] = $"Név {i + 1}";
                values[1] = $"Érték {i}";
                values[2] = "Típus";
                result.AddRow(values);
            }
            return result;
        }

        public static SqlReaderResult GetMockPairSqlReaderResult_Pairs()
        {
            var result = new SqlReaderResult("name,value,type");

            for (var i = 0; i < 100; i++)
            {
                var values = new object[3];
                if (i % 2 == 0)
                {
                    values[0] = "Név";
                    values[1] = "Érték";
                    values[2] = "Típus";
                }
                else
                {
                    values[0] = "Név_2";
                    values[1] = "Érték_2";
                    values[2] = "Típus_2";
                }
                result.AddRow(values);
            }
            return result;
        }
    }
}