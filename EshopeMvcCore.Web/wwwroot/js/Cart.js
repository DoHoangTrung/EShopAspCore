var CartController = function () {
    var culture = $("#hidCulture").val();

    this.initialize = function () {
        loadData();
        registerEvents();
    };

    function registerEvents() {
        btnPlusQuantityClick();
        btnMinusQuantityClick();
        btnRemoveQuantityClick();
        UpdateCartSession();
    };

    function loadData() {

        $.ajax({
            type: "GET",
            url: `/${culture}/cart/GetCartItems`,
        }).done(function (cart) {
            console.log(cart)
            var template = document.getElementById('template').innerHTML;

            var rendered = Mustache.render(template, cart);
            document.getElementById('cart-items-target').innerHTML = rendered;
        }).fail(function (err) {
            console.log(err)
        });
    };

    function btnPlusQuantityClick() {
        $('body').on('click', '.btn-plus', function (e) {
            e.preventDefault();
            var culture = $("#hidCulture").val();

            var id = $(this).data('id');
            var quantity = parseInt($('#quantityProduct' + id).val());

            if (isNaN(quantity) || quantity < 0) {
                alert("quantity is an integer number greate than 0");
                return;
            }

            $.ajax({
                url: `/${culture}/cart/UpdateCart`,
                type: "POST",
                data: {
                    "id": id,
                    "quantity": quantity + 1
                },

            }).done(function (cart) {
                console.log(cart)
                var template = document.getElementById('template').innerHTML;

                var rendered = Mustache.render(template, cart);
                document.getElementById('cart-items-target').innerHTML = rendered;

            }).fail(function (err) {
                console.log(err)
            });
        })
    };

    function btnRemoveQuantityClick() {
        $('body').on('click', '.btn-remove', function (e) {
            e.preventDefault();
            var culture = $("#hidCulture").val();

            var id = $(this).data('id');

            $.ajax({
                url: `/${culture}/cart/UpdateCart`,
                type: "POST",
                data: {
                    "id": id,
                    "quantity": 0
                },

            }).done(function (cart) {
                console.log(cart)
                var template = document.getElementById('template').innerHTML;

                var rendered = Mustache.render(template, cart);
                document.getElementById('cart-items-target').innerHTML = rendered;

                CountCartItems();
            }).fail(function (err) {
                console.log(err)
            });
        })
    };

    function btnMinusQuantityClick() {
        $('body').on('click', '.btn-minus', function (e) {
            e.preventDefault();
            var culture = $("#hidCulture").val();

            var id = $(this).data('id');
            var quantity = parseInt($('#quantityProduct' + id).val());

            if (isNaN(quantity) || quantity < 0) {
                alert("quantity is an integer number greate than 0");
                return;
            }

            $.ajax({
                url: `/${culture}/cart/UpdateCart`,
                type: "POST",
                data: {
                    "id": id,
                    "quantity": quantity - 1
                },

            }).done(function (cart) {
                console.log(cart)
                var template = document.getElementById('template').innerHTML;

                var rendered = Mustache.render(template, cart);
                document.getElementById('cart-items-target').innerHTML = rendered;

                CountCartItems();
            }).fail(function (err) {
                console.log(err)
            });
        })
    };

    function CountCartItems() {
        $.get(`/${culture}/cart/countCartItem`, function (data) {
            $('.labelCartItemCount').text(`[${data}]`);
        })
    };

    function UpdateCartSession() {
        $('#btn-next').click(function (e) {
            e.preventDefault();

            var listProd = [];
            var stWrong = false;
            $('#cart-table tr').each(function () {
                var id = $(this).data('id');
                if (typeof id !== 'undefined') {
                    var quantity = parseInt($('#quantityProduct' + id).val());

                    //check int convert
                    if (isNaN(quantity)) {
                        alert('Số lượng không hợp lệ');
                        stWrong = true;
                        return;
                    }

                    if (quantity <= 0) {
                        alert('Số lượng không thể nhỏ hơn 1');
                        stWrong = true;
                        return;
                    }

                    listProd.push({
                        "Id": id,
                        "Quantity": quantity
                    })
                }
            });

            if (stWrong) return;

            if (listProd.length == 0) return;

            $.ajax({
                type: 'POST',
                url: `/${culture}/cart/updateCartSession`,
                data: {
                    "request": {
                        "Items": listProd
                    }
                },
                success: function (data, textStatus, xhr) {
                    //console.log(xhr.status);
                    window.location.href = `/${culture}/cart/checkout`;
                },
                error: function (jqXHR, error, errorThrown) {
                    alert(jqXHR.responseText)
                }
            })
        })
    }
}