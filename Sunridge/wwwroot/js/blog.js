
function LikeThread(id) {
    $.ajax({
        url: "/api/blog/",
        type: "POST",
        data: {
            "id": id
        }
    });
}