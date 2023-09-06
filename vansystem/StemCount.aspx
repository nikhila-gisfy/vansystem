<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="StemCount.aspx.cs" Inherits="vansystem.StemCount" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager runat="server" ID="sm1"></asp:ScriptManager>
    <section class="content">
        <div class="card" style="margin-top: 100px;
    margin-left: 50px;">
            <div class="card-header">
                <h3 class="card-title">STEM COUNT REPORT</h3>
                <div class="pull-right card-tools">
                </div>
            </div>
            <!-- /.box-header -->
            <!-- /.box-header -->
            <!-- form start -->
            <div class="card-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                           
                <div class="row">
                    <div class="col-md-3" id="dvDivision" style="display:flex">
                         <asp:Button runat="server" ID="btnGenerate" Text="Generate Report" CssClass="btn btn-primary" OnClick="btnGenerate_Click"/>
                    </div>
                  </div>
               </div>
                   
                </div>
            </div>
       
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"></rsweb:ReportViewer>
            <div id="dvloader" runat="server" class="dvloader">
                <div class="PageLoadder">
                </div>
            </div>
                </div>
            </div>
           

    </section>
</asp:Content>

