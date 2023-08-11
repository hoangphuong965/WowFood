$(document).ready(function () {
    $("body").on("click", ".btnDetail", function () {
        var code = $(this).data("code");
        $.ajax({
            url: '/admin/order/GetOrderDetail',
            type: 'GET',
            data: { code: code },
            success: function (res) {
                if (res.success) {
                    var code = res.order.Code;
                    var name = res.order.Customer.UserName;
                    var total = res.totalAmount;
                    var phone = res.order.Customer.Mobile;
                    var created = res.created;
                    var email = res.order.Customer.Email;
                    var status = "Chưa giao hàng";
                    var orderId = res.order.OrderId;

                    $("#madonhang").text(code);
                    $("#name").text(name);
                    $("#TotalAmount").text(total);
                    $("#Phone").text(phone);
                    $("#CreatedDate").text(created);
                    $("#Email").text(email);
                    $(".status").text(status);

                    GetOrderDetail(orderId);
                }
            }
        });
    });
});

function GetOrderDetail(orderid) {
    $.ajax({
        url: '/admin/order/Partial_SanPham',
        type: 'GET',
        data: { OrderId: orderid },
        success: function (res) {
            if (res.success) {
                var result = '';

                result += '<table class="table table-bordered">';
                result += '<thead>';
                result += '<tr>';
                result += '<th>#</th>';
                result += '<th>Tên sản phẩm</th>';
                result += '<th>Giá</th>';
                result += '<th>Số lượng</th>';
                result += '<th>Thành tiền</th>';
                result += '</tr>';
                result += '</thead>';
                result += '<tbody>';

                $.each(res.items, function (index, item) {
                    result += '<tr>';
                    result += '<td>' + (index + 1) + '</td>';
                    result += '<td>' + item.Food.FoodName + '</td>';
                    result += '<td>' + FormatNumber(item.Food.FoodPrice) + '</td>';
                    result += '<td>' + item.Quantity + '</td>';
                    result += '<td>' + FormatNumber(item.Price * item.Quantity) + '</td>';
                    result += '</tr>';
                });

                result += '</tbody>';
                result += '</table>';

            }
            $('#partialContainer').html(result);
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi ở đây
        }
    });
}

function FormatNumber(number) {
    return number.toLocaleString('vi-VN') + ' VND';
}
