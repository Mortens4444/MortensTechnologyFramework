using System;
using System.Collections.Generic;
using System.Linq;
using Mtf.Utils.StringExtensions;

namespace Mtf.Database
{
    public abstract class ReaderResult
    {
        public string CommaSeparatedColumnNames { get; }

        private readonly List<object> rows;
        private readonly List<int> numberOfSameItems;
        private readonly List<int> numberOfSameItemsInPairs;
        private readonly string[] columnNames;
        private readonly object sync;

        protected ReaderResult(string commaSeparatedColoumnNames)
            : this(null, commaSeparatedColoumnNames)
        { }

        protected ReaderResult(List<object> rows = null, string commaSeparatedColoumnNames = null)
        {
            sync = new object();

            lock (sync)
            {
                this.rows = rows ?? new List<object>();
                numberOfSameItems = new List<int>();
                numberOfSameItemsInPairs = new List<int>();
                for (var i = 0; i < this.rows.Count; i++)
                {
                    numberOfSameItems.Add(1);
                    numberOfSameItemsInPairs.Add(1);
                }
            }

            if (commaSeparatedColoumnNames == null)
            {
                return;
            }
            try
            {
                commaSeparatedColoumnNames = commaSeparatedColoumnNames.RemoveNestedParentheses();
                columnNames = commaSeparatedColoumnNames.Split(',');
            }
            catch
            {
                // FIXME !!!
                columnNames = new [] { commaSeparatedColoumnNames };
            }
            CommaSeparatedColumnNames = commaSeparatedColoumnNames;
        }

        public int Length => RowCount;

        public int RowCount
        {
            get
            {
                lock (sync)
                {
                    return rows.Count;
                }
            }
        }

        public int ColumnCount(int rowIndex)
        {
            lock (sync)
            {
                return ((object[])rows[rowIndex]).Length;
            }
        }

        public IEnumerable<string> GetRow(int rowIndex)
        {
            var objects = this[rowIndex];
            return objects.Select(Convert.ToString);
        }

        public object[] this[int rowIndex]
        {
            get
            {
                lock (sync)
                {
                    return (object[])rows[rowIndex];
                }
            }
        }

        public object this[int rowIndex, int columnIndex]
        {
            get
            {
                lock (sync)
                {
                    return ((object[])rows[rowIndex])[columnIndex];
                }
            }
        }

        public object this[int rowIndex, string columnName]
        {
            get
            {
                if (columnNames == null)
                {
                    throw new ArgumentNullException(nameof(columnNames));
                }
                int length;

                lock (sync)
                {
                    length = ((object[]) rows[rowIndex]).Length;
                }

                if (length != columnNames.Length)
                {
                    throw new Exception("Number of columns does not match");
                }

                for (var i = 0; i < columnNames.Length; i++)
                {
                    if (columnNames[i].Equals(columnName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return this[rowIndex, i];
                    }
                }

                throw new Exception("Column not found");
            }
        }

        public void AddRow(object row)
        {
            lock (sync)
            {
                rows.Add(row);
                numberOfSameItems.Add(1);
                numberOfSameItemsInPairs.Add(1);
            }
        }

        public string GetColoumnName(int index)
        {
            if (index < 0 || index >= columnNames.Length)
            {
                throw new IndexOutOfRangeException();
            }
            return columnNames[index];
        }

        public long GetRowLength(int index)
        {
            lock (sync)
            {
                if (index < 0 || index >= rows.Count)
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }
                return ((object[]) rows[index]).Length;
            }
        }

        public void IncrementSameItems(int index)
        {
            lock (sync)
            {
                numberOfSameItems[index]++;
            }
        }

        public void IncrementSameItemsInPair(int index)
        {
            lock (sync)
            {
                numberOfSameItemsInPairs[index]++;
            }
        }

        public int GetSameItemsNumber(int index)
        {
            lock (sync)
            {
                return numberOfSameItems[index];
            }
        }

        public int GetSameItemsNumberInPairs(int index)
        {
            lock (sync)
            {
                return numberOfSameItemsInPairs[index];
            }
        }
    }
}