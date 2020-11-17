﻿$(document).ready(function () {
    function GetSearchOptions() {
        $.ajax({
            url: "/AdminCourse/Search",
            data: {
                query: $("#searchText").val(),
                type: $("#mySelectedSearch").val()
            },
            success: function (result) {
                $("#partial").empty().append(result);
            },
            error: function (result) {
                $("#partialError").empty().append(result);
                console.log(result);
             }

        });
    }

    

    function CreateHoles() {
        $.ajax({
            url: "/AdminCourse/GetHoles",
            data: {
                holes: $("#editHoles").val(),
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
                CreateHoles();
            }
        });
    }
    

    $("#editHoles").on('keyup change',function () {
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


