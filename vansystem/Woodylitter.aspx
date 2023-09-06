<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="Woodylitter.aspx.cs" Inherits="vansystem.Woodylitter" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanel1">
        <ContentTemplate>

            <div class="card" style="height: 826px; background-color: #EFEFEF">
                <div class="container mt-6">
                    <div style="margin-left: -19%; width: 113%; margin-top: 88px;">
                        <h3><b>Plot Enumeration-Woody Litter</b></h3>
                        <div style="margin-left: 450px">
                            <asp:LinkButton runat="server" ID="masterdata" class="btn btn-xs btn-success" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="masterdata_Command" Style="margin-top: -79px;" Text="Master Data"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Tree1" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Tree_Command" Text="Tree"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Bamboo1" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Bamboo_Command" Text="Bamboo"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Shurb" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Shurb_Command" Text="Shurb"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Sapling1" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Sapling_Command" Text="Sapling"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Herb1" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Herb_Command" Text="Herb"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Seedling" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Seedling_Command" Text="Seedling"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Grass" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Grass_Command" Text="Grass"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Climber" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Climber_Command" Text="Climber"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="LeafLitter" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="LeafLitter_Command" Text="Leaf Litter"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="WoodyLitter1" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="WoodyLitter_Command" Text="Woody Litter"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="Soil" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="Soil_Command" Text="Soil"> </asp:LinkButton>
                            <asp:LinkButton runat="server" class="btn btn-xs btn-success" ID="DeadWood" Style="margin-top: -79px;" CommandArgument='<%# Eval("PlotEnumerationId") %>' OnCommand="DeadWood_Command" Text="Dead Wood"> </asp:LinkButton>
                        </div>
                        <div class="card-body" style="background-color: rgb(252, 252, 252); width: 1584px;">
                            <div style="margin-left: 1400px;">
                                PageSize:
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" Style="margin-top: auto; background: rgb(52, 52, 122); color: rgb(247, 251, 252);">
                                    <asp:ListItem Text="10" Value="10" />
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" />
                                </asp:DropDownList>
                            </div>
                            <div class="box-content">
                                <asp:GridView ID="GVWoodylitter" runat="server" AutoGenerateColumns="false" OnRowEditing="GVWoodylitter_RowEditing" OnRowUpdating="GVWoodylitter_RowUpdating" OnRowCancelingEdit="GVWoodylitter_RowCancelingEdit" Style="width: 1520px" DataKeyNames="id">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="enumeration_id" HeaderText="Surveyor ID" ReadOnly="true">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="direction" HeaderText="Select corner plot">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="woody_litter_weight_in_grams" HeaderText="Woody litter weight (in Grms)">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="sub_sample_weight" HeaderText="Sample weight">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="sample_number" HeaderText="Sample Number">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="remarks" HeaderText="Remarks if any">
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