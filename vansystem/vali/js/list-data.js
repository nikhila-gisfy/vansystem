$.fn.dataTable.pipeline = function ( opts ) {
	// Configuration options
	var conf = $.extend( {
		pages: 5,     // number of pages to cache
		url: '',      // script url
		data: null,   // function or object with parameters to send to the server
					// matching how `ajax.data` works in DataTables
		method: 'POST' // Ajax HTTP method
	}, opts );

	// Private variables for storing the cache
	var cacheLower = -1;
	var cacheUpper = null;
	var cacheLastRequest = null;
	var cacheLastJson = null;

	return function ( request, drawCallback, settings ) {
		var ajax          = false;
		var requestStart  = request.start;
		var drawStart     = request.start;
		var requestLength = request.length;
		var requestEnd    = requestStart + requestLength;

		if ( settings.clearCache ) {
			// API requested that the cache be cleared
			ajax = true;
			settings.clearCache = false;
		}
		else if ( cacheLower < 0 || requestStart < cacheLower || requestEnd > cacheUpper ) {
			// outside cached data - need to make a request
			ajax = true;
		}
		else if ( JSON.stringify( request.order )   !== JSON.stringify( cacheLastRequest.order ) ||
			JSON.stringify( request.columns ) !== JSON.stringify( cacheLastRequest.columns ) ||
			JSON.stringify( request.search )  !== JSON.stringify( cacheLastRequest.search )
		) {
			// properties changed (ordering, columns, searching)
			ajax = true;
		}

		// Store the request for checking next time around
		cacheLastRequest = $.extend( true, {}, request );

		if ( ajax ) {
			// Need data from the server
			if ( requestStart < cacheLower ) {
				requestStart = requestStart - (requestLength*(conf.pages-1));

				if ( requestStart < 0 ) {
					requestStart = 0;
				}
			}

			cacheLower = requestStart;
			cacheUpper = requestStart + (requestLength * conf.pages);

			request.start = requestStart;
			request.length = requestLength*conf.pages;

			// Provide the same `data` options as DataTables.
			if ( $.isFunction ( conf.data ) ) {
				// As a function it is executed with the data object as an arg
				// for manipulation. If an object is returned, it is used as the
				// data object to submit
				var d = conf.data( request );
				if ( d ) {
					$.extend( request, d );
				}
			}
			else if ( $.isPlainObject( conf.data ) ) {
				// As an object, the data given extends the default
				$.extend( request, conf.data );
			}

			settings.jqXHR = $.ajax( {
				"type":     conf.method,
				"url":      conf.url,
				"data":     request,
				"dataType": "json",
				"cache":    false,
				"success":  function ( json ) {
					cacheLastJson = $.extend(true, {}, json);

					if ( cacheLower != drawStart ) {
						json.data.splice( 0, drawStart-cacheLower );
					}
					json.data.splice( requestLength, json.data.length );

					drawCallback( json );
				}
			} );
		}
		else {
			json = $.extend( true, {}, cacheLastJson );
			json.draw = request.draw; // Update the echo for each response
			json.data.splice( 0, requestStart-cacheLower );
			json.data.splice( requestLength, json.data.length );

			drawCallback(json);
		}
	}
};

// Register an API method that will empty the pipelined data, forcing an Ajax
// fetch on the next draw (i.e. `table.clearPipeline().draw()`)
$.fn.dataTable.Api.register( 'clearPipeline()', function () {
	return this.iterator( 'table', function ( settings ) {
		settings.clearCache = true;
	} );
} );

