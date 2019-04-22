using System;
using System.Text;
using Mtf.Utils.Generics;
using Mtf.Utils.StringExtensions;

namespace Mtf.Database
{
    public class StoredProcedure
    {
        public string Name { get; }

        public string Source { get; }

        public StoredProcedure(string name, string source)
        {
            Name = name;
            Source = source;
        }

        public static StoredProcedure[] GetStoredProcedures(DatabaseContext dc)
        {
            var rows = dc.ExecuteReader("SELECT name FROM sysobjects WHERE (xtype = 'P') ORDER BY name;");
            var result = new StoredProcedure[rows.Length];
            for (var i = 0; i < rows.Length; i++)
            {
                var spName = Convert.ToString(rows[i, 0]);
                var source = dc.ExecuteStoredProcedure("sp_helptext", "objname", spName);
                var sourceCode = new StringBuilder();

                foreach (var srr in source)
                {
                    for (var j = 0; j < srr.Length; j++)
                    {
                        var line = Convert.ToString(srr[j, 0]);
                        sourceCode.Append(line);
                    }
                }

                result[i] = new StoredProcedure(spName, sourceCode.ToString().Replace("\r\n", "\\r\\n").Trim());
            }
            return result;
        }

        public void CreateStoredProcedure(DatabaseContext dc)
        {
            dc.ExecuteNonQuery(Source);
        }

        public void ModifyStoredProcedure(DatabaseContext dc, StoredProcedure destinationStoredProcedure)
        {
            if (this != destinationStoredProcedure)
            {
                dc.ExecuteNonQuery(Source.Replace("CREATE PROCEDURE", "ALTER PROCEDURE"));
            }
        }

        public override string ToString()
        {
            return Source;
        }

        public override bool Equals(object obj)
        {
            var storedProcedure = obj as StoredProcedure;
            if (storedProcedure != null)
            {
                return String.Equals(ToString(), storedProcedure.ToString(), StringComparison.CurrentCultureIgnoreCase);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(StoredProcedure sp1, StoredProcedure sp2)
        {
            return Equality.IsEqual(sp1, sp2);
        }

        public static bool operator !=(StoredProcedure sp1, StoredProcedure sp2)
        {
            return Equality.IsNotEqual(sp1, sp2);
        }

        public static StoredProcedure GetStoredProcedure(string procedureDescription)
        {
            if (String.IsNullOrEmpty(procedureDescription))
            {
                return null;
            }

            var source = procedureDescription.Substring(procedureDescription.IndexOf("CREATE PROCEDURE "));
            source = source.Replace("\\r\\n", Environment.NewLine);
            return new StoredProcedure(procedureDescription.Substring("CREATE PROCEDURE ", " "), source);
        }
    }

}