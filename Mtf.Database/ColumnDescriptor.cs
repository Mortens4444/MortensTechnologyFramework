namespace Mtf.Database
{
    public class ColumnDescriptor
    {
        public ColumnDescriptor(string columnName, TypeId columnType, object defaultValue = null)
        {
            ColumnName = columnName;
            ColumnType = columnType;
            DefaultValue = defaultValue;
        }

        public string ColumnName { get; }

        public TypeId ColumnType { get; }

        public object DefaultValue { get; }
    }

}