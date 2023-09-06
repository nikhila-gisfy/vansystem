<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="ForestAdminBoundaries.aspx.cs" Inherits="vansystem.ForestAdminBoundaries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-content">
        <div class="row">
            <div class="col-md-4 col-sm-12">
                <div class="tile">
                    <h3 class="tile-title">How to upload forests admin boundaries?</h3>
                    <div class="tile-body">
                        <p>Upload the forest boundaries in shapefile format for the predefined admin hierarchy. The forest admin hierarchy can be defined to the site administrator and the hierarchy will be displayed here accordingly.</p>
                        <p>In most of the cases, it will be division, range, block, compartment, and plots. Upload the shapefile for each of these before proceeding.</p>
                        <p>
                            Once uploaded, the shapefile needs to be verified. <a href="verifyAdminBoundaries.aspx"><b>Click here to verify</b></a>.
                        </p>
                        <br>
                        <p><b>Shape file format:</b></p>
                        <p>
                            Field 1: id<br />
                            Field 2: name<br />
                            Field 3: geom<br />
                        </p>
                    </div>
                    <!--div class="tile-footer"><a class="btn btn-primary" href="#">Link</a></div-->
                </div>
            </div>
            <div class="col-md-8 col-sm-12" id="uploadshapefile" runat="server">
                <div class="tile">
                    <h3 class="tile-title">Upload Forest Admin Boundaries</h3>
                    <div class="tile-body">
                        
                        <div class="row data-upload-wrapper  active " style="margin-bottom: 20px; margin-right: 2px; margin-left: 2px;" id="divisionupload" runat="server">
                            <div class="row col-lg-12">
                                <div class="col-sm-1"><span class="badge-custom badge-light">1</span></div>
                                <div class="col-md-9 col-sm-8" id="bndTypeContainer1">
                                    <div class="form-group" ng-class="{'has-error': uploadFAB_1.file_47.$touched && uploadFAB_1.file_47.$error.required , 'has-success': uploadFAB_1.file_47.$valid }">
                                        <label for="exampleSelect1">Upload Division</label>
                                        <%--<input class="form-control-file data-upload-file" id="file_47" name="file_47" type="file" accept="application/zip" aria-describedby="fileHelp" required>--%>
                                        <asp:FileUpload ID="fileUploadControl" runat="server" />
                                        <small class="form-text text-muted" id="fileHelp1" style="color: #fff !important;">Upload a shape file of Division in zip format</small>
                                        <input class="form-control-file data-upload-param" id="param_47" name="param_47" type="hidden" value="47" />
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-3">
                                    <button id="Uploadfile1" class="btn btn-light" runat="server" onserverclick="Uploadfile1_ServerClick"><i class="fa fa-fw fa-lg fa-check-circle"></i>Upload</button>
                                </div>
                                <div class="col-md-12 validation-error"></div>
                            </div>
                        </div>
                        
                            <div class="row data-upload-wrapper active  " style="margin-bottom: 20px; margin-right: 2px; margin-left: 2px;" id="rangeupload" runat="server">
                                <div class="row col-lg-12">
                                <div class="col-sm-1"><span class="badge-custom badge-light">2</span></div>
                                <div class="col-md-9 col-sm-8" id="bndTypeContainer2">
                                    <div class="form-group" ng-class="{'has-error': uploadFAB_2.file_48.$touched && uploadFAB_2.file_48.$error.required , 'has-success': uploadFAB_2.file_48.$valid }">
                                        <label for="exampleSelect1">Upload Range</label>
                                        <%--<input class="form-control-file data-upload-file" id="file_47" name="file_47" type="file" accept="application/zip" aria-describedby="fileHelp" required>--%>
                                        <asp:FileUpload ID="fileUploadControl1" runat="server" />
                                        <small class="form-text text-muted" id="fileHelp2" style="color: #fff !important;">Upload a shape file of Range in zip format</small>
                                        <input class="form-control-file data-upload-param" id="param_48" name="param_48" type="hidden" value="48" />
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-3">
                                    <button id="Uploadfile2" class="btn btn-light" runat="server" onserverclick="Uploadfile1_ServerClick"><i class="fa fa-fw fa-lg fa-check-circle"></i>Upload</button>
                                </div>
                                <div class="col-md-12 validation-error"></div>
                            </div>
                            </div>
                      
                       

                        <div class="row data-upload-wrapper active  " style="margin-bottom: 20px; margin-right: 2px; margin-left: 2px;" id="blockupload" runat="server">
                            <div class="row col-lg-12">
                                <div class="col-sm-1"><span class="badge-custom badge-light">3</span></div>
                                <div class="col-md-9 col-sm-8" id="bndTypeContainer3">
                                    <div class="form-group" ng-class="{'has-error': uploadFAB_1.file_49.$touched && uploadFAB_1.file_49.$error.required , 'has-success': uploadFAB_1.file_49.$valid }">
                                        <label for="exampleSelect1">Upload Block</label>
                                        <%--<input class="form-control-file data-upload-file" id="file_47" name="file_47" type="file" accept="application/zip" aria-describedby="fileHelp" required>--%>
                                        <asp:FileUpload ID="fileUploadControl2" runat="server" />
                                        <small class="form-text text-muted" id="fileHelp3" style="color: #fff !important;">Upload a shape file of Block in zip format</small>
                                        <input class="form-control-file data-upload-param" id="param_49" name="param_49" type="hidden" value="49" />
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-3">
                                    <button id="Uploadfile3" class="btn btn-light" runat="server" onserverclick="Uploadfile1_ServerClick"><i class="fa fa-fw fa-lg fa-check-circle"></i>Upload</button>
                                </div>
                                <div class="col-md-12 validation-error"></div>
                            </div>
                        </div>

                        <div class="row data-upload-wrapper active  " style="margin-bottom: 20px; margin-right: 2px; margin-left: 2px;" id="compartupload" runat="server">
                            <div class="row col-lg-12">
                                <div class="col-sm-1"><span class="badge-custom badge-light">4</span></div>
                                <div class="col-md-9 col-sm-8" id="bndTypeContainer4">
                                    <div class="form-group" ng-class="{'has-error': uploadFAB_1.file_50.$touched && uploadFAB_1.file_50.$error.required , 'has-success': uploadFAB_1.file_50.$valid }">
                                        <label for="exampleSelect1">Upload Compartment</label>
                                        <%--<input class="form-control-file data-upload-file" id="file_47" name="file_47" type="file" accept="application/zip" aria-describedby="fileHelp" required>--%>
                                        <asp:FileUpload ID="fileUploadControl3" runat="server" />
                                        <small class="form-text text-muted" id="fileHelp4" style="color: #fff !important;">Upload a shape file of Compartment in zip format</small>
                                        <input class="form-control-file data-upload-param" id="param_50" name="param_50" type="hidden" value="50" />
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-3">
                                    <button id="Uploadfile4" class="btn btn-light" runat="server" onserverclick="Uploadfile1_ServerClick"><i class="fa fa-fw fa-lg fa-check-circle"></i>Upload</button>
                                </div>
                                <div class="col-md-12 validation-error"></div>
                            </div>
                        </div>

                        <div class="row data-upload-wrapper active  " style="margin-bottom: 20px; margin-right: 2px; margin-left: 2px;" id="plotupload" runat="server">
                           <div class="row col-lg-12">
                                <div class="col-sm-1"><span class="badge-custom badge-light">5</span></div>
                                <div class="col-md-9 col-sm-8" id="bndTypeContainer5">
                                    <div class="form-group" ng-class="{'has-error': uploadFAB_1.file_51.$touched && uploadFAB_1.file_51.$error.required , 'has-success': uploadFAB_1.file_51.$valid }">
                                        <label for="exampleSelect1">Upload Plot</label>
                                        <%--<input class="form-control-file data-upload-file" id="file_47" name="file_47" type="file" accept="application/zip" aria-describedby="fileHelp" required>--%>
                                        <asp:FileUpload ID="fileUploadControl4" runat="server" />
                                        <small class="form-text text-muted" id="fileHelp5" style="color: #fff !important;">Upload a shape file of Plot in zip format</small>
                                        <input class="form-control-file data-upload-param" id="param_51" name="param_51" type="hidden" value="51" />
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-3">
                                    <button id="Uploadfile5" class="btn btn-light" runat="server" onserverclick="Uploadfile1_ServerClick"><i class="fa fa-fw fa-lg fa-check-circle"></i>Upload</button>
                                </div>
                                <div class="col-md-12 validation-error"></div>
                            </div>
                        </div>
                        <%=Products %>



                    </div>
                    <!--div class="tile-footer"><a class="btn btn-primary" href="#">Link</a></div-->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
