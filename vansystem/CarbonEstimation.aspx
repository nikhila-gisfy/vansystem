<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="CarbonEstimation.aspx.cs" Inherits="vansystem.CarbonEstimation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <head>
        <title>Van - Carbon Estimation</title>
        <style>
            table {
                border-collapse: collapse;
                margin: 50px auto;
                margin-left: 9px;
                margin-top: 14px;
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
                MARGIN-TOP: -33PX;
                max-height: 600px;
                overflow: scroll;
            }
        </style>
        <style type="text/css">
            .Background {
                background-color: Black;
                filter: alpha(opacity=90);
                opacity: 0.8;
            }

            .Popup {
                background-color: #FFFFFF;
                border-width: 3px;
                border-style: solid;
                border-color: black;
                padding-top: 10px;
                padding-left: 10px;
                width: 500px;
                height: 250px;
            }

            .lbl {
                font-size: 16px;
                font-style: italic;
                font-weight: bold;
            }
        </style>
    </head>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanel1">
        <ContentTemplate>
            <div class="card" style="height: 826px; background-color: #EFEFEF">
                <div class="container mt-6">
                    <div style="margin-left: -19%; width: 113%; margin-top: 88px;">
                        <h3><b>Carbon Estimation</b></h3>

                        <div class="card-body" style="background-color: rgb(252, 252, 252); width: 1584px;">
                            <div style="margin-left: 10px;">
                                <asp:Button runat="server" Text="Carbon Estimation" />
                                <asp:Button ID="btnPdf" runat="server" Text="PDF" OnClick="btnPdf_Click" />
                                <asp:Button ID="btnExport" runat="server" Text="Excel" OnClick="btnExport_Click" />
                                <asp:Button ID="btnrange" runat="server" Text="Rangewise" />
                                <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnrange"
                                    CancelControlID="closebtn" BackgroundCssClass="Background">
                                </ajaxToolkit:ModalPopupExtender>

                                <asp:Panel ID="Panl1" runat="server" CssClass="Popup" Style="position: fixed; z-index: 10004; left: 496.5px; top: 128.5px;">

                                    <span id="closebtn" class="close">&times;</span>
                                    <asp:GridView ID="gvrangewise" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="range_name" HeaderText="Range Name">
                                                <ItemStyle Width="210px" />
                                            </asp:BoundField>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="totalcarbon" HeaderText="Total Carbon">
                                                <ItemStyle Width="210px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                            <div style="margin-left: 1404px; margin-top: -27px;">
                                PageSize:
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true">
                                    <asp:ListItem Text="10" Value="10" />
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" />
                                </asp:DropDownList>
                                <hr />
                            </div>
                            <div class="box-content">
                                <asp:GridView ID="gvVLI" runat="server" AutoGenerateColumns="false" OnRowCommand="gvVLI_RowCommand" GridLines="Horizontal">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Range" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRange" runat="server" Text='<%# Eval("range_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Block" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBlock" runat="server" Text='<%# Eval("block_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Division" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDivision" runat="server" Text='<%# Eval("division_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Compartment" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompartment" runat="server" Text='<%# Eval("compartment_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Plot" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlot" runat="server" Text='<%# Eval("plot_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AGB Carbon (in tons)" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAGB" runat="server" Text='<%# Eval("total_agb") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BGB Carbon (in tons)" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBGB" runat="server" Text='<%# Eval("total_bgb") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Carbon (in tons)" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalCarbon" runat="server" Text='<%# Eval("total_carbon") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="btnPdf"></asp:PostBackTrigger>
            <%--<asp:PostBackTrigger ControlID="btnRangewise"></asp:PostBackTrigger>--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
