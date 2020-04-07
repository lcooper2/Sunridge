function LikeThread(id) {
    $.ajax({
        url: "/api/blog/" + id,
        type: "POST",
        data: { "id": id },
        success: function (data) {
            if (data == true) {
                var likeSelector = "likeIcon(" + id + ")";
                var likeIcon = document.getElementById(likeSelector);
                if (likeIcon != null) {
                    document.getElementById(likeSelector).classList.remove("fa-thumbs-down");
                    document.getElementById(likeSelector).classList.add("fa-thumbs-up");
                }
            }
            if (data == false) {
                var likeSelector = "likeIcon(" + id + ")";
                var likeIcon = document.getElementById(likeSelector);
                if (likeIcon != null) {
                    document.getElementById(likeSelector).classList.remove("fa-thumbs-up");
                    document.getElementById(likeSelector).classList.add("fa-thumbs-down");
                }
            }
        }
    });
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