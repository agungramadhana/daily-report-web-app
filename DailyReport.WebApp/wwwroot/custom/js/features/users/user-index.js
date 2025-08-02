"use strict";
var user = function () {

    var initTable = function () {
        var table = $('#kt_datatable');

        // Dummy data
        var users = [
            { id: 1, employeeNumber: "123-456-789", fullName: "John Doe",  userName: "johndoe", roleName: "Super Admin", status: "Active" }
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
                    data: "employeeNumber",
                    title: "Employee Number",
                    className: "text-center"
                },
                {
                    data: "fullName",
                    title: "Full Name",
                    className: "text-center"
                },
                {
                    data: "userName",
                    title: "Username",
                    className: "text-center"
                },
                {
                    data: "roleName",
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
                            <a href='/User/Detail' class="btn btn-icon btn-xs btn-light-success mr-1" data-toggle="tooltip" data-placement="bottom" title="Detail">
                                <i class="fa fa-eye"></i>
                            </a>
                            <a href='/User/Edit' class="btn btn-icon btn-xs btn-light-primary mr-1" data-toggle="tooltip" data-placement="bottom" title="Edit">
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
    user.init();
});