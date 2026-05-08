using QLCuaHangThoiTrang_63133125.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCuaHangThoiTrang_63133125.Controllers
{
    public class danhmuc63133125Controller : Controller
    {
        // GET: Danhmuc
        QLbanhang db = new QLbanhang();
        // GET: Danhmuc
        public ActionResult danhmucpartial()
        {
            var danhmuc = db.LoaiHangs.ToList();
            return PartialView(danhmuc);

        }

    }
}