var CartController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
    };

    function registerEvents() {
        btnPlusQuantityClick();
        btnMinusQuantityClick();
        btnRemoveQuantityClick();
    };

    function loadData() {
        var culture = $("#hidCulture").val();

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
}