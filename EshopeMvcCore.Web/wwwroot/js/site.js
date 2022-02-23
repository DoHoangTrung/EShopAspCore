// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('body').on('click', '.btn-add-cart-quantity', function (e) {
        e.preventDefault();
        const id = $(this).data('id');
        var culture = $("#hidCulture").val();
        var quantity = parseInt($('#quantity' + id).val());

        if (isNaN(quantity)) {
            alert('Số lượng không hợp lệ');
            return;
        }
        else if (quantity <= 0) {
            alert('Số lượng không thể nhỏ hơn 1');
            return;
        }


        var data = {
            "id": id,
            "quantity": quantity,
        };

        $.ajax({
            type: "POST",
            url: `/${culture}/cart/addToCart`,
            data: data,
        }).done(function (res) {
            $.get(`/${culture}/cart/countCartItem`, function (data) {
                $('.labelCartItemCount').text(`[${data}]`);
            })
            alert('Thêm vào giỏ hàng thành công');
        }).fail(function (err) {
            console.log(err)
        });
    });


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
                alert('Thêm sản phẩm thành công');
            })
        }).fail(function (err) {
            console.log(err)
        });
    })
});
