var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/userphotos/",
            "type": "GET",
            "data": { id: 1 },
            "datatype": "json"
        },
        "rowReorder": {
            "selector": 'td:nth-child(3)'
        },
        "responsive": true,
        "columns": [
            { "data": "title", "width": "30%" },
            { "data": "userPhotoCategory.title", "width": "20%" },
            { "data": "applicationUser.fullName", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <div class="text-center">
                        <a href="/Dashboard/AdminDash/PhotosManager/photos/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                            <i class="far fa-edit"></i> Edit
                        </a>
                         <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=Delete('/api/userphotos/'+${data})>
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

function Delete(url) // called delete function passes provided URL
{// opening function brace
    swal({// opening swal brace                             // using the sweet functionality for the alert window
        title: "Are you sure you want to Delete?",          // title of the alert window
        text: "You will not be able to restore the data!",  // message of the alert window
        icon: "warning",                                    // icon of the alert window
        buttons: true,                                      // adds a button to the alert window
        dangerMode: true                                    // adds the red color to the alert
    }).then((willDelete) => {//closing swal brace // opening willdelete brace
        if (willDelete) // checking to see if the user has selected delete
        {//opening outter if brace 
            $.ajax({// opening ajax brace
                type: 'DELETE', // sending delete type to the database
                url: url, // sends the api url 
                success: function (data) {// opening function brace // on finising it will send back notification if it worked
                    if (data.success) {//opening inner if brace  // checks to see if the deletion was successful
                        toastr.success(data.message); // uses toastr to pop up success message
                        dataTable.ajax.reload(); // reloads the page
                    }// closing inner if brace
                    else {// opening else brace
                        toastr.error(data.message); // uses toastr to pop up error message
                    }// closing else brace
                } // closing function brace
            });// closing ajax brace
        }// closing outter if brace
    });// close willdelete brace
}// closing function brace