"use strict";
var report = function () {

    var initTable = function () {
        var table = $('#kt_datatable');

        table.DataTable({
            responsive: true,
            pagingType: 'full_numbers',
            processing: true,
            serverSide: true,
            ajax: {
                url: '/Report/DatatableReport',
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
                    data: "fullName",
                    name: "fullName",
                    title: "Full Name",
                    className: "text-center"
                },
                {
                    data: "areaName",
                    name: "areaName",
                    title: "Area Name",
                    className: "text-center"
                },
                {
                    data: "latitude",
                    name: "latitude",
                    title: "Latitude",
                    className: "text-center"
                },
                {
                    data: "longitude",
                    name: "longitude",
                    title: "Longitude",
                    className: "text-center"
                },
                {
                    data: "date",
                    name: "date",
                    title: "Date",
                    className: "text-center",
                    render: function (data) {
                        return moment(data).format('YYYY-MM-DD')
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
                        return `
                            <a href='/Report/Detail/${id}' class="btn btn-icon btn-xs btn-light-success mr-1" title="Detail">
                                <i class="fa fa-eye"></i>
                            </a>
                            <a href='/Report/Edit/${id}' class="btn btn-icon btn-xs btn-light-primary mr-1" title="Edit">
                                <i class="fa fa-edit"></i>
                            </a>
                            <button data-id='${id}' class="btn btn-icon btn-xs btn-light-danger btn-delete-report" title="Delete"s>
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

        table.on('click', '.btn-delete-report', function (e) {
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
                        url: '/Report/Delete/' + id,
                        type: 'POST',
                        success: function (response) {
                            if (response.isSuccess) {
                                Swal.fire({
                                    position: "center",
                                    icon: "success",
                                    title: "User has been deleted",
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
    report.init();
});