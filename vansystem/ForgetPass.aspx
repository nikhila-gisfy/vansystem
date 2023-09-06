<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPass.aspx.cs" Inherits="vansystem.ForgetPass" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="FES">
    <meta name="keyword" content="">
    <link rel="shortcut icon" href="/vali/img/favicon.png" />
    <title>Van -  Homepage </title>
    <link rel="stylesheet" type="text/css" href="vali/css/main.css">
    <link rel="stylesheet" type="text/css" href="vali/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="vali/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="vali/css/font-awesome-custom.css">
    <link rel="stylesheet" type="text/css" href="vali/css/custom.css">
    <link rel="stylesheet" type="text/css" href="global/css/ol4.css">
    <script type="text/javascript" src="vali/js/jquery-3.2.1.min.js">

    </script>
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
                    <h1 class="ribbon"><strong class="ribbon-content">Forgot Password</strong></h1>
                    <div class="animate form" id="login">
                        <form class="form-horizontal" runat="server" id="userLogin" method="post">
                            <div class="input-group">
                                <span class="input-group-addon glyphicon glyphicon-user" id="basic-addon1"></span>
                                <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                                    ControlToValidate="txtEmail" Display="Dynamic"
                                    ErrorMessage="Please enter EmailID" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <span class="devtoAsterix">*</span>
                            </div>
                            <div class="clearfix"></div>
                            <div class="text-center">
                                <asp:Button ID="btnuSbmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnuSbmit_Click" />
                                <a class="btn btn-danger" href="LoginPage.aspx">Login
                                </a>
                                <div class="clearfix"></div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="vali/js/plugins/pace.min.js"></script>
    <script src="vali/js/popper.min.js"></script>
    <script src="vali/js/bootstrap.min.js"></script>
    <script src="vali/js/main.js"></script>
    <script src="vali/js/custom.js"></script>
    <script>
        function ShowAlert(msg, type) {
            debugger;
            if (type == 'success') {
                swal("", msg, type).then(function () {
                    window.location = "ForgetPass.aspx";
                });
            }
            else {
                swal("", msg, type);
            }
        }
    </script>
</body>
</html>