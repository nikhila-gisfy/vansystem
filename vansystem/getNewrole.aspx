<%@ Page Title="" Language="C#" MasterPageFile="~/AdminVanUser.Master" AutoEventWireup="true" CodeBehind="getNewrole.aspx.cs" Inherits="vansystem.getNewrole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="FES">
    <meta name="keyword" content="">
    <link rel="shortcut icon" href="/vali/img/favicon.png" />
    <title>Van - Update System User (Adilabad)</title>
    <link rel="stylesheet" type="text/css" href="/vali/css/main.css">
    <link rel="stylesheet" type="text/css" href="/vali/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="/vali/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="/vali/css/font-awesome-custom.css">
    <link rel="stylesheet" type="text/css" href="/vali/css/custom.css">
    <link rel="stylesheet" type="text/css" href="/global/css/ol4.css">
    <script type="text/javascript" src="/vali/js/jquery-3.2.1.min.js"></script>
    <style type="text/css">
        h6.titleBar, h5 {
            margin: 15px 0;
        }

        h5 {
            margin-bottom: 0;
        }

        div.box {
            height: 200px;
            overflow-y: scroll;
            border: 1px solid #cacaca;
            background: #FAFAFA;
            border: 2px solid #ced4da;
            border-radius: 4px;
        }

        .elementBox {
            /* margin: 0 15px; */
            padding: 10px;
            border: 2px solid #ced4da;
            border-radius: 4px;
        }

        .errorMessage {
            background-color: white;
            width: 143px;
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 5px;
            padding-bottom: 5px;
            margin-left: 107px;
            visibility: hidden;
            border-radius: 10px;
            position: relative;
            float: left;
        }

            .errorMessage.top-arrow:after {
                content: " ";
                position: absolute;
                right: 90px;
                top: -15px;
                border-top: none;
                border-right: 10px solid transparent;
                border-left: 10px solid transparent;
                border-bottom: 15px solid white;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-content">
        <form name="simpleForm" class="form-horizontal" method="post" action="/postNewUser">
            <input type="hidden" name="user_role" id="userRole" value="3" />
            <div class="row">
                <div class="col-md-6">
                    <div class="tile">
                        <h3 class="tile-title">New System Role</h3>
                        <div class="tile-body">
                            <div class="form-group row">
                                <label class="control-label col-md-3">Role Name *</label>
                                <div class="col-md-8">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="name" ErrorMessage="Please fill out this field." ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                    <input class="form-control" placeholder="Enter Role name" type="text" runat="server" maxlength="25" name="name" id="name" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label col-md-3">Description *</label>
                                <div class="col-md-8">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="description" ErrorMessage="Please fill out this field." ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                    <input class="form-control" placeholder="Enter Description" type="text" maxlength="10" name="mob_number" runat="server" id="description" />

                                </div>
                            </div>
                        </div>
                        <div class="tile-footer">
                            <div class="row">
                                <div class="col-md-8 col-md-offset-3">
                                    <button class="btn btn-primary" type="submit" runat="server" onserverclick="Unnamed_ServerClick"><i class="fa fa-fw fa-lg fa-check-circle"></i>Save Role Details</button>
                                    <button class="btn btn-danger" type="button" onclick="window.location = 'getRoles.aspx';"><i class="fa fa-fw fa-lg fa-times-circle"></i>Cancel</button>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>

</asp:Content>
