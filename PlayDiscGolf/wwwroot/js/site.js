$(document).ready(function () {
    function GetSearchOptions() {
        $.ajax({
            url: "/AdminCourse/Search",
            data: {
                query: $("#searchText").val()
            },
            success: function (result) {
                $("#partial").empty().append(result);
            }
        });
    }

    $("#submit").click(function () {
        GetSearchOptions();
    });

});

