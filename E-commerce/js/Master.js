 $(document).ready(function () {
    var searchval;
    var productName;
    var container;
    var scrollPos;
    var productsList = [];

    var Products = {
        'idProduct': null,
        'ProductName': null,
        'Category': null,
        'Brand': null,
        'Price': null,
        'Photo': null,
        'Description': null,
        'Rating': null,
        'Rates': null,
        'Favorite': null
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

    function calcProductCount(products, name) {
        var count = 0;
        for (var i = 0; i < products.length; i++) {
            if (name == products[i].ProductName) {
                count++;
            }
        }

        return count;
    }

    function getCurrentProducts() {
        $(".row").find(".product").each(function () {
            Product_id = $(this).find(".product-item").data("id");
            productsIDList.push(Product_id);
        });
    }

    function createProductFavorites(products) {
        if (products.length < 10) {
            $('#FavoriteProductCount').text(products.length);
        }
        else {
            $('#FavoriteProductCount').text("10+");
        }

        for (var i = 0; i < products.length; i++) {
            $("#FavoriteProducts").append("<tr><td class='si-pic'><img src='" + products[i].Photo + "' alt='" + products[i].ProductName + "'/></td >"
                + "<td class='si-text'><div class='product-selected'><p>" + products[i].Price + " TL" + "</p><h6>" + products[i].ProductName + "</h6></div></td>"
                + "<td class='si-close-favorites'><i class='ti-close'></i></td></tr> ");
        }

        $(".si-close-favorites").on("click", function () {
            var productName = $(this).parent().find(".product-selected").find("h6").text();

            $(this).parent().parent().find("tr").remove();
            ajaxCall("removeProductFavorite", productName);
        });
    }

    function createShoppingCart(products) {
        var total = 0;
        var productCount = 0;
        var productName = null;

        products.sort((a, b) => (a.id > b.id) ? 1 : -1);
        $('#ShoppingCartProducts').empty();

        if (products.length < 6) {
            $('#ShoppingCartCount').text(products.length);
        }
        else {
            $('#ShoppingCartCount').text("5+");
        }

        for (var i = 0; i < products.length; i++) {
            productCount = 0;
            productCount = calcProductCount(products, products[i].ProductName);

            if (products[i].Description != "Low Stock!") {
                products[i].Description = "";
            }

            $("#ShoppingCartProducts").append("<tr><td class='si-pic'><img src='" + products[i].Photo + "' alt='" + products[i].ProductName + "'/></td >"
                + "<td class='si-text'><div class='product-selected'><p class='text-danger'>" + products[i].Description + "</p><p>" + products[i].Price + " TL</p><h6>" + products[i].ProductName + " X " + productCount + "</h6></div></td>"
                + "<td class='si-close-cart'><i class='ti-close'></i></td></tr> ");

            total += products[i].Price * productCount;

            i = i + productCount - 1;
        }

        $(".si-close-cart").on("click", function () {
            var productName = $(this).parent().find(".si-pic").find("img").attr("alt");

            $(this).parent().remove();
            products.pop(productName);
            setCookie("0", products);
            ajaxCall("removeShoppingCart", productName);
        });

        $('#ShopTotal').text(total + "TL");
    }

    function ajaxCall(type, value) {
        if (type == "addProductFavorite") {
            $.ajax({
                url: 'ProductService.asmx/AddProductFavorite',
                data: { 'ProductName': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);

                    if (output.message == "Redirect to Login") {
                        scrollPos = $(document).scrollTop();
                        setCookie("scrollPos", scrollPos);
                        setCookie("relocation", "true");
                        setCookie("relocation-link", window.location.href);
                        window.location.replace("Login.aspx");
                    }
                    else if (!output.isError) {
                        createProductFavorites(output.favoriteProductsList);
                        window.location.reload();
                    }
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "getProductFavorites") {
            $.ajax({
                url: 'ProductService.asmx/GetProductFavorites',
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].textContent);

                    if (output.isError && output.message != null) {
                        alert(output.message);
                    }
                    else {
                        createProductFavorites(output.favoriteProductsList);
                    }
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "removeProductFavorite") {
            $.ajax({
                url: 'ProductService.asmx/RemoveProductFavorite',
                data: { 'ProductName': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);

                    createProductFavorites(output.favoriteProductsList);
                    window.location.reload();
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "addShoppingCart") {
            $.ajax({
                url: 'ProductService.asmx/AddShoppingCart',
                data: { 'Product_id': value, 'Quantity': 1},
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);

                    if (output.isError && output.message != "continue") {
                        alert(output.message);
                    }

                    createShoppingCart(output.shoppingCartProducts);
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "getShoppingCart") {
            $.ajax({
                url: 'ProductService.asmx/GetShoppingCart',
                data: { '':  value},
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);

                    if (output.message == "continue") {
                        createShoppingCart(output.shoppingCartProducts);
                    }
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
        else if (type == "removeShoppingCart") {
            $.ajax({
                url: 'ProductService.asmx/RemoveShoppingCart',
                data: { 'ProductName': value },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);

                    if (output.isError && output.message != "continue") {
                        alert(output.message);
                    }

                    createShoppingCart(output.shoppingCartProducts);
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
    }

     $('#searchbox').autocomplete({
         minLength: 1,
         source: function (request, response) {
             $.ajax({
                 url: 'ProductService.asmx/Autocomplete',
                 data: {
                     'searchValue': request.term
                 },
                 method: 'post',
                 dataType: '',
                 success: function (data) {
                     output = JSON.parse(data.all[0].innerHTML);
                     products = output.filteredProducts;
                     response(products);
                 },
                 error: function (data) {
                     alert(JSON.parse(response.all[0].innerHTML));
                 }
             });
         },
         select: function (event, ui) {
             window.location.replace("Product.aspx?Name=" + ui.item.ProductName);
         }
     }).autocomplete("instance")._renderItem = function (ul, item) {
         return $("<li class='row autocomplete' style='width: 100%;'>")
             .append("<img class='col-sm-3' src='" + item.Photo + "' style='width: auto; height: 100px; object-fit: contain; align-items: center;' /><div class='col-sm-9'><h6>" + item.ProductName + "</h6><p>" + item.Brand + "</p></div>")
             .appendTo(ul);
     };

     $('#searchBtn').on("click", function () {
         window.location.replace("SearchProducts.aspx?Search=" + $('#searchbox').val());
    });

    $('.icon_heart_alt').each(function () {
        productName = $(this).attr("value");

        if ($(this).attr("value") == "True") { /*if the product is already in the favorites list btn-dark class will be added*/
            $(this).addClass("btn-dark");
            $(this).parent().css("top", "15px");
            $(this).parent().css("opacity", "1");
        }

        $(this).on("click", function () {
            container = $(this).parent().parent();
            productName = container.find("img").attr("alt");

            if ($(this).hasClass("btn-dark")) {
                $(this).removeClass("btn-dark");
                $('#FavoriteProducts').empty();

                ajaxCall("removeProductFavorite", productName);
            }
            else {
                $(this).addClass("btn-dark");
                $('#FavoriteProducts').empty();

                ajaxCall("addProductFavorite", productName);
            }
        });
    });

    $(".productImg").on("click", function () {
        productName = $(this).attr("alt");
        window.location.replace("Product.aspx?Name=" + productName);
    });

    $(".productName").on("click", function () {
        productName = $(this).text();
        window.location.replace("Product.aspx?Name=" + productName);
    });

    $(".addBagBtn").on("click", function () {
        container = $(this).parent().parent().parent();
        Product_id = container.parent().data("id");
        ajaxCall("addShoppingCart", Product_id);
    });

    ajaxCall("getProductFavorites", null);

    ajaxCall("getShoppingCart", null);
});