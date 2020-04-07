var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/api/classifieds/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "itemName", "width": "15%" },
            { "data": "price", "width": "15%" },
            {
                "data": "listingDate", "width": "15%", "render": function (data) {
                    return moment(data).format('MMMM Do YYYY');
                }
            },
            { "data": "applicationUser.fullName", "width": "15%" },
            { "data": "classifiedCategory.description", "width": "15%" },
            { "data": "description", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href= "/Dashboard/OwnerDash/ClassifiedsList/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:75px; font-size:75%;">
                            <i class="far fa-edit"></i> Edit
                                </a>
                        <a href= "/Dashboard/OwnerDash/ClassifiedsList/images?id=${data}" class="btn btn-dark text-white" style="cursor:pointer; width:75px; font-size:65%;">
                            <i class="far fa-images"></i> Images 
                                </a>
                        <a href= "/Dashboard/OwnerDash/ClassifiedsList/details?id=${data}" class="btn btn-primary text-white" style="cursor:pointer; width:75px; font-size:70%;">
                            <i class="fas fa-history"></i> Details 
                                </a>
                        <a class="btn btn-danger text-white" style="cursor:pointer; width:75px; font-size:75%;" onclick=Delete('/api/classifieds/'+${data})>
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

