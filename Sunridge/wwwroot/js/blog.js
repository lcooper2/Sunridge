$(document).ready(function () {
    tinyMCEInit();
});

function tinyMCEInit() {
    tinymce.init(
        {
            selector: 'textarea',
            plugins: 'lists',
            menubar: 'file edit format'
        }
    );
}

function HasUserLikedPost(commentId) {
    $.ajax({
        url: "/api/blog/",
        type: "GET",
        data: { "commentId": commentId },
        success: function (data) {
            return data;
        }
    });
}

function LikeThread(id) {
    $.ajax({
        url: "/api/blog/" + id,
        type: "POST",
        data: { "id": id },
        error: function (data) {
            alert(data);
        }
    });
    var likeSelector = "likeIcon(" + id + ")";
    var likeIcon = document.getElementById(likeSelector);

    if (likeIcon.classList.contains("far"))
    {
            document.getElementById(likeSelector).classList.remove("far");
            document.getElementById(likeSelector).classList.add("fas");
            document.getElementById(likeSelector).classList.remove("text-muted");
            document.getElementById(likeSelector).classList.add("text-danger");
    }
    else
    {
            document.getElementById(likeSelector).classList.remove("fas");
            document.getElementById(likeSelector).classList.add("far");
            document.getElementById(likeSelector).classList.remove("text-danger");
            document.getElementById(likeSelector).classList.add("text-muted");
    }
    likeIcon.parentElement.style.visibility = "hidden";
    setTimeout(function () { likeIcon.parentElement.style.visibility = "visible"; }, 750);
};

function toggleReplyBox(commentId) {
    var selector = "reply(" + commentId + ")";
    var domElem = document.getElementById(selector);
    if (domElem.style.display == "none" || domElem.style.display == "") {
        domElem.style.display = "inline";
    }
    else {
        domElem.style.display = "none";
    }
}

function AddComment(threadId) {
    var selector = "text(" + threadId + ")";
    var text = $(document.getElementById(selector)).val().trim();
    $.ajax({
        url: "/api/blog/",
        type: "POST",
        data: { "comment": text, "threadId": threadId }
    });
};

function showComments(threadId) {
    
    var commentSelector = "comments(" + threadId + ")";
    var buttonSelector = "showCommentsButton(" + threadId + ")";
    var iconSelector = "showCommentsIcon(" + threadId + ")";

    if (document.getElementById(commentSelector).style.display == "none" || document.getElementById(commentSelector).style.display == "") {
        document.getElementById(commentSelector).style.display = "block";

        document.getElementById(buttonSelector).firstChild.textContent = "Hide Comments ";

        document.getElementById(iconSelector).classList.remove("fa-arrow-down");
        document.getElementById(iconSelector).classList.add("fa-arrow-up");
    }
    else {
        document.getElementById(commentSelector).style.display = "none";

        document.getElementById(buttonSelector).firstChild.textContent = "View Comments ";

        document.getElementById(iconSelector).classList.remove("fa-arrow-up");
        document.getElementById(iconSelector).classList.add("fa-arrow-down");
    }
};

function toggleCommentBox(id) {
    var selector = "box(" + id + ")";
    var domElem = document.getElementById(selector);
    if (domElem.style.display == "none" || domElem.style.display == "") {
        domElem.style.display = "block"
    }
    else {
        domElem.style.display = "none";
    }
        };

function hideCommentBox(id) {
    var selector = "box(" + id + ")";
    var domElem = document.getElementById(selector);
    domElem.innerHTML = "";
};

function Delete(id) {
    swal({
        title: "Are you sure you want to delete this post?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: "/api/blog/" + id,
                data: { "threadId": id },
                success: function (data) {
                    if (data == true) {
                        toastr.success(data.message);
                        var cardSelector = "card(" + id + ")"
                        document.getElementById(cardSelector).style.display = "none";
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
};