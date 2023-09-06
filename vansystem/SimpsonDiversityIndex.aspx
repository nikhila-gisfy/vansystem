<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="SimpsonDiversityIndex.aspx.cs" Inherits="vansystem.SimpsonDiversityIndex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <head>
        <title>Van - Simpson Diversity Index</title>
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
    </head>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanel1">
        <ContentTemplate>
            <div class="card" style="height: 826px; background-color: #EFEFEF">
                <div class="container mt-3">
                    <div style="margin-left: -207px; width: 113%; margin-top: 88px;">
                        <h3><b>Simpson Diversity Index</b></h3>
                        <div class="card-body" style="background-color: rgb(252, 252, 252); width: 1580px;">
                            <div style="margin-left: 10px;">
                                <asp:Button ID="btnPdf" runat="server" Text="PDF" OnClick="btnPdf_Click" />
                                <asp:Button ID="btnExport" runat="server" Text="Excel" OnClick="btnExport_Click" />
                                <asp:Button ID="btnRangewise" runat="server" Text="Rangewise" OnClick="btnRangewise_Click" />
                            </div>
                            <div style="margin-left: 1190px; margin-top: -27px;">
                                PageSize:
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10" />
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" />
                                </asp:DropDownList>
                                <hr />
                            </div>
                            <div class="box-content">
                                <asp:GridView ID="gvVLI" runat="server" AutoGenerateColumns="false" OnRowCommand="gvVLI_RowCommand" GridLines="Horizontal">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Plot" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlot" runat="server" Text='<%# Eval("Plot") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="SC Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSCName" runat="server" Text='<%# Eval("SCName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Longitude" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLongitude" runat="server" Text='<%# Eval("Longitude") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Latitude" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLatitude" runat="server" Text='<%# Eval("Latitude") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total No of Species" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalNoOfSpecies" runat="server" Text='<%# Eval("TotalNoOfSpecies") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No of Species" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoOfSpecies" runat="server" Text='<%# Eval("NoOfSpecies") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pi" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPi" runat="server" Text='<%# Eval("Pi") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pi Squre" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPiSqure" runat="server" Text='<%# Eval("PiSqure") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Simpson Value" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSimpsonValue" runat="server" Text='<%# Eval("SimpsonValue") %>'></asp:Label>
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
            <asp:PostBackTrigger ControlID="btnRangewise"></asp:PostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
