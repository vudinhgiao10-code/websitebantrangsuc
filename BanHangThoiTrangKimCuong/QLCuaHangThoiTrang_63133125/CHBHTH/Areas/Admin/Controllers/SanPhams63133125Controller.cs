using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCuaHangThoiTrang_63133125.Models;
using PagedList;
using System.IO;

namespace QLCuaHangThoiTrang_63133125.Areas.Admin.Controllers

{
    public class SanPhams63133125Controller : Controller
    {
        private QLbanhang db = new QLbanhang();

        // GET: SanPhams
        public ActionResult Index(string searchString)
        {
            ViewBag.SearchString = searchString;

            var sanPhams = db.SanPhams.Include(s => s.LoaiHang).Include(s => s.NhaCungCap);

            // Filter by product name if searchString is not empty
            if (!string.IsNullOrEmpty(searchString))
            {
                sanPhams = sanPhams.Where(s => s.TenSP.Contains(searchString));
            }

            var u = Session["use"] as QLCuaHangThoiTrang_63133125.Models.TaiKhoan;
            if (u.PhanQuyen.TenQuyen == "Adminstrator")
            {
                return View(sanPhams.OrderByDescending(s => s.MaSP).ToList());
            }

            return RedirectPermanent("~/Home/Index");
        }


        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,GiaBan,SoLuong,MoTa,MaLoai,MaNCC,AnhSP")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu có ảnh được chọn
                if (sanPham.AnhSPFile != null && sanPham.AnhSPFile.ContentLength > 0)
                {
                    // Tạo tên file mới và đường dẫn lưu file
                    var fileName = Path.GetFileName(sanPham.AnhSPFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);

                    // Lưu file vào thư mục
                    sanPham.AnhSPFile.SaveAs(path);

                    // Lưu tên file vào thuộc tính AnhSP trong database
                    sanPham.AnhSP = fileName;
                }

                // Thêm sản phẩm vào cơ sở dữ liệu
                db.SanPhams.Add(sanPham);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai", sanPham.MaLoai);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai", sanPham.MaLoai);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,GiaBan,SoLuong,MoTa,MaLoai,MaNCC,AnhSP")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu có ảnh mới được chọn
                if (sanPham.AnhSPFile != null && sanPham.AnhSPFile.ContentLength > 0)
                {
                    // Xóa ảnh cũ nếu có
                    var oldPath = Path.Combine(Server.MapPath("~/Content/Images"), sanPham.AnhSP);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }

                    // Tạo tên file mới và đường dẫn lưu file
                    var fileName = Path.GetFileName(sanPham.AnhSPFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);

                    // Lưu file mới vào thư mục
                    sanPham.AnhSPFile.SaveAs(path);

                    // Cập nhật tên file vào database
                    sanPham.AnhSP = fileName;
                }

                // Cập nhật sản phẩm trong cơ sở dữ liệu
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai", sanPham.MaLoai);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            return View(sanPham);
        }
        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}