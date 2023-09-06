<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="HydrologicalServices.aspx.cs" Inherits="vansystem.HydrologicalServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table {
            width: 450px;
            border-collapse: collapse;
            margin: 50px auto;
            margin-left: 1px;
            margin-top: 8px;
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
    <style>
        th {
            background: #51c551 !important;
            color: white !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.4);
        }

        th, td {
            padding: 0.75rem;
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
                        <h3><b>Ecosystem Services (Hydrological)</b></h3>
                        <div class="card-body" style="background-color: rgb(252, 252, 252); width: 1584px;">

                            <div style="display: flex">

                                <%--  <asp:LinkButton id="btnDownloadCSV" runat="server" type="button" class="btn btn-secondary" Text="Download CSV"><span class="fa fa-download"></span></asp:LinkButton>--%>
                                <div style="margin-right: 921px; margin-bottom: auto">
                                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                </div>

                                <div style="margin-left: 100px;">
                                    Show
                                zA<asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" Style="margin-top: auto; background: rgb(52, 52, 122); color: rgb(247, 251, 252);">
                                    <asp:ListItem Text="10" Value="10" />
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" />
                                </asp:DropDownList>
                                    Entries:
                               
                                </div>z
                                <%--<asp:Button ID="btnJson" runat="server" Text="Download Raw Data" OnClick="btnJson_Click" />--%>
                            </div>


                            <hr />

                            <div class="box-content">
                                <asp:GridView ID="GVHydrological" runat="server">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="HydrologicalServicesName" HeaderText="Surveyer Name" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="PhoneNumber" HeaderText="Surveyer number">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Designation" HeaderText="Surveyer Designation" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Latitude" HeaderText="Latitude" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Longitude" HeaderText="Longitude" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Altitude" HeaderText="Altitude" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Accuracy" HeaderText="Accuracy" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="StateName" HeaderText="State Name" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                          <asp:BoundField ItemStyle-Width="150px" DataField="circle_name" HeaderText="Circle Name" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="DivisionName" HeaderText="Division Name" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="RangeName" HeaderText="Range Name" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="BlockName" HeaderText="Block Name" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="VillageId" HeaderText="Village Name">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>

                                        <asp:BoundField ItemStyle-Width="150px" DataField="ForestTypeId" HeaderText="Type of forest">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>




                                        <asp:BoundField ItemStyle-Width="150px" DataField="HydroType" HeaderText="HydroType">
                                          </asp:BoundField>



                                        <asp:BoundField ItemStyle-Width="150px" DataField="Nature" HeaderText="Nature">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Stream" HeaderText="Stream">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Duration" HeaderText="Duration">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="VillageUse" HeaderText="VillageUse">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterPolluted" HeaderText="WaterPolluted">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Incidents" HeaderText="Incidents">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="ReportRiver" HeaderText="ReportRiver">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Ph" HeaderText="Ph">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Tds" HeaderText="Tds">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Ecoil" HeaderText="Ecoil">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterName" HeaderText="WaterName">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterbodyType" HeaderText="WaterbodyType">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterbodyStatus" HeaderText="WaterbodyStatus">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterbodySeason" HeaderText="WaterbodySeason">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterBodyPort" HeaderText="WaterBodyPort">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterExtent" HeaderText="WaterExtent">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterBodyReport" HeaderText="WaterBodyReport">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterPh" HeaderText="WaterPh">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterTds" HeaderText="WaterTds">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterEcoil" HeaderText="WaterEcoil">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterComm" HeaderText="WaterComm">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterFlora" HeaderText="WaterFlora">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterFauna" HeaderText="WaterFauna">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterBirds" HeaderText="WaterBirds">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="WaterBodyUsed" HeaderText="WaterBodyUsed">

                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                       
                                          <asp:BoundField ItemStyle-Width="150px" DataField="WaterVisitors" HeaderText="WaterVisitors">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Waterbody_livelihood_income" HeaderText="Waterbody_livelihood_income">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>

                                         <asp:BoundField ItemStyle-Width="150px" DataField="Well_ground" HeaderText="Well_ground">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                      <asp:BoundField ItemStyle-Width="150px" DataField="Well_depth" HeaderText="Well_depth">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Well_jan" HeaderText="Well_jan">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Well_may" HeaderText="Well_may" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Well_sept" HeaderText="Well_sept">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Well_ph" HeaderText="Well_ph">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Well_tds" HeaderText="Well_tds">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Well_ecoil" HeaderText="Well_ecoil">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Well_status" HeaderText="Well_status" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                          <asp:BoundField ItemStyle-Width="150px" DataField="Well_used" HeaderText="Well_used" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Well_comm" HeaderText="Well_comm" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        
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
