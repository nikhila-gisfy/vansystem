<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GISReportGenerator.aspx.cs" Inherits="vansystem.GISReportGenerator" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Report Page</title>
    <style>
        .PageLoadder {
            position: fixed;
            left: 50%;
            top: 40%;
            width: 100%;
            height: 100%;
            z-index: 99999999;
        }

        .dvloader {
            margin: 0px;
            display: none;
            padding: 0px;
            position: absolute;
            right: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            background-color: rgb(255, 255, 255);
            z-index: 300;
            opacity: 0.8;
        }
    </style>
</head>
<body>
    <section class="content">
        <form id="form2" runat="server">
            <h1>Map report</h1>
            <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>             
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"></rsweb:ReportViewer>
                <div id="dvloader" runat="server" class="dvloader">
                    <div class="PageLoadder">
                    </div>
                </div>
            </div>
        </form>
    </section>
</body>
</html>
