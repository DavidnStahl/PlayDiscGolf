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

    function GetCourses(value) {
        $.ajax({
            url: "/AdminCourse/SelectedLocation",
            data: {
                id: value
            },
            success: function (result) {
                $("#courseform").empty().append(result);
            }
        });
    }


    $("#submit").click(function () {
        GetSearchOptions();
    });


    $(".locationButton").click(function (event) {
        var location = event.target.id
        GetCourses(location)
    });


});

