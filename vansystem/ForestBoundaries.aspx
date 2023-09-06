<%@ Page Title="" Language="C#" MasterPageFile="~/AdminVanUser.Master" AutoEventWireup="true" CodeBehind="ForestBoundaries.aspx.cs" Inherits="vansystem.ForestBoundaries" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">	#map {		border: 1px solid #cecece;		height: calc(80vh);		width: 100%;	}	#legend .label {		color: #000;	}</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="app-content pd10">
        <div class="col-md-12">
            <h3>View Forest boundaries</h3>
        </div>
        <div class="clearfix"></div>
        <div style="padding: 15px 0;">
            <div class="col-md-4 col-sm-12">
                <div class="tile">
                    <div id="layerController"></div>
                    <br />
                    <p>If you find error in any boundaries, please delete all boudaries using following button and re-upload correct boundaries.</p>
                  

                </div>
            </div>
            <div class="col-md-8 col-sm-12" style="max-height: 85vh;">
                <div class="tile">
                    <div id="map" style="position: relative;">
                        <div id="legend">
                            <div id="legendTitleBar">Legend</div>
                            <div id="legendContent"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
