<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Household.aspx.cs" Inherits="vansystem.Household" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
        table {
            width: 450px;
            border-collapse: collapse;
            margin: 50px auto;
           margin-left: 1px;
            margin-top:8px;
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
         .box-content {            max-height: 600px;            overflow: scroll;        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanel1">
        <ContentTemplate>
           <div class="card" style="height: 826px; background-color: #EFEFEF">
                <div class="container mt-6">
                    <div style="margin-left: -19%; width: 113%; margin-top: 88px;">
                        <h3><b>Household</b></h3>
                        <div class="card-body" style="background-color: rgb(252, 252, 252); width: 1591px;">
                            <div style="margin-left: 903px;">
                               
                               <%--  <asp:LinkButton id="btnDownloadCSV" runat="server" type="button" class="btn btn-secondary" Text="Download CSV"><span class="fa fa-download"></span></asp:LinkButton>--%>
                                    
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />

                                <asp:Button ID="btnJson" runat="server" Text="Download Raw Data" OnClick="btnJson_Click" />
                                PageSize:
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10" />
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" />
                                </asp:DropDownList>
                                <hr />
                                    
                            </div>

                            <%-- style="overflow-y: scroll; overflow-x: hidden; height: 200px; width: 600px;"--%>
                            <div class="box-content">

                                <asp:GridView ID="gvHousehold" runat="server" AutoGenerateColumns="false" DataKeyNames="id" OnRowEditing="gvHousehold_RowEditing" OnRowUpdating="gvHousehold_RowUpdating"  OnRowCancelingEdit="gvHousehold_RowCancelingEdit"   GridLines="Horizontal">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="200%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Survey ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurveyID" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Surveyor Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurveyorName" runat="server" Text='<%# Eval("surveyor") %>'></asp:Label>
                                            </ItemTemplate>
                                             <EditItemTemplate>
                                                <asp:TextBox ID="txtSurveyorName" runat="server" Text='<%# Eval("surveyor") %>' Width="140"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Surveyor Phone No" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurveyorPhoneNo" runat="server" Text='<%# Eval("phonenumber") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtSurveyorPhoneNo" runat="server" Text='<%# Eval("phonenumber") %>' Width="140"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("designation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate_created" runat="server" Text='<%# Eval("Date_created") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Latitude" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLatitude" runat="server" Text='<%# Eval("latitude") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Longitude" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLongitude" runat="server" Text='<%# Eval("longitude") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Altitude" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAltitude" runat="server" Text='<%# Eval("altitude") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accuracy" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccuracy" runat="server" Text='<%# Eval("accuracy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblState" runat="server" Text='<%# Eval("state") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                       
                                        <asp:TemplateField HeaderText="Division" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDivision" runat="server" Text='<%# Eval("division") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Range" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRange" runat="server" Text='<%# Eval("range") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Block" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBlock" runat="server" Text='<%# Eval("block") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Village" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVillage" runat="server" Text='<%# Eval("village") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name of Panchayat" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpanchayat" runat="server" Text='<%# Eval("panchayat") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Type of hamlet/village" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhamlet" runat="server" Text='<%# Eval("typeofvillage") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Name of the respodent" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrespodent" runat="server" Text='<%# Eval("respodent") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Relation with the head of family" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRelation" runat="server" Text='<%# Eval("relationtohead") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name of Head of family" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnameofhead" runat="server" Text='<%# Eval("nameofhead") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Total Family Members" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfamilymembers" runat="server" Text='<%# Eval("familymembers") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Type Of Housing" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltypeofhousing" runat="server" Text='<%# Eval("typeofhousing") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Ration Card" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrationcard" runat="server" Text='<%# Eval("rationcard") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Type Of Card" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltypeof_card" runat="server" Text='<%# Eval("typeof_card") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="NREGS Card" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnregs" runat="server" Text='<%# Eval("nregs") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Livestock Owned by Family" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllivestock" runat="server" Text='<%# Eval("livestock") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Type of Livestock" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltypeoflivestock" runat="server" Text='<%# Eval("typeoflivestock") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Breed" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllive_stock_breed" runat="server" Text='<%# Eval("live_stock_breed") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Number of selected livestock" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnumberoflivestock" runat="server" Text='<%# Eval("numberoflivestock") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Number of surviving offspring" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnumberofsurviving" runat="server" Text='<%# Eval("numberofsurviving") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Mortality per year" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmortality" runat="server" Text='<%# Eval("mortality") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Livestock Uses" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbluses" runat="server" Text='<%# Eval("uses") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Primary Occupation" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprimary_occupation" runat="server" Text='<%# Eval("primary_occupation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Selected Months" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblselected_month" runat="server" Text='<%# Eval("selected_month") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Average number of days engaged in primary occupation activity in a month" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnumber_of_days" runat="server" Text='<%# Eval("number_of_days") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Income from that activity ( Yearly)" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblincome" runat="server" Text='<%# Eval("income") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Secondary Occupation" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsecondary_occupation" runat="server" Text='<%# Eval("secondary_occupation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Select months" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblso_selected_month" runat="server" Text='<%# Eval("so_selected_month") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Number of days engaged in secondary occupation activity in a month" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblso_number_of_days" runat="server" Text='<%# Eval("so_number_of_days") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Income from that activity ( Yearly) (SO)" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblso_income" runat="server" Text='<%# Eval("so_income") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Any seasonal migration from the household?" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblso_seasonal_migration" runat="server" Text='<%# Eval("so_seasonal_migration") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Number of people migrating" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnumberofpeoplemigration" runat="server" Text='<%# Eval("numberofpeoplemigration") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="What is the purpose of migration" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpurposeofmigration" runat="server" Text='<%# Eval("purposeofmigration") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Select months" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblselectedmonthsmigration" runat="server" Text='<%# Eval("selectedmonthsmigration") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Where do you collect?" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWhere_collect" runat="server" Text='<%# Eval("Where_collect") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Cost of the fodder purchased" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfodder_purchased_cost" runat="server" Text='<%# Eval("fodder_purchased_cost") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Quantity (kg) of fodder purchased in a year" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfodder_purchased_quantity" runat="server" Text='<%# Eval("fodder_purchased_quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Income earned from selling" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfodder_sold_income" runat="server" Text='<%# Eval("fodder_sold_income") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Women spend time in the collection of" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblwomen_spend_time_in_collection_of" runat="server" Text='<%# Eval("women_spend_time_in_collection_of") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        

                                         <asp:TemplateField HeaderText="Time spend in hours" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblwomen_spend_time_in_collection" runat="server" Text='<%# Eval("women_spend_time_in_collection") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Distance Travelled to collect resources" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblwomen_distance_travel_for_collection" runat="server" Text='<%# Eval("women_distance_travel_for_collection") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Laborious work or Hard work be reduced" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhardwork_be_reduced_in_collection" runat="server" Text='<%# Eval("hardwork_be_reduced_in_collection") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Is the Forest department consulting communities well before on the Enclosure/Fencing of areas for plantation(s)?" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblforest_department_consulting_community" runat="server" Text='<%# Eval("forest_department_consulting_community") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       

                                          <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" CssClass="btn btn-primary" CommandName="edit"  runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" CommandName="Update" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" CommandName="cancel" />
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>
                             
                                
                            </div>
                             <ul class="pagination" style="margin-top: 6px;margin-left: 9px;color:#5cb85c">

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
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnJson" />
        </Triggers>
    </asp:UpdatePanel>
            
</asp:Content>
