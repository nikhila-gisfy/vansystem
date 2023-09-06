<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="vansystem.LoginPage" EnableSessionState="True" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="FES" />
    <meta name="keyword" content="" />
    <link rel="shortcut icon" href="vali/img/favicon.png" />
    <title>Van -  Homepage </title>

    <link rel="stylesheet" type="text/css" href="vali/css/main.css" />
    <link rel="stylesheet" type="text/css" href="vali/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="vali/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="vali/css/font-awesome-custom.css" />
    <link rel="stylesheet" type="text/css" href="vali/css/custom.css" />
    <link rel="stylesheet" type="text/css" href="global/css/ol4.css" />
    <script type="text/javascript" src="vali/js/jquery-3.2.1.min.js"></script>
   
     <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.12.15/dist/sweetalert2.all.min.js"></script>  
    <link rel='stylesheet' href='https://cdn.jsdelivr.net/npm/sweetalert2@7.12.15/dist/sweetalert2.min.css'></link> 
    <script type="text/javascript">
        function ShowAlert(msg, type) {
            debugger;
            if (type == 'success') {
                swal("", msg, type).then(function () {
                    window.location = "LoginPage.aspx";
                });
            }
            else {
                swal("", msg, type);
            }
        }

    </script>
</head>
<body class="app ">
    <div id="loader" style="display: none;">
        <div class="sk-folding-cube">
            <div class="sk-cube1 sk-cube"></div>
            <div class="sk-cube2 sk-cube"></div>
            <div class="sk-cube4 sk-cube"></div>
            <div class="sk-cube3 sk-cube"></div>
            <em>Van</em>
        </div>F
    </div>
    <header class="app-header" style="background: #fff; border-bottom: 4px solid #004990;">
        <img src="vali/img/logo-USAID.png" style="position: absolute; left: 20px; top: 2px;">
        <img src="vali/img/ministry-logo.png" style="position: absolute; right: 20px; top: 2px;">
    </header>
    <div class="app-content login video">
        <div class="video-box">
            <video muted autoplay loop id="myVideo">
                <source src="video/Van_Website_Banner.mp4" type="video/mp4">
            </video>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-lg-7 col-sm-7"></div>
                <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12 home-login-box">
                    <h1 class="ribbon"><strong class="ribbon-content">Login</strong></h1>
                    <div class="animate form" id="login">
                        <form class="form-horizontal" runat="server">
                            <div class="input-group">
                                <span class="input-group-addon glyphicon glyphicon-user" id="basic-addon1"></span>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="User Name" aria-describedby="basic-addon1" autofocus="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNewPwd" runat="server"
                                    ControlToValidate="txtUserName" Display="Dynamic"
                                    ErrorMessage="Please enter User Name" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <span class="devtoAsterix">*</span>
                            </div>
                            <div class="input-group">
                                <span class="input-group-addon glyphicon glyphicon-lock" id="basic-addon2"></span>
                                <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" CssClass="form-control" aria-describedby="basic-addon2" autocomplete="false" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="txtPassword" Display="Dynamic"
                                    ErrorMessage="Please enter Password" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <span class="devtoAsterix">*</span>
                            </div>
                            <div class="login button text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                <p class="change_link" style="margin-top: 20px;">
                                    <a class="text-reset" href="ForgetPass.aspx">Forgot Password?</a>
                                </p>
                            </div>
                        </form>
                    </div>
                    <div class="clearfix"></div>
                    <div class="text-center">
                        <a class="btn btn-info pull-right btn-user-manual" href="#">
                            <i class="fa fa-book fa-lg" style="color: #FFF; font-size: 120%; vertical-align: baseline;"></i>User Manual
                        </a>
                         <%--<a class="btn btn-info pull-right btn-mobile-app" href="C:\inetpub\wwwroot\VanITSystemnew\Report\app-debugv1.apk" target="_blank" download="download">--%>
                       <a class="btn btn-info pull-right btn-mobile-app" href="Report/VanIt.apk" download>
                            <i class="fa fa-android fa-lg" style="color: #FFF; font-size: 120%; vertical-align: baseline;"></i>Get Mobile App
                        </a>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <footer class="app-footer">
        <div class="col-md-12 col-sm-12">
                        <p class="text-center">Van System is based on the Integrated Forest Management Toolbox (IFMT) developed and Managed by&nbsp;<a href="https://www.gisfy.co.in/" target="_blank"><img src="vali/images/gisfyLogo.png" style="height: 30px;" /></a>.</p>

        </div>
    </footer>
    <script src="vali/js/plugins/pace.min.js"></script>
    <script src="vali/js/popper.min.js"></script>
    <script src="vali/js/bootstrap.min.js"></script>
    <script src="vali/js/main.js"></script>
    <script src="vali/js/custom.js"></script>
</body>
</html>
