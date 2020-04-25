//$(document).ready(function () {
//    //tinyMCEInit();
//});

//var numPosts = 10;
//function showPosts(totalPosts) {
//    for (var i = (numPosts); i < (numPosts + numPosts); i++) {
//        var elem = document.getElementById(i);
//        if (elem != null) {
//            elem.style.display = "block";
//        }
        
//    }
//    numPosts += numPosts;
//    if (numPosts >= totalPosts - 1) {
//        document.getElementById("showMore").style.display = "none";
//    }
//}
//function tinyMCEInit(selectorText) {
//    tinymce.init(
//        {
//            selector: selectorText,
//            plugins: 'lists',
//            menubar: 'file edit format',
//            entity_encoding: "raw"
//        }
//    );
//}

//function HasUserLikedPost(commentId) {
//    $.ajax({
//        url: "/api/blog/",
//        type: "GET",
//        data: { "commentId": commentId },
//        success: function (data) {
//            return data;
//        }
//    });
//}

//// This method actually toggles the like button. We will immediately update
//// the button color and text so as to immediately respond to the user. After
//// recieving the like count back from the controller, update the like count.
//// If we have any issues with the db, revert the changes. After clicking, the
//// button is set to disabled for 1 second. Function checks at the beginning
//// if the button is disabled and returns if it is so as to prevent spamming.
//// Theres probably a better way to do this but I hardly know javascript so...
//function LikeThread(id) {
//    var likeSelector = "likeIcon(" + id + ")";
//    var likeIcon = document.getElementById(likeSelector);
//    if (likeIcon.hasAttribute("disabled")) { return;}
//    $.ajax({
//        url: "/api/blog/" + id,
//        type: "POST",
//        data: { "id": id },
//        success: function (data) {
//            var likeSpanSelector = "likeSpan(" + id + ")";
//            var likeSpan = document.getElementById(likeSpanSelector);
//            if (data != 0) {
//                likeSpan.innerHTML = data;
//            }
//            else {
//                likeSpan.innerHTML = "";
//            }
//        },
//        error: function (data) {
//            var likeSelector = "likeIcon(" + id + ")";
//            var likeIcon = document.getElementById(likeSelector);

//            if (likeIcon.classList.contains("far")) {
//                likeIcon.classList.remove("far");
//                likeIcon.classList.add("fas");
//                likeIcon.classList.remove("text-muted");
//                likeIcon.classList.add("text-danger");
//            }
//            else {
//                likeIcon.classList.remove("fas");
//                likeIcon.classList.add("far");
//                likeIcon.classList.remove("text-danger");
//                likeIcon.classList.add("text-muted");
//            }
//        }
//    });


//    if (likeIcon.classList.contains("far")) {
//        likeIcon.classList.remove("far");
//        likeIcon.classList.add("fas");
//        likeIcon.classList.remove("text-muted");
//        likeIcon.classList.add("text-danger");
//    }
//    else {
//        likeIcon.classList.remove("fas");
//        likeIcon.classList.add("far");
//        likeIcon.classList.remove("text-danger");
//        likeIcon.classList.add("text-muted");
//    }
//    likeIcon.setAttribute("disabled", "true");
//    setTimeout(function () { likeIcon.removeAttribute("disabled"); }, 1000);
//};

//function LikeComment(id) {
//    var likeSelector = "likeCommentIcon(" + id + ")";
//    var likeIcon = document.getElementById(likeSelector);
//    if (likeIcon.hasAttribute("disabled")) { return; }
//    $.ajax({
//        url: "/api/blog/" + id,
//        type: "POST",
//        data: { "id": id },
//        success: function (data) {
//            var likeSpanSelector = "likeCommentSpan(" + id + ")";
//            var likeSpan = document.getElementById(likeSpanSelector);
//            if (data != 0) {
//                likeSpan.innerHTML = data;
//            }
//            else {
//                likeSpan.innerHTML = "";
//            }
//        },
//        error: function (data) {
//            var likeSelector = "likeCommentIcon(" + id + ")";
//            var likeIcon = document.getElementById(likeSelector);

