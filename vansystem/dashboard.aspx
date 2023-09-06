<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="vansystem.dashboard1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <style type="text/css">
        #map {
            height: 600px;
            border: 1px solid gray;
            padding: 10px;
        }

        .slider {
            -webkit-appearance: none;
            width: 100%;
            height: 5px;
            border-radius: 5px;
            background: #d3d3d3;
            outline: none;
            -webkit-transition: .2s;
            transition: opacity .2s;
        }

            .slider::-webkit-slider-thumb {
                -webkit-appearance: none;
                appearance: none;
                width: 10px;
                height: 10px;
                border-radius: 50%;
                background: #191C76;
                cursor: pointer;
            }

            .slider::-moz-range-thumb {
                width: 10px;
                height: 10px;
                border-radius: 50%;
                background: #191C76;
                cursor: pointer;
            }

        .opacity {
            display: none;
        }

        .showSettings {
            cursor: pointer;
        }

        #ddlselectform {
            background: #004990;
            color: white;
            position: absolute;
            z-index: 1000;
            right: 20px;
            top: 75px;
        }

        .cnts, .surveyTot, .number {
            text-align: right;
        }

        @media (min-width: 992px) {
            .col-md-8, .col-md-4 {
                margin-right: 1%;
            }

            .col-md-8 {
                width: 65%;
            }

            .col-md-4 {
                width: 33%;
            }
        }
    </style>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.5.2/css/buttons.dataTables.min.css">
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.3/dist/leaflet.css"
        integrity="sha256-kLaT2GOSpHechhsozzB+flnD+zUyjE2LlfWPgU04xyI="
        crossorigin="" />
    <script src="https://unpkg.com/leaflet@1.9.3/dist/leaflet.js"
        integrity="sha256-WBkoXOwTeyKclOHuWtc+i2uENFpDZ9YPdf5Hf+D7ewM="
        crossorigin=""></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-content">
        <div class="tile col-md-8" style="max-height: 85vh;">
            <div class="card mb-3 text-white bg-dark">
                <div class="card-body" style="font-size: 14px; padding: 10px; font-weight: bold;">
                    Total Sampling Plots - 406
				<asp:Label for="" ID="lbltsc" runat="server" Text="Total Survey Completed -"></asp:Label>
                    <asp:Label for="" ID="lbltsc2" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div style="display: flex">
                <div class="preview">
                    <h3 class="tile-title">Data Collection Status</h3>
                </div>

                <div style="margin-left: 500px;">
                    <asp:DropDownList ID="ddlselectform" runat="server"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlselectform_SelectedIndexChanged"
                        Style="color: rgb(247, 251, 252); border-radius: 5px; background: rgb(52, 52, 122); padding: 5px">
                    </asp:DropDownList>
                </div>
            </div>

            <asp:HiddenField ID="hdndivision" runat="server" Value="" />
            <asp:HiddenField ID="hdnplots" runat="server" Value="" />
            <asp:HiddenField ID="hdnlon" runat="server" Value="" />
            <asp:HiddenField ID="hdnlat" runat="server" Value="" />

            <div id="map" class="col-md-12">
                <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
                <script>
                    debugger;

                    var division = $("#<%=hdndivision.ClientID %>").val();
                    var plot = $("#<%=hdnplots.ClientID %>").val();
                    var lon = $("#<%=hdnlon.ClientID %>").val();
                    var lat = $("#<%=hdnlat.ClientID %>").val();


                    var OpenStreet = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                        id: 'mapbox/streets-v11', attribution: 'Open Street',
                    });

                    var divisions = L.tileLayer.wms('http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&', {
                        layers: division,
                        format: 'image/png',
                        transparent: true
                    });
                    var plots = L.tileLayer.wms('http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&', {
                        layers: plot,
                        format: 'image/png',
                        transparent: true
                    });
                    var otherlayer = [OpenStreet, divisions, plots]
                    var map = L.map('map', {
                        zoomControl: false,
                        center: [lon, lat],
                        zoom: 10,
                        layers: otherlayer
                    });
                </script>
                </script>
            </div>
            <div id='latlonInfo'></div>
            <div id="featureInfo"></div>
        </div>
        <div class="tile col-md-4" style="max-height: 85vh; overflow-y: scroll;">
            <div class="card mb-3 text-white bg-dark">
                <div class="card-body" style="font-size: 14px; padding: 10px; font-weight: bold;">
                    Plot Approach
                </div>
            </div>
            <div>

                <asp:GridView ID="GVplotapproach" runat="server" AutoGenerateColumns="false" GridLines="none" frame="void"
                    rules="rows" Width="450" Height="200" CellPadding="4" CellSpacing="4">
                    <RowStyle BackColor="White" BorderColor="ControlLight" />
                    <HeaderStyle BorderColor="ControlLight" />
                    <Columns>
                        <asp:BoundField DataField="division" HeaderText="Division" />
                        <asp:BoundField DataField="range" HeaderText="Range" />
                        <asp:BoundField DataField="total_plots" HeaderText="Total Plots" />
                        <asp:BoundField DataField="total_surveys" HeaderText="Total Surveys" />
                    </Columns>
                </asp:GridView>
                <hr />
            </div>
            <div class="card mb-3 text-white bg-dark">
                <div class="card-body" style="font-size: 14px; padding: 10px; font-weight: bold;">
                    Plot Discription
                </div>
            </div>
            <div>

                <asp:GridView ID="GVplotdiscription" runat="server" AutoGenerateColumns="false" GridLines="none" frame="void"
                    rules="rows" Width="450" Height="200">
                    <RowStyle BackColor="White" BorderColor="ControlLight" />
                    <HeaderStyle BorderColor="ControlLight" />
                    <Columns>
                        <asp:BoundField DataField="division" HeaderText="Division" />
                        <asp:BoundField DataField="range" HeaderText="Range" />
                        <asp:BoundField DataField="total_plots" HeaderText="Total Plots" />
                        <asp:BoundField DataField="total_surveys" HeaderText="Total Surveys" />
                    </Columns>
                </asp:GridView>
                <hr />
            </div>

            <div class="card mb-3 text-white bg-dark">
                <div class="card-body" style="font-size: 14px; padding: 10px; font-weight: bold;">
                    Plot Enumeration
                </div>
            </div>
            <div>

                <asp:GridView ID="GVplotenumeration" runat="server" AutoGenerateColumns="false" GridLines="none" frame="void"
                    rules="rows" Width="450" Height="200">
                    <RowStyle BackColor="White" BorderColor="ControlLight" />
                    <HeaderStyle BorderColor="ControlLight" />
                    <Columns>
                        <asp:BoundField DataField="division" HeaderText="Division" />
                        <asp:BoundField DataField="range" HeaderText="Range" />
                        <asp:BoundField DataField="total_plots" HeaderText="Total Plots" />
                        <asp:BoundField DataField="total_surveys" HeaderText="Total Surveys" />
                    </Columns>
                </asp:GridView>
                <hr />
            </div>
            <div class="card mb-3 text-white bg-dark">
                <div class="card-body" style="font-size: 14px; padding: 10px; font-weight: bold;">
                    Village Level Information
                </div>
            </div>
            <div>

                <asp:GridView ID="GVvillagelevelinformation" runat="server" AutoGenerateColumns="false" GridLines="none" frame="void"
                    rules="rows" Width="450" Height="200">
                    <RowStyle BackColor="White" BorderColor="ControlLight" />
                    <HeaderStyle BorderColor="ControlLight" />
                    <Columns>
                        <asp:BoundField DataField="division" HeaderText="Division" />
                        <asp:BoundField DataField="range" HeaderText="Range" />
                        <asp:BoundField DataField="total_plots" HeaderText="Total Plots" />
                        <asp:BoundField DataField="total_surveys" HeaderText="Total Surveys" />
                    </Columns>
                </asp:GridView>
            </div>
            <hr />
            <div class="card mb-3 text-white bg-dark">
                <div class="card-body" style="font-size: 14px; padding: 10px; font-weight: bold;">
                    Household		
                </div>
            </div>
            <div>

                <asp:GridView ID="GVhousehold" runat="server" AutoGenerateColumns="false" GridLines="none" frame="void"
                    rules="rows" Width="450" Height="200">
                    <RowStyle BackColor="White" BorderColor="ControlLight" />
                    <HeaderStyle BorderColor="ControlLight" />
                    <Columns>
                        <asp:BoundField DataField="division" HeaderText="Division" />
                        <asp:BoundField DataField="range" HeaderText="Range" />
                        <asp:BoundField DataField="total_plots" HeaderText="Total Plots" />
                        <asp:BoundField DataField="total_surveys" HeaderText="Total Surveys" />
                    </Columns>
                </asp:GridView>
            </div>
            <hr />
            <div class="card mb-3 text-white bg-dark">
                <div class="card-body" style="font-size: 14px; padding: 10px; font-weight: bold;">
                    Timber Extraction
                </div>
            </div>
            <div>

                <asp:GridView ID="GVtimberextarction" runat="server" AutoGenerateColumns="false" GridLines="none" frame="void"
                    rules="rows" Width="450" Height="200">
                    <RowStyle BackColor="White" BorderColor="ControlLight" />
                    <HeaderStyle BorderColor="ControlLight" />
                    <Columns>
                        <asp:BoundField DataField="division" HeaderText="Division" />
                        <asp:BoundField DataField="range" HeaderText="Range" />
                        <asp:BoundField DataField="total_plots" HeaderText="Total Plots" />
                        <asp:BoundField DataField="total_surveys" HeaderText="Total Surveys" />
                    </Columns>
                </asp:GridView>
            </div>
            <hr />
            <div class="card mb-3 text-white bg-dark">
                <div class="card-body" style="font-size: 14px; padding: 10px; font-weight: bold;">
                    NTFP extraction
                </div>
            </div>
            <div>

                <asp:GridView ID="GVNTFPextraction" runat="server" AutoGenerateColumns="false" GridLines="none" frame="void"
                    rules="rows" Width="450" Height="200">
                    <RowStyle BackColor="White" BorderColor="ControlLight" />
                    <HeaderStyle BorderColor="ControlLight" />
                    <Columns>
                        <asp:BoundField DataField="division" HeaderText="Division" />
                        <asp:BoundField DataField="range" HeaderText="Range" />
                        <asp:BoundField DataField="total_plots" HeaderText="Total Plots" />
                        <asp:BoundField DataField="total_surveys" HeaderText="Total Surveys" />
                    </Columns>
                </asp:GridView>
            </div>
            <hr />
            <div class="card mb-3 text-white bg-dark">
                <div class="card-body" style="font-size: 14px; padding: 10px; font-weight: bold;">
                    Provision Services
                </div>
            </div>
            <div>

                <asp:GridView ID="GVprovisioningservices" runat="server" AutoGenerateColumns="false" GridLines="none" frame="void"
                    rules="rows" Width="450" Height="200">
                    <RowStyle BackColor="White" BorderColor="ControlLight" />
                    <HeaderStyle BorderColor="ControlLight" />
                    <Columns>
                        <asp:BoundField DataField="division_name" HeaderText="Division" />
                        <asp:BoundField DataField="range_name" HeaderText="Range" />
                        <asp:BoundField DataField="total_rows" HeaderText="Total Surveys" />
                    </Columns>
                </asp:GridView>
            </div>
            <hr />
            <div class="card mb-3 text-white bg-dark">
                <div class="card-body" style="font-size: 14px; padding: 10px; font-weight: bold;">
                    Hydrological Services
                </div>
            </div>
            <div>

                <asp:GridView ID="GVhydrologicalservices" runat="server" AutoGenerateColumns="false" GridLines="none" frame="void"
                    rules="rows" Width="450" Height="200">
                    <RowStyle BackColor="White" BorderColor="ControlLight" />
                    <HeaderStyle BorderColor="ControlLight" />
                    <Columns>
                        <asp:BoundField DataField="division_name" HeaderText="Division" />
                        <asp:BoundField DataField="range_name" HeaderText="Range" />
                        <asp:BoundField DataField="total_rows" HeaderText="Total Surveys" />
                    </Columns>
                </asp:GridView>
            </div>
            <hr />
            <div class="card mb-3 text-white bg-dark">
                <div class="card-body" style="font-size: 14px; padding: 10px; font-weight: bold;">
                    Cultural Services
                </div>
            </div>
            <div>

                <asp:GridView ID="GVculturalservices" runat="server" AutoGenerateColumns="false" GridLines="none" frame="void"
                    rules="rows" Width="450" Height="200">
                    <RowStyle BackColor="White" BorderColor="ControlLight" />
                    <HeaderStyle BorderColor="ControlLight" />
                    <Columns>
                        <asp:BoundField DataField="division_name" HeaderText="Division" />
                        <asp:BoundField DataField="range_name" HeaderText="Range" />
                        <asp:BoundField DataField="total_rows" HeaderText="Total Surveys" />
                    </Columns>
                </asp:GridView>
            </div>
            <hr />
        </div>

    </div>
</asp:Content>
