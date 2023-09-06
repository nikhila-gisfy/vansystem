<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="TabularReports.aspx.cs" Inherits="vansystem.TabularReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Van - Download Reports</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-content pd10">
        <div class="col-md-12">
            <h3 class="tile-title">Download Reports</h3>
        </div>
        <div class="clearfix"></div>
        <div style="padding: 15px;">
            <div class="tile">
                <ul>
                     <li><asp:LinkButton runat="server" Text="Compartment History Reporting Format 1" ID="btnCH1" OnClick="btnCH1_Click"></asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" Text="Compartment History Reporting Format 2A" ID="btn2A" OnClick="btn2A_Click"></asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" Text="Compartment History Reporting Format 2B" ID="btn2B" OnClick="btn2B_Click"></asp:LinkButton></li>
                   <%-- <li><a href="Report/Compartment History Reporting Format 3.pdf">Compartment History Reporting Format 3</a></li>
                    <li><a href="Report/Compartment History Reporting Format 5.pdf">Compartment History Reporting Format 5</a></li>--%>
                </ul>
            </div>
        </div>
    </div>
	<script type="text/javascript" src="/vali/js/plugins/chart.js"></script>
    <script src="/vali/js/plugins/pace.min.js"></script>
    <script src="/vali/js/popper.min.js"></script>
    <script src="/vali/js/bootstrap.min.js"></script>
    <script src="/vali/js/main.js"></script>
    <script src="/vali/js/custom.js"></script>
</asp:Content>
