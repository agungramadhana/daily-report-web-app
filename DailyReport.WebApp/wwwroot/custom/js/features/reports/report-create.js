"use strict";
var report = function () {
    var buttonMapPreview;
    var buttonSubmit;
    var currentMap = null; // Store the map instance here

    var create = function () {
        var validation;

        validation = FormValidation.formValidation(
            KTUtil.getById('report-form'),
            {
                fields: {
                    areaName: {
                        validators: {
                            notEmpty: {
                                message: 'Area name is required'
                            }
                        }
                    },
                    date: {
                        validators: {
                            notEmpty: {
                                message: 'Date is required'
                            }
                        }
                    },
                    latitude: {
                        validators: {
                            notEmpty: {
                                message: 'Latitude is required'
                            }
                        }
                    },
                    longitude: {
                        validators: {
                            notEmpty: {
                                message: 'Longitude is required'
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
                                AreaName: $('#area-name').val(),
                                Date: moment().format($('#date').val()),
                                Latitude: $('#latitude').val(),
                                Longitude: $('#longitude').val(),
                                ReportNote: $('#report-note').val()
                            };
                            $.ajax({
                                url: '/Report/Create',
                                type: "POST",
                                dataType: "json",
                                contentType: "application/json; charset=utf-8",
                                data: JSON.stringify(formData),
                                success: function (response) {
                                    if (response.isSuccess) {
                                        Swal.fire({
                                            position: "center",
                                            icon: "success",
                                            title: "Success Add Report",
                                            showConfirmButton: false,
                                            timer: 3000,
                                        }).then(function (result) {
                                            if (result.dismiss === "timer") {
                                                window.location.href = "/Report/Index"
                                            } else {
                                                window.location.href = "/Report/Index"
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

        $('.summernote').summernote({
            height: 100
        });

        if (buttonMapPreview) {
            $(buttonMapPreview).off('click').on('click', function (e) {
                e.preventDefault();
                var lat = $('#latitude').val();
                var long = $('#longitude').val();
                var areaName = $('#area-name').val();

                lat = parseFloat(lat);
                long = parseFloat(long);

                if (isNaN(lat)) lat = null;
                if (isNaN(long)) long = null;

                maps(lat, long, areaName);
            });
        }
    };

    var maps = function (lat, long, areaName) {
        // Clear any existing map using the stored reference
        if (currentMap !== null) {
            currentMap.remove();
        }

        currentMap = L.map('kt_leaflet_3', {
            center: [-8.85, 125.60], // focus on Dili
            zoom: 8
        });

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        }).addTo(currentMap);

        var leafletIcon = L.divIcon({
            html: `<span class="svg-icon svg-icon-danger svg-icon-3x"><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1"><g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd"><rect x="0" y="24" width="24" height="0"/><path d="M5,10.5 C5,6 8,3 12.5,3 C17,3 20,6.75 20,10.5 C20,12.8325623 17.8236613,16.03566 13.470984,20.1092932 C12.9154018,20.6292577 12.0585054,20.6508331 11.4774555,20.1594925 C7.15915182,16.5078313 5,13.2880005 5,10.5 Z M12.5,12 C13.8807119,12 15,10.8807119 15,9.5 C15,8.11928813 13.8807119,7 12.5,7 C11.1192881,7 10,8.11928813 10,9.5 C10,10.8807119 11.1192881,12 12.5,12 Z" fill="#000000" fill-rule="nonzero"/></g></svg></span>`,
            bgPos: [10, 10],
            iconAnchor: [20, 37],
            popupAnchor: [0, -37],
            className: 'leaflet-marker'
        });

        if (lat !== null && long !== null && areaName !== null) {
            var marker1 = L.marker([lat, long], { icon: leafletIcon }).addTo(currentMap);
            marker1.bindPopup(areaName, { closeButton: false });

            // Center the map on the marker with a higher zoom level
            currentMap.setView([lat, long], 12);
        }

        L.control.scale().addTo(currentMap);
    };

    return {
        init: function () {
            buttonMapPreview = document.getElementById('btn-map-preview');
            buttonSubmit = document.getElementById('btn-submit-report')
            create();
            maps(null, null, null);
        }
    };
}();

jQuery(document).ready(function () {
    report.init();
});