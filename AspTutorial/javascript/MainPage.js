$(document).ready(function () {
    var input;
    var output;
    var rowNumber; 
    var lastData;
    var Products = {
        'idProducts': null,
        'ProductName': null,
        'Category': null,
        'Price': null,
        'Photo': null,
        'Description': null
    }
    var Images = {
        'idPhotos': null,
        'PhotoName': null,
        'PhotoSource': null,
        'PhotoSize': null,
        'Product_id': null
    }
    var Comments = {
        'idComments': null,
        'Comment': null,
        'Commentor': null,
        'Likes': null,
        'Dislikes': null,
        'Product_id': null
    }
    var User = {
        'idUser': null,
        'Name': null,
        'SurName': null,
        'Email': null
    }

    var color;
    var id;
    var selector; 
    var clicked = false;
    var editable;
    var login;

    function ajaxCall(type, value) {
        if (type == "showProducts") {
            $.ajax({
                url: 'StudentService.asmx/ShowProducts',
                data: {},
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    createTable(response.all[0].innerHTML);
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "sortOutCategory") {
            $.ajax({
                url: 'StudentService.asmx/SortOutCategory',
                data: { 'input': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    refreshTable(response.all[0].innerHTML);
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "sortOutPrice") {
            $.ajax({
                url: 'StudentService.asmx/SortOutPrice',
                data: { 'input': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    refreshTable(response.all[0].innerHTML);
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "deleteById") {
            $.ajax({
                url: 'StudentService.asmx/DeleteProduct',
                data: { 'id': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    refreshTable(response.all[0].innerHTML);
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "updateByValues") {
            $.ajax({
                url: 'StudentService.asmx/UpdateProduct',
                data: { 'values': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    alert(response.all[0].innerHTML);
                    refreshTable(response.all[0].innerHTML);
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "addProduct") {
            $.ajax({
                url: 'StudentService.asmx/AddProduct',
                data: { 'values': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    alert(response.all[0].innerHTML);
                    refreshTable(response.all[0].innerHTML);
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "uploadImage") {
            $.ajax({
                url: 'fileUploader.ashx',
                data: value,
                method: 'post',
                contentType: false,
                processData: false,
                success: function (result) {
                    //we got the response
                    alert('Successfully called');
                },
                error: function (jqxhr, status, exception) {
                    alert('Exception:', exception);
                }
            });
        }
        else if (type == "addImage") {
            $.ajax({
                url: 'StudentService.asmx/AddImage',
                data: { 'values': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    alert(response.all[0].innerHTML);
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "showImages") {
            $.ajax({
                url: 'StudentService.asmx/ShowImages',
                data: { 'Product_id': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    createImagesTable(response.all[0].innerHTML);
                },
                error: function (response) {
                    alert("alert");
                }
            });
        }
        else if (type == "selectComment") {
            $.ajax({
                url: 'StudentService.asmx/SelectComment',
                data: { 'Product_id': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    Comments = JSON.parse(response.all[0].innerHTML);
                    $(lastData).text(Comments["comments"][0].Comment + " ");
                    $('<input class="mt-1 btn btn-info CommentsBtn" type="button" value="See All" />').appendTo((lastData));

                    $('.CommentsBtn').unbind().click(function () {
                        $('.pop-outerComments').fadeIn('slow');
                        $('.pop-comments').fadeIn('slow');

                        var parent = $(this).parent().parent();
                        id = parseInt(parent.find('td:eq(0)').text());
                        ajaxCall("showComments", id);
                    });
                },
                error: function (response) {
                    $(lastData).text("No comments about this product");

                }
            });
        }
        else if (type == "showComments") {
            $.ajax({
                url: 'StudentService.asmx/ShowComments',
                data: { 'Product_id': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    createCommentsTable(response.all[0].innerHTML);
                },
                error: function (response) {
                    alert("alert");
                }
            });
        }
        else if (type == "addComment") {
            $.ajax({
                url: 'StudentService.asmx/AddComment',
                data: { 'values': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    ajaxCall("showComments", id);
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "getSession") {
            $.ajax({
                url: 'StudentService.asmx/GetSessionData',
                data: {},
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    User = JSON.parse(response.all[0].innerHTML);
                    if (User.Email == null || User.length == 0) {
                        login = false;

                        $('#userInfo').hide();
                    }
                    else {
                        login = true;

                        $('.ChooseImageBtn').prop('disabled', false);
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
    }

    function refreshTableStyle() {
        $('td').on('focus', function () {
            if ($(this).attr("contentEditable") == "true") {
                var before = $(this).html();
            }
            else {
                editable = false;
            }
        }).on('focusout', function () {
            if (editable) {
                if (before != $(this).html()) {
                    for (i = 0; i < objKeys.length; i++) {
                        if (objKeys[i] == "fotograf") {
                            Products[objKeys[i]] = $(this).parent().find("td").eq(i).find("img").attr("src");
                        }
                        else {
                            Products[objKeys[i]] = $(this).closest('tr').find('td:eq(' + i + ')').text();
                            alert(Student[objKeys[i]]);
                        }
                    }
                    ajaxCall("addByValues", JSON.stringify(Products));
                    alert(JSON.stringify(Student));
                }
            }
            else {
                return null;
            }
        });
    }

    function createImagesTable(data) {
        Images = JSON.parse(data);

        if (Images["photos"].length == 0) {
            $('<tr><th>NO IMAGES TO SHOW</th></tr>').appendTo('#ImagesTable');
            $('#ImagesTable').css({
                "display": "block",
                "overflow- y": "auto",
                "white - space": "nowrap",
                "width": "100%"
            });
        }

        else {
            objKeys = Object.keys(Images["photos"][0]);
            imageTable = Images["photos"];
            $('.pop-inner').find('h3').text("Images of Product_id = " + imageTable[0]["Product_id"]);
            //Header row and tableCss:
            $('<tr id="ImageHeaderRow"></tr > ').appendTo('#ImagesTable');
            $('#ImagesTable').css({
                "display": "block",
                "overflow- y": "auto",
                "white - space": "nowrap",
                "width": "100%"
            });

            for (i = 0; i < objKeys.length + 1; i++) {
                if (i == 0) {
                    $('<th class="text-center"></th>').appendTo('#ImageHeaderRow');
                }
                else {
                    $('<th class="text-center"></th>').appendTo('#ImageHeaderRow');
                    lastData = $("#ImageHeaderRow th").last();
                    $(lastData).text(objKeys[i - 1]);
                }
            }
            $('<th class="text-center">Image</th>').appendTo('#ImageHeaderRow');

            for (i = 0; i < imageTable.length; i++) {
                $('<tr id="Imagetr' + i + '" class="tableRow"></tr').appendTo('#ImagesTable');
                $('<td><input class="mt-4 btn btn-warning DeleteBtn" type="button" value="X"></td>').appendTo("#Imagetr" + i);

                for (j = 0; j < objKeys.length + 1; j++) {
                    if (j == objKeys.length) {
                        $('<td><img src="' + imageTable[i]["PhotoSource"] + '" width="80" height="70"></td>').appendTo("#Imagetr" + i);
                    }
                    else {
                        $('<td></td>').appendTo("#Imagetr" + i);
                        lastData = $("#Imagetr" + i + " td").last();
                        tempKeys = objKeys[j];
                        tempValue = imageTable[i][tempKeys];
                        $(lastData).text(tempValue);
                    }
                }

                $('#Imagetr' + i).css({
                    "background-color": "#grey",
                    "text-align": "center"
                });
            }
        }
    }

    function createCommentsTable(data) {
        $('#CommentsTable').empty();
        Comments = JSON.parse(data);

        $('#closeComments').unbind().click(function () {
            $('.pop-comments').fadeOut('slow');
            $('.pop-outerComments').fadeOut('slow');
            $('.pop-comments').find('h3').text(" ");
            $('#CommentsTable').empty();
        });

        if (login) {
            $('#commentArea').prop("disabled", false);
            $('#addCommentLink').hide();

            $('#addCommentButton').unbind().click(function () {
                if (!($('#commentArea').val() == "")) {
                    Comments.Comment = $('#commentArea').val();
                    Comments.Commentor = User.Name;
                    Comments.Likes = 0;
                    Comments.DisLikes = 0;
                    Comments.Product_id = id;

                    ajaxCall("addComment", JSON.stringify(Comments));
                }
            });
        }

        else {
            $('#commentArea').prop("disabled", true);
            $('#addCommentButton').hide();
        }

        if (Comments["comments"].length == 0) {
            $('<tr><th>NO COMMENTS TO SHOW</th></tr>').appendTo('#CommentsTable');
            $('#CommentsTable').css({
                "display": "block",
                "overflow- y": "auto",
                "white - space": "nowrap",
                "width": "100%"
            });
        }

        else {
            objKeys = Object.keys(Comments["comments"][0]);
            CommentTable = Comments["comments"];
            $('.pop-comments').find('h3').text("Comments of Product_id = " + CommentTable[0]["Product_id"]);
            //Header row and tableCss:
            $('<tr id="CommentHeaderRow"></tr > ').appendTo('#CommentsTable');
            $('#CommentsTable').css({
                "display": "block",
                "overflow- y": "auto",
                "white - space": "nowrap",
                "width": "100%"
            });

            for (i = 0; i < objKeys.length + 1; i++) {
                if (i == 0) {
                    $('<th class="text-center"></th>').appendTo('#CommentHeaderRow');
                }
                else {
                    $('<th class="text-center"></th>').appendTo('#CommentHeaderRow');
                    lastData = $("#CommentHeaderRow th").last();
                    $(lastData).text(objKeys[i - 1]);
                }
            }

            for (i = 0; i < CommentTable.length; i++) {
                $('<tr id="Commenttr' + i + '" class="tableRow"></tr').appendTo('#CommentsTable');
                $('<td><input class="mt-4 btn btn-warning DeleteBtn" type="button" value="X"></td>').appendTo("#Commenttr" + i);

                for (j = 0; j < objKeys.length + 1; j++) {
                    $('<td></td>').appendTo("#Commenttr" + i);
                    lastData = $("#Commenttr" + i + " td").last();
                    tempKeys = objKeys[j];
                    tempValue = CommentTable[i][tempKeys];
                    $(lastData).text(tempValue);
                }

                $('#Commenttr' + i).css({
                    "background-color": "#grey",
                    "text-align": "center"
                });
            }
        }
    }

    function createTable(data) {
        var last;
        Products = JSON.parse(data);
        objKeys = Object.keys(Products["outputTable"]["products"][0]);
        outputTable = Products["outputTable"]["products"];
        rowNumber = outputTable.length;

        //Header row and tableCss:
        $('<tr id="HeaderRow"></tr>').appendTo('#ResultsTable');
        
        for (var i = 0; i < objKeys.length + 2; i++) {
            if (i == 0) {
                $('<th></th>').appendTo('#HeaderRow');
            }
            else if (i == objKeys.length + 1) {
                $('<th class="text-center"></th>').appendTo('#HeaderRow');
                last = $('th').last();
                $('th').last().text("Comments");

            }
            else {
                if (objKeys[i - 1] == "ProductName" || objKeys[i - 1] == "Price" ) {
                    $('<th class="text-center"></th>').appendTo('#HeaderRow');
                    last = $('th').last();
                    $('<input class="btn btn-info" id="' + objKeys[i - 1] + 'BtnUp" type="button">').appendTo(last);
                    $('th').last().append('<input class="btn btn-info" id="' + objKeys[i - 1] + 'BtnDown" type="button">');
                    $('th').last().text(objKeys[i - 1]);
                }
                else {
                    $('<th class="text-center"></th>').appendTo('#HeaderRow');
                    $('th').last().text(objKeys[i - 1]);
                }
            }
        }

        //Button css:
        $('.BtnUp').css({
            "height": "15px",
            "width": "15px"
        });

        $('.BtnDown').css({
            "height": "15px",
            "width": "15px"
        });

        //SortOut button functions:
        $('#ProductNameBtnUp').on("click", function () {
            ajaxCall("sortOutCategory", "BtnUp");
        });
        $('#ProductNameBtnDown').on("click", function () {
            ajaxCall("sortOutCategory", "BtnDown");
        });
        $('#PriceBtnUp').on("click", function () {
            ajaxCall("sortOutPrice", "BtnUp");
        });
        $('#PriceBtnDown').on("click", function () {
            ajaxCall("sortOutPrice", "BtnDown");
        }); 

        //Data Rows:
        for (i = 0; i < outputTable.length; i++) {
            $('<tr id="tr' + i + '" class="tableRow"></tr').appendTo('#ResultsTable');
            $('<input class="mt-4 btn btn-warning DeleteBtn" type="button" value="X">').appendTo("#tr" + i);
            $('<input class="mt-4 btn btn-info ChooseImageBtn" type="button" value="Choose Image" name"imgFile" disabled>').appendTo("#tr" + i);

            if (login) {
                $('.ChooseImageBtn').prop('disabled', false);
            }

            for (j = 0; j < objKeys.length + 1; j++) {
                if (objKeys[j] == "ProductName" || objKeys[j] == "Price" || objKeys[j] == "Category") {
                    $('<td class="text-center" contentEditable="true"></td>').appendTo("#tr" + i);

                    lastData = $("#tr" + i + " td").last();
                    tempKeys = objKeys[j];
                    tempValue = outputTable[i][tempKeys];
                    $(lastData).text(tempValue);
                }
                else if (objKeys[j] == "Photo") {
                    tempKeys = objKeys[j];
                    if (outputTable[i][tempKeys] == "") {
                        $('<td class="text-center"><img src="/images/DefaultPhoto.jpg" width="100" height="80" ></td>').appendTo("#tr" + i);
                    }
                    else {
                        $('<td class="text-center"><img src="/images/' + outputTable[i][tempKeys] + '" width="100" height="80" ></td>').appendTo("#tr" + i);
                    }
                }
                else if (j == objKeys.length) {
                    tempValue = outputTable[0]["id"]
                    $('<td class="text-center"></td>').appendTo("#tr" + i);
                    lastData = $("#tr" + i + " td").last();
                    ajaxCall("selectComment", tempValue);

                }
                else {
                    $('<td class="text-center"></td>').appendTo("#tr" + i);

                    lastData = $("#tr" + i + " td").last();
                    tempKeys = objKeys[j];
                    tempValue = outputTable[i][tempKeys];
                    $(lastData).text(tempValue);
                }
            }
        }

        refreshTableStyle();

        $('.ChooseImageBtn').unbind().click( function () {
            $('.pop-outer').fadeIn('slow');
            $('.pop-inner').fadeIn("slow");

            var parent = $(this).parent();
            var src = parent.find("td").eq(4).find("img").attr("src");
            id = parseInt(parent.find('td:eq(0)').text());
            $('.images').attr('src', src);
            ajaxCall("showImages", id);

            $('#AddImageBtn').unbind().click(function () {
                $('.pop-inner').fadeOut("fast");
                $('.pop-inner-file').fadeIn("slow");
                $('.ImageBtn').unbind().change(function (e) {
                    var fd = new FormData();
                    var files = $(this)[0].files;
                    var fileName = files[0].name;
                    var fileSource = "images/" + fileName;
                    var fileSize = files[0].size;

                    $('.text-area').val(fileName);

                    fd.append('file', files[0]);
                    ajaxCall("uploadImage", fd);
                    $('#imgShowCase').attr('src', '/images/' + fileName);

                    $('#saveImage').unbind().click(function () {
                        for (var member in Images) delete Images[member];
                        // Check file selected or not
                        fileName = $('.text-area').val();
                        var randomNumber = Math.floor(Math.random() * 1000 + 1);
                        var randomLetters = Math.random().toString(36).substr(2, 3);
                        fileName = randomLetters + fileName + randomNumber;
                        Images["PhotoName"] = fileName;
                        Images["PhotoSource"] = fileSource
                        Images["PhotoSize"] = fileSize;
                        Images["Product-id"] = id;

                        if (files.length > 0) {
                            ajaxCall("addImage", JSON.stringify(Images))

                        } else {
                            alert("Please select a file.");
                        }
                        $('.pop-inner-file').fadeOut('slow');
                        $('.pop-outer').fadeOut('slow');
                        $('.pop-inner').find('h3').text(" ");
                        $('#ImagesTable').empty();
                    });
                });

                $('#closeInner').unbind().click(function () {
                    $('.pop-inner-file').fadeOut('slow');
                    $('.pop-outer').fadeOut('slow');
                    $('.pop-inner').find('h3').text(" ");
                    $('#ImagesTable').empty();
                });
            });


            $('#closeFile').unbind().click(function () {
                $('.pop-inner').fadeOut("slow");
                $('.pop-outer').fadeOut('slow');
                $('.pop-inner').find('h3').text(" ");
                $('#ImagesTable').empty();
            });
        });

        $('#addButton').unbind().click( function () {
            $('<tr id="new_tr' + rowNumber + '" class="tableRow"></tr').appendTo('#ResultsTable');
            $('<input class="mt-4 btn btn-warning DeleteBtn" type="button" value="X">').appendTo("#new_tr" + rowNumber);
            $('<input class="mt-4 btn btn-info ChooseImageBtn" type="button" value="Choose Image" name"imgFile" disabled>').appendTo("#new_tr" + rowNumber);
            for (j = 0; j < objKeys.length; j++) {
                if (objKeys[j] == "ProductName" || objKeys[j] == "Price" || objKeys[j] == "Category") {

                    $('<td contentEditable="true"></td>').appendTo("#new_tr" + rowNumber);
                }
                else {
                    $('<td></td>').appendTo("#new_tr" + rowNumber);
                }
            }

            rowNumber += 1;
        });

        $('.DeleteBtn').unbind().click(function () {
            id = $(this).parent().attr('id');
            $('#' + id).remove();
            id = $(this).closest('tr').find('td:eq(0)').text();
            alert(id);
            ajaxCall("deleteById", id);
        });
    }

    function refreshTable(data) {
        Products = JSON.parse(data);
        objKeys = Object.keys(Products["outputTable"]["products"][0]);
        outputTable = Products["outputTable"]["products"];
        rowNumber = outputTable.length;

        for (i = 0; i < rowNumber; i++) {
            for (j = 0; j < objKeys.length; j++) {
                tempKeys = objKeys[j];
                tempValue = outputTable[i][tempKeys];
                $('#tr' + i).each(function () {
                    if (objKeys[j] == "fotograf") {
                        if (tempValue == "") {
                            $(this).find("td").eq(j).find("img").attr("src", "/images/DefaultPhoto.jpg");
                        }
                        else {
                            $(this).find("td").eq(j).find("img").attr("src", "/images/" + tempValue);
                        }
                    }
                    else {
                        $(this).find("td").eq(j).text(tempValue);
                    }
                });
            }
        }

        for (i = outputTable.length; i < rowNumber; i++) {
            $('#new_tr' + i).remove();
        }
    }

    $('#saveButton').unbind().click(function () {
        for (i = outputTable.length; i < rowNumber; i++) {
            for (j = 1; j < objKeys.length; j++) {
                Products[objKeys[j]] = $('#new_tr' + i).find('td:eq(' + j + ')').text();
            }
            ajaxCall("addByValues", JSON.stringify(Products));
        }
    });

    ajaxCall("getSession", null);
    ajaxCall("showProducts");
});
