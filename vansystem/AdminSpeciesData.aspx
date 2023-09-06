<%@ Page Title="" Language="C#" MasterPageFile="~/VanUser.Master" AutoEventWireup="true" CodeBehind="SpeciesData_1.aspx.cs" Inherits="vansystem.SpeciesData_1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="FES">
    <meta name="keyword" content="">
    <link rel="shortcut icon" href="/vali/img/favicon.png" />
	<title>Van - Upload Species&#039; Data</title>
	<link rel="stylesheet" type="text/css" href="/vali/css/main.css">
	<link rel="stylesheet" type="text/css" href="/vali/css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="/vali/css/font-awesome.min.css">
	<link rel="stylesheet" type="text/css" href="/vali/css/font-awesome-custom.css">
	<link rel="stylesheet" type="text/css" href="/vali/css/custom.css">
	<link rel="stylesheet" type="text/css" href="/global/css/ol4.css">
	<script type="text/javascript" src="/vali/js/jquery-3.2.1.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="app-content">
	<div class="row">
		<div class="col-md-6 col-sm-12">
		  <div class="tile">
			<h3 class="tile-title">How to upload species' data?</h3>
			<div class="scrollbar tile-body" style="max-height: 400px;overflow-y: scroll;">
				<p>
					Upload the species format as mentioned in the steps below: 
					<ul>
						<li>Reupload the species list after deleting the previously added list.</li>
						<li>Recheck the list before uploading. (Uploading a wrong file will lead to system error)</li>
					</ul>
				</p>
				<p>

					<h6>Step 1 :</h6>
					Find habit id that system understands in following table
					<table class="table table-sm">
						<thead>
							<tr>
								<td>Name</td>
								<td>Habit Type</td>
								<td>Habit Name</td>
							</tr>
						</thead>
						<tbody>
														<tr>
								<td>Herb</td>
								<td>1</td>
								<td>flora</td>
							</tr>
														<tr>
								<td>Shrub</td>
								<td>2</td>
								<td>flora</td>
							</tr>
														<tr>
								<td>Sapling</td>
								<td>3</td>
								<td></td>
							</tr>
														<tr>
								<td>Seedling</td>
								<td>4</td>
								<td></td>
							</tr>
														<tr>
								<td>None</td>
								<td>5</td>
								<td>all</td>
							</tr>
														<tr>
								<td>Bamboo</td>
								<td>6</td>
								<td>flora</td>
							</tr>
														<tr>
								<td>Tree</td>
								<td>7</td>
								<td>flora</td>
							</tr>
														<tr>
								<td>Amphibian</td>
								<td>8</td>
								<td>fauna</td>
							</tr>
														<tr>
								<td>Bird</td>
								<td>9</td>
								<td>fauna</td>
							</tr>
														<tr>
								<td>Mammal</td>
								<td>10</td>
								<td>fauna</td>
							</tr>
														<tr>
								<td>Reptile</td>
								<td>11</td>
								<td>fauna</td>
							</tr>
														<tr>
								<td>Climber</td>
								<td>12</td>
								<td>flora</td>
							</tr>
														<tr>
								<td>Grass</td>
								<td>13</td>
								<td>flora</td>
							</tr>
														<tr>
								<td>Orchid</td>
								<td>14</td>
								<td></td>
							</tr>
														<tr>
								<td>Weed</td>
								<td>15</td>
								<td>flora</td>
							</tr>
														<tr>
								<td>Butterfly</td>
								<td>16</td>
								<td>fauna</td>
							</tr>
														<tr>
								<td>Dragonfly</td>
								<td>17</td>
								<td>fauna</td>
							</tr>
														<tr>
								<td>Spider</td>
								<td>18</td>
								<td>fauna</td>
							</tr>
														<tr>
								<td>Fish</td>
								<td>19</td>
								<td>fauna</td>
							</tr>
														<tr>
								<td>opt5</td>
								<td>20</td>
								<td>fauna</td>
							</tr>
													</tbody>
					</table>
				</p>
				<p>
					<h6>Step 2 :</h6>
					The habit id could be entered referring the above table. Species file should contain Habit Type, Species Id, Local Name, Scientific Name as columns. Please see <b>sample</b> below:
				</p>
				<table class="table">
					<thead>
						<tr>
							<td>Habit Type</td>
							<td>Species Id</td>
							<td>Local Name</td>
							<td>Scientific Name</td>
						</tr>
					</thead>
					<tbody>
						<tr>
							<td>1</td>
							<td>23</td>
							<td>Neem</td>
							<td>Azadirachta indica</td>
						</tr>
						<tr>
							<td>9</td>
							<td>25</td>
							<td>Crow</td>
							<td>Corvus Splendens</td>
						</tr>
						<tr>
							<td>8</td>
							<td>24</td>
							<td>Frog</td>
							<td>Hoplobatrachus tigerinus</td>
						</tr>
					</tbody>
				</table>
				<div class="alert alert-info">
					<b>Note:</b> The file must be comma separated ‘CSV’ (comma separated values) file and must consist following columns.
				</div>
				<div class="alert alert-danger">
					<b>Note :</b> Any change in the species list after data collection will lead to loss of collected data.
				</div>
			</div>
			<!--div class="tile-footer"><a class="btn btn-primary" href="#">Link</a></div-->
		  </div>
		</div>
		<div class="col-md-6 col-sm-12">
		  <div class="tile">
			<h3 class="tile-title">Upload Species</h3>
			<div class="tile-body">

									<form id="uploadSpeciesData" method="post" action="/DataUpload/postSpeciesData" enctype="multipart/form-data">
						<div id="formContainer">
							<div id="parentsWrapper">
								<div class="row" style="margin-bottom: 20px;">
									<div class="col-md-1"><span class="badge-custom badge-primary">1</span></div>
									<div class="col-md-11" id="bndTypeContainer">
										<div class="form-group">
											<asp:Label ID="lbldivision" runat="server" Text=""></asp:Label>         
											
										</div>
									</div>
								</div>
							</div>
						</div>
							<div class="row" id="csvUpload" style="margin-bottom: 20px;">
								<div class="col-md-1"><span class="badge-custom badge-primary">3</span></div>
								<div class="col-md-11">
									<div class="form-group">
										<label for="exampleInputFile">Upload CSV file</label>
										<%--<input class="form-control-file" id="csv_file" name="csv_file" type="file" accept=".csv" aria-describedby="fileHelp">--%>
										
										<asp:FileUpload ID="csv_file" runat="server" accept=".csv" />
										<asp:RegularExpressionValidator ID="regexValidator" runat="server"
                                           ControlToValidate="csv_file"
                                             ErrorMessage="Only csv files are allowed" ClientValidationFunction="changeColor" 
                                           ForeColor="Red" 
                                           ValidationExpression="(.*\.([cC][sS][vV])$)">
                                       </asp:RegularExpressionValidator>
										
										<small class="form-text text-muted" id="fileHelp">Upload comma seperated csv file containing species list</small>
									</div>
								</div>
							</div>
							<div class="row" id="submitButton" style="margin-bottom: 20px;">
								<div class="col-md-1"><span class="badge-custom badge-primary">4</span></div>
								<div class="col-md-11">		
									<div class="tile-footer">
										<button id="uploaddata" runat="server" onserverclick="uploaddata_ServerClick" class="btn btn-primary" type="submit">Upload</button>
									</div>
								</div>
							</div>
						</div>
					</form>
					
				</div>
				</div>
			</div>
			<!--div class="tile-footer"><a class="btn btn-primary" href="#">Link</a></div-->
		  </div>
		</div>
	
</asp:Content>