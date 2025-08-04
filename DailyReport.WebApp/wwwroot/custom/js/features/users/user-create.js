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
            e.preeventDefault();

            validation.validate().then(function (status) {
                if (status == 'Valid') {
                    alert('valid');
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