function LikeThread(id) {
    $.ajax({
        url: "/api/blog/",
        type: "POST",
        data: { "id": id }
    });
};

function AddComment(threadId) {
    var selector = "text(" + threadId + ")";
    var text = $(document.getElementById(selector)).val().trim();
    $.ajax({
        url: "/api/blog/OnPostComment",
        type: "POST",
        data: { "comment": text, "threadId": threadId }
    });
};

function getText(textAreaId) {
    var selector = "#text(" + id + ")";
    return $(selector).val().trim();
}

function showCommentBox(id) {
    var selector = "box(" + id + ")";
    var domElem = document.getElementById(selector);
    domElem.innerHTML =
        "<h5>Add a comment below...</h5>" +
        "<div class='card-body d-flex flex-row'>" +
        "<textarea class='form-control z-depth-1' id='text(" + id +")' style='min-width: 100 %' placeholder='Your comment here'></textarea >" +
        "</div>" +
        "<div class='row'>" +
        "<div class='col-4 p-1 offset-1'>" +
        "<button class='btn btn-sm btn-info form-control' onclick='AddComment(" + id + ")'><i class='fas fa-sticky-note'></i> Post</button>" +
        "</div>" +
        "<div class='col-4 p-1 offset-1'>" +
        "<button class='btn btn-sm btn-danger form-control' onclick='hideCommentBox(" + id + ")'><i class='far fa-trash-alt'></i> Cancel</button>" +
        "</div>" +
        "</div>";
};

function hideCommentBox(id) {
    var selector = "box(" + id + ")";
    var domElem = document.getElementById(selector);
    domElem.innerHTML = "";
}