var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/board/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "title", "width": "40%" },
            { "data": "name", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <div class="text-center">
                        <a href="/Dashboard/board/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                            <i class="far fa-edit"></i> Edit
                        </a>
                         <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=Delete('/api/board/'+${data})>
                            <i class="far fa-trash-alt"></i> Delete
                        </a>
                   </div>`;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}

