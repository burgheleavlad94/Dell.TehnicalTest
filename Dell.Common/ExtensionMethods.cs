using System;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Dell.Common
{
    public static class ExtensionMethods
    {
        public static void OpenEx(this DbConnection connection)
        {
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
        }

        public static void CloseEx(this DbConnection connection)
        {
            if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                connection.Close();
        }

        public static void CloseEx(this DbDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
                reader.Close();
        }

        public static int GetParamReturnValueEx(this DbCommand command)
        {
            int returnVal = 0;
            //var retVal = command.Parameters?.Cast<DbParameter>().Where(p => p.ParameterName == "@retVal").FirstOrDefault();
            //if (retVal != null && retVal.Value != DBNull.Value)
            //    returnVal = (int)retVal.Value;

            var tmp = command.Parameters?.Cast<DbParameter>().FirstOrDefault(p => p.Direction == ParameterDirection.ReturnValue)?.Value;
            if (tmp != null && tmp != DBNull.Value)
                returnVal = (int)tmp;
            return returnVal;

        }
    }
}
