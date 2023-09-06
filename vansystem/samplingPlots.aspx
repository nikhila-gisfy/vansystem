<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="samplingPlots.aspx.cs" Inherits="vansystem.samplingPlots" %>

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


    <div class="app-content pd10">
        <div style="padding: 15px">
            <div class="tile col-md-3">
                <h3>Plot Sampling Form</h3>
                <p>&nbsp;</p>
                <form action="/DataUpload/samplingPlots" method="POST" enctype="multipart/form-data">

                    <div class="form-group">
                        <label for="confLevel">Value of t-statistic(tv) <br/><h6>value of t-statistic with v degrees of freedom and 5% significance level</h6> </label>
                        <asp:TextBox runat="server" ID="txt379"   CssClass="form-control" ReadOnly="true" value="1.96"  ></asp:TextBox>

                        <asp:Label ID="lbl379" runat="server"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label for="errLevel">Allowable error (%)</label>
                        <asp:TextBox runat="server" ID="txterrLevel"   CssClass="form-control" ReadOnly="true" value="10"  ></asp:TextBox>

                        <asp:Label ID="lblerrLevel" runat="server"></asp:Label>
                       <%-- <input type="text" name="errLevel" id="errLevel" class="form-control" readonly>--%>
                    </div>
                    <div class="form-group">
                        <label for="plot">Total number of grid (N)<br/><h6>total number of plots of optimum size of main characteristic in the population (division/Range etc.)</h6> </label>
                        <asp:TextBox runat="server" ID="txtgrid" ReadOnly="true"  CssClass="form-control"  ></asp:TextBox>

                        <asp:Label ID="lblgrid" runat="server"></asp:Label>
                       <%-- <input type="text" name="plot" id="plot" class="form-control" value="0.1">--%>
                    </div>

                    <div class="form-group">
                        <label for="preSample">Coefficient of variation(CV)<br/><h6>Coefficient of variation of the main characteristic/attribute (which can be calculated through past WP)</h6></label>
                        <asp:TextBox runat="server" ID="txtCV"   CssClass="form-control"  ></asp:TextBox>

                        <asp:Label ID="lblCV" runat="server"></asp:Label>
                    </div>
                    <div class="form-group">
                      <asp:Button runat="server" ID="btnSubmit" Text="Calculate n"  class="btn btn-success" OnClick="btnSubmit_Click"  />
                        <asp:Label ID="NLabel" runat="server"></asp:Label>
                       <%-- <input type="submit" value="Calculate n" class="btn btn-success">--%>
                    </div>
                  <%--  <div class="form-group">
                        <input id="checkStatus" type="button" value="Check Status" class="btn btn-primary col-md-12" />
                        <div style="text-align: center;">
                            <span class="badge" id="messageStatus"></span>
                        </div>
                    </div>--%>
                    <div class="form-group downloadFiles">
                        <a class="btn btn-primary col-md-12" href="#" id="downloadKML"><i class="fa fa-map-pin fa-fw"></i>Download KML file</a>
                    </div>
                    <div class="form-group downloadFiles">
                        <a class="btn btn-primary col-md-12" href="#" id="downloadShapeFile"><i class="fa fa-map-marker fa-fw"></i>Download Shape file</a>
                    </div>
                    <div class="form-group downloadFiles">
                        <a class="btn btn-primary col-md-12" href="#" id="downloadCSV"><i class="fa fa-file-excel-o fa-fw"></i>Download CSV file</a>
                    </div>
                </form>
            </div>
            <div class="tile col-md-9">
                <div id="map" class="col-md-12">
                    <asp:HiddenField ID="hdndivision" runat="server" Value="" />

                    <asp:HiddenField ID="hdnblock" runat="server" Value="" />
                    <asp:HiddenField ID="hdncompartment" runat="server" Value="" />
                    <asp:HiddenField ID="hdnplots" runat="server" Value="" />
                    <asp:HiddenField ID="hdnlon" runat="server" Value="" />
                    <asp:HiddenField ID="hdnlat" runat="server" Value="" />
                    <asp:HiddenField ID="hdngrid" runat="server" Value="" />
                    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
                    <script>
                        debugger;

                        var division = $("#<%=hdndivision.ClientID %>").val();

                        var block = $("#<%=hdnblock.ClientID %>").val();
                        var compartment = $("#<%=hdncompartment.ClientID %>").val();
                        var plot = $("#<%=hdnplots.ClientID %>").val();
                        var lon = $("#<%=hdnlon.ClientID %>").val();
                        var lat = $("#<%=hdnlat.ClientID %>").val();
                        var grid = $("#<%=hdngrid.ClientID %>").val();


                        var OpenStreet = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                            id: 'mapbox/streets-v11', attribution: 'Open Street',
                        });

                        var divisions = L.tileLayer.wms('http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&', {
                            layers: division,
                            format: 'image/png',
                            transparent: true
                        });

                        var block = L.tileLayer.wms('http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&', {
                            layers: block,
                            format: 'image/png',
                            transparent: true
                        });
                        var compartment = L.tileLayer.wms('http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&', {
                            layers: compartment,
                            format: 'image/png',
                            transparent: true
                        });
                        var plots = L.tileLayer.wms('http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&', {
                            layers: plot,
                            format: 'image/png',
                            transparent: true
                        });
                        var grid = L.tileLayer.wms('http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&', {
                            layers: grid,
                            format: 'image/png',
                            transparent: true
                        });
                        var otherlayer = [OpenStreet, divisions, block, compartment, plots, grid]
                        var map = L.map('map', {
                            zoomControl: false,
                            center: [lon, lat],
                            zoom: 10,
                            layers: otherlayer
                        });
                    </script>

                </div>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
    <script src="/vali/js/ol3.js"></script>
    <script>
        var baseurl = 'https://database.vanapp.org/geoserver/vanapp/wms';
        var create_by = 'adilabad@van';
        var center = [78.6376512395771, 19.6273983953445];
    </script>
    <script src="/vali/js/sampling-plots.js"></script>
  
    <div id="dataModal" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog"
        aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Update Field</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary saveFieldChanges">Save changes</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
