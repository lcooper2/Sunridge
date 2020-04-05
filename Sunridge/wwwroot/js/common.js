var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/api/common/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "assetName", "width": "30%" },
            { "data": "purchasePrice", "width": "10%" },
            { "data": "description", "width": "30%" },
            { "data": "status", "width": "30%" },
            { "data": "date", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href= "/Dashboard/AdminDash/CommonArea/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                            <i class="far fa edit"></i> Edit   
                                </a>
                        <a href= "/Dashboard/AdminDash/CommonArea/Maintenance/Maintenance" class="btn btn-primary text-white" style="cursor:pointer; width:100px;">
                            <i class="fas fa-tools"></i> Maintenance
                                </a>
                        <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=Delete('/api/key/'+${data})>
                            <i class="far fa-trash-alt"></i> Delete
                        </a>
                            </div >`;
                }, "width": "50%"
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