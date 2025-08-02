"use strict";
var role = function () {

    var initTable = function () {
        var table = $('#kt_datatable');

        // Dummy data
        var roles = [
            { id: 1, name: "Super Admin", status: "Active" },
            { id: 2, name: "Administrator", status: "Active" },
            { id: 3, name: "Manager", status: "Active" },
            { id: 4, name: "Editor", status: "Active" },
            { id: 5, name: "Author", status: "Active" },
            { id: 6, name: "Contributor", status: "Active" },
            { id: 7, name: "Subscriber", status: "Active" },
            { id: 8, name: "Guest", status: "Inactive" },
            { id: 9, name: "Moderator", status: "Active" },
            { id: 10, name: "Analyst", status: "Inactive" }
        ];

        table.DataTable({
            responsive: true,
            pagingType: 'full_numbers',
            data: roles,
            columns: [
                {
                    data: "id",
                    title: "No",
                    className: "text-center"
                },
                {
                    data: "name",
                    title: "Role Name",
                    className: "text-center"
                },
                {
                    data: "status",
                    title: "Status",
                    className: "text-center",
                    render: function (data, type, row) {
                        var badgeClass = data === "Active" ? "badge-success" : "badge-danger";
                        return `<span class="badge ${badgeClass}">${data}</span>`;
                    }
                },
                {
                    title: "Actions",
                    className: "text-center",
                    orderable: false,
                    width: '125px',
                    defaultContent: '', // Add this line to prevent the parameter error
                    render: function (data, type, row) {
                        return `
                            <a href='/Role/Detail' class="btn btn-icon btn-xs btn-light-success mr-1" data-toggle="tooltip" data-placement="bottom" title="Detail">
                                <i class="fa fa-eye"></i>
                            </a>
                            <a href='/Role/Edit' class="btn btn-icon btn-xs btn-light-primary mr-1" data-toggle="tooltip" data-placement="bottom" title="Edit">
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
    role.init();
});