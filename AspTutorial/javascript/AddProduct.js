$(document).ready(function () {
    var fd;
    var files;
    var fileName;
    var fileSize;
    var fileSource;
    var Images = {
        'idPhotos': null,
        'PhotoName': null,
        'PhotoSource': null,
        'PhotoSize': null,
        'Product_id': null
    }
    var Products = {
        'idProducts': null,
        'ProductName': null,
        'Category': null,
        'Price': null,
        'Photo': null,
        'Description': null
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
                    //we got the response
                    alert('Successfully called');

                    $('#imgShowCase').attr('src', './images/' + fileName);
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
                    getProductID();
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "addImage") {
            $.ajax({
                url: 'ProductService.asmx/AddImage',
                data: { 'values': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    updateProduct(Products.id);
                },
                error: function (response) {
                    alert(response);
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
                    Products = JSON.parse(response.all[0].innerHTML);
                    Products = Products["productsTable"]["products"][0];
                    if (typeof (files) == "undefined") {
                        alert("Please upload an image");
                        deleteProduct(Products.id);
                    }
                    else {
                        saveImage();
                    }
                },
                error: function (response) {
                    alert(response);
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
                    alert(response);
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
                    alert("Success");

                },
                error: function (response) {
                    alert(response);
                }
            });
        }
    }

    $('#fileInput').unbind().change(function (e) {
        fd = new FormData();
        files = $(this)[0].files;
        fileName = files[0].name;
        fileSource = "./images/" + fileName;
        fileSize = files[0].size;

        fd.append('file', files[0]);
        ajaxCall("uploadImage", fd);
    });

    $('#Category').change(function () {  
        if ($(this).val() != "Select category") {
            $('#saveProductInfo').prop("disabled", false);

            $('#saveProductInfo').on("click", function () {
                Products.idProducts = 0;
                Products.ProductName = $('#Name').val();
                Products.Description = $('#Description').val();
                Products.Category = $("#Category option:selected").text();
                Products.Price = $('#Price').val();

                ajaxCall("addProduct", JSON.stringify(Products));
            });
        }
    });

    function saveImage() {
        for (var member in Images) delete Images[member];
        // Check file selected or not
        fileName = "Image";
        var randomNumber = Math.floor(Math.random() * 1000 + 1);
        var randomLetters = Math.random().toString(36).substr(2, 3);
        fileName = randomLetters + fileName + randomNumber;
        Images["PhotoName"] = fileName;
        Images["PhotoSource"] = fileSource;
        Images["PhotoSize"] = fileSize;
        Images["Product_id"] = Products.id;

        if (files.length > 0) {
            ajaxCall("addImage", JSON.stringify(Images));
        } else {
            alert("Please select a file.");
        }
    }

    function getProductID() {
        ajaxCall("getProduct", JSON.stringify(Products));
    }

    function deleteProduct(id) {
        ajaxCall("deleteProduct", id);
    }

    function updateProduct(id) {
        Products.Photo = Images.PhotoSource;
        Products.idProducts = id;
        ajaxCall("updateProduct", JSON.stringify(Products));
    }
});