
function LikeThread(appUserId, commentId) {
    $.ajax({
        url: "/api/blog/",
        type: "POST",
        data: {
            "applicationUserId": appUserId,
            "commendId": commendId
        }
    });
}