<%@ Page Title="" Language="C#" MasterPageFile="~/AdminVanUser.Master" AutoEventWireup="true" CodeBehind="NTFPExtractionAdmin.aspx.cs" Inherits="vansystem.NTFPExtractionAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Van - NTFP Extraction</title>
    <style type="text/css">
        label {
            font-weight: normal;
        }

        table {
            width: 450px;
            border-collapse: collapse;
            margin: 50px auto;
            margin-left: 9px;
            margin-top: 14px;
        }

        th {
            background: #5cb85c;
            color: white;
            font-weight: bold;
            text-align: center;
        }

        td, th {
            padding: 29px;
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

        a {
            color: white;
            text-decoration: none;
        }
    </style>
    <script type="text/javascript">
        function ShowAlert(msg, type) {
            debugger;
            if (type == 'Add successfully') {
                swal("", msg, type).then(function () {
                    window.location = "TimberExtraction.aspx";
                });
            }
            else {
                swal("", msg, type);
            }
        }
        function NumberCheck(input) {
            debugger;
            let value = input.value;
            let numbers = value.replace(/[^0-9]/g, "");
            input.value = numbers;
        }

        function DecimalCheck(input) {
            debugger;
            let value = input.value;
            let numbers = value.replace(/[^0-9.]/g, "");
            input.value = numbers;
        }

        function SplCharCheck(input) {
            debugger;
            let value = input.value;
            let numbers = value.replace(/[^A-Za-z.,/_&( )@]/g, "").trimStart();
            input.value = numbers;
        }
        $(document).ready(function () {
            $('#data thead tr#filterrow th').each(function () {
                var title = $('#data thead th').eq($(this).index()).text();
                $(this).html('<input type="text" onclick="stopPropagation(event);" placeholder="Search ' + title + '" />');
            });

            // DataTable
            var table = $('#data').DataTable({
                orderCellsTop: true,
                iDisplayLength: 25
            });

            // Apply the filter
            $("#data thead input").on('keyup change', function () {
                table
                    .column($(this).parent().index() + ':visible')
                    .search(this.value)
                    .draw();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanel1">
        <ContentTemplate>
            <div class="app-content">
                <div class="row">
                    <div class="col-md-4 col-sm-12">
                        <div class="tile">
                            <h3 class="tile-title">Add new NTFP Extraction record</h3>
                            <div class="scrollbar tile-body" style="max-height: 100%; overflow-y: auto;">
                                <div class="col-md-9 col-sm-12">
                                    <div class="form-group">
                                        <asp:Label ID="lblYear" runat="server" Text="Reporting Year"></asp:Label>
                                        <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" placeholder="Reporting Year"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Enter Year" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtYear"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblName" runat="server" Text="Name of The Major NTFP/Spp."></asp:Label>
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name of The Major NTFP/Spp."></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Enter Name of the major NTFP/Spp." SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblTotalcollection" runat="server" Text="Total Collection (kg, metric tons, std. Bags)"></asp:Label>
                                        <asp:TextBox ID="txtTotalcollection" runat="server" CssClass="form-control" placeholder="Total Collection (kg, metric tons, std. Bags)"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Enter Total collection" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtTotalcollection"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblActualcollection" runat="server" Text="Difference in The Target and Actual Collection"></asp:Label>
                                        <asp:TextBox ID="txtActualcollection" runat="server" CssClass="form-control" placeholder="Difference in The Target and Actual Collection"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Enter Difference in the target and actual collection" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtActualcollection"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblproductivity" runat="server" Text="Measures Taken Maintaining The Productivity"></asp:Label>
                                        <asp:TextBox ID="txtproductivity" runat="server" CssClass="form-control" placeholder="Measures Taken Maintaining The Productivity"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Enter Measures taken maintaining the productivity" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtproductivity"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblBudget" runat="server" Text="Budget Allocation for NTFP Management"></asp:Label>
                                        <asp:TextBox ID="txtBudget" runat="server" CssClass="form-control" placeholder="Budget Allocation for NTFP Management"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Enter Budget allocation for NTFP management" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtBudget"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" CssClass="btn btn-success" />
                                    </div>

                                </div>
                            </div>
                            <!--div class="tile-footer"><a class="btn btn-primary" href="#">Link</a></div-->
                        </div>
                    </div>
                    <div class="col-md-8 col-sm-12">
                        <div class="tile">
                            <h3 class="tile-title">Timber Extraction Records</h3>
                            <div class="tile-body" style="overflow-x: auto; font-size: 90%; max-height: 700px;">
                                Show
                            <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <asp:ListItem Text="10" Value="10" />
                                <asp:ListItem Text="25" Value="25" />
                                <asp:ListItem Text="50" Value="50" />
                            </asp:DropDownList>
                                entries
                            <asp:GridView runat="server" AlternatingRowStyle-CssClass="infoarea" ID="gvActivity" AutoGenerateColumns="false" AllowSorting="true" AllowPaging="true" OnSorting="gvActivity_Sorting" PageSize="10" OnPageIndexChanging="gvActivity_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="ReportingYear" HeaderText="Reporting Year" SortExpression="ReportingYear" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="NTFPName" HeaderText="Name of The Major NTFP/Spp." SortExpression="NTFPName" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Totalcollection" HeaderText="Total Collection (kg, metric tons, std. Bags)" SortExpression="Totalcollection" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="DiffTargetActual" HeaderText="Difference in The Target and Actual Collection" SortExpression="DiffTargetActual" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="ProductivityMeasures" HeaderText="Measures Taken Maintaining The Productivity" SortExpression="ProductivityMeasures" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="BudgetAllocation" HeaderText="Budget Allocation for NTFP Management" SortExpression="BudgetAllocation" ItemStyle-Width="10%" />
                                </Columns>
                            </asp:GridView>
                            </div>
                            <ul class="pagination" style="margin-top: 6px; margin-left: 9px; color: #5cb85c">

                                <asp:Repeater ID="rptPager" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                Enabled='<%# Eval("Enabled") %>' OnClick="lnkPage_Click"></asp:LinkButton>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
