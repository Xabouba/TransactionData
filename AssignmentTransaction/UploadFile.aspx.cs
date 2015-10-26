using AssignmentTransaction.Content;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AssignmentTransaction
{
    public partial class UploadFile : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Upload a CSV file to TransactionDB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadCsvDataToDatabase(object sender, EventArgs e)
        {
            try
            {
                UploadCsvFile();
            }
            catch (Exception ex)
            {
                MessageHandler.HandleMsg(divMessage, "error", "Error! " + ex.Message);

            }
        }

        /// <summary>
        /// Upload by checking and then importing data to the DB
        /// </summary>
        private void UploadCsvFile()
        {
            string errors = "";
            int i = 0, j = 0;
            FileUpload inputFile = flucsv;

            // Check if file exists
            if (inputFile.HasFile)
            {
                string contentType = inputFile.PostedFile.ContentType.ToLower();
                var Extension = inputFile.FileName.Substring(inputFile.FileName.IndexOf('.') + 1).ToLower();
                // Check if file is CSV
                if (Extension == "csv")
                {
                    // Create a datatable to bulk insert into the DB
                    DataTable csvData = DataAccess.InitDataTable();

                    // First read the file to check format
                    using (StreamReader reader = new StreamReader(File.OpenRead(Server.MapPath("~/CSV/" + Path.GetFileName(inputFile.FileName)))))
                    {
                        // For each line of the file
                        while (!reader.EndOfStream)
                        {
                            i++;
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            // Test the correctness of the whole line and returns a code error
                            int test = ErrorCode.TestCSVLine(values.ToArray());
                            // If no error, create new row to insert
                            if (test == 0)
                            {
                                DataRow dataRow = csvData.NewRow();
                                // Use Trim to remove blank spaces
                                dataRow["Account"] = values[0].Trim();
                                dataRow["Description"] = values[1].Trim();
                                dataRow["CurrencyCode"] = values[2].Trim();
                                dataRow["Amount"] = values[3].Trim();
                                csvData.Rows.Add(dataRow);
                            }
                            // Else display issue on website
                            else
                            {
                                j++;
                                errors += "Error on row " + i + ": " + ErrorCode.TestIntDetails(test) + "! <br/>";
                            }
                        }
                        // Insert the current datatable to DB
                        DataAccess.InsertDataIntoSQLServerUsingSQLBulkCopy(csvData);
                    }

                    if (i > 0){
                        MessageHandler.HandleMsg(divSuccess, "message-success", "Results: " + (i - j) + "/" + i + " rows correctly imported!");
                    }
                    if (j > 0)
                    {
                        MessageHandler.HandleMsg(divMessage, "message-error", errors);
                    }
                }
                else
                {
                    MessageHandler.HandleMsg(divMessage, "error", "Please select a valid CSV file!!");
                }
            }
        }



    }
}