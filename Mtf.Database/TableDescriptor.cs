namespace Mtf.Database
{
    public class TableDescriptor
    {
        public TableDescriptor(string tableName, params ColumnDescriptor[] columns)
        {
            TableName = tableName;
            Columns = columns;
        }

        public string TableName { get; }

        public ColumnDescriptor[] Columns { get; }
    }
}