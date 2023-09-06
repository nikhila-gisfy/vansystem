<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPass.aspx.cs" Inherits="vansystem.ResetPass" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="FES">
    <meta name="keyword" content="">
    <link rel="shortcut icon" href="vali/img/favicon.png" />
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
                    <h1 class="ribbon"><strong class="ribbon-content">Reset Password</strong></h1>
                    <div class="animate form" id="login">
                        <form class="form-horizontal" method="post" runat="server">
                            <div class="input-group">
                                <span class="input-group-addon glyphicon glyphicon-lock" id="basic-addon1"></span>
                                <asp:TextBox ID="txtNewPassword" placeholder="New Password" runat="server" aria-describedby="basic-addon2" autocomplete="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNewPwd" runat="server"
                                    ControlToValidate="txtNewPassword" Display="Dynamic"
                                    ErrorMessage="Please enter new password" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <span class="devtoAsterix">*</span>
                            </div>
                            <div class="input-group">
                                <span class="input-group-addon glyphicon glyphicon-lock" id="basic-addon2"></span>
                                <asp:TextBox ID="txtConPass" runat="server" placeholder="Confirm Password" aria-describedby="basic-addon2" autocomplete="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvConfirmPwd" runat="server" ControlToValidate="txtConPass" Display="Dynamic" ErrorMessage="Please re-enter password to confirm" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cmvConfirmPwd" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConPass" Display="Dynamic" ErrorMessage="Password didn't match" ForeColor="Red" SetFocusOnError="True"></asp:CompareValidator>
                                <span class="devtoAsterix">*</span>
                            </div>
                            <div class="login button text-center">
                                <asp:Button ID="btnRPass" runat="server" Text="Reset Password" CssClass="btn btn-primary" OnClick="btnRPass_Click" />
                            </div>
                            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label><br />
                            <asp:Label ID="lblExpired" runat="server" Text="" Style="color: #FF0000"></asp:Label>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <footer class="app-footer" style="display: none;">
        <div class="col-md-12 col-sm-12">
            <p class="text-center">Van System is based on the Integrated Forest Management Toolbox (IFMT) developed by <a href="http://www.indiaobservatory.org.in" target="_blank">India Observatory</a> an initiative of <a href="http://www.fes.org.in" target="_blank">Foundation for Ecological Security (FES)</a>.</p>
        </div>
    </footer>
    <script src="vali/js/plugins/pace.min.js"></script>
    <script src="vali/js/popper.min.js"></script>
    <script src="vali/js/bootstrap.min.js"></script>
    <script src="vali/js/main.js"></script>
    <script src="vali/js/custom.js"></script>

</body>
</html>


