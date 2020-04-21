$(document).ready(function () {
    tinyMCEInit();
});

var numPosts = 2;
function showPosts(totalPosts) {
    for (var i = (numPosts); i < (numPosts + numPosts); i++) {
        var elem = document.getElementById(i);
        if (elem != null) {
            elem.style.display = "block";
        }
        
    }
    numPosts += numPosts;
    if (numPosts >= totalPosts - 1) {
        document.getElementById("showMore").style.display = "none";
    }
}
function tinyMCEInit() {
    tinymce.init(
        {
            selector: 'textarea',
            plugins: 'lists',
            menubar: 'file edit format',
            entity_encoding: "raw"
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

// This method actually toggles the like button. We will immediately update
// the button color and text so as to immediately respond to the user. After
// recieving the like count back from the controller, update the like count.
// If we have any issues with the db, revert the changes. After clicking, the
// button is set to disabled for 1 second. Function checks at the beginning
// if the button is disabled and returns if it is so as to prevent spamming.
// Theres probably a better way to do this but I hardly know javascript so...
function LikeThread(id) {
    var likeSelector = "likeIcon(" + id + ")";
    var likeIcon = document.getElementById(likeSelector);
    if (likeIcon.hasAttribute("disabled")) { return;}
    $.ajax({
        url: "/api/blog/" + id,
        type: "POST",
        data: { "id": id },
        success: function (data) {
            // Woohoo :)
            var likeSpanSelector = "likeSpan(" + id + ")";
            var likeSpan = document.getElementById(likeSpanSelector);
            if (data != 0) {
                likeSpan.innerHTML = data;
            }
            else {
                likeSpan.innerHTML = "";
            }
        },
        error: function (data) {
            var likeSelector = "likeIcon(" + id + ")";
            var likeIcon = document.getElementById(likeSelector);

            if (likeIcon.classList.contains("far")) {
                likeIcon.classList.remove("far");
                likeIcon.classList.add("fas");
                likeIcon.classList.remove("text-muted");
                likeIcon.classList.add("text-danger");
            }
            else {
                likeIcon.classList.remove("fas");
                likeIcon.classList.add("far");
                likeIcon.classList.remove("text-danger");
                likeIcon.classList.add("text-muted");
            }
        }
    });


    if (likeIcon.classList.contains("far")) {
        likeIcon.classList.remove("far");
        likeIcon.classList.add("fas");
        likeIcon.classList.remove("text-muted");
        likeIcon.classList.add("text-danger");
    }
    else {
        likeIcon.classList.remove("fas");
        likeIcon.classList.add("far");
        likeIcon.classList.remove("text-danger");
        likeIcon.classList.add("text-muted");
    }
    likeIcon.setAttribute("disabled", "true");
    setTimeout(function () { likeIcon.removeAttribute("disabled"); }, 1000);

    
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
                        toastr.success("Post Successfully Deleted!");
                        var cardSelector = "card(" + id + ")";
                        var boxSelector = "box(" + id + ")";
                        var commentsSelector = "comments(" + id + ")";
                        document.getElementById(cardSelector).style.display = "none";
                        document.getElementById(boxSelector).style.display = "none";
                        document.getElementById(commentsSelector).style.display = "none";
                    }
                    else {
                        toastr.error("Failed to delete post. Please contact your administrator to get it removed.");
                    }
                }
            })
        }
    })
};

function Modal(id) {
    var modalSelector = "modal(" + id + ")";
    var modal = document.getElementById(modalSelector);

    var imageSelector = "img(" + id + ")";
    var img = document.getElementById(imageSelector);

    var modalImageSelector = "modalImage(" + id + ")";
    var modalImg = document.getElementById(modalImageSelector);

    img.onclick = function () {
        modal.style.display = "block";
        modalImg.src = this.src;
    }

    var closeSelector = "close(" + id + ")";
    var close = document.getElementById(closeSelector);
    close.onclick = function () {
        modal.style.display = "none";
    }
};