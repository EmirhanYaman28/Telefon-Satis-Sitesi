using System;
using System.IO; 
using iPhone.Data;
using iPhone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Authorization;


namespace iPhone.Controllers
{
    public class TelefonlarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;
        public TelefonlarController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;

        }


        public IActionResult Index()
        {
            IEnumerable<Telefon> telList = _context.telefons;
            return View(telList);
        }




        public IActionResult Görüntüle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tlf = _context.telefons.Find(id);
            if (tlf == null)
            {
                return NotFound();
            }
            return View(tlf);
        }






        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateViewTelefon tel)

        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (tel.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + tel.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    tel.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Telefon newProduct = new Telefon
                {
                    Model = tel.Model,
                    Ekran = tel.Ekran,
                    İşlemci = tel.İşlemci,
                    Kamera = tel.Kamera,
                    Renk = tel.Renk,
                    Fiyatı = tel.Fiyatı,
                    PhotoPath = uniqueFileName

                };


                _context.telefons.Add(newProduct);
                _context.SaveChanges();
                TempData["SuccessMsg"] = tel.Model + " isimli ürün , ürün listesine eklendi";
                return RedirectToAction("Index");
            }

            return View();
        }



        public IActionResult Edit(int? id)
        {
            var product = _context.telefons.Find(id);

            CreateViewTelefon createViewProduct = new CreateViewTelefon
            {
                Model = product.Model,
                Ekran = product.Ekran,
                İşlemci = product.İşlemci,
                Kamera = product.Kamera,
                Renk = product.Renk,
                Fiyatı = product.Fiyatı,
                PhotoFileName = product.PhotoPath
            };

            if (product == null)
            {
                return NotFound();
            }
            return View(createViewProduct);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreateViewTelefon tel)
        {
            if (id != tel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string uniqueFileName = tel.PhotoFileName;
                if (tel.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + tel.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    tel.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Telefon updateProduct = new Telefon
                {
                    Id = tel.Id,
                    Model = tel.Model,
                    Ekran = tel.Ekran,
                    İşlemci = tel.İşlemci,
                    Kamera = tel.Kamera,
                    Renk = tel.Renk,
                    Fiyatı = tel.Fiyatı,
                    PhotoPath = uniqueFileName
                };

                _context.telefons.Update(updateProduct);
                _context.SaveChanges();
                TempData["SuccessMsg"] = updateProduct.Model + " isimli ürün, güncellendi";
                return RedirectToAction("Index");
            }
            return View(tel);
        }





        public IActionResult Delete(int? id)
        {
            var product = _context.telefons.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int? id)
        {
            var product = _context.telefons.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.telefons.Remove(product);
            _context.SaveChanges();
            TempData["SuccessMsg"] = product.Model + " isimli ürün, ürün listesinden silindi";
            return RedirectToAction("Index");
        }


    }
}
