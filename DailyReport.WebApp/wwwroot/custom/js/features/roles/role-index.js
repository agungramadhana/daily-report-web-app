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
                        search: { value: d.search.value },
                        order: {
                            column: orderColumnName,
                            dir: orderDirection
                        }
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
                        return `
                            <a href='/Role/Detail/${id}' class="btn btn-icon btn-xs btn-light-success mr-1" title="Detail">
                                <i class="fa fa-eye"></i>
                            </a>
                            <a href='/Role/Edit/${id}' class="btn btn-icon btn-xs btn-light-primary mr-1" title="Edit">
                                <i class="fa fa-edit"></i>
                            </a>
                            <button data-id='${id}' class="btn btn-icon btn-xs btn-light-danger btn-delete-role" title="Delete">
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