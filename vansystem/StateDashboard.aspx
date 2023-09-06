<%@ Page Title="" Language="C#" MasterPageFile="~/Van.Master" AutoEventWireup="true" CodeBehind="StateDashboard.aspx.cs" Inherits="vansystem.StateDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="vali/css/morris.css">
    <link rel="stylesheet" type="text/css" href="vali/css/dataTable-reset.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.5.2/css/buttons.dataTables.min.css">
    <link rel="stylesheet" type="text/css" href="vali/css/jquery.dataTables.min.css">
    <script>
        var baseurl = '{{params.base_url}}';
        var uuid = '{{params.uuid}}';
    </script>
    <style type="text/css">
        #mapContainer {
            position: relative;
            box-shadow: 2px 0px 5px 0px rgb(0 0 0 / 20%);
        }

        #map {
            height: calc(100vh - 75px);
            width: 100%;
        }

        .opacity {
            display: none;
        }

        .showSettings {
            cursor: pointer;
        }

        #formSelect {
            background: #004990;
            color: white;
        }

        #drpDwnCntrl {
            position: absolute;
            z-index: 1000;
            right: 20px;
            top: 75px;
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

        #mapPreview {
            height: calc(100vh - 75px);
            width: 100%;
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

        .no-padd {
            padding: 0;
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

        #latlonInfo {
            position: absolute;
            bottom: 10px;
            left: 10px;
        }

        #mapChckList {
            max-height: 300px;
            overflow-y: scroll;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.5.2/css/buttons.dataTables.min.css">
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.3/dist/leaflet.css"
        integrity="sha256-kLaT2GOSpHechhsozzB+flnD+zUyjE2LlfWPgU04xyI="
        crossorigin="" />
    <script src="https://unpkg.com/leaflet@1.9.3/dist/leaflet.js"
        integrity="sha256-WBkoXOwTeyKclOHuWtc+i2uENFpDZ9YPdf5Hf+D7ewM="
        crossorigin=""></script>
    <script src="/vali/js/morris.min.js"></script>
    <script src="/vali/js/raphael-min.js"></script>


    <script type="text/javascript">
        var divisions;
        $(document).ready(function () {
            divisions = new Morris.Bar({
                element: 'hero-graph-divisions',
                data: [],
                xkey: 'division_name',
                ykeys: ['total_plots', 'total_surveyed', 'total_pending'],
                labels: ['Total Plots', 'Total Plots Surveyed', 'Total Plots Pending'],
                hideHover: 'false',
                barColors: ['#6883a3', '#76C1FA', '#F36368'],
                fillOpacity: 0.5,
                smooth: true,
                padding: 15,
                horizontal: true
            });

            getDivisionCounts();
        });

        function getDivisionCounts() {

            var params = {};

            $.ajax({
                type: "POST",
                url: "StateDashboard.aspx/loadDivisionStatus",
                data: JSON.stringify({ total_plots: 'total_plots', total_surveyed: 'total_surveyed', total_pending:'total_pending'}),
                contentType: "application/json;",

                success: function (response) {
                    divisions.setData(response.data);
                },
                error: function (response) {
                    if (response.responseJSON.status == 'failed') {
                        /* $('#graphBox').remove();*/
                        return false;
                    }
                }
            });
        }
</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
    <div class="app-content no-padd">
        <div class="col-lg-7 no-padd">
            <div id="mapContainer">
                <div id="map" class="col-md-12">
                    <script>
                        var OpenStreet = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                            id: 'mapbox/streets-v11', attribution: 'Open Street',
                        });

                        var dilabad = L.tileLayer.wms('http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&', {
                            layers: 'tblDivision',
                            format: 'image/png',
                            transparent: true
                        });

                        var otherlayer = [OpenStreet, dilabad]
                        var map = L.map('map', {
                            zoomControl: false,
                            center: [17.8006, 79.009],
                            zoom: 8,
                            layers: otherlayer
                        });
                    </script>

                </div>
                <div id='latlonInfo'></div>
                <div id="featureInfo"></div>
            </div>
        </div>
        <div class="col-lg-5" id="graphBox">
            <div class="row">
                <%--<div class="col-lg-12 col-md-12 no-padd">
                    <div class="card-body" style="font-size: 14px; padding: 10px; font-weight: bold;">
                        <h4 class="text-center">Division-wise Plots Status</h4>
                    </div>

                    <div class="col-lg-12" style="height: calc(50vh - 60px); overflow-y: scroll;">
                        <div id="hero-graph-divisions" class="graph" style="min-height: 1800px;"></div>
                    </div>
                </div>--%>
                <hr />
                <div class="tile col-md-12" style="max-height: 90vh; overflow-y: scroll;">
                    <div class="card mb-3 text-white bg-dark">
                        <div class="card-body" style="font-size: 14px; padding: 10px; font-weight: bold;">
                            <h4>Division-wise Counts</h4>
                        </div>
                    </div>
                    <div>
                        <asp:GridView ID="GVDivision" runat="server" AutoGenerateColumns="False" frame="void"
                            rules="rows" Width="450px" Height="200px">
                            <RowStyle BackColor="White" BorderColor="ControlLight" />
                            <EditRowStyle Font-Bold="False" />
                            <HeaderStyle BorderColor="ControlLight" />
                            <Columns>
                                <asp:BoundField DataField="division_name" HeaderText="Division"/>
                                <asp:BoundField DataField="total_ranges" HeaderText="Range" />
                                <asp:BoundField DataField="total_blocks" HeaderText="Block" />
                                <asp:BoundField DataField="total_comps" HeaderText="Compartment" />
                                <asp:BoundField DataField="total_surveyed" HeaderText="Surveyed Plot" />
                                <asp:BoundField DataField="total_plots" HeaderText="Plot" />
                                <asp:BoundField DataField="pending_plots" HeaderText="Pending Plot" />
                                <asp:BoundField DataField="percentage_pending" HeaderText="Percentage Pending" />
                            </Columns>
                        </asp:GridView>
                        <hr />
                    </div>

                </div>

            </div>
        </div>


    </div>
</asp:Content>
