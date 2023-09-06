<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="PlotDescription.aspx.cs" Inherits="vansystem.PlotDescription" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <style>
        table {
            width: 3000px;
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
            width: 400px;
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
        /*.ItemStyle
        {
            
            width: 300px;
           
        }*/


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
                        <h3><b>Plot Description</b></h3>
                        <div class="card-body" style="background-color: rgb(252, 252, 252); width: 1584px;">
                            <div style="margin-left: 1110px;">

                                <%--  <asp:LinkButton id="btnDownloadCSV" runat="server" type="button" class="btn btn-secondary" Text="Download CSV"><span class="fa fa-download"></span></asp:LinkButton>--%>

                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />

                               <%-- <asp:Button ID="btnJson" runat="server" Text="Download Raw Data" OnClick="btnJson_Click" />--%>
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
                                <asp:GridView ID="GVPlotDescription" OnPageIndexChanging="GVPlotDescription_PageIndexChanging"  AutoGenerateColumns="False" runat="server" DataKeyNames="PlotDescId" OnRowCancelingEdit="GVPlotDescription_RowCancelingEdit" OnRowEditing="GVPlotDescription_RowEditing" OnRowUpdating="GVPlotDescription_RowUpdating" OnRowDataBound="GVPlotDescription_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="PlotDescId" HeaderText="Survey ID" ReadOnly="True">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PlotDescName" HeaderText="Surveyor Name">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>

                                          <asp:BoundField DataField="PhoneNumber" HeaderText="Surveyor Phone No">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Designation" HeaderText="Surveyor Designation">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>

                                         <asp:BoundField DataField="DateCreated" HeaderText="Survey Date">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Latitude" HeaderText="Latitude" ReadOnly="True" DataFormatString="{0:F6}">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Longitude" HeaderText="Longitude" ReadOnly="True" DataFormatString="{0:F6}">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Altitude" HeaderText="Altitude" ReadOnly="True" DataFormatString="{0:F1}">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Accuracy" HeaderText="Accuracy" ReadOnly="True" DataFormatString="{0:F1}">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="StateName" HeaderText="State" ReadOnly="True">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DivisionName" HeaderText="Division" ReadOnly="True">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RangeName" HeaderText="Range" ReadOnly="True">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BlockName" HeaderText="Block" ReadOnly="True">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VillageName" HeaderText="Village">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompartmentName" HeaderText="Compartment Number">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PlotName" HeaderText="Plot Number">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                            <asp:TemplateField HeaderText="Legal Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllegalStatus" runat="server" Text='<%# Eval("LegalStatus")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddllegalStatus" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Land use type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLanduse" runat="server" Text='<%# Eval("Landuse")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlLanduse" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:BoundField DataField="Rocks" HeaderText="Type of rock">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                      
                                        <asp:TemplateField HeaderText="Topography of the plot">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGeneral" runat="server" Text='<%# Eval("General")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlGeneral" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Slope of the plot">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSlope" runat="server" Text='<%# Eval("Slope")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlSlope" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="Soildepth" HeaderText="Soil Depth (in cm)">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                      
                                        <asp:TemplateField HeaderText="Soil Texture">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSoiltexture" runat="server" Text='<%# Eval("Soiltexture")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlSoiltexture" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Soil Permeability">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSoilpermeability" runat="server" Text='<%# Eval("Soilpermeability")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlSoilpermeability" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Soil Erosion">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSoilerosion" runat="server" Text='<%# Eval("Soilerosion")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlSoilerosion" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Crop composition of the plot">
                                            <ItemTemplate>
                                                <%# TrimTo(Eval("Crop"), 30) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                         <asp:TemplateField HeaderText="Regeneration status of the plot">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegeneration" runat="server" Text='<%# Eval("Regeneration")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlRegeneration" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Grazing">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCc_Grazing" runat="server" Text='<%# Eval("Cc_Grazing")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlCc_Grazing" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Injury/ damage to crop, if any">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInjury" runat="server" Text='<%# Eval("Injury")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlInjury" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Presence Of Bamboo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPresenceOfBamboo" runat="server" Text='<%# Eval("PresenceOfBamboo")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlPresenceOfBamboo" runat="server">

                                                    <asp:ListItem Text="Yes" Value="1,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Bamboo Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBambooQuality" runat="server" Text='<%# Eval("BambooQuality")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlBambooQuality" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>                                     
                                          <asp:TemplateField HeaderText="Bamboo Regeneration">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBambooRegeneration" runat="server" Text='<%# Eval("BambooRegeneration")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlBambooRegeneration" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Presence Of Grass">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPresenceOfGrass" runat="server" Text='<%# Eval("PresenceOfGrass")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlPresenceOfGrass" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Presence Of Weeds">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPresenceOfWeeds" runat="server" Text='<%# Eval("PresenceOfWeeds")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlPresenceOfWeeds" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name Of Weed">
                                            <ItemTemplate>
                                                <%# TrimTo(Eval("NameOfWeed"), 30) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:BoundField DataField="Plantation" HeaderText="Plantation">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                           <asp:TemplateField HeaderText="Type of Water body">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWaterBodyType" runat="server" Text='<%# Eval("WaterBodyType")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlWaterBodyType" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status of Water body">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWaterBodyStatus" runat="server" Text='<%# Eval("WaterBodyStatus")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlWaterBodyStatus" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seasonality of Water body">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWaterBodySeasonality" runat="server" Text='<%# Eval("WaterBodySeasonality")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlWaterBodySeasonality" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Potability of Water body">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWaterBodyPotability" runat="server" Text='<%# Eval("WaterBodyPotability")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlWaterBodyPotability" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="TypeOfDegradation" HeaderText="Type Of Degradation" Visible="false">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Charcoal Making">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCharcoalMaking" runat="server" Text='<%# Eval("CharcoalMaking")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlCharcoalMaking" runat="server">

                                                    <asp:ListItem Text="Yes" Value="118,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="Development Projects">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDevelopmentProjects" runat="server" Text='<%# Eval("DevelopmentProjects")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlDevelopmentProjects" runat="server">

                                                    <asp:ListItem Text="Yes" Value="119,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Fire">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFire" runat="server" Text='<%# Eval("Fire")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlFire" runat="server">

                                                    <asp:ListItem Text="Yes" Value="120,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Firewood Extraction">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFirewoodExtraction" runat="server" Text='<%# Eval("FirewoodExtraction")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlFirewoodExtraction" runat="server">

                                                    <asp:ListItem Text="Yes" Value="121,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Grazing">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrazing" runat="server" Text='<%# Eval("Grazing")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlGrazing" runat="server">

                                                    <asp:ListItem Text="Yes" Value="122,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                     
                                        <asp:TemplateField HeaderText="Illegal Logging">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIllegalLogging" runat="server" Text='<%# Eval("IllegalLogging")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlIllegalLogging" runat="server">

                                                    <asp:ListItem Text="Yes" Value="123,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Mining">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMining" runat="server" Text='<%# Eval("Mining")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlMining" runat="server">

                                                    <asp:ListItem Text="Yes" Value="124,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                      
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
                                      
                                        <asp:TemplateField HeaderText="Invasive Species">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvasiveSpecies" runat="server" Text='<%# Eval("InvasiveSpecies")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlInvasiveSpecies" runat="server">

                                                    <asp:ListItem Text="Yes" Value="126,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Flood">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFlood" runat="server" Text='<%# Eval("Flood")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlFlood" runat="server">

                                                    <asp:ListItem Text="Yes" Value="127,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Drought">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDrought" runat="server" Text='<%# Eval("Drought")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlDrought" runat="server">

                                                    <asp:ListItem Text="Yes" Value="128,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Landslides">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLandslides" runat="server" Text='<%# Eval("Landslides")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlLandslides" runat="server">

                                                    <asp:ListItem Text="Yes" Value="129,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Avalanche">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAvalanche" runat="server" Text='<%# Eval("Avalanche")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlAvalanche" runat="server">

                                                    <asp:ListItem Text="Yes" Value="130,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Storm">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStorm" runat="server" Text='<%# Eval("Storm")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlStorm" runat="server">

                                                    <asp:ListItem Text="Yes" Value="131,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="Null"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Cyclone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCyclone" runat="server" Text='<%# Eval("Cyclone")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlCyclone" runat="server">

                                                    <asp:ListItem Text="Yes" Value="132,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                      
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
                                      
                                        <asp:TemplateField HeaderText="Heavy Rainfall">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHeavyRainfall" runat="server" Text='<%# Eval("HeavyRainfall")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlHeavyRainfall" runat="server">

                                                    <asp:ListItem Text="Yes" Value="134,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                     
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
                                     
                                        <asp:TemplateField HeaderText="Other">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOther" runat="server" Text='<%# Eval("Other")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlOther" runat="server">

                                                    <asp:ListItem Text="Yes" Value="136,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="None">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNone" runat="server" Text='<%# Eval("None")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlNone" runat="server">

                                                    <asp:ListItem Text="Yes" Value="137,"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                       
                                         <asp:TemplateField HeaderText="Sighting traces of flagship species: Mammals">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMammals" runat="server" Text='<%# TrimTo(Eval("Mammals"),30)%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ListBox ID="lstMammals" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="Mammals_comments" HeaderText="Mammals Comments">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Sighting traces of flagship species: Birds">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBirds" runat="server" Text='<%# TrimTo(Eval("Birds"),30)%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ListBox ID="lstBirds" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="Birds_comments" HeaderText="Birds Comments">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Sighting traces of flagship species: Reptiles">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReptiles" runat="server" Text=' <%# TrimTo(Eval("Reptiles"),30)%>'></asp:Label>

                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ListBox ID="lstReptiles" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="Reptiles_comments" HeaderText="Reptiles Comments">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>

                                       
                                        <asp:TemplateField HeaderText="Sighting traces of flagship species: Amphibians">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmphibians" runat="server" Text='<%# TrimTo(Eval("Amphibians"),30)%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ListBox ID="lstAmphibians" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="Amphibians_comments" HeaderText="Amphibians_Comments">
                                            <%-- <ItemStyle Width="150px"></ItemStyle>--%>
                                        </asp:BoundField>
                                     <asp:TemplateField HeaderText="Plants">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlants" runat="server" Text='<%# TrimTo(Eval("Plants"),30)%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ListBox ID="lstPlants" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                         <asp:BoundField DataField="Plants_comments" HeaderText="Plants Comments">
                                           
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
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
                            <ul class="pagination"  id="pagingnation" runat="server" style="margin-top: 6px; margin-left: 9px; color: #5cb85c">
                                 
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

</asp:Content>
