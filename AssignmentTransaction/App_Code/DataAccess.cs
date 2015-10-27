using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace AssignmentTransaction.App_Code
{
    abstract static class DataAccess
    {
        /// <summary>
        /// Initialize a datatable containing transaction data
        /// Used with bulk insert into the DB
        /// </summary>
        /// <returns></returns>
        public static DataTable InitDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable = new System.Data.DataTable("Transactions");
            dataTable.Columns.Add("Account", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("CurrencyCode", typeof(string));
            dataTable.Columns.Add("Amount", typeof(double));

            return dataTable;
        }

        /// <summary>
        /// Insert a datatable to the transaction table
        /// 
        /// </summary>
        /// <param name="csvFileData"></param>
        public static void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData)
        {
            // Connect to DB and open connection
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            dbConnection.Open();

            // Nulk copy the correct data to DB
            using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
            {
                // Use table named Transactions
                s.DestinationTableName = "Transactions";
                foreach (var column in csvFileData.Columns)
                    s.ColumnMappings.Add(column.ToString(), column.ToString());
                // Write to DB
                s.WriteToServer(csvFileData);
            }
            dbConnection.Close();
        }

        /// <summary>
        /// Return list of dictionnary containing all transaction data
        /// </summary>
        /// <returns>list of dictionarry containing all transaction data from DB</returns>
        public static List<Dictionary<string, object>> GetListTransactionData()
        {
            DataTable dt = new DataTable();
            // Connect to DB and open connection
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            // SQL query to return all data from Transactions table
            using (SqlCommand cmd = new SqlCommand("SELECT Id,Account,Description,CurrencyCode,Amount FROM Transactions"))
            {
                dbConnection.Open();
                cmd.Connection = dbConnection;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                return rows;
            }
        }
    }
}