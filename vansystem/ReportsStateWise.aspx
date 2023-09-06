﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Van.Master" AutoEventWireup="true" CodeBehind="ReportsStateWise.aspx.cs" Inherits="vansystem.ReportsStateWise" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager runat="server" ID="sm1"></asp:ScriptManager>
    <section class="content">
        <div class="card" style="margin-top: 100px;
    margin-left: 50px;">
            <div class="card-header">
                <h3 class="card-title">State Reports</h3>
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
                        
                        
                        <asp:Button runat="server" ID="btnstatewise" Text="Generate State-Wise" CssClass="btn btn-primary" style="margin-left:20px" />
                         <asp:Button runat="server" ID="btndivisionwise" Text="Generate Division-Wise" CssClass="btn btn-primary" OnClick="btndivisionwise_Click1" style="margin-left:20px" />
                        <asp:Button runat="server" ID="btndiaclasswise" Text="Generate DiaClass-Wise" CssClass="btn btn-primary" style="margin-left:20px" />
                        <asp:Button runat="server" ID="btnstemcount" Text="Generate DiaClass-Wise" CssClass="btn btn-primary" style="margin-left:20px" />
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
