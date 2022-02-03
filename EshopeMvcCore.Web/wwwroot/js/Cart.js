var CartController = function () {
    this.initialize = function () {
        loadData();
    },

    loadData = function () {
        var culture = $("#hidCulture").val();

        $.ajax({
            type: "GET",
            url: `/${culture}/cart/GetCartItems`,
            dataType : ''
        }).done(function (cart) {
            console.log(cart)
            var template = document.getElementById('template').innerHTML;
            
            var rendered = Mustache.render(template, cart);
            document.getElementById('cart-items-target').innerHTML = rendered;
        }).fail(function (err) {
            console.log(err)
        });
    }
}