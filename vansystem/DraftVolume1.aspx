<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="DraftVolume1.aspx.cs" Inherits="vansystem.DraftVolume1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager runat="server"></asp:ScriptManager>
     <section class="container mt-3 mt-lg-auto mt-sm-1 pt-2">
           <asp:Button Text="Download Template" class="btn btn-primary btn-sm small" ID="btnDownload" runat="server" OnClick="btnDownload_Click1"  CausesValidation="false"></asp:Button>
         </section>
</asp:Content>
