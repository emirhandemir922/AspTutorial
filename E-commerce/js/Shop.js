$(document).ready(function () {
    var Product_id;
    var productsIDList = [];
    var productCount;
    var pageCount;
    var oldURL;
    var newURL;

    var filters = {
        'brand': "",
        'minPrice': null,
        'maxPrice': null,
        'memory': null,
        'minScreen': null,
        'maxScreen': null
    }

    function getCurrentProducts() {
        $(".row").find(".product").each(function () {
            Product_id = $(this).find(".product-item").data("id");
            productsIDList.push(Product_id);
        });
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

    function eraseCookie(key) {
        var keyValue = getCookie(key);
        setCookie(key, keyValue, '-1');
    }

    scrollPos = getCookie("scrollPos");

    if (scrollPos == null) {
        scrollPos = 0;
        setCookie("scrollPos", scrollPos);
        $('html, body').animate({
            scrollTop: scrollPos + 'px'
        }, 150);
    }
    else {
        scrollPos = getCookie("scrollPos");
        $('html, body').animate({
            scrollTop: scrollPos + 'px'
        }, 150);

        scrollPos = 200; /*default value for the page*/
    }

    function param(name) {
        return (location.search.split(name + '=')[1] || '').split('&')[0]; /*get category name from the link*/
    }

    sort = param("sort");
    category = param("category");
    page = param("page");
    filters.brand = param("brand");
    filters.minPrice = param("minPrice");
    filters.maxPrice = param("maxPrice");
    filters.memory = param("memory");
    filters.minScreen = param("minScreen");
    filters.maxScreen = param("maxScreen");

    if (sort != null || sort != "") {
        $('select .sorting option:contains("Sort Products")').text("xx");
    }

    $("select.sorting").change(function () {
        if (sort != "") {
            selectedSorting = $(this).children("option:selected").val();

            oldURL = window.location.href;
            let url = new URL(oldURL);
            url.searchParams.set("sort", selectedSorting);

            newURL = url.href;
            window.location.replace(newURL);
        }
        else {
            selectedSorting = $(this).children("option:selected").val();

            oldURL = window.location.href;
            newURL = oldURL + "&sort=" + selectedSorting;
            window.history.pushState({ path: newURL }, "", newURL);
            location.reload();
        }
    });

    $.each(filters, function (key, value) {
        if (key != null || key != "") {
            $(".filter-widget").each(function () {
                if ($(this).css("display") != "none") { /*if statement to check if the category has the filter or not*/
                    if ($(this).hasClass("brand") && key == "brand") {
                        $(".brandMultipleCheckbox").each(function () {
                            const brands = filters.brand.split("-");

                            container = $(this).parent();
                            for (var i = 0; i < brands.length; i++) {
                                if (container.find("label").data("brand") == brands[i]) {
                                    $(this).prop('checked', true);
                                }
                            }
                        });
                    }
                    else if ($(this).hasClass("price") && key == "minPrice") {
                        $("#minamount").val(value);
                        minamount = (value / 24000) * 100 + '%';

                        $("#slider-min").css('left', minamount);
                    }
                    else if ($(this).hasClass("price") && key == "maxPrice") {
                        $("#maxamount").val(value);
                        maxamount = (value / 24000) * 100 + '%';

                        $("#slider-max").css('left', maxamount);
                    }
                    else if ($(this).hasClass("memory") && key == "memory") {
                        $(".memoryMultipleCheckbox").each(function () {
                            const memories = filters.memory.split("-");

                            container = $(this).parent();
                            for (var i = 0; i < memories.length; i++) {
                                if (container.find("label").data("memory") == memories[i]) {
                                    $(this).prop("checked", true);
                                }
                            }
                        });
                    }
                    else if ($(this).hasClass("screen") && (key == "minScreen" || key == "maxScreen")){
                        $('.screenCheckbox').each(function () {
                            container = $(this).parent();

                            if ((container.find("label").data("minscreen") == filters.minScreen) && (container.find("label").data("maxscreen") == filters.maxScreen)) {
                                $(this).prop("checked", true);
                            }
                        });
                    }
                }
            });
        }
    });

    if (page == "" ) {
        page = 1;
    }

    page = parseInt(page);
    productCount = $('#ProductsCount').text();

    switch (category) { /*switch statement for all the categories*/
        case "Main":
            //$(".tags").hide();
            break;
        case "Smartphones":
            break;
        case "Accessories":
            break;
        case "Bluetooth Headphones":
            break;
        case "Gaming":
            break;
    }

    $(".filterBtn").on("click", function () {
        page = 1;

        $(".filter-widget").each(function () {
            if ($(this).css("display") != "none") { /*if statement to check if the category has the filter or not*/
                if ($(this).hasClass("brand")) {
                    checkedCount = 0;
                    filters.brand = "";

                    $(".brandMultipleCheckbox").each(function () {
                        if ($(this).is(":checked")) {
                            container = $(this).parent();
                            filters.brand += container.find("label").data("brand") + "-";
                        }
                    });

                    if (filters.brand == null || filters.brand == "") {
                        filters.brand = "Main";
                    }
                }
                else if ($(this).hasClass("price")) {
                    filters.minPrice = $("#minamount").val();
                    filters.maxPrice = $("#maxamount").val();

                    if (filters.minPrice == null || filters.minPrice == "") {
                        filters.minPrice = 0;
                    }

                    if (filters.maxPrice == null || filters.maxPrice == "") {
                        filters.maxPrice = 99999;
                    }
                }
                else if ($(this).hasClass("memory")) {
                    filters.memory = "";

                    $(".memoryMultipleCheckbox").each(function () {
                        if ($(this).is(":checked")) {
                            container = $(this).parent();
                            filters.memory += container.find("label").data("memory") + "-";
                        }
                    });
                }
                else if ($(this).hasClass("screen")) {
                    $(".screenCheckbox").each(function () {
                        if ($(this).is(":checked")) {
                            container = $(this).parent();
                            filters.minScreen = container.find("label").data("minscreen");
                            filters.maxScreen = container.find("label").data("maxscreen");
                        }
                    });

                    if (filters.minScreen == null || filters.minScreen == "") {
                        filters.minScreen = 0;
                    }

                    if (filters.maxScreen == null || filters.maxScreen == "") {
                        filters.maxScreen = 99;
                    }
                }
            }
        });

        if (param("brand") == "") {
            oldURL = window.location.href;
            let url = new URL(oldURL);
            url.searchParams.set("page", page);

            newURL = url + "&brand=" + filters.brand + "&minPrice=" + filters.minPrice + "&maxPrice=" + filters.maxPrice + "&memory=" + filters.memory + "&minScreen=" + filters.minScreen + "&maxScreen=" + filters.maxScreen;
            window.history.pushState({ path: newURL }, "", newURL);
            window.location.reload();
        }

        else {
            oldURL = window.location.href;
            let url = new URL(oldURL);
            url.searchParams.set("page", page);
            url.searchParams.set("brand", filters.brand);
            url.searchParams.set("minPrice", filters.minPrice);
            url.searchParams.set("maxPrice", filters.maxPrice);
            url.searchParams.set("memory", filters.memory);
            url.searchParams.set("minScreen", filters.minScreen);
            url.searchParams.set("maxScreen", filters.maxScreen);

            newURL = url.href;
            window.location.replace(newURL);
        }
    });

    $(".pagelink-numeric").each(function () {
        pageNumber = $(this).text(); //numbers 0 to 5 
        pageNumber = parseInt(pageNumber);
        productCount = parseInt(productCount);
        pageCount = productCount / 9;
        pageCount = Math.ceil(pageCount);

        if (page > 3) {
            if (pageNumber == 1) {
                $(this).addClass("btn-dark");
                pageNumber = page - 2;
                $(this).text(pageNumber);
            }
            else if (pageNumber == 2) {
                $(this).addClass("btn-dark");
                pageNumber = page - 1;
                $(this).text(pageNumber);
            }
            else if (pageNumber == 3) {
                $(this).addClass("btn-primary");
                $(this).text(page);
            }
            else if (pageNumber == 4) {
                $(this).addClass("btn-dark");
                pageNumber = page + 1;
                $(this).text(pageNumber);
            }
            else if (pageNumber == 5) {
                $(this).addClass("btn-dark");
                pageNumber = page + 2;
                $(this).text(pageNumber);
            }

            if (pageNumber > pageCount) {
                $(this).remove();
            }
        }
        else {
            if (page == pageNumber) {
                $(this).addClass("btn-primary");
            }
            else {
                if (pageNumber > pageCount) {
                    $(this).attr("disabled", true);
                }

                $(this).addClass("btn-dark");
            }
        }
    });

    $(".pagelink-numeric").on("click", function () {
        pageNumber = $(this).text();
        page = pageNumber;
        oldURL = window.location.href;
        let url = new URL(oldURL);
        url.searchParams.set("page", page);

        newURL = url.href;
        window.history.pushState({ path: newURL }, "", newURL);
        location.reload();
    });

    $(".pagelink-next").on("click", function () {
        page += 1;

        if (page > pageCount) {
            location.reload();
        }
        else {
            oldURL = window.location.href;
            let url = new URL(oldURL);
            url.searchParams.set("page", page);

            newURL = url.href;
            window.history.pushState({ path: newURL }, "", newURL);
            location.reload();
        }
    });

    $(".pagelink-previous").on("click", function () {
        page -= 1;

        if (page == 0) {
            location.reload();
        }
        else {
            oldURL = window.location.href;
            let url = new URL(oldURL);
            url.searchParams.set("page", page);

            newURL = url.href;
            window.history.pushState({ path: newURL }, "", newURL);
            location.reload();
        }
    });

    $(".pagelink-first").on("click", function () {
        page = 1;

        oldURL = window.location.href;
        let url = new URL(oldURL);
        url.searchParams.set("page", page);

        newURL = url.href;
        window.history.pushState({ path: newURL }, "", newURL);
        location.reload();
    });

    $(".pagelink-last").on("click", function () {
        page = pageCount;

        oldURL = window.location.href;
        let url = new URL(oldURL);
        url.searchParams.set("page", page);

        newURL = url.href;
        window.history.pushState({ path: newURL }, "", newURL);
        location.reload();
    });
});