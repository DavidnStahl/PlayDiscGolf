$(document).ready(function () {



    jQueryAjaxPost = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    console.log(res)
                    $("#searchFormHome").empty().append(res);
                },
                error: function (err) {
                    console.log(err)
                }
            })
            //to prevent default form submit event
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }

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


    
    function AddPlayerToPlayerCard() {
        $.ajax({
            url: "/ScoreCard/AddPlayer",
            data: {
                newName: $("#nameInputPlayAjax").val()
            },
            success: function (result) {
                console.log(result)
                $("#playersInScoreCard").empty().append(result);

            }
        });
        
    }

    function RemovePlayerToPlayerCard(val) {
        $.ajax({
            url: "/ScoreCard/RemovePlayer",
            data: {
                removePlayer: val
            },
            success: function (result) {
                console.log(result)
                $("#playersInScoreCard").empty().append(result);

            }
        });

    }


    function changeUserScore(val) {
        $.ajax({
            url: "/ScoreCard/UpdateScoreCard",
            data: {
                value : val
            },
            success: function (result) {
                console.log(result)
                $("#playersInScoreCard").empty().append(result);

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

    $("#addPlayerAjax").click(function () {
        AddPlayerToPlayerCard();
    });

    $(".changeUserScore").click(function () {
        changeUserScore($(this).attr("id"))
    });

    $(".tryThis").on('click', function () {
        var value = $(this).prev().val();
        RemovePlayerToPlayerCard(value);
        
    });

    $(".locationButton").click(function (event) {
        var location = event.target.id
        GetCourses(location)
    });


    

});