//            if (likeIcon.classList.contains("far")) {
//                likeIcon.classList.remove("far");
//                likeIcon.classList.add("fas");
//                likeIcon.classList.remove("text-muted");
//                likeIcon.classList.add("text-danger");
//            }
//            else {
//                likeIcon.classList.remove("fas");
//                likeIcon.classList.add("far");
//                likeIcon.classList.remove("text-danger");
//                likeIcon.classList.add("text-muted");
//            }
//        }
//    });


//    if (likeIcon.classList.contains("far")) {
//        likeIcon.classList.remove("far");
//        likeIcon.classList.add("fas");
//        likeIcon.classList.remove("text-muted");
//        likeIcon.classList.add("text-danger");
//    }
//    else {
//        likeIcon.classList.remove("fas");
//        likeIcon.classList.add("far");
//        likeIcon.classList.remove("text-danger");
//        likeIcon.classList.add("text-muted");
//    }
//    likeIcon.setAttribute("disabled", "true");
//    setTimeout(function () { likeIcon.removeAttribute("disabled"); }, 1000);
//}

//function toggleReplyBox(commentId) {
//    var selector = "reply(" + commentId + ")";
//    tinyMCEInit("textarea");
//    var domElem = document.getElementById(selector);
//    if (domElem.style.display == "none" || domElem.style.display == "") {
//        domElem.style.display = "inline";
//    }
//    else {
//        domElem.style.display = "none";
//    }
//}

//function toggleReplies(commentId) {
//    var selector = "commentReplies(" + commentId + ")";
//    var domElem = document.getElementsByClassName(selector);
//    for (var i = 0; i < domElem.length; i++) {
//        if (domElem[i].style.display == "none" || domElem[i].style.display == "") {
//            domElem[i].style.display = "block";
//        }
//        else {
//            domElem[i].style.display = "none";
//        }
//    }

//    var selector = "toggleReplies(" + commentId + ")";
//    var domElem = document.getElementById(selector);
//    if (domElem.innerHTML.indexOf("View") != -1) {
//        domElem.innerHTML = "Hide Replies <i class='fa fa-arrow-up'></i>";
//        return;
//    }
//    if (domElem.innerHTML.indexOf("Hide") != -1) {
//        domElem.innerHTML = "View Replies <i class='fa fa-arrow-down'></i>";
//        return;
//    }
//}

//function AddComment(threadId) {
//    var selector = "text(" + threadId + ")";
//    var text = $(document.getElementById(selector)).val().trim();
//    $.ajax({
//        url: "/api/blog/",
//        type: "POST",
//        data: { "comment": text, "threadId": threadId }
//    });
//};

//function showComments(threadId, numComments) {

//    var commentSelector = "comments(" + threadId + ")";
//    var buttonSelector = "showCommentsButton(" + threadId + ")";
//    var iconSelector = "showCommentsIcon(" + threadId + ")";

//    if (document.getElementById(commentSelector).style.display == "none" || document.getElementById(commentSelector).style.display == "") {
//        document.getElementById(commentSelector).style.display = "block";

//        document.getElementById(buttonSelector).firstChild.textContent = "Hide Comment(s) ";

//        document.getElementById(iconSelector).classList.remove("fa-arrow-down");
//        document.getElementById(iconSelector).classList.add("fa-arrow-up");
//    }
//    else {
//        document.getElementById(commentSelector).style.display = "none";

//        document.getElementById(buttonSelector).firstChild.textContent = "View " + numComments + " Comment(s) ";

//        document.getElementById(iconSelector).classList.remove("fa-arrow-up");
//        document.getElementById(iconSelector).classList.add("fa-arrow-down");
//    }
//};

//function toggleCommentBox(id) {
//    var selector = "box(" + id + ")";
//    tinyMCESelector = "textarea";
//    tinyMCEInit(tinyMCESelector);
//    var domElem = document.getElementById(selector);
//    if (domElem.style.display == "none" || domElem.style.display == "") {
//        domElem.style.display = "block"
//    }
//    else {
//        domElem.style.display = "none";
//    }
//};

