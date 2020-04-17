var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/user/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "fullName", "width": "auto" },
            { "data": "userName", "width": "auto" },
            {
                "data": {
                    id: "id", lockoutEnd: "lockoutEnd"
                },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        // user is currently locked out

                        return ` <div class="text-center">
                        <a class="btn btn-danger text-white"onclick = "LockUnlock('${data.id}')"  style="cursor:pointer; width:auto;">
                            <i class="fas fa-lock-open" ></i> Unlock
                        </a>
                        <a href= "/Dashboard/AdminDash/User/Profile?id=${data.id}" class="btn btn-primary text-white" style="cursor:pointer; width:auto;">
                            <i class="fas fa-history"></i>Info   
                        </a></div>`;
                    }
                    else {
                        return ` <div class="text-center">
                        <a class="btn btn-success text-white"onclick = "LockUnlock('${data.id}')"  style="cursor:pointer; width:auto;">
                            <i class="fas fa-lock" ></i> Lock
                        </a>
                        <a href= "/Dashboard/AdminDash/User/Profile?id=${data.id}" class="btn btn-primary text-white" style="cursor:pointer; width:auto;">
                            <i class="fas fa-history"></i>Info   
                        </a></div>`;
                    }
                }, "width": "auto"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}

function LockUnlock(id) // called LockUnlock function passes provided URL
{// opening function brace 
    $.ajax({// opening ajax brace
        type: 'POST', // sending delete type to the database
        url: '/api/User', // sends the hard coded api 
        data: JSON.stringify(id),
        contentType: "application/json",
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
}// closing function brace