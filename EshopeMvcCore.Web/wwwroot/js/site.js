// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('body').on('click', '.btn-add-cart', function (e) {
    e.preventDefault();
    const id = $(this).data('id');
    var culture = $("#hidCulture").val();

    var data = {
        "id": id,
    };

    $.ajax({
        type: "POST",
        url: `/${culture}/cart/addToCart`,
        data: data,
    }).done(function (res) {
        $.get(`/${culture}/cart/countCartItem`, function (data) {
            $('.labelCartItemCount').text(`[${data}]`);
        })
    }).fail(function (err) {
        console.log(err)
    });
})