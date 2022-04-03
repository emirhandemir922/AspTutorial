$(document).ready(function () {
    var newCommentWLike = {
        'id': null,
        'User_id': null,
        'Likes': null,
        'Dislikes': null,
        'Liked': null,
        'Disliked': null
    }

    var container;
    var dislike_btn;
    var like_btn;
    var liked;
    var scrollPos;
    var ProductName = $('.productName').text();
    var Product_id = $('.productID').text();

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

    function calcProductCount(products, name) {
        var count = 0;
        for (var i = 0; i < products.length; i++) {
            if (name == products[i].ProductName) {
                count++;
            }
        }

        return count;
    }

    function createShoppingCartProducts(products) {
        var total = 0;
        var productCount = 0;

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

            $("#ShoppingCartProducts").append("<tr><td class='si-pic'><img src='" + products[i].Photo + "' alt='" + products[i].ProductName + "'/></td >"
                + "<td class='si-text'><div class='product-selected'><p>" + products[i].Price + "</p><h6>" + products[i].ProductName + " X " + productCount + "</h6></div></td>"
                + "<td class='si-close-cart'><i class='ti-close'></i></td></tr> ");

            total += products[i].Price * productCount;

            i = i + productCount - 1;
        }

        $(".si-close-cart").on("click", function () {
            var CartProductName = $(this).parent().find(".si-pic").find("img").attr("alt");

            $(this).parent().remove();
            products.pop(CartProductName);
            setCookie("0", products);
            ajaxCall("removeShoppingCart", CartProductName);
        });

        $('#ShopTotal').text(total + "TL");
    }

    scrollPos = getCookie("scrollPos");
    newComment = getCookie("comment");

    if (scrollPos == null) {
        scrollPos = 0;
        setCookie("scrollPos", scrollPos);
        $('html, body').animate({
            scrollTop: scrollPos + 'px'
        },150);
    }
    else {
        scrollPos = getCookie("scrollPos");
        $('html, body').animate({
            scrollTop: scrollPos + 'px'
        }, 150);
        setCookie("scrollPos", 0);
    }

    if (newComment != null) {
        $('#CommentArea').val(newComment);
    }

    function ajaxCall(type, ...values) {
        if (type == "addComment") {
            $.ajax({
                url: 'ProductService.asmx/AddComment',
                data: { 'comment': values[0], 'Product_id': values[1] },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);

                    if (output.isError) {
                        switch (output.message) {
                            case "relocate to login":
                                setCookie("relocation", "true");
                                setCookie("relocation-link", window.location.href);
                                setCookie("scrollPos", scrollPos);
                                window.location.replace("Login.aspx");
                                break;
                            case "Please edit your previously made comment instead of writing a new one":
                                alert("Please edit your previously made comment instead of writing a new one");
                                eraseCookie("comment");
                                break;
                            case "Something went wrong with database, please try again later":
                                alert("Something went wrong with database, please try again later");
                                eraseCookie("comment");
                                break;
                            case "You have to purchase the product or wait for the shipment to be delivered before making a comment":
                                alert("You have to purchase the product or wait for the shipment to be delivered before making a comment");
                                eraseCookie("comment");
                                break;
                        }
                    }
                    else {
                        window.location.reload();
                        setCookie("scrollPos", scrollPos);
                    }
                },
                error: function (response) {
                    alert("AddComment");
                    alert(response.message);
                }
            });
        }
        else if (type == "commentLike") {
            $.ajax({
                url: 'ProductService.asmx/CommentLike',
                data: {'values': values[0]},
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);

                    if (output.isError){
                        switch (output.message) {
                            case "relocate to login":
                                setCookie("relocation", "true");
                                setCookie("relocation-link", window.location.href);
                                setCookie("scrollPos", scrollPos);
                                window.location.replace("Login.aspx");
                                break;
                            case "unlock dislike button":
                                container = $('[data-id="' + newCommentWLike.id + '"]').find('.avatar-text');
                                like_btn = container.find('img.CommentLikeBtn');
                                dislike_btn = container.find('img.CommentDislikeBtn');

                                like_btn.attr("src", "img/thumbs-up-clicked.png");
                                like_btn.prev().text(newCommentWLike.Likes);
                                like_btn.attr("data-liked", "1");
                                like_btn.addClass("clicked");

                                dislike_btn.attr("src", "img/thumbs-down.png");
                                dislike_btn.removeClass("clicked");
                                dislike_btn.attr("data-disliked", "0");

                                if (newCommentWLike.Dislikes == 0) {
                                    dislike_btn.prev().text(newCommentWLike.Dislikes);
                                }
                                else {
                                    dislike_btn.prev().text(newCommentWLike.Dislikes - 1);
                                }
                                break;
                            //default:
                            //    setCookie('id', output.message);
                            //    setCookie('link', window.location.href);
                            //    window.location.replace("Login.aspx");
                            //    break;
                        }
                    }
                },
                error: function (response) {
                    alert("CommentLike");
                    alert(response.message);
                }
            });
        }
        else if (type == "commentDislike") {
            $.ajax({
                url: 'ProductService.asmx/CommentDislike',
                data: { 'values': values[0] },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);

                    if (output.isError) {
                        switch (output.message) {
                            case "relocate to login":
                                setCookie("relocation", "true");
                                setCookie("relocation-link", window.location.href);
                                setCookie("scrollPos", scrollPos);
                                window.location.replace("Login.aspx");
                                break;
                            case "unlock like button":
                                container = $('[data-id="' + newCommentWLike.id + '"]').find('.avatar-text');
                                like_btn = container.find('img.CommentLikeBtn');
                                dislike_btn = container.find('img.CommentDislikeBtn');

                                like_btn.attr("src", "img/thumbs-up.png");
                                like_btn.attr("data-liked", "0");
                                like_btn.removeClass("clicked");

                                if (newCommentWLike.Likes == 0) {
                                    like_btn.prev().text(newCommentWLike.Likes);
                                }
                                else {
                                    like_btn.prev().text(newCommentWLike.Likes - 1);
                                }

                                dislike_btn.attr("src", "img/thumbs-down-clicked.png");
                                dislike_btn.attr("data-disliked", "1");
                                dislike_btn.prev().text(newCommentWLike.Dislikes);
                                dislike_btn.addClass("clicked");
                                break;
                            //default:
                            //    setCookie('id', output.message);
                            //    setCookie('link', window.location.href);
                            //    window.location.replace("Login.aspx");
                            //    break;
                        }
                    }
                },
                error: function (response) {
                    alert("CommentDislike");
                    alert(response.message);
                }
            });
        }
        else if (type == "addShoppingCart") {
            $.ajax({
                url: 'ProductService.asmx/AddShoppingCart',
                data: { 'Product_id': values[0], 'Quantity': values[1] },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);

                    if (output.isError && output.message != "continue") {
                        alert(output.message);
                    }

                    createShoppingCartProducts(output.shoppingCartProducts);
                },
                error: function (response) {
                    alert("Something went wrong");
                    alert(response.message);
                }
            });
        }
        else if (type == "removeShoppingCart") {
            $.ajax({
                url: 'ProductService.asmx/RemoveShoppingCart',
                data: { 'ProductName': values[0] },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);

                    if (output.isError && output.message != "continue") {
                        alert(output.message);
                    }

                    createShoppingCartProducts(output.shoppingCartProducts);
                },
                error: function (response) {
                    alert("RemoveShoppingCart");
                    alert(response.message);
                }
            });
        }
        else if (type == "checkStock") {
            $.ajax({
                url: 'ProductService.asmx/CheckStock',
                data: { 'ProductName': values[0] },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);

                    if (output.isError) {
                        alert(output.message);
                    }
                    else {
                        addCart(output.message);
                    }
                },
                error: function (response) {
                    alert("CheckStock");
                    alert(response.message);
                }
            });
        }
    }

    relocation = getCookie("relocation");

    function addCart(Stock) {
        Quantity = $('#Quantity').val();

        Quantity = parseInt(Quantity);
        Stock = parseInt(Stock);

        if (Quantity > Stock) {
            $('#QuantityError').text("You have exceeded current stock your order quantity has been adjusted for the maximum number for you");
            $('#Quantity').val(Stock);
        }
        else {
            ajaxCall("addShoppingCart", Product_id, Quantity);
        }
    }

    $('#AddComment').on("click", function () {
        scrollPos = $(document).scrollTop();
        setCookie("scrollPos", scrollPos);

        newComment = $('#CommentArea').val();
        setCookie("comment", newComment);
        ajaxCall("addComment", newComment, Product_id);
    });

    $('#addCartBtn').on("click", function () {
        ajaxCall("checkStock", ProductName);
    });

    function createLikes() {
        $('.avatar-text img').each(function () {
            if ($(this).data("liked") === 1) {
                liked = true;
                $(this).attr("src", "img/thumbs-up-clicked.png");
                $(this).addClass("clicked");
                $(this).attr("disabled", "true");

            }
            else if ($(this).data("liked") === 0) {
                $(this).on("click", function () {
                    if (!$(this).hasClass("clicked") && !($(this).attr("disabled") == "disabled") && !liked) {

                        $(this).attr("disabled", "true");
                        newCommentWLike.id = $(this).parent().parent().data("id");
                        newCommentWLike.Comment = $(this).parent().find(".comment").text();
                        newCommentWLike.Commentor = $(this).parent().find("h5").text();
                        newCommentWLike.User_id = 0;
                        newCommentWLike.Likes = parseInt($(this).parent().find("span.like-value").text()) + 1;
                        newCommentWLike.Dislikes = parseInt($(this).parent().find("span.dislike-value").text());
                        newCommentWLike.Liked = 1;
                        newCommentWLike.Disliked = 0;

                        scrollPos = $(document).scrollTop();
                        setCookie("scrollPos", scrollPos);
                        ajaxCall("commentLike", JSON.stringify(newCommentWLike));
                    }
                });
            }

            if ($(this).data("disliked") === 1) {
                $(this).attr("src", "img/thumbs-down-clicked.png");
                $(this).addClass("clicked");
                liked = true;
                $(this).attr("disabled", "true");

            }
            else if ($(this).data("disliked") === 0) {
                $(this).on("click", function () {
                    if (!$(this).hasClass("clicked") && !($(this).attr("disabled") == "disabled") && !liked) {

                        $(this).attr("disabled", "true");
                        newCommentWLike.id = $(this).parent().parent().data("id");
                        newCommentWLike.Comment = $(this).parent().find(".comment").text();
                        newCommentWLike.Commentor = $(this).parent().find("h5").text();
                        newCommentWLike.User_id = 0;
                        newCommentWLike.Likes = parseInt($(this).parent().find("span.like-value").text());
                        newCommentWLike.Dislikes = parseInt($(this).parent().find("span.dislike-value").text()) + 1;
                        newCommentWLike.Liked = 0;
                        newCommentWLike.Disliked = 1;

                        scrollPos = $(document).scrollTop();
                        setCookie("scrollPos", scrollPos);
                        ajaxCall("commentDislike", JSON.stringify(newCommentWLike));
                    }
                });
            }
        });
    }

    createLikes();
});