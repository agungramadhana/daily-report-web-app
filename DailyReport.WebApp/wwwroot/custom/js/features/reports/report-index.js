"use strict";
var report = function () {

    var initTable = function () {
        var table = $('#kt_datatable');

        // Dummy data
        var users = [
            { id: 1, fullName: "John Doe", area: "Dili", latitude: "-8.5569", longitude: "125.5603", date: "2025-08-01 00:00:00" },
            { id: 2, fullName: "John Doe", area: "Anaro", latitude: "-8.9965", longitude: "125.5083", date: "2025-08-01 00:00:00" }
        ];

        table.DataTable({
            responsive: true,
            pagingType: 'full_numbers',
            data: users,
            columns: [
                {
                    data: "id",
                    title: "No",
                    className: "text-center"
                },
                {
                    data: "fullName",
                    title: "Full Name",
                    className: "text-center"
                },
                {
                    data: "area",
                    title: "Area",
                    className: "text-center"
                },
                {
                    data: "latitude",
                    title: "Latitude",
                    className: "text-center"
                },
                {
                    data: "longitude",
                    title: "Longitude",
                    className: "text-center"
                },
                {
                    data: "date",
                    title: "Date",
                    className: "text-center"
                },
                {
                    title: "Actions",
                    className: "text-center",
                    orderable: false,
                    width: '125px',
                    defaultContent: '', // Add this line to prevent the parameter error
                    render: function (data, type, row) {
                        return `
                            <a href='/Report/Detail' class="btn btn-icon btn-xs btn-light-success mr-1" data-toggle="tooltip" data-placement="bottom" title="Detail">
                                <i class="fa fa-eye"></i>
                            </a>
                            <a href='/Report/Edit' class="btn btn-icon btn-xs btn-light-primary mr-1" data-toggle="tooltip" data-placement="bottom" title="Edit">
                                <i class="fa fa-edit"></i>
                            </a>
                            <button class="btn btn-icon btn-xs btn-light-danger" data-toggle="tooltip" data-placement="bottom" title="Delete">
                                <i class="fa fa-trash"></i>
                            </button>
                        `;
                    }
                }
            ]
        });
    };

    return {
        init: function () {
            initTable();
        }
    };
}();

jQuery(document).ready(function () {
    report.init();
});