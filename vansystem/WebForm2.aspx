<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="vansystem.WebForm2" %>



<!DOCTYPE html>
<html lang="en">
<head>



    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="FES">
    <meta name="keyword" content="">
    <link rel="shortcut icon" href="/vali/img/favicon.png" />
    <title>Van - Automated Plot Sampling</title>
    <link rel="stylesheet" type="text/css" href="/vali/css/main.css">
    <link rel="stylesheet" type="text/css" href="/vali/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="/vali/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="/vali/css/font-awesome-custom.css">
    <link rel="stylesheet" type="text/css" href="/vali/css/custom.css">
    <link rel="stylesheet" type="text/css" href="/global/css/ol4.css">
    <script type="text/javascript" src="/vali/js/jquery-3.2.1.min.js"></script>
</head>
<body class="app  sidebar-mini ">
    <div id="loader">
        <div class="sk-folding-cube">
            <div class="sk-cube1 sk-cube"></div>
            <div class="sk-cube2 sk-cube"></div>
            <div class="sk-cube4 sk-cube"></div>
            <div class="sk-cube3 sk-cube"></div>
            <em>Van</em>
        </div>
    </div>
    <header class="app-header" style="background: #fff; border-bottom: 4px solid #004990;">
        <a class="app-sidebar__toggle" href="#" data-toggle="sidebar"></a>
        <a href="/">
            <img src="/vali/img/van-logo.jpg" alt="logo" /></a>
        <ul class="app-nav">
            <!-- User Menu-->
            <!--li class="dropdown">
    <a class="app-nav__item" href="/Home/about">
      <i class="fa fa-info fa-lg"></i></a>
    </li-->
            <li class="dropdown">
                <a class="app-nav__item" href="#" data-toggle="dropdown"><i class="fa fa-user fa-lg"></i></a>
                <ul class="dropdown-menu settings-menu dropdown-menu-right">
                    <li>
                        <a class="dropdown-item" href="/AppSync/sync">
                            <i class="fa fa-refresh fa-lg"></i>Sync Forms
                        </a>
                    </li>
                    <li>
                        <a class="dropdown-item" href="/User/account">
                            <i class="fa fa-address-book fa-lg"></i>My Account
                        </a>
                    </li>
                    <li>
                        <a class="dropdown-item" href="/User/logout">
                            <i class="fa fa-sign-in fa-lg"></i>Logout
                        </a>
                    </li>
                </ul>
            </li>
        </ul>
    </header>
    <!--div class="app-sidebar__overlay" data-toggle="sidebar"></div-->
    <aside class="app-sidebar">
        <div class="app-sidebar__user">
            <!--<img class="app-sidebar__user-avatar" src="" alt="User Image">-->
            <span style="display: inline-block; border: 1px solid #fff; background: #36b439; padding: 4px 8px; border-radius: 30px; margin-right: 10px;"><i class="fa fa-user" style="font-size: 150%;"></i></span>
            <div>
                <p class="app-sidebar__user-name">Adilabad</p>
                <p class="app-sidebar__user-designation"></p>
            </div>
        </div>
        <ul class="app-menu">
            <li><a class="app-menu__item" href="/Home/about"><i class="app-menu__icon fa fa-info fa-fw"></i><span class="app-menu__label">About</span></a></li>
            <li><a class="app-menu__item" href="/Home/dashboard"><i class="app-menu__icon fa fa-dashboard fa-fw"></i><span class="app-menu__label">Dashboard</span></a></li>
            <li class="treeview"><a class="app-menu__item" href="#" data-toggle="treeview"><i class="app-menu__icon fa fa-upload fa-fw"></i><span class="app-menu__label">Data Upload</span><i class="treeview-indicator fa fa-angle-right"></i></a>
                <ul class="treeview-menu">
                    <li><a class="treeview-item" href="/DataUpload/ForestAdminBoundaries"><i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Forest Admin Boundaries</a></li>
                    <li><a class="treeview-item" href="/DataUpload/speciesData"><i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Species Data</a></li>
                    <li><a class="treeview-item" href="/DataUpload/samplingPlots"><i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Sampling Plots</a></li>
                    <li><a class="treeview-item" href="/DataUpload/rareSpeciesData"><i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>RareSpecies Data</a></li>
                </ul>
            </li>
            <li><a class="app-menu__item" href="/DataVerification/verifyAdminBoundaries"><i class="app-menu__icon fa fa-map-o fa-fw"></i><span class="app-menu__label">Forest Boundaries</span><i class="treeview-indicator fa fa-angle-right"></i></a></li>
            <li class="treeview">
                <a class="app-menu__item" href="#" data-toggle="treeview">
                    <i class="app-menu__icon fa fa-edit fa-fw"></i>
                    <span class="app-menu__label">NWPC Forms</span>
                    <i class="treeview-indicator fa fa-angle-right fa-fw"></i>
                </a>
                <ul class="treeview-menu">
                    <li><a class="treeview-item" href="/DataEdit/listData?form=5"><i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Plot Approach</a></li>
                    <li><a class="treeview-item" href="/DataEdit/listData?form=2"><i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Plot Description</a></li>
                    <li><a class="treeview-item" href="/DataManager/listData?form=enumeration"><i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Plot Enumeration</a></li>
                    <li><a class="treeview-item" href="/DataEdit/listData?form=4"><i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Village Level Information</a></li>
                    <li><a class="treeview-item" href="/DataEdit/listData?form=6"><i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Household</a></li>
                    <li><a class="treeview-item" href="/Extraction/getTimberExtraction"><i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Timber Extraction</a></li>
                    <li><a class="treeview-item" href="/Extraction/getNTFPExtraction"><i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>NTFP extraction</a></li>
                </ul>
            </li>
            <li class="treeview">
                <a class="app-menu__item" href="#" data-toggle="treeview">
                    <i class="app-menu__icon fa fa-tasks fa-fw"></i>
                    <span class="app-menu__label">Biodiversity Services</span>
                    <i class="treeview-indicator fa fa-angle-right fa-fw"></i>
                </a>
                <ul class="treeview-menu">
                    <li>
                        <a class="treeview-item" href="/DataView/rareSpecies">
                            <i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Rare Species Information
                        </a>
                    </li>
                    <li>
                        <a class="treeview-item" href="/DataView/labelList">
                            <i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Label List
                        </a>
                    </li>
                    <!-- <li>
					<a class="treeview-item" href="/DataView/listLabelData">
						<i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i> Label Data
					</a>
				</li>
				<li>
					<a class="treeview-item" href="/DataView/listForm">
						<i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i> Assign List to User
					</a>
				</li> -->
                </ul>
            </li>
            <li class="treeview">
                <a class="app-menu__item" href="#" data-toggle="treeview">
                    <i class="app-menu__icon fa fa-tasks fa-fw"></i>
                    <span class="app-menu__label">Ecosystem Services</span>
                    <i class="treeview-indicator fa fa-angle-right fa-fwt"></i>
                </a>
                <ul class="treeview-menu">
                    <li>
                        <a class="treeview-item" href="/DataView/provisioning">
                            <i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Provision Services
                        </a>
                    </li>
                    <li>
                        <a class="treeview-item" href="/DataView/hydrological">
                            <i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Hydrological Services
                        </a>
                    </li>
                    <li>
                        <a class="treeview-item" href="/DataView/cultural">
                            <i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Cultural/Tourism Services
                        </a>
                    </li>
                </ul>
            </li>
            <li class="treeview">
                <a class="app-menu__item" href="#" data-toggle="treeview">
                    <i class="app-menu__icon fa fa-file-text-o fa-fw"></i>
                    <span class="app-menu__label">Reports</span>
                    <i class="treeview-indicator fa fa-angle-right fa-fw"></i>
                </a>
                <ul class="treeview-menu">
                    <li>
                        <a class="treeview-item" href="/Report/carbonEstimation">
                            <i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Carbon Estimation
                        </a>
                    </li>
                    <li>
                        <a class="treeview-item" href="/Report/growingStock">
                            <i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Growing Stock
                        </a>
                    </li>
                    <li>
                        <a class="treeview-item" href="/Report/shannonDiversity">
                            <i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Shannon Diversity Index
                        </a>
                    </li>
                    <li>
                        <a class="treeview-item" href="/Report/simpsonDiversity">
                            <i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Simpson Diversity Index
                        </a>
                    </li>
                    <li>
                        <a class="treeview-item" href="/Report/ivIndex">
                            <i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Importance Value Index (IVI)
                        </a>
                    </li>
                    <li>
                        <a class="treeview-item" href="/Report/download">
                            <i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Tabular Reports
                        </a>
                    </li>
                </ul>
            </li>
            <li><a class="app-menu__item" href="/GisDashboard/getGisDashboard"><i class="app-menu__icon fa fa-globe fa-fw"></i><span class="app-menu__label">GIS Dashboard</span></a></li>
            <li class="treeview">
                <a class="app-menu__item" href="#" data-toggle="treeview">
                    <i class="app-menu__icon fa fa-user fa-fw"></i>
                    <span class="app-menu__label">User Management</span>
                    <i class="treeview-indicator fa fa-angle-right fa-fw"></i>
                </a>
                <ul class="treeview-menu">
                    <li><a class="treeview-item" href="/User/getUsers"><i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Users</a></li>
                </ul>
            </li>
            <li class="treeview">
                <a class="app-menu__item" href="#" data-toggle="treeview">
                    <i class="app-menu__icon fa fa-download fa-fw"></i>
                    <span class="app-menu__label">Working Plan</span>
                    <i class="treeview-indicator fa fa-angle-right fa-fw"></i>
                </a>
                <ul class="treeview-menu">
                    <li>
                        <a class="treeview-item" href="/WorkingPlan/downloadV1">
                            <i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Draft Volume I
                        </a>
                    </li>
                    <li>
                        <a class="treeview-item" href="/WorkingPlan/downloadV2">
                            <i class="icon fa fa-dot-circle-o fa-xxs fa-fw"></i>Draft Volume II
                        </a>
                    </li>
                </ul>
            </li>
        </ul>
    </aside>
    <style type="text/css">
        #mapContainer {
            position: relative;
        }

        #mapPreview {
            height: calc(100vh - 175px);
            width: 100%;
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

        @media (min-width: 992px) {
            .col-md-9, .col-md-3 {
                margin-right: 1%;
            }

            .col-md-9 {
                width: 74%;
            }

            .col-md-3 {
                width: 24%;
            }
        }
    </style>
    <div class="app-content pd10">
        <div style="padding: 15px">
            <div class="tile col-md-3">
                <h3>Plot Sampling Form</h3>
                <p>&nbsp;</p>
                <form action="/DataUpload/samplingPlots" method="POST" enctype="multipart/form-data">

                    <div class="form-group">
                        <label for="confLevel">Confidence Level</label>
                        <select name="confLevel" id="confLevel" class="form-control">
                            <option value="Select">Select</option>
                            <option value="90">90</option>
                            <option value="91">91</option>
                            <option value="92">92</option>
                            <option value="93">93</option>
                            <option value="94">94</option>
                            <option value="95">95</option>
                            <option value="96">96</option>
                            <option value="97">97</option>
                            <option value="98">98</option>
                            <option value="99">99</option>
                            <option value="100">100</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label for="errLevel">Level of Error</label>
                        <input type="text" name="errLevel" id="errLevel" class="form-control" readonly>
                    </div>
                    <div class="form-group">
                        <label for="plot">Plot size</label>
                        <input type="text" name="plot" id="plot" class="form-control" value="0.1">
                    </div>

                    <div class="form-group">
                        <label for="preSample">Coefficient of variation</label>
                        <select name="cv" id="cv" class="form-control">
                            <option value="Select">Select</option>
                            <option value="0.5">0.5</option>
                            <option value="0.6">0.6</option>
                            <option value="0.7">0.7</option>
                            <option value="0.8">0.8</option>
                            <option value="0.9">0.9</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <input type="submit" value="Calculate n" class="btn btn-success">
                    </div>
                    <div class="form-group">
                        <input id="checkStatus" type="button" value="Check Status" class="btn btn-primary col-md-12" />
                        <div style="text-align: center;">
                            <span class="badge" id="messageStatus"></span>
                        </div>
                    </div>
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
                <div id="mapContainer">
                    <div id='mapPreview'></div>
                    <div id='latlonInfo'></div>
                    <div id="featureInfo"></div>
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
    <script type="text/javascript" src="/vali/js/plugins/chart.js"></script>
    <script src="/vali/js/plugins/pace.min.js"></script>
    <script src="/vali/js/popper.min.js"></script>
    <script src="/vali/js/bootstrap.min.js"></script>
    <script src="/vali/js/main.js"></script>
    <script src="/vali/js/custom.js"></script>
</body>
</html>
