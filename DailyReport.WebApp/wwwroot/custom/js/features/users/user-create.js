"use strict";
var user = function () {
    var buttonSubmit;

    var create = function () {
        var validation;

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validation = FormValidation.formValidation(
            KTUtil.getById('user-form'),
            {
                fields: {
                    employeeNumber: {
                        validators: {
                            notEmpty: {
                                message: 'Employee number is required'
                            }
                        }
                    },
                    fullName: {
                        validators: {
                            notEmpty: {
                                message: 'Full Name is required'
                            }
                        }
                    },
                    userName: {
                        validators: {
                            notEmpty: {
                                message: 'Username is required'
                            }
                        }
                    },
                    email: {
                        validators: {
                            notEmpty: {
                                message: 'Email is required'
                            },
                            emailAddress: {
                                message: 'The value is not a valid email address'
                            }
                        }
                    },
                    phoneNumber: {
                        validators: {
                            notEmpty: {
                                message: 'Phone number is required'
                            }
                        }
                    },
                    address: {
                        validators: {
                            notEmpty: {
                                message: 'Address is required'
                            }
                        }
                    },
                    password: {
                        validators: {
                            notEmpty: {
                                message: 'Password is required'
                            }
                        }
                    },
                    confirmPassword: {
                        validators: {
                            notEmpty: {
                                message: 'Confirm Password is required'
                            },
                            identical: {
                                compare: function () {
                                    return document.querySelector('[name="password"]').value;
                                },
                                message: 'The password and its confirm are not the same'
                            }
                        }
                    },
                    userRole: {
                        validators: {
                            notEmpty: {
                                message: 'Role is required'
                            }
                        }
                    },
                    userStatus: {
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

        $(buttonSubmit).on('click', function (e) {
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
                                EmployeeNumber: $('#employee-number').val(),
                                FullName: $('#full-name').val(),
                                Username: $('#user-name').val(),
                                Email: $('#email').val(),
                                PhoneNumber: $('#phone-number').val(),
                                Address: $('#address').val(),
                                Password: $('#password').val(),
                                RoleId: $('#user-role').val(),
                                IsActive: $('#user-status').val() === 'true'
                            };
                            $.ajax({
                                url: '/User/Create',
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
                                                window.location.href = "/User/Index"
                                            } else {
                                                window.location.href = "/User/Index"
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
            buttonSubmit = document.querySelector('#btn-submit-user');
            create();
        }
    };
}();

jQuery(document).ready(function () {
    user.init();
});