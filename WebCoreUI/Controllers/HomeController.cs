using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreUI.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var result = _productService.GetAll();

            return View(result);
        }

        public IActionResult UrunEkle()
        {
            return View();
        }

        public JsonResult UrunEkleIslem(Product product)
        {
            var result = _productService.Add(product);
            return Json(result);
        }

        
        public IActionResult UrunSil(int id)
        {
            var p = _productService.GetById(id);
            var result = _productService.Delete(p.Data);
            return RedirectToAction("Index");
        }
    }
}
