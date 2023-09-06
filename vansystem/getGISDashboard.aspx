<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="getGISDashboard.aspx.cs" Inherits="vansystem.getGISDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/vali/css/ol3.css" type="text/css" />
<link rel="stylesheet" href="/vali/css/map.css" type="text/css" />
	<link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.3/dist/leaflet.css"
          integrity="sha256-kLaT2GOSpHechhsozzB+flnD+zUyjE2LlfWPgU04xyI="
          crossorigin="" /><script src="https://unpkg.com/leaflet@1.9.3/dist/leaflet.js"
            integrity="sha256-WBkoXOwTeyKclOHuWtc+i2uENFpDZ9YPdf5Hf+D7ewM="
            crossorigin=""></script>
	  <link rel = "stylesheet" href = "http://cdn.leafletjs.com/leaflet-0.7.3/leaflet.css"/>
      <script src = "http://cdn.leafletjs.com/leaflet-0.7.3/leaflet.js"></script>
<style type="text/css">
	.downloadMapReport{
	    width: 35px;
	    height: 20px;
	    float: right;
	}
	#map {
		border: 1px solid #cecece;
		height: calc(80vh);
		width: 100%;
	}
	#legend .label {
		color: #000;
	}
	#legendContent {
		overflow-y: auto;
		height: 85%;
		padding: 5px 5px;
	}
	#legend {
       background-color: rgba(255, 255, 255, 0.95);
       color: #000;
      position: absolute;
      right: 5px;
      bottom: 0;
      display: block;
      z-index: 10;
      width: 25%;
      height: 40%;
      text-align: left;
      max-width: 300px;
      box-shadow: 0 2px 10px rgb(0 0 0 / 20%);
    }
	#legendTitleBar {
      background: #000000b8;
      color: #fff;
      text-align: left;
      width: 100%;
      cursor: pointer;
      padding: 2px 0 2px 7px;
      font-size: 0.91em;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="app-content pd10">
	<div class="col-md-12">
		<h3 class="pull-left">GIS Dashboard</h3>
		<div class="pull-left" style="margin-left: 20px;">
						<button id="generateShannon" class="btn btn-success btn-sm" onclick="window.location = '/GisDashboard/generateShannon'">Create Shannon</button>
						<button id="addBoundary" class="btn btn-success btn-sm" onclick="window.location = '/GisDashboard/addBoundary'">Add custom map</button>
		</div>
	</div>
		
	<div class="clearfix"></div>
		<div style="padding:15px 0;">
		<div class="col-md-4 col-sm-12">
			<div class="tile" style="overflow-y: auto; overflow-x: hidden; max-height: calc(100vh - 155px);">
								<h4><strong>Forest Boundaries</strong></h4>
				<div class="clearfix"></div>
				<div>
										<div id="labelCtr_0" class="list-group-item row">
						<div class="col-md-12 row lyrCtrLeft">
							<div class="col-md-8">Division</div>
							<div class="col-md-4 row">
								<div class="downloadMapReport col-md-6" title="Download Report">
									<a href="" runat="server" id="divisionlink" onserverclick="divisionlink_ServerClick"><i class="fa fa-download fa-lg"></i></a>
								</div>
								<div class="material-switch pull-right col-md-6" title="Set layer visibility">
									<input type="checkbox" id="checkDivision" checked="checked" onclick="fnText('adilabad')" /><label for="checkDivision" class="label-success"></label>
								</div>
							</div>
						</div>
					</div>
										<div id="labelCtr_1" class="list-group-item row">
						<div class="col-md-12 row lyrCtrLeft">
							<div class="col-md-8">Range</div>
							<div class="col-md-4 row">
								<div class="downloadMapReport col-md-6" title="Download Report">
									<a href="" class="downloadLinkMap" runat="server" id="rangelink" onserverclick="divisionlink_ServerClick"><i class="fa fa-download fa-lg"></i></a>
								</div>
								<div class="material-switch pull-right col-md-6" title="Set layer visibility">
									<input type="checkbox" id="checkRange" onclick="fnText('range')" /><label for="checkRange" class="label-success"></label>
								</div>
							</div>
						</div>
					</div>
										<div id="labelCtr_2" class="list-group-item row">
						<div class="col-md-12 row lyrCtrLeft">
							<div class="col-md-8">Block</div>
							<div class="col-md-4 row">
								<div class="downloadMapReport col-md-6" title="Download Report">
									<a href="" class="downloadLinkMap" runat="server" id="blocklink" onserverclick="divisionlink_ServerClick"><i class="fa fa-download fa-lg"></i></a>
								</div>
								<div class="material-switch pull-right col-md-6" title="Set layer visibility">
									<input type="checkbox" id="checkblock" onclick="fnText('block')" /><label for="checkblock" class="label-success"></label>
								</div>
							</div>
						</div>
					</div>
										<div id="labelCtr_3" class="list-group-item row">
						<div class="col-md-12 row lyrCtrLeft">
							<div class="col-md-8">Compartment</div>
							<div class="col-md-4 row">
								<div class="downloadMapReport col-md-6" title="Download Report">
									<a href="" class="downloadLinkMap" runat="server" id="compartmentlink" onserverclick="divisionlink_ServerClick"><i class="fa fa-download fa-lg"></i></a>
								</div>
								<div class="material-switch pull-right col-md-6" title="Set layer visibility">
									<input type="checkbox" id="checkcompartment" onclick="fnText('compartment')" /><label for="checkcompartment" class="label-success"></label>
								</div>
							</div>
						</div>
					</div>
										<div id="labelCtr_4" class="list-group-item row">
						<div class="col-md-12 row lyrCtrLeft">
							<div class="col-md-8">Plots</div>
							<div class="col-md-4 row">
								<div class="downloadMapReport col-md-6" title="Download Report">
									<a href="" class="downloadLinkMap" runat="server" id="plotlink" onserverclick="divisionlink_ServerClick"><i class="fa fa-download fa-lg"></i></a>
								</div>
								<div class="material-switch pull-right col-md-6" title="Set layer visibility">
									<input type="checkbox" id="checkPlots" onclick="fnText('plots')" /><label for="checkPlots" class="label-success"></label>
								</div>
							</div>
						</div>
					</div>
									</div>
							
							</div>
		</div>
		
		 <asp:HiddenField ID="hdndivision" runat="server" Value="" />
        <asp:HiddenField ID="hdnrange" runat="server" Value="" />
        <asp:HiddenField ID="hdnblock" runat="server" Value="" />
        <asp:HiddenField ID="hdncompartment" runat="server" Value="" />
		<asp:HiddenField ID="hdnplots" runat="server" Value="" />
        <asp:HiddenField ID="hdnlon" runat="server" Value="" />
		<asp:HiddenField ID="hdnlat" runat="server" Value="" />
			<div class="col-md-8 col-sm-12" style="max-height: 85vh;">
				<div class="tile">
						<div id="map" style="position: relative;">
						 <script>
                             var a = $("#<%=hdndivision.ClientID %>").val();
                             var b = $("#<%=hdnrange.ClientID %>").val();
                             var c = $("#<%=hdnblock.ClientID %>").val();
                             var d = $("#<%=hdncompartment.ClientID %>").val();
                             var e = $("#<%=hdnplots.ClientID %>").val();
                             var lon = $("#<%=hdnlon.ClientID %>").val();
                             var lat = $("#<%=hdnlat.ClientID %>").val();
                             var OpenStreet = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                                 id: 'mapbox/streets-v11', attribution: 'Open Street',
                             });

                             var division = L.tileLayer.wms('http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&', {
                                 layers: a,
                                 format: 'image/png',
                                 transparent: true
                             });
                             var range = L.tileLayer.wms('http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&', {
                                 layers: b,
                                 format: 'image/png',
                                 transparent: true
                             });
                             var block = L.tileLayer.wms('http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&', {
                                 layers: c,
                                 format: 'image/png',
                                 transparent: true
                             });
                             var compartment = L.tileLayer.wms('http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&', {
                                 layers: d,
                                 format: 'image/png',
                                 transparent: true
                             });
                             var plots = L.tileLayer.wms('http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&', {
                                 layers: e,
                                 format: 'image/png',
                                 transparent: true
                             });
                             var otherlayer = [OpenStreet, division /*plots*/]
                             var map = L.map('map', {
                                 zoomControl: true,
                                 center: [lon, lat],
                                 zoom: 10,
                                 layers: otherlayer
                             });
                             function fnText(str) {
                                 if (str == "adilabad") {
                                     if ($("#checkDivision").is(":checked")) {
                                         adilabad.addTo(map);
                                     }
                                     else {
                                         map.removeLayer(adilabad)
                                     }
                                 }
                                 if (str == "plots") {
                                     if ($("#checkPlots").is(":checked")) {
                                         plots.addTo(map);
                                     }
                                     else {
                                         map.removeLayer(plots)
                                     }
                                 }
                                 if (str == "range") {
                                     if ($("#checkRange").is(":checked")) {
                                         range.addTo(map);
                                     }
                                     else {
                                         map.removeLayer(range)
                                     }
                                 }
                                 if (str == "block") {
                                     if ($("#checkblock").is(":checked")) {
                                         block.addTo(map);
                                     }
                                     else {
                                         map.removeLayer(block)
                                     }
                                 }
                                 if (str == "compartment") {
                                     if ($("#checkcompartment").is(":checked")) {
                                         compartment.addTo(map);
                                     }
                                     else {
                                         map.removeLayer(compartment)
                                     }
                                 }

                             }
                         </script>
					
					
					<div id="legend">
						<div id="legendTitleBar">Legend</div>
						<div id="legendContent"><div id="row_0" class="legendRow"><div class="label">Division</div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:division_master"></div><div id="row_1" class="legendRow"><div class="label">Range</div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:range_master"></div><div id="row_2" class="legendRow"><div class="label">Block</div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:block_master"></div><div id="row_3" class="legendRow"><div class="label">Compartment</div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:compartment_master"></div><div id="row_4" class="legendRow"><div class="label">Plots</div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:plot_master"></div><div id="row_5" class="legendRow"><div class="label">ROAD</div>
							<%--<img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cst_rds626fc09f447eb"></div><div id="row_6" class="legendRow"><div class="label">TS_VSS</div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cTS_VSS_w626fbd779cee3"></div><div id="row_7" class="legendRow"><div class="label">HABITATION </div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cHABI_16176626fbe6c82c48"></div><div id="row_8" class="legendRow"><div class="label">GRAM PANCHAYAT</div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cGP8326_w626fc0624dc65"></div><div id="row_9" class="legendRow"><div class="label"></div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cts_stream_W626fbb8de7fa6"></div><div id="row_10" class="legendRow"><div class="label"></div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cTS_VSS_w626fbc8567805"></div><div id="row_11" class="legendRow"><div class="label">VILLAGE </div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cVill10922_w626fbdc642347"></div><div id="row_12" class="legendRow"><div class="label"></div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cTS_VSS_w626fbc70d68d2"></div><div id="row_13" class="legendRow"><div class="label"></div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cGP8326_w626fba0d835a4"></div><div id="row_14" class="legendRow"><div class="label">URBAN BLOCK </div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cUrban_blocks_F_128626fbda9eb9fe"></div><div id="row_15" class="legendRow"><div class="label"></div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cst_rds626fbbd9a00d7"></div><div id="row_16" class="legendRow"><div class="label"></div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cTS_Mandals_w626fbc1f6177d"></div><div id="row_17" class="legendRow"><div class="label">MANDAL</div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cTS_Mandals_w626fc0d41bcb0"></div><div id="row_18" class="legendRow"><div class="label"></div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cGP8326_w626fba3424a47"></div><div id="row_19" class="legendRow"><div class="label"></div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cGP8326_w626fba710957c"></div><div id="row_20" class="legendRow"><div class="label">STREAM </div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:cts_stream_W626fc0b40b704"></div><div id="row_21" class="legendRow"><div class="label">Soil Type</div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:dynamic_soil_range"></div><div id="row_22" class="legendRow"><div class="label">Road Network</div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:dynamic_road_range"></div><div id="row_23" class="legendRow"><div class="label">Shannon Diversity Compartment</div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:shannon_diversity_compartments"></div><div id="row_24" class="legendRow"><div class="label">Shannon Diversity Plots</div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:shannon_diversity_plots"></div><div id="row_25" class="legendRow"><div class="label">Geology</div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:dynamic_geology"></div><div id="row_26" class="legendRow"><div class="label">Railway Network</div><img src="https://database.vanapp.org/geoserver/vanapp/wms?REQUEST=GetLegendGraphic&amp;VERSION=1.0.0&amp;FORMAT=image/png&amp;WIDTH=20&amp;HEIGHT=20&amp;LAYER=vanapp:dynamic_railway_range"></div></div>--%>
					</div>
				</div>
			</div>
			
	</div>
                      
				</div>
			</div>
			</div>
</div>
	
<script src="/vali/js/plugins/sweetalert.min.js"></script>
<script src="/vali/js/ol3.js"></script>
<script src="/vali/js/ol-popup.js"></script>
<script src="/vali/js/gis-dashboard.js"></script>
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
