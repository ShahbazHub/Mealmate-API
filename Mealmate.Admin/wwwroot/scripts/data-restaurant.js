"use strict";
var KTData = function () {

	var initTable1 = function () {
		var table = $('#kt_datatable_branch');

		// begin first table
		table.DataTable({
			responsive: true,

			// DOM Layout settings
			dom: 'f' + `<'row'<'col-sm-12'tr>>
			<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>`,

			lengthMenu: [5, 10, 25, 50],

			pageLength: 10,
			pagingType: 'full_numbers',

			language: {
				'lengthMenu': 'Display _MENU_',
			},

			// Order settings
			order: [[1, 'desc']],

			headerCallback: function (thead, data, start, end, display) {
				thead.getElementsByTagName('th')[0].innerHTML = `
                    <label class="checkbox checkbox-single">
                        <input type="checkbox" value="" class="group-checkable"/>
                        <span></span>
                    </label>`;
			},

			columnDefs: [
				{
					targets: 0,
					width: '30px',
					className: 'dt-left',
					orderable: false,
					render: function (data, type, full, meta) {
						return `
                        <label class="checkbox checkbox-single">
                            <input type="checkbox" value="" class="checkable"/>
                            <span></span>
                        </label>`;
					},
				}
			],
		});

		table.on('change', '.group-checkable', function () {
			var set = $(this).closest('table').find('td:first-child .checkable');
			var checked = $(this).is(':checked');

			$(set).each(function () {
				if (checked) {
					$(this).prop('checked', true);
					$(this).closest('tr').addClass('active');
				}
				else {
					$(this).prop('checked', false);
					$(this).closest('tr').removeClass('active');
				}
			});
		});

		table.on('change', 'tbody tr .checkbox', function () {
			$(this).parents('tr').toggleClass('active');
		});
	};
	var initTable2 = function () {
		var table = $('#kt_datatable_hall');

		// begin first table
		table.DataTable({
			responsive: true,

			// DOM Layout settings
			dom: 'f' + `<'row'<'col-sm-12'tr>>
			<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>`,

			lengthMenu: [5, 10, 25, 50],

			pageLength: 10,
			pagingType: 'full_numbers',

			language: {
				'lengthMenu': 'Display _MENU_',
			},

			// Order settings
			order: [[1, 'desc']],

			headerCallback: function (thead, data, start, end, display) {
				thead.getElementsByTagName('th')[0].innerHTML = `
                    <label class="checkbox checkbox-single">
                        <input type="checkbox" value="" class="group-checkable"/>
                        <span></span>
                    </label>`;
			},

			columnDefs: [
				{
					targets: 0,
					width: '30px',
					className: 'dt-left',
					orderable: false,
					render: function (data, type, full, meta) {
						return `
                        <label class="checkbox checkbox-single">
                            <input type="checkbox" value="" class="checkable"/>
                            <span></span>
                        </label>`;
					},
				}
			],
		});

		table.on('change', '.group-checkable', function () {
			var set = $(this).closest('table').find('td:first-child .checkable');
			var checked = $(this).is(':checked');

			$(set).each(function () {
				if (checked) {
					$(this).prop('checked', true);
					$(this).closest('tr').addClass('active');
				}
				else {
					$(this).prop('checked', false);
					$(this).closest('tr').removeClass('active');
				}
			});
		});

		table.on('change', 'tbody tr .checkbox', function () {
			$(this).parents('tr').toggleClass('active');
		});
	};
	var initTable3 = function () {
		var table = $('#kt_datatable_table');

		// begin first table
		table.DataTable({
			responsive: true,

			// DOM Layout settings
			dom: 'f' + `<'row'<'col-sm-12'tr>>
			<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>`,

			lengthMenu: [5, 10, 25, 50],

			pageLength: 10,
			pagingType: 'full_numbers',

			language: {
				'lengthMenu': 'Display _MENU_',
			},

			// Order settings
			order: [[1, 'desc']],

			headerCallback: function (thead, data, start, end, display) {
				thead.getElementsByTagName('th')[0].innerHTML = `
                    <label class="checkbox checkbox-single">
                        <input type="checkbox" value="" class="group-checkable"/>
                        <span></span>
                    </label>`;
			},

			columnDefs: [
				{
					targets: 0,
					width: '30px',
					className: 'dt-left',
					orderable: false,
					render: function (data, type, full, meta) {
						return `
                        <label class="checkbox checkbox-single">
                            <input type="checkbox" value="" class="checkable"/>
                            <span></span>
                        </label>`;
					},
				}
			],
		});

		table.on('change', '.group-checkable', function () {
			var set = $(this).closest('table').find('td:first-child .checkable');
			var checked = $(this).is(':checked');

			$(set).each(function () {
				if (checked) {
					$(this).prop('checked', true);
					$(this).closest('tr').addClass('active');
				}
				else {
					$(this).prop('checked', false);
					$(this).closest('tr').removeClass('active');
				}
			});
		});

		table.on('change', 'tbody tr .checkbox', function () {
			$(this).parents('tr').toggleClass('active');
		});
	};
	return {

		//main function to initiate the module
		init1: function () {
			initTable1();
		},
		init2: function () {
			initTable2();
		},
		init3: function () {
			initTable3();
		}
	};
}();

//jQuery(document).ready(function () {
//	KTData.init();
//});

function loadBranches() {
	$.ajax({
		url: '/Restaurant/Branch/Detail/',
		type: 'GET',
		success: function (response) {
			$("#branch-data").html(response);
			KTData.init1();
		},
		error: function (err) {
			console.log(err);
		}
	});
}

function loadHalls(restaurantId) {
	$.ajax({
		url: '/Setting/Hall/Detail/',
		type: 'GET',
		data: { restaurantId: restaurantId },
		success: function (response) {
			$("#hall-data").html(response);
			KTData.init2();
		},
		error: function (err) {
			console.log(err);
		}
	});
}

function loadTables(hallId) {
	$.ajax({
		url: '/Setting/Table/Detail/',
		type: 'GET',
		data: { hallId: hallId },
		success: function (response) {
			console.log(response);
			$("#table-data").html(response);
			KTData.init3();
		},
		error: function (err) {
			console.log(err);
		}
	});
}
