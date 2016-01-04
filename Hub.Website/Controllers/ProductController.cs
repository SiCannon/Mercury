using System.Web.Mvc;
using Hub.Domain.Abstract;
using Hub.Domain.Entity;
using Hub.Domain.Service;
using Hub.Website.Models;

namespace Hub.Website.Controllers
{
    public class ProductController : Controller
    {
        public ViewResult List()
        {
            var viewModel = new ProductListViewModel
            {
                Products = repo.ListAll()
            };

            return View(viewModel);
        }

        public ViewResult Edit(int? id)
        {
            return View(repo.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Save(product);
                return RedirectToAction("List");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Delete(int? id)
        {
            return View(repo.GetById(id));
        }

        [HttpPost]
        public ActionResult Delete(Product product)
        {
            repo.Delete(product);
            return RedirectToAction("List");
        }

        IProductService repo = new ProductService();
    }
}