//function hideCommentBox(id) {
//    var selector = "box(" + id + ")";
//    var domElem = document.getElementById(selector);
//    domElem.innerHTML = "";
//};

//function Delete(id) {
//    swal({
//        title: "Are you sure you want to delete this post?",
//        text: "You will not be able to restore the data!",
//        icon: "warning",
//        buttons: true,
//        dangerMode: true
//    }).then((willDelete) => {
//        if (willDelete) {
//            $.ajax({
//                type: 'DELETE',
//                url: "/api/blog/" + id,
//                data: { "threadId": id },
//                success: function (data) {
//                    if (data == true) {
//                        toastr.success("Post Successfully Deleted!");
//                        var cardSelector = "card(" + id + ")";
//                        var boxSelector = "box(" + id + ")";
//                        var commentsSelector = "comments(" + id + ")";
//                        document.getElementById(cardSelector).style.display = "none";
//                        document.getElementById(boxSelector).style.display = "none";
//                        document.getElementById(commentsSelector).style.display = "none";
//                    }
//                    else {
//                        toastr.error("Failed to delete post. Please contact your administrator to get it removed.");
//                    }
//                }
//            })
//        }
//    })
//};

//function Modal(id) {
//    var modalSelector = "modal(" + id + ")";
//    var modal = document.getElementById(modalSelector);

//    var imageSelector = "img(" + id + ")";
//    var img = document.getElementById(imageSelector);

//    var modalImageSelector = "modalImage(" + id + ")";
//    var modalImg = document.getElementById(modalImageSelector);

//    img.onclick = function () {
//        modal.style.display = "block";
//        modalImg.src = this.src;
//    }

//    var closeSelector = "close(" + id + ")";
//    var close = document.getElementById(closeSelector);
//    close.onclick = function () {
//        modal.style.display = "none";
//    }
//};

