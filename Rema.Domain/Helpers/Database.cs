using System;
using System.Collections.Generic;
using System.Data;
using Oracle.DataAccess.Client;

namespace Rema.Domain.Helpers
{
    class Database
    {
        public static string GetString(IDataRecord row, string fieldName, bool trim = true)
        {
            int index = row.GetOrdinal(fieldName);
            if (row.IsDBNull(index))
                return null;
            else
            {
                if (trim)
                    return row.GetString(index).Trim();
                else
                    return row.GetString(index);
            }
        }

        public static int? GetInt32(IDataRecord row, string fieldName)
        {
            int index = row.GetOrdinal(fieldName);
            if (row.IsDBNull(index))
                return null;
            else
            {
                if (row[index] is decimal)
                    return (int)row.GetDecimal(index);
                else
                    return row.GetInt32(index);
            }
        }

        public static DateTime? GetDateTime(IDataRecord row, string fieldName)
        {
            int index = row.GetOrdinal(fieldName);
            if (row.IsDBNull(index))
                return null;
            else
                return row.GetDateTime(index);
        }

        public static List<T> GetList<T>(string query, Func<IDataRecord, T> CreateObject)
        {
            using (OracleConnection con = new OracleConnection(Helpers.Config.GetAppSetting("RemaConnectionString")))
            using (OracleCommand cmd = new OracleCommand(query, con))
            {
                cmd.CommandType = CommandType.Text;
                con.Open();
                var rdr = cmd.ExecuteReader();
                var result = new List<T>();
                while (rdr.Read())
                {
                    result.Add(CreateObject(rdr));
                }
                return result;
            }
        }
    }
}
