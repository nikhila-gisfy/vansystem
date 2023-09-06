<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="PlotEnumeration.aspx.cs"
    Inherits="vansystem.PlotEnumeration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <meta name="viewport" content="width=device-width, initial-scale=1.0">
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
    <style>     th {        background:#51c551!important;        color:white!important;        position: sticky!important;        top: 0;        box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.4);    }    th, td {        padding: 0.75rem;    }</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanel1">
        <ContentTemplate>
           <div class="card" style="height: 826px; background-color: #EFEFEF">
                <div class="container mt-6">
                    <div style="margin-left: -19%; width: 113%; margin-top: 88px;">
                        <h3><b>Plot Enumeration</b></h3>
                        <div style="margin-left: 450px">
                            <asp:LinkButton runat="server" ID="masterdata" class="btn btn-xs btn-success" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="masterdata_Command" Style="margin-top: -79px;" Text="Master Data"> </asp:LinkButton>

                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Tree" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Tree_Command" Text="Tree"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Bamboo" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Bamboo_Command" Text="Bamboo"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Shurb" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Shurb_Command" Text="Shurb"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Sapling" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Sapling_Command" Text="Sapling"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Herb" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Herb_Command" Text="Herb"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Seedling" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Seedling_Command" Text="Seedling"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Grass" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Grass_Command" Text="Grass"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Climber" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Climber_Command" Text="Climber"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="LeafLitter" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="LeafLitter_Command" Text="Leaf Litter"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="WoodyLitter" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="WoodyLitter_Command" Text="Woody Litter"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Soil" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Soil_Command" Text="Soil"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="DeadWood" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="DeadWood_Command" Text="Dead Wood"> </asp:LinkButton>
                        </div>

                        <div class="card-body" style="background-color: rgb(252, 252, 252); width: 1584px;">
                               <div style="margin-left: 600px; display:flex;">
                                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel(Plot Enumeration)" OnClick="btnExport_Click" style="margin-left:20px" />
                                     <asp:Button ID="btnExport1" runat="server" Text="Export to Excel(Plot Enumeration-Dry Data)" OnClick="btnExport1_Click" style="margin-left:20px;margin-right:100px;" />
                              PageSize:
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10" />
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" />
                                </asp:DropDownList>
                                <hr />
                             </div>
                            <div class="box-content">

                                <asp:GridView ID="GVPlotEnumeration" AutoGenerateColumns="False"  runat="server" DataKeyNames="PlotEnumerationId">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="PlotEnumerationId" HeaderText="Surveyor ID">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Surveyor" HeaderText="Surveyor Name">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Mobile" HeaderText="Surveyor_Phone_No">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Designation" HeaderText="Surveyor_Designation">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Date_created" HeaderText="Survey_Date">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Latitude" HeaderText="Latitude" DataFormatString="{0:F6}">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Longitude" HeaderText="Longitude" DataFormatString="{0:F6}">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Altitude" HeaderText="Altitude" DataFormatString="{0:F1}">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Accuracy" HeaderText="Accuracy" DataFormatString="{0:F1}">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                           <asp:BoundField ItemStyle-Width="150px" DataField="StateName" HeaderText="State">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="DivisionName" HeaderText="Division">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="RangeName" HeaderText="Range">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="BlockName" HeaderText="Block">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="VillageId" HeaderText="Village">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="compartmentname" HeaderText="Compartment Number">


                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                          <asp:BoundField ItemStyle-Width="150px" DataField="PlotId" HeaderText="Plot Number">


                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Tree" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">

                                            <ItemTemplate>

                                                <asp:LinkButton runat="server" class="btn btn-xs btn-success" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Tree_link" Text="Tree"> </asp:LinkButton>

                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bamboo" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">

                                            <ItemTemplate>

                                                <asp:LinkButton runat="server" class="btn btn-xs btn-success" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Bamboo_link" Text="Bamboo"> </asp:LinkButton>

                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shurb" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">

                                            <ItemTemplate>

                                                <asp:LinkButton runat="server" class="btn btn-xs btn-success" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Shrub_link" Text="Shrub"> </asp:LinkButton>

                                            </ItemTemplate>


                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sapling" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>

                                                <asp:LinkButton runat="server" class="btn btn-xs btn-success" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Sapling_link" Text="Sapling"> </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Herb" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>

                                                <asp:LinkButton runat="server" class="btn btn-xs btn-success" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Herb_link" Text="Herb"> </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Seedling" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>

                                                <asp:LinkButton runat="server" class="btn btn-xs btn-success" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Seedling_link" Text="Seedling"> </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grass" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>

                                                <asp:LinkButton runat="server" class="btn btn-xs btn-success" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Grass_link" Text="Grass"> </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Climber" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>

                                                <asp:LinkButton runat="server" class="btn btn-xs btn-success" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Climber_link" Text="Climber"> </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leaf Litter" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>

                                                <asp:LinkButton runat="server" class="btn btn-xs btn-success" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Leaflitter_link" Text="Leaf Litter"> </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Woody Litter" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">

                                            <ItemTemplate>

                                                <asp:LinkButton runat="server" class="btn btn-xs btn-success" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Woodylitter_link" Text="Woody Litter"> </asp:LinkButton>

                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Soil" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">

                                            <ItemTemplate>

                                                <asp:LinkButton runat="server" class="btn btn-xs btn-success" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Soil_link" Text="Soil"> </asp:LinkButton>

                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dead Wood" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">

                                            <ItemTemplate>

                                                <asp:LinkButton runat="server" class="btn btn-xs btn-success" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Deadwood_link" Text="Dead Wood"> </asp:LinkButton>

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
          <Triggers>
            <asp:PostBackTrigger ControlID="btnExport1" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>
