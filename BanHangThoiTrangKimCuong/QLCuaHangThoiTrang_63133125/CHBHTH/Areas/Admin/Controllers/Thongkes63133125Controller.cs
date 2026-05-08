using QLCuaHangThoiTrang_63133125.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangThoiTrang_63133125.Areas.Admin.Controllers
{
    public class Thongkes63133125Controller : Controller
    {
        private QLbanhang db = new QLbanhang();
        // GET: Thongkes
        public ActionResult Index()
        {
            var donhangs = db.DonHangs.ToList();
            var u = Session["use"] as QLCuaHangThoiTrang_63133125.Models.TaiKhoan;
            if (u.PhanQuyen.TenQuyen == "Adminstrator")
            {
                var dataThongke = (from s in db.DonHangs
                                   join x in db.TaiKhoans on s.MaNguoiDung equals x.MaNguoiDung
                                   group s by s.MaNguoiDung into g
                                   select new ThongKes
                                   {
                                       TenNguoiDung = g.FirstOrDefault().TaiKhoan.HoTen,
                                       TongTien = g.Sum(x => x.TongTien),
                                       DienThoai = g.FirstOrDefault().TaiKhoan.DienThoai,
                                       SoLuong = g.Count()
                                   });
                var dataFinal = dataThongke.OrderByDescending(s => s.TongTien).Take(5).ToList();
                return View(dataFinal);
            }
            return RedirectPermanent("~/Home/Index");

         
        }
    }
}