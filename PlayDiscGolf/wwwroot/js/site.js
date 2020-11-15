$(document).ready(function () {
    function GetSearchOptions() {
        $.ajax({
            url: "/AdminCourse/Search",
            data: {
                query: $("#searchText").val(),
                searchType: $("#mySelectedSearch").val()
            },
            success: function (result) {
                $("#partial").empty().append(result);
            }
        });
    }

    function CreateHoles() {
        $.ajax({
            url: "/AdminCourse/CreateHoles",
            data: {
                holes: $("#selectedNumberOfHoles").val(),
                courseID: $("#courseID").val()
            },
            success: function (result) {
                $("#partialHoles").empty().append(result);
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
    

    $("#submitHoles").click(function () {
        CreateHoles();
    });

    $("#submit").click(function () {
        GetSearchOptions();
    });


    $(".locationButton").click(function (event) {
        var location = event.target.id
        GetCourses(location)
    });


    

});


