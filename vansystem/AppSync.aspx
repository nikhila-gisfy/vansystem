<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="AppSync.aspx.cs" Inherits="vansystem.AppSync" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Styling for the treeview menu */
        .treeview-menu {
            max-height: 200px;
            overflow-y: auto;
            padding: 5px;
        }

            /* Styling for the scrollbar */
            .treeview-menu::-webkit-scrollbar {
                width: 8px;
                /* Set width of the scrollbar */
                background-color: #f8f8f8;
                /* Set background color of the scrollbar */
            }

            /* Styling for the scrollbar thumb */
            .treeview-menu::-webkit-scrollbar-thumb {
                background-color: #2196f3;
                /* Set color of the scrollbar thumb */
                border-radius: 4px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
        <div class="app-content">
            <div class="row">
                <div class="col-md-4 col-sm-12">
                    <div class="tile">
                        <h3 class="tile-title">How to sync data from ODK aggregate?</h3>
                        <div class="tile-body">
                            <p>This is dummy content. This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content </p>
                            <p>This is dummy content. This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content This is dummy content </p>
                        </div>
                        <!--div class="tile-footer"><a class="btn btn-primary" href="#">Link</a></div-->
                    </div>
                </div>
                <div class="col-md-8 col-sm-12">
                    <form method="get" action="AppSync.aspx">
                        <div class="tile">
                            <h3 class="tile-title">Sync Data</h3>
                            <div class="tile-body">
                                <div class="row" style="margin-bottom: 20px;">
                                    <div class="col-md-1"><span class="badge-custom badge-primary">1</span></div>
                                    <div class="col-md-11" id="bndTypeContainer">
                                        <div class="form-group">
                                            <label for="exampleSelect1">Select form to sync</label>
                                            <select class="form-control" name="form">
                                                <option value="0">Select form</option>
                                                <option value="5">Plot Approach</option>
                                                <option value="2">Plot Description</option>
                                                <option value="3">Plot Enumeration</option>
                                                <option value="4">Village Level Information</option>
                                                <option value="6">Household</option>
                                                <option value="7">Timber Extraction</option>
                                                <option value="8">NTFP extraction</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tile-footer">
                                <div class="row">
                                    <div class="col-md-8 col-md-offset-3">
                                        <button class="btn btn-primary" type="submit"><i class="fa fa-fw fa-lg fa-check-circle"></i>Fetch Data</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>

</asp:Content>
