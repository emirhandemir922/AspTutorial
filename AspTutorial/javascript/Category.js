$(document).ready(function () {
    var Products = {
        'idProducts': null,
        'ProductName': null,
        'Category': null,
        'Price': null,
        'Photo': null,
        'Description': null
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
    var rowNumber;
    var lastData;
    var id;
    var login;
    var admin;

    function ajaxCall(type, value) {
        if (type == "showProducts") {
            $.ajax({
                url: 'ProductService.asmx/ShowProducts',
                data: { 'category': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    createGrid(response.all[0].innerHTML);
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
    }

    function createGrid(data) {
        var last;
        outputTable = JSON.parse(data);

        if (outputTable["outputTable"]["products"].length == 0) {
            $('#Grid').append('<h3 class="text text-center">There is no product in this category</h3>');
        }
        else {
            objKeys = Object.keys(outputTable["outputTable"]["products"][0]);
            Products = outputTable["outputTable"]["products"];
            rowNumber = Products.length;

            for (i = 0; i < rowNumber; i++) {
                $('#Grid').append('<div class="col-md-3 mt-5"><div class="card"><div class="card-block text-center" id="' + Products[i].id + '"></div ></div ></div >');
                $('#' + Products[i].id + '').append('<img class="image" src="' + Products[i].Photo + '"/>');
                $('#' + Products[i].id).append('<h4 class="card-title"><a class="link-dark text-decoration-none">' + Products[i].ProductName + '</a></h4>');
                $('#' + Products[i].id).append('<h6 class="card-subtitle text-muted">' + Products[i].Category + '</h6>');
                $('#' + Products[i].id).append('<p class="card-text p-y-1">' + Products[i].Price + ' TL</p>');

                $('#' + Products[i].id).on("click", function () {
                    window.location.replace("Product.aspx?id=" + this.id);
                });
            }
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

    ajaxCall("showProducts", getUrlVars().Category);
});