<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VillageLI.aspx.cs" EnableEventValidation="false" Inherits="vansystem.VillageLI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style>
        table {
            width: 450px;
            border-collapse: collapse;
            margin: 50px auto;
            margin-left: 40px;
        }

        th {
            background: #5cb85c;
            color: white;
            font-weight: bold;
            text-align: center;
        }

        td, th {
            padding: 10px;
            border: 1px solid #ccc;
            text-align: center;
            font-size: 15px;
        }

        .labels tr td {
            background-color: #2cc16a;
            font-weight: bold;
            color: #fff;
        }

        .label tr td label {
            display: block;
        }


        [data-toggle="toggle"] {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanel1">
        <ContentTemplate>
            <div class="card" style="height: 783px; background-color: #EFEFEF">
                <div class="container mt-3">
                    <div style="margin-left: -9%; width: 113%; margin-top: 88px;">
                        <h3><b>Village Level Information</b></h3>
                        <div class="card-body" style="background-color: rgb(252, 252, 252); width: 1364px;">
                            <div style="margin-left: 950px;">
                               <%-- <asp:LinkButton id="btnDownloadCSV" runat="server" type="button" class="btn btn-secondary" Text="Download CSV" OnClick="btnDownloadCSV_Click"></asp:LinkButton>--%>
                               <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                            </div>
                            
                            <asp:GridView ID="gvVLI" runat="server" AutoGenerateColumns="false" OnRowCommand="gvVLI_RowCommand" OnPageIndexChanging="gvVLI_PageIndexChanging"  AllowPaging="true" PageSize="6">
                                <Columns>
                                  
                                    <asp:TemplateField HeaderText="Survey ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSurveyID" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Surveyor Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSurveyorName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Surveyor Phone No" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSurveyorPhoneNo" runat="server" Text='<%# Eval("phonenumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Latitude" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLatitude" runat="server" Text='<%# Eval("Latitude") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Longitude" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLongitude" runat="server" Text='<%# Eval("Longitude") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Altitude" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAltitude" runat="server" Text='<%# Eval("Altitude") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Accuracy" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccuracy" runat="server" Text='<%# Eval("Accuracy") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate_created" runat="server" Text='<%# Eval("Date_created") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    
                                </Columns>

                            </asp:GridView>

                        </div>
                       
                    </div>
                </div>
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
