var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/api/events/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "subject", "width": "15%" },
            { "data": "description", "width": "15%" },
            { "data": "location", "width": "15%" },
            {
                "data": "start", "width": "15%", "render": function (data) {
                    return moment(data).format('MMMM Do YYYY HH:MM');
                }
            },
            {
                "data": "end", "width": "15%", "render": function (data) {
                    return moment(data).format('MMMM Do YYYY HH:MM');
                }
            },
            {
                "data": "isFullDay", "width": "15%", "render": function (data) {
                    if (data == false) {
                        return "No";
                    } else {
                        return "Yes";
                    }
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href= "/Dashboard/AdminDash/Events/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:75px; font-size:75%;">
                            <i class="far fa-edit"></i> Edit
                                </a>
                        <a class="btn btn-danger text-white" style="cursor:pointer; width:75px; font-size:75%;" onclick=Delete('/api/events/'+${data})>
                            <i class="far fa-trash-alt"></i> Delete
                        </a>
                            </div >`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

