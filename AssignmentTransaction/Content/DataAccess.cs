using AssignmentTransaction.Content.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace AssignmentTransaction.Content
{
    abstract class DataAccess
    {
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

        // Insert a datatable to the transaction table
        public static void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData)
        {
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionStringName"].ConnectionString);
            dbConnection.Open();
            using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
            {
                s.DestinationTableName = "Transactions";
                foreach (var column in csvFileData.Columns)
                    s.ColumnMappings.Add(column.ToString(), column.ToString());
                s.WriteToServer(csvFileData);
            }
            dbConnection.Close();
        }
        // 
        public static List<Dictionary<string, object>> GetListTransactionData()
        {
            DataTable dt = new DataTable();
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionStringName"].ConnectionString);
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

        public static string GetListTransactionDataHTML()
        {
            string htmlStr = "";
            SqlConnection dbConnection = new SqlConnection("Data Source=WKS-W7-LDN-0392;Initial Catalog=TransactionDB;Integrated Security=SSPI;");
            using (SqlCommand cmd = new SqlCommand("SELECT Id,Account,Description,CurrencyCode,Amount FROM Transactions"))
            {
                dbConnection.Open();
                cmd.Connection = dbConnection;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    htmlStr += "<tr><td>" + reader.GetInt32(0) + "</td><td>" + reader.GetString(1)
                        + "</td><td>" + reader.GetString(2) + "</td><td>" + reader.GetString(3)
                        + "</td><td>" + (double)reader.GetDecimal(4) + "</td></tr>";
                }
                dbConnection.Close();
            }
            return htmlStr;
        }
    }
}