﻿<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="Bamboo.aspx.cs" Inherits="vansystem.Bamboo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table {
            width: 450px;
            border-collapse: collapse;
            margin: 50px auto;
            margin-left: 1px;
            margin-top:8px;
        }

        th {
            background: #5cb85c;
            color: white;
            font-weight: bold;
            text-align: center;
        }

        td, th {
            padding: 10px;
            border: 1px solid #ccc;
            text-align: center;
            font-size: 15px;
        }

        .labels tr td {
            background-color: #2cc16a;
            font-weight: bold;
            color: #fff;
        }

        .label tr td label {
            display: block;
        }


        [data-toggle="toggle"] {
            display: none;
        }

        .box-content {
            max-height: 600px;
            overflow: scroll;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanel1">
        <ContentTemplate>

             <div class="card" style="height: 826px; background-color: #EFEFEF">
                <div class="container mt-6">
                    <div style="margin-left: -19%; width: 113%; margin-top: 88px;">
                        <h3><b>Plot Enumeration-Bamboo</b></h3>
                         <div style="margin-left:450px">
                        <div class="card-body" style="background-color: rgb(252, 252, 252); width: 1584px;">
                            <div style="margin-left: 1400px;">
                               PageSize:
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" style="margin-top:auto;background: rgb(52, 52, 122);color: rgb(247, 251, 252);">
                                    <asp:ListItem Text="10" Value="10" />
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" />
                                </asp:DropDownList>
                            </div>
                            <div class="box-content">
                                <asp:GridView ID="GVBamboo" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames="id" OnRowEditing="GVBamboo_RowEditing" OnRowCancelingEdit="GVBamboo_RowCancelingEdit" OnRowUpdating="GVBamboo_RowUpdating" OnRowDataBound="GVBamboo_RowDataBound" style="width:1520px">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="PlotEnumerationId" HeaderText="Surveyor ID" ReadOnly="true">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Local Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLocal_Name" runat="server" Text='<%# Eval("Local_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlLocal_Name" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField ItemStyle-Width="150px" DataField="bamboonumber" HeaderText="Specify other">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                       <%-- <asp:BoundField ItemStyle-Width="150px" DataField="bambooname" HeaderText="Bamboo Name">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>--%>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="bamboodiameter" HeaderText="Diameter of Bamboo Clump (in m)">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="bambooculms" HeaderText="No. of Bamboo Culms">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="bamboogbh" HeaderText="GBH">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="bambooheight" HeaderText="Height (in meters)">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="bambooremarks" HeaderText="Remarks if any">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                                                             <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" CssClass="btn btn-primary" CommandName="Edit" runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" CommandName="Update" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" CommandName="cancel" />
                                            </EditItemTemplate>

                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <ul class="pagination" style="margin-top: 6px; margin-left: 9px; color: #5cb85c">

                            <asp:Repeater ID="rptPager" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                            Enabled='<%# Eval("Enabled") %>' OnClick="lnkPage_Click"></asp:LinkButton>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>