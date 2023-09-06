<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="PlotApproach.aspx.cs" Inherits="vansystem.PlotApproach" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- Include jQuery (example with version 3.x) -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<!-- Include Bootstrap JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

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

        @media (min-width: 768px) {
            .col-md-8, .col-md-4 {
                margin-right: -30%;
            }

            .col-md-8 {
                width: 65%;
            }

            .col-md-4 {
                width: 33%;
            }
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
         .sticky-columns {
        position: relative;
    }

   
    </style>
  <style>
    .fixed-column {
        position: sticky;
        left: 0;
        z-index: 1;
        background-color: white; /* Adjust the background color as needed */
    }
    .box-content{
         position: relative;
    }
</style>

   
   



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanel1" style="margin-left:4%;">
        <ContentTemplate>

            <div class="card" style="height: 826px; margin-left:4%; background-color: #EFEFEF;">
                <div class="container mt-6">
                    <div style="margin-left: -19%; width: 113%; margin-top: 88px;">
                        <h3><b>Plot Approach</b></h3>
                        <div class="card-body" style="background-color: rgb(252, 252, 252); width: 1584px;">
                            <div style="margin-left: 1110px;">


                                <%--  <asp:LinkButton id="btnDownloadCSV" runat="server" type="button" class="btn btn-secondary" Text="Download CSV"><span class="fa fa-download"></span></asp:LinkButton>--%>

                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />

                                <%--<asp:Button ID="btnJson" runat="server" Text="Download Raw Data" OnClick="btnJson_Click" />--%>
                                PageSize:
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10" />
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" />
                                    <asp:ListItem Text="All" Value="All" />
                                </asp:DropDownList>
                                <hr />

                            </div>



                            <div class="box-content">
                              
                                <asp:GridView ID="GVPlotApproach" AutoGenerateColumns="False" runat="server" DataKeyNames="PlotApproachId" OnRowEditing="GVPlotApproach_RowEditing" OnRowUpdating="GVPlotApproach_RowUpdating" OnRowCancelingEdit="GVPlotApproach_RowCancelingEdit" OnRowDataBound="GVPlotApproach_RowDataBound" AllowPaging="true" OnPageIndexChanging="GVPlotApproach_PageIndexChanging">
                                    <Columns>
                                       
                                              <asp:BoundField ItemStyle-Width="150px" DataField="PlotApproachId" HeaderText="SurveyID" ReadOnly="True">
                                            <ItemStyle Width="150px" CssClass="fixed-column"  />
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="name" HeaderText="Surveyor Name">
                                            <ItemStyle Width="150px"/>
                                         
                                        </asp:BoundField>
                                       
                                      


                                        <asp:BoundField ItemStyle-Width="150px" DataField="PhoneNumber" HeaderText="Surveyor Phone No">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField ItemStyle-Width="150px" DataField="Designation" HeaderText="Surveyor Designation" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>

                                         <asp:BoundField ItemStyle-Width="150px" DataField="Date_created" HeaderText="Survey Date" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                              <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField ItemStyle-Width="150px" DataField="Latitude" HeaderText="Latitude" ReadOnly="True" DataFormatString="{0:F6}">
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>

                                        <asp:BoundField ItemStyle-Width="150px" DataField="Longitude" HeaderText="Longitude" ReadOnly="True" DataFormatString="{0:F6}">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Altitude" HeaderText="Altitude" ReadOnly="True" DataFormatString="{0:F1}">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>


                                        <asp:BoundField ItemStyle-Width="150px" DataField="Accuracy" HeaderText="Accuracy" ReadOnly="True" DataFormatString="{0:F1}">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="StateName" HeaderText="State" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="DivisionName" HeaderText="Division" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="RangeName" HeaderText="Range" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="BlockName" HeaderText="Block" ReadOnly="True">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="VillageId" HeaderText="Village">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="CompartmentName" HeaderText="Compartment Number">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>

                                         <asp:TemplateField HeaderText="Topography of the plot">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGeneralTopography" runat="server" Text='<%# Eval("GeneralTopography")%>'></asp:Label>
                                            </ItemTemplate>
                                              <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlGeneralTopography" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Slope of the plot">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSlope" runat="server" Text='<%# Eval("Slope")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlSlope" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField ItemStyle-Width="150px" DataField="TypeOfDegradation" HeaderText="Type of Degradation" Visible="false">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>
                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="CharcoalMaking" HeaderText="Charcoal Making">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Charcoal Making">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCharcoalMaking" runat="server" Text='<%# Eval("CharcoalMaking")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlCharcoalMaking" runat="server">

                                                    <asp:ListItem Text="Yes" Value="118,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="DevelopmentProjects" HeaderText="Development Projects">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Development Projects">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDevelopmentProjects" runat="server" Text='<%# Eval("DevelopmentProjects")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlDevelopmentProjects" runat="server">

                                                    <asp:ListItem Text="Yes" Value="119,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="Fire" HeaderText="Fire">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Fire">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFire" runat="server" Text='<%# Eval("Fire")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlFire" runat="server">

                                                    <asp:ListItem Text="Yes" Value="120,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:BoundField ItemStyle-Width="150px" DataField="FirewoodExtraction" HeaderText="Firewood Extraction">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Firewood Extraction">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFirewoodExtraction" runat="server" Text='<%# Eval("FirewoodExtraction")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlFirewoodExtraction" runat="server">

                                                    <asp:ListItem Text="Yes" Value="121,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="Grazing" HeaderText="Grazing">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Grazing">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrazing" runat="server" Text='<%# Eval("Grazing")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlGrazing" runat="server">

                                                    <asp:ListItem Text="Yes" Value="122,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="IllegalLogging" HeaderText="Illegal Logging">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Illegal Logging">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIllegalLogging" runat="server" Text='<%# Eval("IllegalLogging")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlIllegalLogging" runat="server">

                                                    <asp:ListItem Text="Yes" Value="123,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:BoundField ItemStyle-Width="150px" DataField="Mining" HeaderText="Mining">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Mining">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMining" runat="server" Text='<%# Eval("Mining")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlMining" runat="server">

                                                    <asp:ListItem Text="Yes" Value="124,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="PathogenicAttack" HeaderText="Pathogenic Attack">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Pathogenic Attack">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPathogenicAttack" runat="server" Text='<%# Eval("PathogenicAttack")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlPathogenicAttack" runat="server">

                                                    <asp:ListItem Text="Yes" Value="125,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="InvasiveSpecies" HeaderText="Invasive Species">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Invasive Species">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvasiveSpecies" runat="server" Text='<%# Eval("InvasiveSpecies")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlInvasiveSpecies" runat="server">

                                                    <asp:ListItem Text="Yes" Value="126,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="Flood" HeaderText="Flood">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Flood">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFlood" runat="server" Text='<%# Eval("Flood")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlFlood" runat="server">

                                                    <asp:ListItem Text="Yes" Value="127,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="Drought" HeaderText="Drought">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Drought">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDrought" runat="server" Text='<%# Eval("Drought")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlDrought" runat="server">

                                                    <asp:ListItem Text="Yes" Value="128,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="Landslides" HeaderText="Landslides">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Landslides">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLandslides" runat="server" Text='<%# Eval("Landslides")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlLandslides" runat="server">

                                                    <asp:ListItem Text="Yes" Value="129,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="Avalanche" HeaderText="Avalanche">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Avalanche">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAvalanche" runat="server" Text='<%# Eval("Avalanche")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlAvalanche" runat="server">

                                                    <asp:ListItem Text="Yes" Value="130,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="Storm" HeaderText="Storm">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Storm">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStorm" runat="server" Text='<%# Eval("Storm")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlStorm" runat="server">

                                                    <asp:ListItem Text="Yes" Value="131,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="Cyclone" HeaderText="Cyclone">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Cyclone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCyclone" runat="server" Text='<%# Eval("Cyclone")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlCyclone" runat="server">

                                                    <asp:ListItem Text="Yes" Value="132,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="Earthquake" HeaderText="Earthquake">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Earthquake">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEarthquake" runat="server" Text='<%# Eval("Earthquake")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlEarthquake" runat="server">

                                                    <asp:ListItem Text="Yes" Value="133,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:BoundField ItemStyle-Width="150px" DataField="HeavyRainfall" HeaderText="Heavy Rainfall">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Heavy Rainfall">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHeavyRainfall" runat="server" Text='<%# Eval("HeavyRainfall")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlHeavyRainfall" runat="server">

                                                    <asp:ListItem Text="Yes" Value="134,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="HeavySnowfall" HeaderText="Heavy Snowfall">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Heavy Snowfall">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHeavySnowfall" runat="server" Text='<%# Eval("HeavySnowfall")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlHeavySnowfall" runat="server">

                                                    <asp:ListItem Text="Yes" Value="135,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="Other" HeaderText="Other">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Other">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOther" runat="server" Text='<%# Eval("Other")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlOther" runat="server">

                                                    <asp:ListItem Text="Yes" Value="136,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField ItemStyle-Width="150px" DataField="None" HeaderText="None">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="None">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNone" runat="server" Text='<%# Eval("None")%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlNone" runat="server">

                                                    <asp:ListItem Text="Yes" Value="137,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                       

                                        <asp:TemplateField HeaderText="Sighting traces of flagship species:Mammals">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMammals" runat="server" Text='<%# TrimTo(Eval("Mammals"),30)%>'></asp:Label>


                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:ListBox ID="lstMammals" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Mammals_comments" HeaderText="Mammals_Comments">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        
                                        <asp:TemplateField HeaderText="Sighting traces of flagship species:Birds">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBirds" runat="server" Text='<%# TrimTo(Eval("Birds"),30)%>'></asp:Label>

                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>

                                                <asp:ListBox ID="lstBirds" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField ItemStyle-Width="150px" DataField="Birds_comments" HeaderText="Birds Comments">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>

                                        <%--  <asp:BoundField ItemStyle-Width="150px" DataField="Reptiles" HeaderText="Reptiles">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Sighting traces of flagship species:Reptiles">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReptiles" runat="server" Text=' <%# TrimTo(Eval("Reptiles"),30)%>'></asp:Label>

                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:ListBox ID="lstReptiles" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Reptiles_comments" HeaderText="Reptiles_Comments">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                      

                                        <%--   <asp:BoundField ItemStyle-Width="150px" DataField="Amphibians" HeaderText="Amphibians">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Sighting traces of flagship species:Amphibians">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmphibians" runat="server" Text='<%# TrimTo(Eval("Amphibians"),30)%>'></asp:Label>

                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>

                                                <asp:ListBox ID="lstAmphibians" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                          <asp:BoundField ItemStyle-Width="150px" DataField="Amphibians_comments" HeaderText="Amphibians_Comments">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="Plants" HeaderText="Plants">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Plants">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlants" runat="server" Text='<%# TrimTo(Eval("Plants"),30)%>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                            <EditItemTemplate>
                                                <asp:ListBox ID="lstPlants" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Fuel" HeaderText="Removal of fuel wood">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Fodder" HeaderText="Removal of fodder">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>
                                      <%--  <asp:BoundField ItemStyle-Width="150px" DataField="Formname" HeaderText="Form Name">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="remark" HeaderText="Remark">
                                            <ItemStyle Width="150px" />
                                             <HeaderStyle CssClass="scrolled"></HeaderStyle>
                <ItemStyle CssClass="scrolled"></ItemStyle>
                                        </asp:BoundField>
                                        
                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="device_id" HeaderText="Device_Id">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Form_id" HeaderText="Form_Id">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Date_created" HeaderText="Date_Created">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                       
                                       
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Version" HeaderText="Version">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Synced" HeaderText="Synced">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Sync_date" HeaderText="Sync_Date">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>
                                       
                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="Form_sync_date" HeaderText="Form_Sync_Date">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="TRIAL427" HeaderText="TRIAL427">

                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>--%>





                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" CssClass="btn btn-primary" CommandName="Edit" runat="server" CommandArgument='<%# Container.DataItemIndex %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" CommandName="Update" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" CommandName="cancel" />
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>


                            </div>

                            <ul class="pagination" id="pagingnation" runat="server" style="margin-top: 6px; margin-left: 9px; color: #5cb85c">

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
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnJson" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%-- <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
        rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/css/bootstrap-multiselect.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/js/bootstrap-multiselect.js"></script>

    <script type="text/javascript">
        function initializeMultiselect() {
            $('[id*=lstMammals]').multiselect({
                includeSelectAllOption: true
            });
        }

        function initializeMultiselect1() {
            $('[id*=lstBirds]').multiselect({
                includeSelectAllOption: true
            });
        }

        function initializeMultiselect2() {
            $('[id*=lstReptiles]').multiselect({
                includeSelectAllOption: true
            });
        }

        function initializeMultiselect3() {
            $('[id*=lstAmphibians]').multiselect({
                includeSelectAllOption: true
            });
        }

        function initializeMultiselect4() {
            $('[id*=lstPlants]').multiselect({
                includeSelectAllOption: true
            });
        }

        // Function to re-initialize the multiselect after partial postback (ASP.NET AJAX)
        function pageLoad(sender, args) {
            initializeMultiselect();
            initializeMultiselect1();
            initializeMultiselect2();
            initializeMultiselect3();
            initializeMultiselect4();
        }
        // Initialize the multiselect on page load
        $(document).ready(function () {
            initializeMultiselect();
            initializeMultiselect1();
            initializeMultiselect2();
            initializeMultiselect3();
            initializeMultiselect4();
        });


    </script>
    <script>
        // Function to initialize sticky columns
        function initStickyColumns() {
            var grid = document.getElementById("<%= GVPlotApproach.ClientID %>");
            var headers = grid.querySelectorAll(".sticky-columns th");

            headers.forEach(function (header) {
                var originalWidth = header.offsetWidth;
                var cellIndex = header.cellIndex;
                var originalCell = header.parentElement;

                header.style.width = originalWidth + "px";

                var cells = grid.querySelectorAll("tr td:nth-child(" + (cellIndex + 1) + ")");
                cells.forEach(function (cell) {
                    cell.style.minWidth = originalWidth + "px";
                    cell.style.maxWidth = originalWidth + "px";
                });
            });
        }

        // Hook up the initialization function to the window load event
        window.addEventListener("load", initStickyColumns);
    </script>




</asp:Content>

