
/*---------------------------------------------------------------------
			Validacion
-----------------------------------------------------------------------*/

$("#LoginFormValidate").validate({

	errorContainer: "#err",
	errorLabelContainer: "#err",
	wrapper: "span",

	rules: {

		pass: {
			required: true,
			minlength: 8
		},
		Email: {
			required: true,
			email: true
		}
		
	},
	messages: {
		
		Email: {
			required: "El campo Email es obligatorio",
			email: "El formato del email debe ser abc@domain.tld"
		},
		pass: {
			required: "El campo Contraseña es obligatorio",
			minlength: "El Contraseña debería tener al menos 8 caracteres"
		},

		

	},

});