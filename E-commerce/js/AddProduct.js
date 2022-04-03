$(document).ready(function () {
    var fd;
    var files;
    var fileName;
    var fileSize;
    var fileSource;
    var i = 0;
    var MiniImages = [];

    var MiniImage = {
        'idPhoto': null,
        'PhotoName': null,
        'PhotoSource': null,
        'PhotoSize': null,
        'Product_id': null
    }

    var CoverImage = {
        'idPhoto': null,
        'PhotoName': null,
        'PhotoSource': null,
        'PhotoSize': null,
        'Product_id': null
    }

    var Products = {
        'idProducts': null,
        'ProductName': null,
        'Category': null,
        'Brand': null,
        'Price': null,
        'Memory': null,
        'Screen': null,
        'Battery': null,
        'Processor': null,
        'Camera': null,
        'Photo': null,
        'Description': null,
        'Rating': null,
        'Rates': null,
        'Favorite': false,
        'SimilarProductNames': null
    }

    function ajaxCall(type, value) {
        if (type == "uploadImage") {
            $.ajax({
                url: 'fileUploader.ashx',
                data: value,
                method: 'post',
                contentType: false,
                processData: false,
                success: function (result) {
                    $('#imgShowCase').attr('src', '/img/products/' + CoverImage.PhotoName);
                },
                error: function (jqxhr, status, exception) {
                    alert('Exception:', exception);
                }
            });
        }
        else if (type == "uploadImageMini") {
            $.ajax({
                url: 'fileUploader.ashx',
                data: value,
                method: 'post',
                contentType: false,
                processData: false,
                success: function (result) {
                    $('#imgShowCaseMini' + i).attr('src', '/img/products/' + MiniImages[i].PhotoName);
                    i++;

                    if (i > 2) {
                        i = 0;
                    }
                },
                error: function (jqxhr, status, exception) {
                    alert('Exception:', exception);
                }
            });
        }
        else if (type == "addProduct") {
            $.ajax({
                url: 'ProductService.asmx/AddProduct',
                data: { 'values': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    if (JSON.parse(response.all[0].innerHTML).isError) {
                        alert("This product name already exists");
                    }
                    else {
                        getProductID();
                    }
                },
                error: function (response) {
                    alert("ss");
                }
            });
        }
        else if (type == "addCoverImage") {
            $.ajax({
                url: 'ProductService.asmx/AddImage',
                data: { 'values': JSON.stringify(value) },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    updateProduct(Products.id, value.PhotoName);
                },
                error: function (response) {
                    alert("kk");
                }
            });
        }
        else if (type == "addMiniImage") {
            $.ajax({
                url: 'ProductService.asmx/AddImage',
                data: { 'values': JSON.stringify(value) },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    saveMiniImages();
                    j++;
                },
                error: function (response) {
                    alert("yy");
                }
            });
        }
        else if (type == "getProduct") {
            $.ajax({
                url: 'ProductService.asmx/GetProductID',
                data: { 'values': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);
                    Products = output["productsTable"]["products"][0];
                    if (typeof (files) == "undefined") {
                        alert("Please upload an image");
                        deleteProduct(Products.id);
                    }
                    else {
                        saveCoverImage();
                        saveMiniImages();
                    }
                },
                error: function (response) {
                    alert("");
                }
            });
        }
        else if (type == "deleteProduct") {
            $.ajax({
                url: 'ProductService.asmx/DeleteProduct',
                data: { 'id': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {

                },
                error: function (response) {
                    alert("delete");
                }
            });
        }
        else if (type == "updateProduct") {
            $.ajax({
                url: 'ProductService.asmx/UpdateProduct',
                data: { 'values': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    window.location.replace("HomePage.aspx");
                },
                error: function (response) {
                    alert("update");
                }
            });
        }
    }

    $('#fileInput').unbind().change(function (e) {
        fd = new FormData();
        files = $(this)[0].files;
        CoverImage.PhotoName = files[0].name;
        CoverImage.PhotoSource = "/img/products/" + CoverImage.PhotoName;
        CoverImage.PhotoSize = files[0].size;

        fd.append('file', files[0]);
        ajaxCall("uploadImage", fd);
    });

    $('#fileInputMini').unbind().change(function (e) {
        fd = new FormData();
        files = $(this)[0].files;
        MiniImage.PhotoName = files[0].name;
        MiniImage.PhotoSource = "/img/products/" + MiniImage.PhotoName;
        MiniImage.PhotoSize = files[0].size;

        MiniImages.push(MiniImage);

        fd.append('file', files[0]);
        ajaxCall("uploadImageMini", fd);
    });

    $('#saveProductInfo').on("click", function () {
        Products.idProducts = 0;
        Products.ProductName = $('#Name').val();
        Products.Description = $('#Description').val();
        Products.Category = $('#Category option:selected').index();
        Products.Brand = $('#Brand option:selected').index();
        Products.Price = $('#Price').val();
        Products.Memory = $('#Memory option:selected').index();
        Products.Screen = $('#Screen').val();
        Products.Battery = $('#Battery').val();
        Products.Processor = $('#Processor').val();
        Products.Camera = $('#Camera').val();
        Products.Likes = 0;
        Products.Rating = 0;
        Products.Rates = 0;

        if (Products.ProductName == "" || Products.Description == "" || Products.Category == "" || Products.Brand == "" || Products.Price == "") {
            alert("You have to fill the parts with (*)");
        }
        else {
            ajaxCall("addProduct", JSON.stringify(Products));
        }
    });

    //$('.form-group').focusout(function () {
    //    if ($('#Name').val() != "" && $('#Description').val() != "" && $('#Category option:selected').index() != "" && $('#Brand option:selected').index() != "" && $('#Price').val() != "") {
    //        $('#fileInput').attr("disabled", false);
    //        $('#fileInputMini').attr("disabled", false);
    //    }
    //});

    $('.deleteImg').on("click", function () {
        $(this).prev().attr("src", "/img/DefaultProduct.jpg");
    });

    function saveCoverImage() {
        //var randomNumber = Math.floor(Math.random() * 1000 + 1);
        var randomLetters = Math.random().toString(36).substr(2, 3);
        fileName = CoverImage.PhotoName;
        fileName = randomLetters + fileName;
        CoverImage.PhotoName = fileName;
        CoverImage.Product_id = Products.id;

        if (files.length > 0) {
            ajaxCall("addCoverImage", CoverImage);
        } else {
            alert("Please Upload Pictures");
        }
    }

    function saveMiniImages() {
        fileName = MiniImages[j].PhotoName;
        MiniImages[j].PhotoName = MiniImages[j].PhotoName + j;
        MiniImages[j].Product_id = Products.id;

        if (files.length > 0 && j < 3) {
            ajaxCall("addMiniImage", MiniImages[j]);
        } else {
            alert("Please Upload Pictures");
        }
    }

    function getProductID() {
        ajaxCall("getProduct", JSON.stringify(Products));
    }

    function deleteProduct(id) {
        ajaxCall("deleteProduct", id);
    }

    function updateProduct(id, PhotoSource) {
        Products.Photo = CoverImage.PhotoName;
        Products.idProducts = id;
        ajaxCall("updateProduct", JSON.stringify(Products));
    }
});