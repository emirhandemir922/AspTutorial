$(document).ready(function () {
    var User = {
        'idUser': null,
        'Name': null,
        'SurName': null,
        'Email': null,
        'Password': null
    }

    function ajaxCall(type, values) {
        if (type == "getSession") {
            $.ajax({
                url: 'ProductService.asmx/GetSessionData',
                data: {},
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    User = JSON.parse(response.all[0].innerHTML);

                    if (User.type == "Guest") {
                        $('#userHeader').text(User.Name + User.SurName);
                        $('#Name').attr("placeholder", User.Name);
                        $('#SurName').attr("placeholder", User.SurName);
                        $('#Email').attr("placeholder", User.Email);
                        $('#AddProduct').hide();
                    }
                    else {
                        $('#userHeader').text(User.Name + " " + User.SurName);
                        $('#Name').attr("placeholder", User.Name);
                        $('#SurName').attr("placeholder", User.SurName);
                        $('#Email').attr("placeholder", User.Email);
                    }
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "editUser") {
            $.ajax({
                url: 'ProductService.asmx/EditUser',
                data: { 'values': values },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    loginRequest(User);
                },
                error: function (response) {
                    alert("AJAX went wrong");
                }
            });
        }
        else if (type == "login") {
            $.ajax({
                url: 'ProductService.asmx/LoginRequest',
                data: { 'values': values },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    alert("success");
                },
                error: function (response) {
                    alert("Something went wrong !");
                }
            });
        }
    }

    ajaxCall("getSession", null);

    $('#addProduct').on("click", function () {
        window.location.replace("AddProduct.aspx");
    });

    $('#saveBtn').on("click", function () {
        if ($('#Name').val() == "") {
            User.Name = User.Name;
        }
        else {
            User.Name = $('#Name').val();
        }

        if ($('#SurName').val() == "") {
            User.SurName = User.SurName;
        }
        else {
            User.SurName = $('#SurName').val();
        }

        if ($('#Email').val() == "") {
            User.Email = User.Email;
        }
        else {
            User.Email = $('#Email').val();
        }

        if ($('#password1').val() == $('#password2').val() && !($('#password1').val() == "")) {
            User.Password = $('#password1').val();
        }
        else {
            User.Password = "Dummy";
        }

        ajaxCall("editUser", JSON.stringify(User));
    });

    function loginRequest(values) {
        ajaxCall("login", JSON.stringify(values));
    }
});
