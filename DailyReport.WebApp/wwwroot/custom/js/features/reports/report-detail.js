"use strict";
var report = function () {
    var detail = function () {
        // Format the date display
        var date = $('#report-date').val();
        $('#date').val(moment(date).format('YYYY-MM-DD'));

        var latitude = $('#latitude').val().replace(',', '.');
        $('#latitude').val(latitude)
        var longitude = $('#longitude').val().replace(',', '.');
        $('#longitude').val(longitude)

        // Initialize Summernote with the content from hidden input
        $('.summernote').summernote({
            minHeight: 100
        });

        // Set Summernote content and disable it
        var reportNote = $('#report-note').val();
        $('.summernote').summernote('code', reportNote);
        $('.summernote').summernote('disable');
    };

    var maps = function () {
        // Get latitude and longitude from input fields
        var latitude = $('#latitude').val().replace(',', '.');
        var longitude = $('#longitude').val().replace(',', '.');
        var areaName = $('#area-name').val();

        // Create map centered on the provided coordinates
        var leaflet = L.map('kt_leaflet_3', {
            center: [-8.85, 125.60], // fokus ke Dili
            zoom: 8 // Increased zoom level to focus better on the marker
        });

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        }).addTo(leaflet);

        var leafletIcon = L.divIcon({
            html: `<span class="svg-icon svg-icon-danger svg-icon-3x"><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1"><g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd"><rect x="0" y="24" width="24" height="0"/><path d="M5,10.5 C5,6 8,3 12.5,3 C17,3 20,6.75 20,10.5 C20,12.8325623 17.8236613,16.03566 13.470984,20.1092932 C12.9154018,20.6292577 12.0585054,20.6508331 11.4774555,20.1594925 C7.15915182,16.5078313 5,13.2880005 5,10.5 Z M12.5,12 C13.8807119,12 15,10.8807119 15,9.5 C15,8.11928813 13.8807119,7 12.5,7 C11.1192881,7 10,8.11928813 10,9.5 C10,10.8807119 11.1192881,12 12.5,12 Z" fill="#000000" fill-rule="nonzero"/></g></svg></span>`,
            bgPos: [10, 10],
            iconAnchor: [20, 37],
            popupAnchor: [0, -37],
            className: 'leaflet-marker'
        });

        // Add marker at the specified coordinates
        var marker1 = L.marker([latitude, longitude], { icon: leafletIcon }).addTo(leaflet);
        marker1.bindPopup(areaName, { closeButton: false });

        L.control.scale().addTo(leaflet);
    };

    return {
        init: function () {
            detail();
            maps();
        }
    };
}();

jQuery(document).ready(function () {
    report.init();
});