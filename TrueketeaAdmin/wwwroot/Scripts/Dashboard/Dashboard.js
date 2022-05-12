$(document).ready(function () {


	
	init_DataTables();



	/*---------------------------------------------------------------------
				Tipo de contraseña
	-----------------------------------------------------------------------*/


	$('#password').keyup(function (e) {
		var strongRegex = new RegExp("^(?=.{8,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$", "g");
		var mediumRegex = new RegExp("^(?=.{7,})(((?=.*[A-Z])(?=.*[a-z]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$", "g");
		var enoughRegex = new RegExp("(?=.{6,}).*", "g");

		if (false == enoughRegex.test($(this).val())) {
			$('#passstrength').html('Very weak!');
			$('#passstrength').css("color", "#B71C1C")
		} else if (strongRegex.test($(this).val())) {
			$('#passstrength').html('Strong!');
			$('#passstrength').css("color", "#66FF00")
			
		} else if (mediumRegex.test($(this).val())) {
			$('#passstrength').html('Medium!');
			$('#passstrength').css("color", "#E67E22")
		} else {
			$('#passstrength').html('Weak!');
			$('#passstrength').css("color", "#FDD835")
		}

		if ($(this).val() === '') {
			$('#passstrength').html('');
        }

		return true;
	});


	$('.show-password').click(function() {

		var password = document.querySelector('.Password1');
		var slash = document.querySelector('.show-password');

		if (password.type === 'password') {
			password.type = "text";
			slash.classList.toggle("fa-eye-slash");

		}
		else {
			password.type = "password";
			slash.classList.remove('fa-eye-slash');
		}

	});
});

function init_DataTables() {

	console.log('run_datatables');

	if (typeof ($.fn.DataTable) === 'undefined') { return; }
	console.log('init_DataTables');

	var handleDataTableButtons = function () {
		if ($("#datatable-buttons").length) {
			$("#datatable-buttons").DataTable({
				dom: "Blfrtip",
				buttons: [
					{
						extend: "copy",
						className: "btn-sm"
					},
					{
						extend: "csv",
						className: "btn-sm"
					},
					{
						extend: "excel",
						className: "btn-sm"
					},
					{
						extend: "pdfHtml5",
						className: "btn-sm"
					},
					{
						extend: "print",
						className: "btn-sm"
					},
				],
				responsive: true
			});
		}
	};

	TableManageButtons = function () {
		"use strict";
		return {
			init: function () {
				handleDataTableButtons();
			}
		};
	}();

	$('#datatable').dataTable({
		language: {
			"emptyTable": "No hay información",
			"search": "Buscar:",
			"info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
			"infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
			"lengthMenu": "Mostrar _MENU_ Entradas",
			"loadingRecords": "Cargando...",
			"processing": "Procesando...",
			"paginate": {
				"first": "Primero",
				"last": "Ultimo",
				"next": "Siguiente",
				"previous": "Anterior"
			}
		}
	});

	$('#datatable-keytable').DataTable({
		keys: true
	});

	$('#datatable-responsive').DataTable();

	$('#datatable-scroller').DataTable({
		ajax: "js/datatables/json/scroller-demo.json",
		deferRender: true,
		scrollY: 380,
		scrollCollapse: true,
		scroller: true
	});

	$('#datatable-fixed-header').DataTable({
		fixedHeader: true
	});

	var $datatable = $('#datatable-checkbox');

	$datatable.dataTable({
		'order': [[1, 'asc']],
		'columnDefs': [
			{ orderable: false, targets: [0] }
		]
	});
	$datatable.on('draw.dt', function () {
		$('checkbox input').iCheck({
			checkboxClass: 'icheckbox_flat-green'
		});
	});

	TableManageButtons.init();

};
	


