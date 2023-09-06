<%@ Page Title="" Language="C#" MasterPageFile="~/AdminVanUser.Master" AutoEventWireup="true" CodeBehind="Admingetusers.aspx.cs" Inherits="vansystem.Admingetusers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="FES">
    <meta name="keyword" content="">
    <link rel="shortcut icon" href="/vali/img/favicon.png" />
    <title>Van - System Users</title>
    <link rel="stylesheet" type="text/css" href="/vali/css/main.css">
    <link rel="stylesheet" type="text/css" href="/vali/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="/vali/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="/vali/css/font-awesome-custom.css">
    <link rel="stylesheet" type="text/css" href="/vali/css/custom.css">
    <link rel="stylesheet" type="text/css" href="/global/css/ol4.css">
    <script type="text/javascript" src="/vali/js/jquery-3.2.1.min.js"></script>


    <style>
        th {
            background: #51c551 !important;
            color: white !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.4);
        }

        th, td {
            padding: 0.25rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-content">
        <div class="row">
            <div class="col-md-12 col-sm-12">
                <div class="tile">
                    <div class="pull-right">
                        <a href="AdmingetNewuser.aspx" class="btn btn-xs btn-primary"><i class="fa fa-plus-circle"></i>Add User</a>
                    </div>
                    <h3 class="tile-title">System Users</h3>
                    <div class="tile-body">

                        <div class="table-responsive">

                            <table class="table table-dark" id="users">

                                <asp:GridView ID="GVgetuser" runat="server" AutoGenerateColumns="false" GridLines="none" frame="void"
                                    rules="rows" CellPadding="4" CellSpacing="4" Width="1380" DataKeyNames="uid" OnRowEditing="GVgetuser_RowEditing">
                                    <RowStyle BackColor="White" BorderColor="ControlLight" />
                                    <HeaderStyle BorderColor="ControlLight" />
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="user_id" HeaderText="User Name" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="name" HeaderText="Name" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="mob_number" HeaderText="Mobile" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="email" HeaderText="Email" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="designation" HeaderText="Role" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" CssClass="btn btn-primary" CommandName="Edit" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>

                            </table>


                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="/vali/js/plugins/sweetalert.min.js"></script>
    <script src="/vali/js/plugins/jquery.dataTables.min.js"></script>
    <script src="/vali/js/users.js"></script>
</asp:Content>
