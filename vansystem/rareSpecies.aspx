<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="rareSpecies.aspx.cs" Inherits="vansystem.rareSpecies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <head>
        <title>Van - Rare Species</title>
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
                    <div style="margin-left: -19%; width: 113%; margin-top: 88px;">
                        <h3><b>Rare Species</b></h3>
                        <div class="card-body" style="background-color: rgb(252, 252, 252); width: 1583px;">
                            <div style="margin-left: 10px;">
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                <%-- <asp:Button ID="btnJson" runat="server" Text="Download Raw Data" OnClick="btnJson_Click" />--%>
                            </div>
                            <div style="margin-left: 1398px; margin-top: -27px;">
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
                                        <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Range" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRange" runat="server" Text='<%# Eval("range") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Block" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBlock" runat="server" Text='<%# Eval("block") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surveyor Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurveyorName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Other Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOtherName" runat="server" Text='<%# Eval("name_other") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone Number" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPhoneNumber" runat="server" Text='<%# Eval("phonenumber") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("designation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Trees (per Ha)" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrees" runat="server" Text='<%# Eval("treesperha") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Area" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblArea" runat="server" Text='<%# Eval("area") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Species Availability" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpeciesAvailability" runat="server" Text='<%# Eval("species_availability") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("datecreated") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collector Location" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCollectorLocation" runat="server" Text='<%# Eval("collector_location") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Device Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeviceName" runat="server" Text='<%# Eval("devicename") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Species Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrare_species_name" runat="server" Text='<%# Eval("rare_species_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Species Other Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrare_species_other_name" runat="server" Text='<%# Eval("rare_species_other_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Height" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblheight" runat="server" Text='<%# Eval("height") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Girth" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgirth" runat="server" Text='<%# Eval("girth") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Latitude" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllatitude" runat="server" Text='<%# Eval("latitude", "{0:F6}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Longitude" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllongitude" runat="server" Text='<%# Eval("longitude" , "{0:F6}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Altitude" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblaltitude" runat="server" Text='<%# Eval("altitude", "{0:F1}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Accuracy" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblaccuracy" runat="server" Text='<%# Eval("accuracy", "{0:F1}") %>'></asp:Label>
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
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>


