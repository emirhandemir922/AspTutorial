$(document).ready(function () {
    function ajaxCall(type, rating, product_id, order_id) {
        if (type == "rateProduct") {
            $.ajax({
                url: 'ProductService.asmx/RateProduct',
                data: { 'rating': rating, 'Product_id': product_id, 'Order_id': order_id },
                method: 'post',
                datatype: 'json',
                success: function (response) {
                    output = JSON.parse(response.all[0].innerHTML);

                    if (output.isError) {
                        if (output.message == "Redirect to Login") {
                            setCookie("relocation", "true");
                            setCookie("relocation-link", window.location.href);
                            setCookie("scrollPos", scrollPos);
                            window.location.replace("Login.aspx");
                        }
                        else {
                            alert("Something went wrong");
                        }
                    }
                    else if (output.message == "Successfull call") {
                        location.reload();
                    }
                },
                error: function (response) {
                    alert("RateProduct Error");
                    alert(response.message);
                }
            });
        }
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

    $('.ProductsTable tbody tr').each(function () {
        var rating = $(this).attr('class');

        if (rating > 0) {
            $(this).find('fieldset').hide();

            if (rating % 2 == 0) {
                $(this).find('.RatingImg').attr("src", "/img/RatingStars/" + (rating / 2) + ".jpg");
            }
            else {
                $(this).find('.RatingImg').attr("src", "/img/RatingStars/" + ((rating - 1) / 2) + "-half.jpg");
            }
        }
        else if (rating == 0) {
            $(this).find('.RatingImg').hide();
        }
        else {
            alert("Unexpected Error - You will be re-directed to homepage.");
            window.location.replace("HomePage.aspx");
        }
    });

    $('.star').on("click", function () {
        scrollPos = $(document).scrollTop();
        setCookie("scrollPos", scrollPos);
        productRating = $(this).attr('name');
        product_id = $(this).parent().parent().attr("name");
        order_id = $(this).parent().parent().parent().parent().attr('id');
        ajaxCall("rateProduct", productRating, product_id, order_id);
    });
});