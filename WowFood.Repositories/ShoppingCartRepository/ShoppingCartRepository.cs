using System;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Text;
using System.Threading.Tasks;
using WowFood.DomainModels;
using WowFood.Repositories.Helper;

namespace WowFood.Repositories.ShoppingCartRepository
{
    public class ShoppingCartRepository
    {
        private readonly WowFoodDatabaseDbContext db;
        List<ShoppingCartItem> cart = HttpContext.Current.Session["Cart"] as List<ShoppingCartItem>;

        public ShoppingCartRepository(WowFoodDatabaseDbContext wowFoodDatabaseDbContext)
        {
            db = wowFoodDatabaseDbContext;
        }

        public void AddToCart(int FoodID)
        {
            if (cart == null)
            {
                //nếu cart chưa tồn tại, tạo mới một giỏ hàng
                cart = new List<ShoppingCartItem>();
            }

            // kiểm tra món ăn đã tồn tại trong giỏ hàng chưa
            var existingItem = cart.FirstOrDefault(item => item.FoodId == FoodID);
            if (existingItem != null)
            {
                // Nếu món ăn đã tồn tại trong giỏ hàng, tăng số lượng lên 1
                existingItem.Quantity++;
                existingItem.TotalPrice = existingItem.Quantity * existingItem.Price;
            }
            else
            {
                // Nếu món ăn chưa tồn tại trong giỏ hàng, thêm mới một món ăn vào giỏ hàng
                var food = GetFoodById(FoodID); // Hàm này để lấy thông tin món ăn từ cơ sở dữ liệu hoặc bất kỳ nguồn dữ liệu nào khác
                if (food != null)
                {
                    var newItem = new ShoppingCartItem
                    {
                        FoodId = food.FoodID,
                        FoodName = food.FoodName,
                        ProductImg = food.Photo,
                        Quantity = 1,
                        Price = food.FoodPrice,
                        TotalPrice = food.FoodPrice
                    };
                    cart.Add(newItem);
                }
            }
            // Lưu giỏ hàng vào session
            HttpContext.Current.Session["Cart"] = cart;
        }

        public void ClearCart()
        {
            cart.Clear();
        }


        private Food GetFoodById(int foodId)
        {
            // Lấy thông tin món ăn từ cơ sở dữ liệu dựa trên foodId
            // Code để lấy thông tin món ăn từ cơ sở dữ liệu
            var food = db.Foods.FirstOrDefault(x => x.FoodID == foodId);
            return food;
        }

        public int GetTotalQuantity()
        {
            return cart.Sum(x => x.Quantity);
        }

        public decimal GetTotalCart()
        {
            return cart.Sum(x => x.TotalPrice);
        }

        public void UpdateQuantity(int foodId, int quantity)
        {
            var Food = cart.FirstOrDefault(c => c.FoodId == foodId);
            if(Food != null)
            {
                Food.Quantity = quantity;
                Food.TotalPrice = Food.Price * Food.Quantity;
            }
        }

        public void Delete(int foodId)
        {
            var Food = cart.FirstOrDefault(c => c.FoodId == foodId);
            if (Food != null)
            {
                cart.Remove(Food);
            }
        }


        //==================================================================

        public void CreateOrder(Order order, int userID, StringBuilder filePathAdmin, StringBuilder filePathCustomer)
        {
            order.CreatedDate = DateTime.Now;
            order.CustomerId = userID;
            order.TotalAmount = GetTotalCart();
            Random rd = new Random();
            order.Code = "WowFood" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
            
            order.OrderDetails = new List<OrderDetails>();
            foreach (var item in cart)
            {
                var OrderDetails = new OrderDetails
                {
                    FoodId = item.FoodId,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                order.OrderDetails.Add(OrderDetails);
            }
            if(GetTotalCart() == 0)
            {
                return;
            }
            db.Orders.Add(order);
            db.SaveChanges();

            // gui mail
            var strSanPham = "";
            var thanhtien = decimal.Zero;

            var user = db.Orders.Where(o => o.CustomerId == userID).Select(o => o.Customer).FirstOrDefault();

            foreach(var sp in cart)
            {
                strSanPham += "<tr>";
                strSanPham += "<td>" + sp.FoodName + "</td>";
                strSanPham += "<td>" + sp.Quantity + "</td>";
                strSanPham += "<td>" + Format.FormatNumber(sp.TotalPrice, 0) + "</td>";
                strSanPham += "</tr>";
                thanhtien += sp.Price * sp.Quantity;
            }

            // admin
            filePathAdmin = filePathAdmin.Replace("{{MaDon}}", order.Code);
            filePathAdmin = filePathAdmin.Replace("{{SanPham}}", strSanPham);
            filePathAdmin = filePathAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
            filePathAdmin = filePathAdmin.Replace("{{TenKhachHang}}", user.UserName);
            filePathAdmin = filePathAdmin.Replace("{{Phone}}", user.Mobile);
            filePathAdmin = filePathAdmin.Replace("{{Email}}", user.Email);
            filePathAdmin = filePathAdmin.Replace("{{DiaChiNhanHang}}", order.Address);
            filePathAdmin = filePathAdmin.Replace("{{ThanhTien}}", Format.FormatNumber(thanhtien, 0));
            SendMail.Send("WowFood", "Đơn hàng mới #" + order.Code, filePathAdmin.ToString(), WebConfigurationManager.AppSettings["EmailAdmin"]);

            // customer
            filePathCustomer = filePathCustomer.Replace("{{MaDon}}", order.Code);
            filePathCustomer = filePathCustomer.Replace("{{SanPham}}", strSanPham);
            filePathCustomer = filePathCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
            filePathCustomer = filePathCustomer.Replace("{{TenKhachHang}}", user.UserName);
            filePathCustomer = filePathCustomer.Replace("{{Phone}}", user.Mobile);
            filePathCustomer = filePathCustomer.Replace("{{Email}}", user.Email);
            filePathCustomer = filePathCustomer.Replace("{{DiaChiNhanHang}}", order.Address);
            filePathCustomer = filePathCustomer.Replace("{{ThanhTien}}", Format.FormatNumber(thanhtien, 0));
            SendMail.Send("WowFood", "Đơn hàng  #" + order.Code, filePathCustomer.ToString(), user.Email);
        }
    }
}
