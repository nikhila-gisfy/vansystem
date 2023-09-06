<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="ProvisioningServices.aspx.cs" Inherits="vansystem.ProvisioningServices" %>

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
                        <h3><b>Ecosystem Services (provisioning)</b></h3>
                        <div class="card-body" style="background-color: rgb(252, 252, 252); width: 1584px;">



                            <div style="display: flex">

                                <%--  <asp:LinkButton id="btnDownloadCSV" runat="server" type="button" class="btn btn-secondary" Text="Download CSV"><span class="fa fa-download"></span></asp:LinkButton>--%>
                                <div style="margin-right: 921px; margin-bottom: auto">
                                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                </div>

                                <div style="margin-left: 100px;">
                                    Show
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" Style="margin-top: auto; background: rgb(52, 52, 122); color: rgb(247, 251, 252);">
                                    <asp:ListItem Text="10" Value="10" />
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" />
                                </asp:DropDownList>
                                    Entries:
                               
                                </div>
                                <%--<asp:Button ID="btnJson" runat="server" Text="Download Raw Data" OnClick="btnJson_Click" />--%>
                            </div>
                            <hr />

                            <div class="box-content">
                                <asp:GridView ID="GVProservices" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="lProvisioningServicesName" HeaderText="Surveyer Name" ReadOnly="True">
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




                                        <asp:BoundField ItemStyle-Width="150px" DataField="Spn_pro_type" HeaderText="Spn_pro_type">
                                          </asp:BoundField>



                                        <asp:BoundField ItemStyle-Width="150px" DataField="Spn_pro_unit" HeaderText="Spn_pro_unit">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_name1" HeaderText="Pro_name1">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_units1" HeaderText="Pro_units1">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_gen1" HeaderText="Pro_gen1">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_name2" HeaderText="Pro_name2">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_units2" HeaderText="Pro_units2">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_gen2" HeaderText="Pro_gen2">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_name3" HeaderText="Pro_name3">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_units3" HeaderText="Pro_units3">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_gen3" HeaderText="Pro_gen3">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_name4" HeaderText="Pro_name4">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_units4" HeaderText="Pro_units4">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_gen4" HeaderText="Pro_gen4">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Spn_pro_other" HeaderText="Spn_pro_other">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Spn_pro_type_y" HeaderText="Spn_pro_type_y">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Spn_pro_unit_y" HeaderText="Spn_pro_unit_y" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_name1_y" HeaderText="Pro_name1_y">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_units1_y" HeaderText="Pro_units1_y">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_gen1_y" HeaderText="Pro_gen1_y">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_name2_y" HeaderText="Pro_name2_y">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_units2_y" HeaderText="Pro_units2_y">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_gen2_y" HeaderText="Pro_gen2_y">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_name3_y" HeaderText="Pro_name3_y">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_units3_y" HeaderText="Pro_units3_y">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_gen3_y" HeaderText="Pro_gen3_y">

                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                       
                                          <asp:BoundField ItemStyle-Width="150px" DataField="Pro_units4_y" HeaderText="Pro_units4_y" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_gen4_y" HeaderText="Pro_gen4_y">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_measure" HeaderText="Pro_measure" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_budget" HeaderText="Pro_budget" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Spn_pro_demand" HeaderText="Spn_pro_demand" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Spn_pro_excess" HeaderText="Spn_pro_excess">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_yes" HeaderText="Pro_yes" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_total" HeaderText="Pro_total" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_record" HeaderText="Pro_record" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Spn_pro_grazer" HeaderText="Spn_pro_grazer" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_area" HeaderText="Pro_area" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_name" HeaderText="Pro_name">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>

                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_migration" HeaderText="Pro_migration">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                         <asp:BoundField ItemStyle-Width="150px" DataField="Pro_camps" HeaderText="Pro_camps" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Pro_approx" HeaderText="Pro_approx">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Women_spend_time_in_collection_of" HeaderText="Women_spend_time_in_collection_of" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Women_spend_time_in_collection" HeaderText="Women_spend_time_in_collection" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Women_distance_travel_for_collection" HeaderText="Women_distance_travel_for_collection" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Hardwork_be_reduced_in_collection" HeaderText="Hardwork_be_reduced_in_collection" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Rorest_department_consulting_community" HeaderText="Rorest_department_consulting_community" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="TRIAL355" HeaderText="TRIAL355" ReadOnly="True">
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
