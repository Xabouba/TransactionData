# TransactionData

This repository contains a whole Visual Studio ASP.NET Web Application and a SQL query file to create a new Database.

SETUP INSTRUCTIONS

1/ Download zip repository and unzip folder.
2/ In Visual Studio, open an existing project and select the AssignmentTransaction.sln file.
3/ In Microsoft SQL Server Management, open and run the SQL Query CreateDB_Table.sql
4/ In the Web.config file (ln.19), in ConnectionStrings parameters, update the connectionString data source to the current installed SQL server used (by default it's the local database)
5/ Build and view in browser to test the application

APPLICATION TEST INSTRUCTIONS

The application contains 3 pages:
1/ Home page with links to the 2 other pages
2/ Transactions: Displaying all transaction data stored in DB using a Javascript plugin to display an infinite numbers of rows in DB.
The data is displayed in a table containing: Transaction ID, Account, Description, Currency Code & Amount
3/ Upload file: Enable user to upload CSV file
To test it, copy/paste CSV file into the CSV folder and select the file when asked.
The CSV file needs to be in the following format: Account | Description | Currency Code | Amount
Each column of each row is checked before uploaded to the table in the DB
Each row containing at least one error is not uploaded and an error code is displayed on the current page.

Several CSV files are included:
1/ ~10 rows CSV file with no errors
2/ ~10 rows CSV file with errors
3/ ~100k rows Large CSV file with no errors
4/ ~10k rows CSV file with all errors
5/ ~100k rows CSV file with all errors