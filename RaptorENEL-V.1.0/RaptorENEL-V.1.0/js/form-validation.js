$(document).ready(function() {


    if ($.isFunction($.fn.validate)) {

        $('#addEdit_user_validate').validate({
            focusInvalid: false,
            ignore: "",
            rules: {
                NAME: {
                    minlength: 5,
                    maxlength: 50,
                    required: true
                },
                COMPANY: {
                    minlength: 3,
                    maxlength: 50,
                    required: true
                },
                EMAIL: {
                    required: true,
                    email: true
                },
                PHONE: {
                    required: true,
                    phone: /^(\d{1})(\s{1})(\(\d{3}\))(\s{1})(\d{3})(\s{1})(\d{4})$/
                },
                GENDER: {
                    emptySelect: "-1",
                    required: true
                },
                ROLE_ID: {
                    emptySelect: "-1",
                    required: true
                }

            },

            invalidHandler: function (event, validator) {
                //display error alert on form submit    
                //alert("formulario con error")
            },

            errorPlacement: function (label, element) { // render error placement for each input type   
                console.log(label);
                $('<span class="error"></span>').insertAfter(element.parent()).append(label)
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
                parent.css("margin-bottom", "0px");
            },

            highlight: function (element) { // hightlight error inputs
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
            },

            unhighlight: function (element) { // revert the change done by hightlight

            },

            success: function (label, element) {
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-error').addClass('has-success');
                parent.children().remove('.error');
                parent.css("margin-bottom", "20px");
            } ,

            submitHandler: function (form) {
                $('#modal_loading').show();
                form.submit();
            }
        });

    }

    if ($.isFunction($.fn.bootstrapWizard)) {
    }

});
