<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="IVICal.aspx.cs" Inherits="vansystem.IVICal" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .dropdown-style {
            background-color: darkblue;
            color: white;
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 5px;
            width: 200px
        }
        .submit-button {
            background-color:deepskyblue;
            color: white;
            padding: 8px 15px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-top: 10px;
           text-align:center;
            
            
        
        }
    </style>
</asp:Content>

  <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1"></asp:ScriptManager>
    <section class="content">
       <div class="card" style="margin-top: 100px;
    margin-left: 50px;">
            <div class="card-header">
                <h3 class="card-title">Importance Value Index (IVI)</h3>
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

                         <asp:DropDownList ID="ddlRangeName" name="BindRangeDropdown" runat="server" CssClass="dropdown-style">
                              <asp:ListItem Text="Select Range Type" Value="" />
                            
                        </asp:DropDownList>

                        <!-- Add the second dropdown below the first dropdown -->
                        <asp:DropDownList ID="ddlHabitType" name="BindHabitDropdown" runat="server" CssClass="dropdown-style">
                             <asp:ListItem Text="Select Habit Type" Value="" />
                            
                        </asp:DropDownList>
                      
                        </div>
                   

                        <div class="col-md-8 col-md-offset-3">
                            <br />
                                    <button class="btn btn-primary" type="submit" runat="server" onserverclick="Unnamed_ServerClick"><i class="fa fa-fw fa-lg fa-check-circle"></i>Get IVI Values</button>
                        <!-- Rest of the content for the IVI Calculation page goes here -->
                        <!-- Add your buttons, grids, and other controls as needed -->
                               
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