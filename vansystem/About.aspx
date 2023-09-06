<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="vansystem.About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Van - About</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-content">
        <div class="row">
            <div class="col-md-12 col-sm-12">
                <img src="vali/img/about-us-banner.jpg" class="img-responsive" alt="About Us" />
                <div class="card distSep">
                    <div class="card-body">
                        <h5 class="card-title">About <i>Van</i></h5>
                        <p class="card-text">
                            <strong><i>Van</i></strong> - an integrated system, for collection of forest inventory and ecosystem data (mobile app), and automation system of data analysis (web portal) for Working Plan Preparation.
                        </p>
                    </div>
                </div>
                <div class="card distSep">
                    <div class="card-body">
                        <h5 class="card-title">Modules of <i>Van</i></h5>
                        <p class="card-text">
                            A repository of data from publicly available open data sources that can provide certain mandatory spatial layers as required by National Working Plan Code 2014 (for sustainable management of Forest and Biodiversity in India), MoEFCC, GoI.&nbsp;
                        </p>
                        <div class="col-md-6 col-sm-12 distSep">
                            <img src="vali/img/mobile-app.jpg" class="img-fluid" alt="Mobile App" />
                        </div>
                        <div class="col-md-6 col-sm-12 distSep">
                            <img src="vali/img/web-portal.jpg" class="img-fluid" alt="Web Portal" />
                        </div>
                    </div>
                </div>
                <div class="card distSep">
                    <div class="card-body">
                        <h5 class="card-title">Evolution of <i>Van</i></h5>
                        <img src="vali/img/schematic-diagram.jpg" class="img-fluid img-evolution" alt="Schematic Diagram" />
                        <p class="card-text">
                            The <i>Van</i> system comprises: 
							<ol>
                                <li>a mobile application for collecting forest inventory data (sampling point and ecosystem data), socioeconomic data;</li>
                                <li>a cloud-based data repository; and </li>
                                <li>a web portal that uses automated modules to generate quantitative and geospatial data analysis in a sample working plan document.</li>
                            </ol>
                        </p>
                        <p>The Integrated Forest Management Toolbox (IFMT) developed by Foundation for Ecological Security (FES) is the foundational tool for the development of Van. Functionalities from mForest, a mobile application and Forest Data Management System (FDMS) developed under Forest-PLUS (Forest-PLUS 2.0’s predecessor program) were integrated into IFMT under Forest-PLUS 2.0 program to design and develop Van. Van’s development, its testing and validation has been done in close consultation with the MoEFCC and the SFDs in the target states, who provided valuable feedback and support in finalizing the system.</p>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
