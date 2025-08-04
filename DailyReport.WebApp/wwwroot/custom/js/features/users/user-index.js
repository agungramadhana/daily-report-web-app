"use strict";
var user = function () {

    var initTable = function () {
        var table = $('#kt_datatable');

        table.DataTable({
            responsive: true,
            pagingType: 'full_numbers',
            processing: true,
            serverSide: true,
            ajax: {
                url: '/User/DatatableUser',
                type: 'POST',
                dataType: "json",
                data: function (d) {
                    const orderColumnIndex = d.order[0]?.column;
                    const orderDirection = d.order[0]?.dir;
                    const orderColumnName = d.columns[orderColumnIndex]?.data;

                    return {
                        draw: d.draw,
                        start: d.start,
                        length: d.length,
                        keyword: d.search.value,
                        orderCol: orderColumnName,
                        orderType: orderDirection
                    };
                },
                dataSrc: function (json) {
                    if (json && json.isSuccess && json.payload) {
                        // These must be set at the root level for DataTables
                        json.recordsTotal = json.payload.recordsTotal;
                        json.recordsFiltered = json.payload.recordsFiltered;
                        return json.payload.data;
                    }
                    console.error("Invalid response structure", json);
                    return [];
                }
            },
            columns: [
                {
                    data: null,
                    name: "no",
                    title: "No",
                    className: 'text-center',
                    orderable: false,
                    render: function (data, type, row, meta) {
                        return meta.settings._iDisplayStart + meta.row + 1;
                    }
                },
                {
                    data: "employeeNumber",
                    name: "employeeNumber",
                    title: "Employee Number",
                    className: "text-center"
                },
                {
                    data: "fullName",
                    name: "fullName",
                    title: "Full Name",
                    className: "text-center"
                },
                {
                    data: "userName",
                    name: "userName",
                    title: "Username",
                    className: "text-center"
                },
                {
                    data: "roleName",
                    name: "roleName",
                    title: "Role Name",
                    className: "text-center"
                },
                {
                    data: "isActive",
                    name: "isActive",
                    title: "Status",
                    className: "text-center",
                    render: function (data) {
                        const badgeClass = data ? "badge-success" : "badge-danger";
                        const statusName = data ? "Active" : "Inactive";
                        return `<span class="badge ${badgeClass}">${statusName}</span>`;
                    }
                },
                {
                    data: null,
                    name: "actions",
                    title: "Actions",
                    className: "text-center",
                    orderable: false,
                    width: '125px',
                    render: function (data, type, row) {
                        const id = row.id || '';
                        const isActive = row.isActive == "true" || row.isActive == true ? "disabled" : "";
                        return `
                            <a href='/User/Detail/${id}' class="btn btn-icon btn-xs btn-light-success mr-1" title="Detail">
                                <i class="fa fa-eye"></i>
                            </a>
                            <a href='/User/Edit/${id}' class="btn btn-icon btn-xs btn-light-primary mr-1" title="Edit">
                                <i class="fa fa-edit"></i>
                            </a>
                            <button data-id='${id}' class="btn btn-icon btn-xs btn-light-danger btn-delete-user" title="Delete" ${isActive}>
                                <i class="fa fa-trash"></i>
                            </button>
                        `;
                    }
                }
            ],
            error: function (xhr, error, thrown) {
                console.error('DataTables error:', error, thrown);
            }
        });

        table.on('click', '.btn-delete-user', function (e) {
            e.preventDefault();
            e.stopPropagation();

            var id = $(this).data('id');
            deleteUser(id);
        });

        function deleteUser(id) {
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/User/Delete/' + id,
                        type: 'POST',
                        success: function (response) {
                            if (response.isSuccess) {
                                Swal.fire({
                                    position: "center",
                                    icon: "success",
                                    title: "Role has been deleted",
                                    showConfirmButton: false,
                                    timer: 3000,
                                })
                                    .then(function (result) {
                                        if (result.dismiss === "timer") {
                                            $('#kt_datatable').DataTable().ajax.reload();
                                        } else {
                                            $('#kt_datatable').DataTable().ajax.reload();
                                        }
                                    });
                            } else {
                                Swal.fire(
                                    'Error!',
                                    response.message || 'Failed to delete role',
                                    'error'
                                );
                            }
                        },
                        error: function (xhr) {
                            Swal.fire(
                                'Error!',
                                xhr.responseJSON?.message || 'Something went wrong',
                                'error'
                            );
                        }
                    });
                }
            });
        }
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