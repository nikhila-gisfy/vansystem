<%@ Page Title="" Language="C#" MasterPageFile="~/AdminVanUser.Master" AutoEventWireup="true" CodeBehind="TimberExtractionAdmin.aspx.cs" Inherits="vansystem.TimberExtractionAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            let value = input.value;<a href="TimberExtraction.aspx">TimberExtraction.aspx</a>
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

    <style type="text/css">
        label {
            font-weight: normal;
        }
        /*.table-dark, .table-dark > th, .table-dark > td {
	    background-color: #f1f3f5 !important;
	}*/
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

        a {
            color: white;
            text-decoration: none;
        }

        .swal-overlay--show-modal .swal-modal {
            width: 30%;
            height: 31%;
        }

        .swal-text {
            font-size: 18px;
        }

        .swal-footer {
            padding: 6% 37%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanel1">
        <ContentTemplate>
            <div class="app-content">
                <div class="row">
                    <div class="col-md-4 col-sm-12">
                        <div class="tile">
                            <h3 class="tile-title">Add new Timber Extraction record</h3>
                            <div class="scrollbar tile-body" style="max-height: 100%; overflow-y: auto;">
                                <div class="col-md-9 col-sm-12">

                                    <div class="form-group">
                                        <asp:Label ID="lblYear" runat="server" Text="Reporting Year"></asp:Label>
                                        <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" placeholder="Reporting Year"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Please enter the details" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtYear" ></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblwoodTQ" runat="server" Text="Wood Type/Quality"></asp:Label>
                                        <asp:TextBox ID="txtwoodTQ" runat="server" oninput="SplCharCheck(this);" CssClass="form-control" placeholder="Wood Type/Quality"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Please enter the details" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtwoodTQ"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblWoodExtraction" runat="server" Text="Wood Extraction(cmt)"></asp:Label>
                                        <asp:TextBox ID="txtWoodExtraction" runat="server" oninput="NumberCheck(this);" CssClass="form-control" placeholder="Wood Extraction(cmt)"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Please enter the details" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtWoodExtraction"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblPoles" runat="server" Text="Poles/Small Wood Type/ Quality"></asp:Label>
                                        <asp:TextBox ID="txtPoles" runat="server" oninput="SplCharCheck(this);" CssClass="form-control" placeholder="Poles/Small Wood Type/ Quality"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Please enter the details" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtPoles"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblPolescmt" runat="server" Text="Poles/Small Wood Extraction (cmt)"></asp:Label>
                                        <asp:TextBox ID="txtPolescmt" runat="server" oninput="NumberCheck(this);" CssClass="form-control" placeholder="Poles/Small Wood Extraction (cmt)"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Please enter the details" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtPolescmt"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblTotalextraction" runat="server" Text="Total Extraction(cmt)"></asp:Label>
                                        <asp:TextBox ID="txtTotalextraction" oninput="NumberCheck(this);" runat="server" CssClass="form-control" placeholder="Total Extraction(cmt)"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Please enter the details" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtTotalextraction"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblTotalextractionunauthorized" runat="server" Text="Total Extraction from Unauthorized Means (if any)"></asp:Label>
                                        <asp:TextBox ID="txtTotalextractionunauthorized" oninput="NumberCheck(this);" runat="server" CssClass="form-control" placeholder="Total Extraction from Unauthorized Means (if any)"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Please enter the details" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtTotalextractionunauthorized"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblTotalextractionfromToF" runat="server" Text="Total Extraction from ToF (cmt)"></asp:Label>
                                        <asp:TextBox ID="txtTotalextractionfromToF" oninput="NumberCheck(this);" runat="server" CssClass="form-control" placeholder="Total Extraction from ToF (cmt)"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Please enter the details" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtTotalextractionfromToF"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblComparedwiththeroster" runat="server" Text="Compared with The Roster and Norm"></asp:Label>
                                        <asp:TextBox ID="txtComparedwiththeroster" oninput="NumberCheck(this);" runat="server" CssClass="form-control" placeholder="Compared with The Roster and Norm"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Please enter the details" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtComparedwiththeroster"></asp:RequiredFieldValidator>
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
                            <asp:GridView runat="server" AlternatingRowStyle-CssClass="infoarea" ID="gvActivity" AutoGenerateColumns="false" AllowSorting="true" AllowPaging="true" OnSorting="gvActivity_Sorting" OnPageIndexChanging="gvActivity_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="ReportingYear" HeaderText="Reporting Year" SortExpression="ReportingYear" />
                                    <asp:BoundField DataField="WoodTypequality" HeaderText="Wood Type/Quality" SortExpression="WoodTypequality" />
                                    <asp:BoundField DataField="WoodExtraction" HeaderText="Wood Extraction(cmt)" SortExpression="WoodExtraction" />
                                    <asp:BoundField DataField="PolessmallwoodTypequality" HeaderText="Poles/Small Wood Type/ Quality" SortExpression="PolessmallwoodTypequality" />
                                    <asp:BoundField DataField="PolessmallwoodExtraction" HeaderText="Poles/Small Wood Extraction (cmt)" SortExpression="PolessmallwoodExtraction" />
                                    <asp:BoundField DataField="Totalextraction" HeaderText="Total Extraction(cmt)" SortExpression="Totalextraction" />
                                    <asp:BoundField DataField="TotalExtractionUnAuthroized" HeaderText="Total Extraction from Unauthorized Means (if any)" SortExpression="Totalextractionfromunauthorizedmeans" />
                                    <asp:BoundField DataField="TotalextractionToF" HeaderText="Total Extraction from ToF (cmt)" SortExpression="TotalextractionfromToF" />
                                    <asp:BoundField DataField="CompareRosterNorm" HeaderText="Compared with The Roster and Norm" SortExpression="Comparedwiththerosterandnorm" />
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
                    <!--div class="tile-footer"><a class="btn btn-primary" href="#">Link</a></div-->
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