function triggerUpdate()
{
	$(document).on('click', '.discard', function(){
		elem = $(this);
		surveyId = $(this).attr('data-attr');
		assign = $(this).text();
		$.ajax({
			url : "/DataEdit/discardData",
			type: "post",
			data : "surveyId="+surveyId+"&assign="+assign,
			success : function(response)
			{
				if(response == 1)
				{
					$('.discard[data-attr="'+surveyId+'"]').toggleClass('badge-danger badge-success');
					text = elem.text() == "Included" ? "Excluded" : "Included";
					$('.discard[data-attr="'+surveyId+'"]').text(text);

				}
			}
		});
	});

	$(document).on('dblclick','#gvVLI td',function(){
		if(!$(this).hasClass('disbale-dbl'))
		{
			var fieldData = $(this).find('span').data();
			var attr = $(this).find('span').data('attr');
			var sId = $(this).parent().find('.survey-id').text();
			if(attr == "")
			{
				$.jGrowl("This field can't be edited");
			}else{
				$.ajax({
					url : "/DataEdit/editField",
					type: "post",
					data : "sId="+sId+"&"+$.param(fieldData),
					success : function(response)
					{
						if(response != null)
						{
							$('#dataModal .modal-body').html(response);
							$('#dataModal .modal-body').css('overflow-y', 'visible');
							$('#dataModal .modal-body').css('max-height', $(window).height() * 0.7);
							$('#dataModal').modal({backdrop: 'static', keyboard : false});
							$('#dataModal').modal('show');
							if(!$('#dataModal .modal-footer').is(":visible"))
							{
								$('#dataModal .modal-footer').show();	
							}
							$('#editOption').chosen();
						}
					}
				});
			}
		}
	});

	$(document).on('click','a.more',function(){
		var sId = $(this).parents('tr').find('.survey-id').text();
			
		var values = $(this).parents('span').data('value');
		var label = $(this).parents('span').data('label');

		var valuesData = values.split("|");
		var newValues = valuesData.join("<br />");

		$('#dataModal .modal-body').html("<form class='form'><div>"+newValues+"</div></form>");
		$('#dataModal .modal-body').css('overflow-y', 'auto');
		$('#dataModal .modal-body').css('max-height', $(window).height() * 0.7);
		$('#dataModal .modal-title').html(label+" ( Survey ID: "+sId+" )");
		$('#dataModal').modal({backdrop: 'static', keyboard : false});
		$('#dataModal').modal('show');
		$('#dataModal .modal-footer').hide();		
	});

	$(document).on('click','.saveFieldChanges',function(){

		//Fetch form to apply custom Bootstrap validation
		var form = $("#saveEditOption");
	
		if (form[0].checkValidity() === false) {
			event.preventDefault();
			event.stopPropagation();
		} else {
			var data = $('#saveEditOption').serialize();
			var surveyId = GetQueryStringParams(data, 'survey_id');
			var attrId = GetQueryStringParams(data, 'attr');
			var seqId = GetQueryStringParams(data, 'seq');
			var groupId = GetQueryStringParams(data, 'group_id') == '' ? 0 : GetQueryStringParams(data, 'group_id');
			var pId = GetQueryStringParams(data, 'p_id') == '' ? 0 : GetQueryStringParams(data, 'p_id');
			
			$.ajax({
				data : data,
				type : "post",
				url : "/DataEdit/updateFieldData",
				success : function(response) {
					if(response != 0) {
						$(".f-"+surveyId+'-'+attrId+'-'+groupId+'-'+seqId+'-'+pId).parent().html(response);
						$('#dataModal .close').click();
					} else {
						alert('Error occured.');
					}
				}
			});
		}
		form.addClass('was-validated');
	});
}
var table;
$(document).ready(function () {

	var dataTableHeight = $(window).height() - 380;

	var columns = $('.table-headers th');

	var formId = $('#gvVLI').data('form');
	var formName = $('#gvVLI').data('name');
	/**
	 * Commented untill confirmed
	 */
	// DataTable
	// table = $('#gvVLI').DataTable({
	// 	//"dom": '<"top"iflp>rt<"bottom"p><"clear">',
	// 	"orderCellsTop": true,
	// 	"bProcessing": true,
	// 	"bServerSide": true,
	// 	"ajax": {
	// 		"url": "/DataEdit/loadFormData",
	// 		"type": "POST",
	// 		"data": {
	// 			"form" : formId
	// 		}
	// 	},
	// 	oLanguage: {sProcessing: '<div id="loader"><div class="sk-folding-cube"><div class="sk-cube1 sk-cube"></div><div class="sk-cube2 sk-cube"></div><div class="sk-cube4 sk-cube"></div><div class="sk-cube3 sk-cube"></div>IFMT</div></div>'},
	// 	"iDisplayLength": 10,
	// 	"sEcho": 1,
	// 	"scrollY": dataTableHeight,
	// 	"scrollX": true,
	// 	"pagingType": "full_numbers",
	// 	lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
	// 	//"fnDrawCallback": loadMore,
	// 	"aoColumnDefs": [{ "bSortable": false, "aTargets": [0, 2] }],
	// 	fixedColumns:  {
	// 		leftColumns: 3
	// 	},
	// 	dom: 'Blrtip',
	// 	buttons: [
	// 		{
	// 			extend: 'excelHtml5',
	// 			text: '<i class="fa fa-download"></i> Download Excel',
	// 			filename: formName+"-data",
	// 			action: function(e, dt, button, config) {
	// 				var that = this;
	// 				dt.one('preXhr', function(e, s, data) {
	// 				  	data.length = -1;
	// 				}).one('draw', function(e, settings, json, xhr) {
	// 					var excelButtonConfig = $.fn.DataTable.ext.buttons.excelHtml5;
	// 					var addOptions = { exportOptions: { "page" : "all" }};
	// 					that.page.len( -1 );
	// 					$.extend(true, excelButtonConfig, addOptions);
	// 					excelButtonConfig.action.call(that, e, dt, button, excelButtonConfig);
	// 				}).draw();
	// 			}
	// 		}
	// 	],
	// 	createdRow: function(row, data, index) {
	// 		$('td', row).each(function(index) {
	// 			if($(columns[index]).hasClass('alt')) {
	// 				$(this).addClass('alt');
	// 			}
	// 		});
	// 	}
	// }).on("init.dt", function(e, settings, data) {
	// 	triggerUpdate();
	// });

	table = $('#gvVLI').DataTable({
		"bProcessing": true,
		"bServerSide": true,
		"ajax": {
			"url": "/DataEdit/loadFormData",
			"type": "POST",
			"data": {
				"form" : formId
			}
		},
		searchHighlight: true,
		"iDisplayLength": 10,
		"sEcho": 1,
		"scrollY": dataTableHeight,
		"scrollX": true,
		"pagingType": "full_numbers",
		"aoColumnDefs": [
            { "bSearchable": false, "aTargets": [0, 2] }, 
            { "bSortable": false, "aTargets": [0, 2] }
        ],
		lengthMenu: [[10, 25, 500, -1], [10, 25, 500, "All"]],
		fixedColumns:  {
			leftColumns: 3
		},
		dom: 'lBfrtip',
		buttons: [
			{
				text: '<i class="fa fa-file-excel-o fa-fw"></i> Download CSV</h2>',
				action: function() {
					//var query = $('#formTable .dataTables_scrollHead :input').serialize();
					window.location = '/DataEdit/downloadXls?form='+formId; //+query;
				}
			}
		]
	}).on("init.dt", function(e, settings, data) {
		triggerUpdate();
	});
	$("#catId").chosen({
		placeholder_text_multiple: "Select Categories..."
	}).change(function() {
		if($(this).val() == 'all' || $(this).val() == '') {
			table.columns().visible(true);
			table.rows().recalcHeight().fixedColumns().relayout();
		} else {
			table.columns().visible(false);
			table.columns([0,1,2]).visible(true);
			$.each($(this).val(), function(i,v){
				v = v.replace(/ /g, '-');
				v = v.toLowerCase();
				table.columns('.'+v).visible(true);
				table.rows().recalcHeight().fixedColumns().relayout();
			});
		}
	});

	$('thead tr.filter-row th').each(function (index) {
		var title = $(this).text();
		if(title != '') {
			$(this).html('<input type="text" onclick="stopPropagation(event);" placeholder="Search ' + title + '" id="'+index+'" />');
		}
	});
	
	// Apply the filter
	$(".filter-row input").keypress( function () {
		if (event.which == 13) {
			event.preventDefault();
			table
				.columns(parseInt($(this).attr('id')))
				.search($(this).val())
				.draw();
		}
	});
	$(".dt-buttons").append("<div id='exportRaw'><i class='fa fa-download'/><a href='/DataEdit/downloadRaw?formId="+formId+"'> Download Raw Data</a></div>");
});