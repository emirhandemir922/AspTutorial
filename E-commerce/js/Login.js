$(document).ready(function () {
    var User = {
        'Name': null,
        'SurName': null,
        'Email': null,
        'Password': null,
        'Type': null
    }

    $("[id$=loginMail]").focusout(function () {
        let pattern = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/;
        User.Mail = $("[id$=loginMail]").val();

        if (pattern.test(User.Mail) && User.Mail != "") {
            $("[id$=mailError]").hide();
        }
        else {
            $("[id$=mailError]").show();
            error = true;
        }
    });

    $("[id$=passwordError]").focusout(function () {
        User.Password = $("[id$=loginPassword]").val();

        if (User.Password.length < 8) {
            $("[id$=passwordError]").show();
            error = true;
        }
        else {
            $("[id$=passwordError]").hide();
        }
    });

    $("[id$=mailError]").hide();
    $("[id$=passwordError]").hide();
});
