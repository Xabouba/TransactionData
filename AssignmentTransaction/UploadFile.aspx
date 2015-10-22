<%@ Page Title="UploadFile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="AssignmentTransaction.UploadFile" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1>UploadFile</h1>
        <h2>Upload a CSV file containing transactions data.</h2>
    </hgroup>

    <section class="contact">
        <form id="form1" runat="server">
            <div>
                <div>
                    <h3>
                        Import CSV File for Inserting into database

                    </h3>
                </div>
                <div id="divMessage" runat="server" style="display: none;" ></div>
                <div id="divSuccess" runat="server" style="display: none;"></div>
                <asp:FileUpload ID="flucsv" runat="server" />

                <asp:RequiredFieldValidator ID="rfvCSV" runat="server" ErrorMessage="Please Select a file first"
                    ValidationGroup="validate" Display="Dynamic" ControlToValidate="flucsv">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revCSV" runat="server" ErrorMessage="Upload .CSV File only"
                    ValidationGroup="validate" Display="Dynamic" ValidationExpression="^.*\.(csv|CSV)$"
                    ControlToValidate="flucsv">
                </asp:RegularExpressionValidator>
                <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadCsvDataToDatabase"
                    CausesValidation="true" ValidationGroup="validate" />
            </div>
        </form>
    </section>
</asp:Content>