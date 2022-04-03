$(document).ready(function () {
    var User = {
        'idUser': null,
        'Name': null,
        'SurName': null,
        'Email': null,
        'Password': null,
        'Type': null
    }
    var login = false;
    var error_name = false;
    var error_surname = false;
    var error_mail = false;
    var error_password = false;
    var output;

    function ajaxCall(type, values) {
        if (type == "Register") {
            $.ajax({
                url: 'ProductService.asmx/RegisterRequest',
                data: { 'values': values },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);
                    User = output.user;
                    
                },
                error: function (response) {
                    alert("AJAX went wrong !");
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

                        $('#rName').hide();
                        $('#name_error').hide();
                        $('#rSurName').hide();
                        $('#surname_error').hide();
                        $('#rMail').hide();
                        $('#mail_error').hide();
                        $('#rPassword').hide();
                        $('#password_error').hide();
                        $('#registerButton').hide();
                    }
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
    }

    $('#rName').focusout(function () {
        let pattern = /^[a-zA-Z]*$/;
        User.Name = $('#rName').val();

        if (pattern.test(User.Name) && User.Name != "") {
            $('#name_error').hide();
            error_name = false;
        }
        else {
            $('#name_error').show();
            error_name = true;
        }
    });

    $('#rSurName').focusout(function () {
        let pattern = /^[a-zA-Z]*$/;
        User.SurName = $('#rSurName').val();

        if (pattern.test(User.SurName) && User.SurName != "") {
            $('#surname_error').hide();
            error_surname = false;
        }
        else {
            $('#surname_error').show();
            error_surname = true;
        }
    });

    $('#rMail').focusout(function () {
        let pattern = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/;
        User["E-mail"] = $('#rMail').val();

        if (pattern.test(User["E-mail"]) && User["E-mail"] != "") {
            $('#mail_error').hide();
            error_mail = false;
        }
        else {
            $('#mail_error').show();
            error_mail = true;
        }
    });

    $('#rPassword').focusout(function () {
        User.Password = $('#rPassword').val();

        if (User.Password.length < 8) {
            $('#password_error').text("Password must be atleast 8 characters");
            error_password = true;
        }
        else {
            $('#password_error').hide();
            error_password = false;
        }
    });

    $('#registerButton').unbind().click(function () {
        if (error_name == true || error_surname == true || error_mail == true || error_password == true) {
            alert("NO!");
        }
        else {
            for (var member in User) delete User[member];

            User.idUser = "0";
            User.Name = $('#rName').val();
            User.SurName = $('#rSurName').val();
            User["Email"] = $('#rMail').val();
            User.Password = $('#rPassword').val();

            ajaxCall("Register", JSON.stringify(User));
        }
    });

    ajaxCall("getSession", null);
    var interval = window.setInterval(function () {
        ajaxCall("getSession", null);
    }, 30000);
});