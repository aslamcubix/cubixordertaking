using C_OrderTaking_Api.DCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace C_OrderTaking_Api.Model
{
    public class General
    {
        public DataTable getTableDictionary(string sql,bool isProcedure,Dictionary<string,string> parameters)
        {
            using (SqlConnection sqlConnection = new SqlConnection(GeneralConnection.sqlDataSource))
            {
                DataTable dt = new DataTable();
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    if (isProcedure)
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                    else
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = sql;
                    }
                    foreach (string parameter in parameters.Keys)
                    {
                        sqlCommand.Parameters.AddWithValue(parameter, parameters[parameter]);
                    }
                    sqlCommand.CommandTimeout = 300;
                    SqlDataReader sqldr = sqlCommand.ExecuteReader();
                    if (sqldr.
                        HasRows)
                    {
                        dt.Load(sqldr);
                        return dt;
                    }
                }
            }

            return null;
        }
        public bool SqlbulkInsert(string sql, bool isProcedure, Dictionary<string, string> parameters, DataTable table, string tableparameter)
        {

            using (SqlConnection sqlConnection = new SqlConnection(GeneralConnection.sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    if (isProcedure)
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    else
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.Parameters.AddWithValue(tableparameter, table);
                    foreach (string parameter in parameters.Keys)
                    {
                        sqlCommand.Parameters.AddWithValue(parameter, parameters[parameter]);
                    }

                    sqlCommand.CommandTimeout = 300;
                    if (sqlCommand.ExecuteNonQuery() > 0)
                        return true;
                    else
                        return false;
                }
            }
        }
    }
}
