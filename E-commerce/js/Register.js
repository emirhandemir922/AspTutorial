$(document).ready(function () {
    var User = {
        'idUser': null,
        'Name': null,
        'SurName': null,
        'Email': null,
        'Password': null,
        'Type': null
    }

    $("[id$=registerName]").focusout(function () {
        let pattern = /^[a-zA-Z]*$/;
        User.Name = $("[id$=registerName]").val();

        if (pattern.test(User.Name) && User.Name != "") {
            $("[id$=nameError]").hide();
            error_name = false;
        }
        else {
            $("[id$=nameError]").show();
            error_name = true;
        }
    });

    $("[id$=registerSurName]").focusout(function () {
        let pattern = /^[a-zA-Z]*$/;
        User.SurName = $("[id$=registerSurName]").val();

        if (pattern.test(User.SurName) && User.SurName != "") {
            $("[id$=surNameError]").hide();
            error_surname = false;
        }
        else {
            $("[id$=surNameError]").show();
            error_surname = true;
        }
    });

    $("[id$=registerMail]").focusout(function () {
        let pattern = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/;
        User["E-mail"] = $("[id$=registerMail]").val();

        if (pattern.test(User["E-mail"]) && User["E-mail"] != "") {
            $("[id$=mailError]").hide();
            error_mail = false;
        }
        else {
            $("[id$=mailError]").show();
            error_mail = true;
        }
    });

    $("[id$=registerPassword]").focusout(function () {
        let pattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{8,}$/;
        User.Password = $("[id$=registerPassword]").val();

        if (User.Password.length < 8 || !pattern.test(User.Password)) {
            $("[id$=passwordError]").show();
            error_password = true;
        }
        else {
            $("[id$=passwordError]").hide();
            error_password = false;
        }
    });

    $("[id$=nameError]").hide();
    $("[id$=surNameError]").hide();
    $("[id$=mailError]").hide();
    $("[id$=passwordError]").hide();
});