"use strict";
var role = function () {

    var initTable = function () {
        var table = $('#kt_datatable');

        table.DataTable({
            responsive: true,
            pagingType: 'full_numbers',
            processing: true,
            serverSide: true,
            ajax: {
                url: '/Role/DatatableRole',
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
                    data: "name",
                    name: "name",
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
                            <a href='/Role/Detail/${id}' class="btn btn-icon btn-xs btn-light-success mr-1" title="Detail">
                                <i class="fa fa-eye"></i>
                            </a>
                            <a href='/Role/Edit/${id}' class="btn btn-icon btn-xs btn-light-primary mr-1" title="Edit">
                                <i class="fa fa-edit"></i>
                            </a>
                            <button data-id='${id}' class="btn btn-icon btn-xs btn-light-danger btn-delete-role" title="Delete" ${isActive}>
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

        table.on('click', '.btn-delete-role', function (e) {
            e.preventDefault();
            e.stopPropagation();

            var id = $(this).data('id');
            deleteRole(id);
        });

        function deleteRole(id) {
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
                        url: '/Role/Delete/' + id,
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
                                .then(function (result){
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
    role.init();
});