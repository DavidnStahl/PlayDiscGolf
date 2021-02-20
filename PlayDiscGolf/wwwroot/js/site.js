$(document).ready(function () {


    jQueryAjaxPost = form => {
        console.log(form.id)
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) { 
                    if (form.id === "EditCourse") {

                        $("#courseform").empty().append(res);
                    }
                    if (form.id === "SearchFormAjax") {

                        $("#searchFormHome").empty().append(res);
                    }

                    if (form.id === "SearchFormAdmin") {
                        $("#adminSearchResult").empty().append(res);
                    }
                    if (form.id === "SearchUser") {

                        $("#searchUserName").empty().append(res);
                    }
                    if (form.id === "ChangeEmail") {

                        if (res.success) {
                            location.reload();
                        }
                        
                         $("#ChangeEmail").empty().append(res); 
                    } 
                    if (form.id === "ChangeUsername") {

                        if (res.success) {
                            location.reload();
                        }

                        $("#ChangeUsername").empty().append(res);
                    }
                    if (form.id === "ChangePassword") {

                        if (res.success) {
                            location.reload();
                        }

                        $("#ChangePassword").empty().append(res);
                    }
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
            url: "/Admin/Search",
            data: {
                query: $("#searchText").val(),
                type: $("#mySelectedSearch").val()
            },
            success: function (result) {
                $("#partial").empty().append(result);
            },
            error: function (result) {
                $("#partialError").empty().append(result);
             }

        });
    }

    $('#collapseExampleScorecard').on('shown.bs.collapse', function () {
        $('html, body').animate({
            scrollTop: $("#collapseExampleScorecard").offset().top
        }, 1000);
    });

    function CreateHoles() {
        $.ajax({
            url: "/Admin/GetHoles",
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
                newName: $("#selectFriend").children("option:selected").val()
            },
            success: function (result) {
                $("#playersInScoreCard").empty().append(result);

            }
        });       
    }

    function GetCourses(value) {
        $.ajax({
            url: "/Admin/SelectedLocation",
            data: {
                id: value
            },
            success: function (result) {
                $("#courseform").empty().append(result);
                CreateHoles();
            }
        });
    }

    $("#selectFriend").on("change", function () {
        var value = $(this).children("option:selected").val();

        if (value !== "Choose friend") {
            document.getElementById("addPlayerAjax").hidden = false;
        }
        else {
            document.getElementById("addPlayerAjax").hidden = true;
        }
    });

    $("#editHoles").on('input', function () {
        var e = $("#editHoles").val()
        var y = e < Number(e.min) || e > Number(e.max) ? '' : e;
        var x = parseInt(y)
        $("#editHoles").val(x)
    });

    $(".editHolesCheck").on('input', function () {
        var e = $(this).val()
        var y = e < Number(e.min) || e > Number(e.max) ? '' : e;
        var x = parseInt(y)
        $(this).val(x)
    });



    $("#editHoles").on('keyup change',function () {
        CreateHoles();
    });

    $("#submit").click(function () {
        GetSearchOptions();
    });

    $("#addPlayerAjax").click(function () {

        AddPlayerToPlayerCard();
    });

    $(".locationButton").click(function (event) {
        var location = event.target.id
        GetCourses(location)
    });
});


