<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="CulturalServices.aspx.cs" Inherits="vansystem.CulturalServices" %>

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
                        <h3><b>Ecosystem Services (Cultural)</b></h3>
                        <div class="card-body" style="background-color: rgb(252, 252, 252); width: 1584px;">
                            <div style="display: flex">
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
                            </div>
                            <hr />

                            <div class="box-content">
                                <asp:GridView ID="GVCultural" runat="server" AutoGenerateColumns="false">
                                     <Columns>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="CulturalServicesName" HeaderText="Surveyer Name" ReadOnly="True">
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




                                        <asp:BoundField ItemStyle-Width="150px" DataField="CulturalType" HeaderText="CulturalType">
                                          </asp:BoundField>



                                        <asp:BoundField ItemStyle-Width="150px" DataField="Ownership" HeaderText="Ownership">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="OwnershipEts" HeaderText="OwnershipEts">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Area" HeaderText="Area">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="AreaEts" HeaderText="AreaEts">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="SpecialImportance" HeaderText="SpecialImportance">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="SiteDetail" HeaderText="SiteDetail">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="FeeDetail" HeaderText="FeeDetail">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Tkdl_ipr_ets" HeaderText="Tkdl_ipr_ets">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Communities_ets" HeaderText="Communities_ets">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="NumberOfCluster" HeaderText="NumberOfCluster">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Belief" HeaderText="Belief">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Protection" HeaderText="Protection">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="SocialCustom" HeaderText="SocialCustom">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Multi_sc_month" HeaderText="Multi_sc_month">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Multi_ets_month" HeaderText="Multi_ets_month">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Species" HeaderText="Species" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Visitors" HeaderText="Visitors">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Species_protected_plant" HeaderText="Species_protected_plant">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Species_protected_animal" HeaderText="Species_protected_animal">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Visitors_ets" HeaderText="Visitors_ets">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Multi_ind_know" HeaderText="Multi_ind_know">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Incorporation_ets" HeaderText="Incorporation_ets">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Multi_income_activity" HeaderText="Multi_income_activity">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Development_ets" HeaderText="Development_ets">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Number_ipr_ets" HeaderText="Number_ipr_ets">

                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                       
                                          <asp:BoundField ItemStyle-Width="150px" DataField="Socialcustom_ets" HeaderText="Socialcustom_ets">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Reg_ets" HeaderText="Reg_ets">
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
