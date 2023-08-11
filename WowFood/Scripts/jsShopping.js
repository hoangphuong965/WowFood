$(document).ready(function () {
    ShowCount();
    $("body").on('click', ".addToCart", function (e) {
        e.preventDefault();
        var id = parseInt($(this).data("id"));
        $.ajax({
            url: '/ShoppingCart/AddToCart/',
            type: 'GET',
            data: { id },
            success: function (res) {
                if (res.Success) {
                    ShowCount();
                    toastr.success(res.msg);
                }
            }
        });
    });

    $('#myInput').on('change', function () {
        var value = parseInt($(this).val());
        if (value <= 0) {
            $(this).val(1);
        }
    });

    $('input[type="number"]').change(function () {
        // Lấy giá trị số lượng mới
        var quantity = $(this).val();
        // lấy food id
        var foodId = parseInt($(this).data("id"));
        var name = $(this).data("name");

        if (quantity < 1) {
            Delete(foodId, name);
            return;
        }

        $.ajax({
            url: '/ShoppingCart/UpdateQuantity/',
            type: 'POST',
            data: { foodId: foodId, quantity: quantity },
            success: function (res) {
                if (res.Success) {
                    $(".food_" + foodId).text(res.quantity);
                    $(".TotalPrice_" + foodId).text(res.totalPrice + " đ");
                    $(".tongtien").text(res.totalCart + " đ");
                    ShowCount();
                }
            }
        });
    })

    $("body").on("click", ".deletefood", function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        var name = $(this).data("name");
        Delete(id, name);
    });
})

function Delete(id, name) {
    var conf = confirm(`Bạn có muốn xóa ${name} khỏi giỏ hàng?`);
    if (conf == true) {
        $.ajax({
            url: '/ShoppingCart/Delete',
            type: 'GET',
            data: { foodId: id },
            success: function (rs) {

                if (rs.Success) {
                    $(".trow_" + id).remove();
                    $(".tongtien").text(rs.totalCart + " đ");
                    ShowCount();
                    toastr.success(rs.msg + " " + name);
                    if (rs.totalCart == 0) {
                        location.reload();
                    }
                }
            }
        });
    }
}

function ShowCount() {
    $.ajax({
        url: '/ShoppingCart/ShowCount',
        type: 'GET',
        success: function (rs) {
            $('.cart-icon').attr("data-count", rs.Count);
        }
    });
}