"use strict";
var role = function () {

    var create = function () {
        var validation;

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validation = FormValidation.formValidation(
            KTUtil.getById('role-form'),
            {
                fields: {
                    rolename: {
                        validators: {
                            notEmpty: {
                                message: 'Role name is required'
                            }
                        }
                    },
                    rolestatus: {
                        validators: {
                            notEmpty: {
                                message: 'Status is required'
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    submitButton: new FormValidation.plugins.SubmitButton(),
                    //defaultSubmit: new FormValidation.plugins.DefaultSubmit(), // Uncomment this line to enable normal button submit after form validation
                    bootstrap: new FormValidation.plugins.Bootstrap()
                }
            }
        );

        $('#btn-create-user').on('click', function (e) {
            e.preventDefault();

            validation.validate().then(function (status) {
                if (status == 'Valid') {

                    Swal.fire({
                        html: `Are you sure want to <strong>save</strong>?`,
                        icon: "info",
                        buttonsStyling: false,
                        showCancelButton: true,
                        cancelButtonText: 'Cancel',
                        confirmButtonText: "Submit",
                        customClass: {
                            confirmButton: "btn btn-primary mr-2",
                            cancelButton: 'btn btn-danger'
                        }
                    }).then(function (confirm) {

                        if (confirm.isConfirmed) {

                            var formData = {
                                RoleName: $('#role-name').val(),
                                IsActive: $('#role-status').val() === 'true'
                            };
                            $.ajax({
                                url: '/Role/Create',
                                type: "POST",
                                dataType: "json",
                                contentType: "application/json; charset=utf-8",
                                data: JSON.stringify(formData),
                                success: function (response) {
                                    if (response.isSuccess) {
                                        Swal.fire({
                                            position: "center",
                                            icon: "success",
                                            title: "Success Add Role",
                                            showConfirmButton: false,
                                            timer: 3000,
                                        }).then(function (result) {
                                            if (result.dismiss === "timer") {
                                                window.location.href = "/Role/Index"
                                            } else {
                                                window.location.href = "/Role/Index"
                                            }
                                        });
                                    }
                                    else {
                                        Swal.fire({
                                            position: "center",
                                            icon: "error",
                                            title: response.message,
                                            showConfirmButton: false,
                                            timer: 3000
                                        });
                                    }
                                },
                                error: function (response) {

                                    var errorMessages = [];

                                    var exceptionMessage = response.responseJSON.exceptionMessage;

                                    for (var key in exceptionMessage) {
                                        if (exceptionMessage.hasOwnProperty(key)) {
                                            var errors = exceptionMessage[key].errorMessage;
                                            errorMessages = errorMessages.concat(errors);
                                        }
                                    }

                                    var errorMessageText = errorMessages.join("\n");

                                    Swal.fire({
                                        position: "center",
                                        icon: "error",
                                        title: errorMessageText,
                                        showConfirmButton: false,
                                        timer: 3000
                                    });
                                }
                            });

                        }
                    });
                }
            });
        });

    };

    return {
        init: function () {
            create();
        }
    };
}();

jQuery(document).ready(function () {
    role.init();
});