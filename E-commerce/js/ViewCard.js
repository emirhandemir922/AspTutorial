$(document).ready(function () {
    var ProductName;

    function ajaxCall(type, value) {
        if (type == "removeShoppingProduct") {
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

                    window.location.reload();
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
    }

    $('.removeProduct').on("click", function () {
        var container = $(this).parent().parent();
        ProductName = container.find("img").attr("alt");

        ajaxCall("removeShoppingProduct", ProductName);
    });
});