$(document).ready(function () {
    var User = {
        'idUser': null,
        'Name': null,
        'SurName': null,
        'Email': null
    }
    var id;
    var login;

    function ajaxCall(type, value) {
        if (type == "getSession") {
            $.ajax({
                url: 'ProductService.asmx/GetSessionData',
                data: {},
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    User = JSON.parse(response.all[0].innerHTML);
                    if (User.Email == null || User.length == 0) {
                        login = false;

                        $('#userInfo').hide();
                        $('#logoffButton').hide();
                    }
                    else {
                        login = true;

                        $('#userInfo').text(User.Name + " " + User.SurName);
                        $('#userInfoLink').attr("href", "User.aspx");
                        $('#Register').hide();
                        $('#Login').hide();
                    }
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "deleteSession") {
            $.ajax({
                url: 'ProductService.asmx/DeleteSessionData',
                data: {},
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    a/*jaxCall("getSession", null);*/
                    window.location.replace('Homepage.aspx');  
                },
                error: function (response) {
                    alert("Something went wrong pls try again!");
                }
            });
        }
    }

    function setCookie(key, value, expiry) {
        var expires = new Date();
        expires.setTime(expires.getTime() + (expiry * 24 * 60 * 60 * 1000));
        document.cookie = key + '=' + value + ';expires=' + expires.toUTCString();
    }

    ajaxCall("getSession", null);

    var interval = window.setInterval(function () {
        ajaxCall("getSession", null);
    }, 30000);

    $('#logoffButton').on("click", function () {
        ajaxCall("deleteSession", null);
    });
});