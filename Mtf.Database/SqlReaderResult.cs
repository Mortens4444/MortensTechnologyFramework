using System.Collections.Generic;

namespace Mtf.Database
{
    public class SqlReaderResult : ReaderResult
    {
        public SqlReaderResult()
        { }

        public SqlReaderResult(string commaSeparatedColoumnNames)
            : base(commaSeparatedColoumnNames)
        { }

        public SqlReaderResult(List<object> rows, string commaSeparatedColoumnNames = null)
            : base(rows, commaSeparatedColoumnNames)
        { }
    }
}