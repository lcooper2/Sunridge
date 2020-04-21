var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/api/news/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "year", "width": "15%" },
            { "data": "header", "width": "15%" },
            {
                "data": "newsItemId",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href= "/Dashboard/AdminDash/News/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:75px; font-size:75%;">
                            <i class="far fa-edit"></i> Edit
                                </a>
                        <a href= "/Dashboard/AdminDash/News/details?id=${data}" class="btn btn-primary text-white" style="cursor:pointer; width:75px; font-size:70%;">
                            <i class="fas fa-history"></i> Details 
                                </a>
                        <a class="btn btn-danger text-white" style="cursor:pointer; width:75px; font-size:75%;" onclick=Delete('/api/news/'+${data})>
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

