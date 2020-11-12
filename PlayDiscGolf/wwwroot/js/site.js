$("#AdminSearchLocationInput").change(function () {
    $.ajax({
        url: "https://localhost:44313/Admin?", success: function (result) {
            $("#searchLocationPartialView").html(result);
        }
    });
});
