<%@ Page Title="Transactions data" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayTransactions.aspx.cs" Inherits="AssignmentTransaction.DisplayTransactions" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link type="text/css" href="http://jqueryrock.googlecode.com/svn/trunk/css/jquery-ui-1.9.2.custom.css" rel="stylesheet" />  
    <link type="text/css" href="http://jqueryrock.googlecode.com/svn/trunk/jqgrid/css/ui.jqgrid.css" rel="stylesheet" />  
    <script type="text/javascript" src="http://jqueryrock.googlecode.com/svn/trunk/js/jquery-1.8.3.js"></script>  
    <script type="text/javascript" src="http://jqueryrock.googlecode.com/svn/trunk/js/jquery-ui-1.9.2.custom.js"></script>  
    <script src="http://jqueryrock.googlecode.com/svn/trunk/jqgrid/js/grid.locale-en.js" type="text/javascript"></script>  
    <script src="http://jqueryrock.googlecode.com/svn/trunk/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>  
<script type="text/javascript">

    $(function () {
        $("#dataGrid").jqGrid({
            url: 'DisplayTransactions.aspx/GetAllTransaction',
            datatype: 'json',
            mtype: 'POST',

            serializeGridData: function (postData) {
                return JSON.stringify(postData);
            },

            ajaxGridOptions: { contentType: "application/json" },
            loadonce: true,
            colNames: ['Transaction ID', 'Account', 'Description', 'Currency Code', 'Amount'],
            colModel: [
                            { name: 'Id', index: 'Employee ID', width: 100 },
                            { name: 'Account', index: 'Name', width: 250 },
                            { name: 'Description', index: 'Designation', width: 250 },
                            { name: 'CurrencyCode', index: 'City', width: 250 },
                            { name: 'Amount', index: 'State', width: 150 }
            ],
            pager: '#pagingGrid',
            rowNum: 50,
            rowList: [50, 100, 200],
            viewrecords: true,
            gridview: true,
            height: '300px',
            width:'1000px',
            jsonReader: {
                page: function (obj) { return 1; },
                total: function (obj) { return 1; },
                records: function (obj) { return obj.d.length; },
                root: function (obj) { return obj.d; },
                repeatitems: false,
                id: "0"
            },
            caption: 'Transaction data'
        });
    });


    </script>  
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>List of all the data transaction data in the system.</h2>
    </hgroup>

    <article>
  <table  >  
        <tr>  
            <td style="text-align: center; vertical-align: central; ">  
                <table id="dataGrid" style="text-align: center;"></table>  
                <div id="pagingGrid"></div>  
            </td>  
        </tr>  
    </table> 
    </article>
</asp:Content>