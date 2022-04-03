$(document).ready(function () {
    var User = {
        'Name': null,
        'SurName': null,
        'E-mail': null,
        'Password': null,
        'Type': null
    }
    var login = false;
    var error_mail;
    var error_password;
    var output;
    var login;

    function ajaxCall(type, values) {
        if (type == "Login") {
            $.ajax({
                url: 'ProductService.asmx/LoginRequest',
                data: { 'values': values },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);
                    User = output.user;
                    if (output.isError) {
                        alert("E-mail and/or Password is incorrect !");
                    }
                    else {
                        login = true;

                        var URL = getCookie('url');
                        if (URL !== null) {
                            eraseCookie('url');
                            window.location.replace(URL);
                        }
                        window.location.replace("HomePage.aspx");
                    }
                },
                error: function (response) {
                    alert("Something went wrong !");
                }
            });
        }
        else if (type == "getSession") {
            $.ajax({
                url: 'ProductService.asmx/GetSessionData',
                data: {},
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    User = JSON.parse(response.all[0].innerHTML);
                    if (User.Email == null || User.length == 0) {
                        login = false;
                        $('#login-message').hide();
                    }
                    else {
                        login = true;

                        $('#lMail').hide();
                        $('#mail_error').hide();
                        $('#lPassword').hide();
                        $('#password_error').hide();
                        $('#loginButton').hide();
                    }
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
    }

    function getCookie(key) {
        var keyValue = document.cookie.match('(^|;) ?' + key + '=([^;]*)(;|$)');
        return keyValue ? keyValue[2] : null;
    }

    function setCookie(key, value, expiry) {
        var expires = new Date();
        expires.setTime(expires.getTime() + (expiry * 24 * 60 * 60 * 1000));
        document.cookie = key + '=' + value + ';expires=' + expires.toUTCString();
    }

    function eraseCookie(key) {
        var keyValue = getCookie(key);
        setCookie(key, keyValue, '-1');
    }

    $('#lMail').focusout(function () {
        let pattern = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/;
        User.Mail = $('#lMail').val();

        if (pattern.test(User.Mail) && User.Mail != "") {
            $('#mail_error').hide();
        }
        else {
            $('#mail_error').show();
            error = true;
        }

    });

    $('#lPassword').focusout(function () {
        User.Password = $('#lPassword').val();

        if (User.Password.length < 8) {
            $('#password_error').text("Password must be atleast 8 characters");
            error = true;
        }
        else {
            $('#password_error').hide();
        }
    });

    $('#loginButton').unbind().click(function () {
        if (error_mail == true || error_password == true) {
            alert("Please enter a valid mail and/or password");
        }
        else {
            for (var member in User) delete User[member];

            User.idUser = "0";
            User.Name = null;
            User.SurName = null;
            User["Email"] = $('#lMail').val();
            User.Password = $('#lPassword').val();

            ajaxCall("Login", JSON.stringify(User));
        }
    });

    ajaxCall("getSession", null);
    var interval = window.setInterval(function () {
        ajaxCall("getSession", null);
    }, 30000);
});