$(document).ready(function () { }); var numPosts = 10; function showPosts(e) { for (var t = numPosts; t < numPosts + numPosts; t++) { var s = document.getElementById(t); null != s && (s.style.display = "block") } (numPosts += numPosts) >= e - 1 && (document.getElementById("showMore").style.display = "none") } function tinyMCEInit(e) { tinymce.init({ selector: e, plugins: "lists", menubar: "file edit format", entity_encoding: "raw" }) } function HasUserLikedPost(e) { $.ajax({ url: "/api/blog/", type: "GET", data: { commentId: e }, success: function (e) { return e } }) } function LikeThread(e) { var t = "likeIcon(" + e + ")", s = document.getElementById(t); s.hasAttribute("disabled") || ($.ajax({ url: "/api/blog/" + e, type: "POST", data: { id: e }, success: function (t) { var s = "likeSpan(" + e + ")", n = document.getElementById(s); n.innerHTML = 0 != t ? t : "" }, error: function (t) { var s = "likeIcon(" + e + ")", n = document.getElementById(s); n.classList.contains("far") ? (n.classList.remove("far"), n.classList.add("fas"), n.classList.remove("text-muted"), n.classList.add("text-danger")) : (n.classList.remove("fas"), n.classList.add("far"), n.classList.remove("text-danger"), n.classList.add("text-muted")) } }), s.classList.contains("far") ? (s.classList.remove("far"), s.classList.add("fas"), s.classList.remove("text-muted"), s.classList.add("text-danger")) : (s.classList.remove("fas"), s.classList.add("far"), s.classList.remove("text-danger"), s.classList.add("text-muted")), s.setAttribute("disabled", "true"), setTimeout(function () { s.removeAttribute("disabled") }, 1e3)) } function LikeComment(e) { var t = "likeCommentIcon(" + e + ")", s = document.getElementById(t); s.hasAttribute("disabled") || ($.ajax({ url: "/api/blog/" + e, type: "POST", data: { id: e }, success: function (t) { var s = "likeCommentSpan(" + e + ")", n = document.getElementById(s); n.innerHTML = 0 != t ? t : "" }, error: function (t) { var s = "likeCommentIcon(" + e + ")", n = document.getElementById(s); n.classList.contains("far") ? (n.classList.remove("far"), n.classList.add("fas"), n.classList.remove("text-muted"), n.classList.add("text-danger")) : (n.classList.remove("fas"), n.classList.add("far"), n.classList.remove("text-danger"), n.classList.add("text-muted")) } }), s.classList.contains("far") ? (s.classList.remove("far"), s.classList.add("fas"), s.classList.remove("text-muted"), s.classList.add("text-danger")) : (s.classList.remove("fas"), s.classList.add("far"), s.classList.remove("text-danger"), s.classList.add("text-muted")), s.setAttribute("disabled", "true"), setTimeout(function () { s.removeAttribute("disabled") }, 1e3)) } function toggleReplyBox(e) { var t = "reply(" + e + ")"; tinyMCEInit("textarea"); var s = document.getElementById(t); "none" == s.style.display || "" == s.style.display ? s.style.display = "inline" : s.style.display = "none" } function toggleReplies(e) { for (var t = "commentReplies(" + e + ")", s = document.getElementsByClassName(t), n = 0; n < s.length; n++)"none" == s[n].style.display || "" == s[n].style.display ? s[n].style.display = "block" : s[n].style.display = "none"; t = "toggleReplies(" + e + ")"; -1 == (s = document.getElementById(t)).innerHTML.indexOf("View") ? -1 == s.innerHTML.indexOf("Hide") || (s.innerHTML = "View Replies <i class='fa fa-arrow-down'></i>") : s.innerHTML = "Hide Replies <i class='fa fa-arrow-up'></i>" } function AddComment(e) { var t = "text(" + e + ")", s = $(document.getElementById(t)).val().trim(); $.ajax({ url: "/api/blog/", type: "POST", data: { comment: s, threadId: e } }) } function showComments(e, t) { var s = "comments(" + e + ")", n = "showCommentsButton(" + e + ")", a = "showCommentsIcon(" + e + ")"; "none" == document.getElementById(s).style.display || "" == document.getElementById(s).style.display ? (document.getElementById(s).style.display = "block", document.getElementById(n).firstChild.textContent = "Hide Comment(s) ", document.getElementById(a).classList.remove("fa-arrow-down"), document.getElementById(a).classList.add("fa-arrow-up")) : (document.getElementById(s).style.display = "none", document.getElementById(n).firstChild.textContent = "View " + t + " Comment(s) ", document.getElementById(a).classList.remove("fa-arrow-up"), document.getElementById(a).classList.add("fa-arrow-down")) } function toggleCommentBox(e) { var t = "box(" + e + ")"; tinyMCESelector = "textarea", tinyMCEInit(tinyMCESelector); var s = document.getElementById(t); "none" == s.style.display || "" == s.style.display ? s.style.display = "block" : s.style.display = "none" } function hideCommentBox(e) { var t = "box(" + e + ")"; document.getElementById(t).innerHTML = "" } function Delete(e) { swal({ title: "Are you sure you want to delete this post?", text: "You will not be able to restore the data!", icon: "warning", buttons: !0, dangerMode: !0 }).then(t => { t && $.ajax({ type: "DELETE", url: "/api/blog/" + e, data: { threadId: e }, success: function (t) { if (1 == t) { toastr.success("Post Successfully Deleted!"); var s = "card(" + e + ")", n = "box(" + e + ")", a = "comments(" + e + ")"; document.getElementById(s).style.display = "none", document.getElementById(n).style.display = "none", document.getElementById(a).style.display = "none" } else toastr.error("Failed to delete post. Please contact your administrator to get it removed.") } }) }) } function Modal(e) { var t = "modal(" + e + ")", s = document.getElementById(t), n = "img(" + e + ")", a = document.getElementById(n), o = "modalImage(" + e + ")", d = document.getElementById(o); a.onclick = function () { s.style.display = "block", d.src = this.src }; var l = "close(" + e + ")"; document.getElementById(l).onclick = function () { s.style.display = "none" } }