$(document).ready(function () {

    var input;
    var output;
    var rowNumber;
    var lastData;
    var Products = {
        'id': null,
        'ProductName': null,
        'Category': null,
        'Price': null,
        'Photo': null,
        'Description': null
    }
    var Images = {
        'id': null,
        'PhotoName': null,
        'PhotoSource': null,
        'PhotoSize': null,
        'Product_id': null
    }
    var Comments = {
        'id': null,
        'Comment': null,
        'Commentor': null,
        'Likes': null,
        'Product_id': null
    }
    var CommentLikes = {
        'id': null,
        'Comment_id': null,
        'User_id': null
    }
    var newComment = {
        'id': null,
        'Comment': null,
        'Commentor': null,
        'Likes': null,
        'Product_id': null
    }
    var User = {
        'idUser': null,
        'Name': null,
        'SurName': null,
        'Email': null
    }

    var id;
    var login = false;

    function ajaxCall(type, value) {
        if (type == "getProduct") {
            $.ajax({
                url: 'ProductService.asmx/GetProduct',
                data: {'id': value},
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    Products = JSON.parse(response.all[0].innerHTML);
                    Products = Products["productsTable"]["products"][0];

                    Comments = JSON.parse(response.all[0].innerHTML);
                    Comments = Comments["commentsTable"]["comments"];
                    if (Products.length == 0) {
                        $('#Error-message').show();
                        $('#CommentSection').remove();
                    }
                    else {
                        createContent();
                        if (Comments.length == 0) {
                            createCommentTable("empty");
                        }
                        else {
                            objKeys = Object.keys(Comments[0]);
                            createCommentTable();
                        }
                    }
                },
                error: function (response) {
                    alert("No product to show");
                }
            });
        }
        else if (type == "addComment") {
            $.ajax({
                url: 'ProductService.asmx/AddComment',
                data: { 'values': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    Comments = JSON.parse(response.all[0].innerHTML);
                    Comments = Comments["comments"];
                    objKeys = Object.keys(Comments[0]);
                    $('#CommentArea').val("");

                    createCommentTable();
                },
                error: function (response) {
                    alert(response);
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
                    if (User.Email == null) {
                        login = false;

                        $('#CommentArea').val('Login to write comment here');
                        $('#CommentArea').prop('disabled', true);
                    }
                    else {
                        login = true;

                        checkCommentLikes(User.idUser);
                    }
                },
                error: function (response) {
                    login = false;
                }
            });
        }
        else if (type == "commentLike") {
            $.ajax({
                url: 'ProductService.asmx/CommentLike',
                data: {'values': value},
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    $('#CommentArea').val("");

                },
                error: function (response) {
                    login = false;
                }
            });
        }
        else if (type == "commentUnLike") {
            $.ajax({
                url: 'ProductService.asmx/CommentUnLike',
                data: { 'id': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    $('#CommentArea').val("");

                    checkCommentLikes(User.idUser);
                },
                error: function (response) {
                    login = false;
                }
            });
        }
        else if (type == "getCommentLikes") {
            $.ajax({
                url: 'ProductService.asmx/GetCommentLikes',
                data: { 'User_id': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    CommentLikes = JSON.parse(response.all[0].innerHTML);
                    CommentLikes = CommentLikes["commentlikes"];

                    createCommentTable();
                },
                error: function (response) {
                    alert("Something went wrong!");
                }
            });
        }
    }

    function createContent() {
        $('#ProductContent').empty();
        $('#ProductContent').append('<div class="image p-3" id="image"></div >');
        $('#image').append('<img class="image" src="' + Products.Photo + '" style="width:400px; height:380px;"/>');
        $('#ProductContent').append('<div class="content p-3" id="content"></div >');
        $('#content').append('<h4>' + Products.ProductName + '</h4 >');
        $('#content').append('<h2>' + Products.Price + ' TL</h2>');
        $('#content').append('<p>' + Products.Description + '</p>');
    }

    function createCommentTable(type) {
        $('#Comments').empty();

        if (type == "empty") {
            $('<h6>There is no comment to show for this product</h6>').appendTo('#Comments');
        }
        else {
            for (i = 0; i < Comments.length; i++) {
                $('<div id="' + Comments[i].id + '"></div>').appendTo('#Comments');
                $('<p>' + Comments[i].Comment + '</p>').appendTo('#' + Comments[i].id);
                $('<h6 class="text-success">' + Comments[i].Likes + ' Like(s)</h6>').appendTo('#' + Comments[i].id);
                $('<h5 class="text-primary">-' + Comments[i].Commentor + '</h5>').appendTo('#' + Comments[i].id);

                //for (j = 0; j < CommentLikes.length; j++) {
                //    if (User.idUser == CommentLikes[j].User_id && Comments[i].id == CommentLikes[j].Comment_id) {
                //        $('<h6 class="text-info">You already liked this comment, did you changed your mind ? <button id="UnLikeBtn" class="btn btn-success" disabled>Yes</button></h6>').appendTo('#' + Comments[i].id);
                //    }
                //    else {
                //        $('<h6 class="text-info">Is this comment usefull ? <button id="LikeBtn" class="btn btn-success">Yes</button></h6>').appendTo('#' + Comments[i].id);
                //    }
                //}
            }

            //$('#UnLikeBtn').on("click", function () {
            //    $('#UnLikeBtn').prop('disabled', true);
            //    id = $(this).parent().parent().attr('id');

            //    ajaxCall('commentUnLike', id);
            //});

            //$('#LikeBtn').on("click", function () {
            //    if (login) {
            //        $('#LikeBtn').prop('disabled', true);
            //        id = $(this).parent().parent().attr('id');

            //        for (i = 0; i < Comments.length; i++) {
            //            if (Comments[i].id == id) {
            //                Comments[i].Likes += 1;
            //                createCommentTable();
            //            }
            //        }
            //        CommentLikes.id = 0;
            //        CommentLikes.Comment_id = $(this).parent().parent().attr('id');
            //        CommentLikes.User_id = User.idUser;
            //        ajaxCall('commentLike', JSON.stringify(CommentLikes));
            //    }
            //    else {
            //        var currentURL = $(location).attr('href');
            //        setCookie('url', currentURL, '1');
            //        window.location.replace(currentURL);
            //    }
            //});
        }
    }

    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }

    function setCookie(key, value, expiry) {
        var expires = new Date();
        expires.setTime(expires.getTime() + (expiry * 24 * 60 * 60 * 1000));
        document.cookie = key + '=' + value + ';expires=' + expires.toUTCString();
    }

    function getCookie(key) {
        var keyValue = document.cookie.match('(^|;) ?' + key + '=([^;]*)(;|$)');
        return keyValue ? keyValue[2] : null;
    }

    function eraseCookie(key) {
        var keyValue = getCookie(key);
        setCookie(key, keyValue, '-1');
    }

    //function checkCommentLikes(User_id) {
    //    ajaxCall("getCommentLikes", User_id);
    //}

    ajaxCall("getProduct", getUrlVars().id);
    ajaxCall("getSession", null);

    var interval = window.setInterval(function () {
        ajaxCall("getSession", null);
    }, 30000);

    $('#AddComment').on("click", function () {
        if (login) {
            newComment.id = 0;
            newComment.Commentor = User.Name;
            newComment.Likes = 0;
            newComment.Product_id = Products.id;
            newComment.Comment = $('#CommentArea').val();
            ajaxCall("addComment", JSON.stringify(newComment));
        }
        else {
            var currentURL = $(location).attr('href');
            setCookie('url', currentURL, '1');
            window.location.replace("LoginPage.aspx");
        }
    });
});