using System.Diagnostics;
using DapperCrud.Interfaces;
using DapperCrud.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DapperCrud.Controllers
{
    public class ProductsController : ControllerBase
    {
        private readonly IProduct productRepository;
        public ProductsController(IProduct product)
        {
            this.productRepository = product;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productRepository.Get();
            return Ok(products);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var product = await productRepository.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        

        //public IActionResult Create()
        //{
        //    return View();
        //}

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]ProductModel model)
        {
            if (ModelState.IsValid)
            {
                await productRepository.Add(model);
                return Ok("Data inserted");
            }
            return Ok();
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var product = await productRepository.Find(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProductModel model)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var product = await productRepository.Find(id);

            if (product == null)
            {
                return BadRequest();
            }
            await productRepository.Update(model);
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var product = await productRepository.Find(id);

            if (product == null)
            {
                return BadRequest();
            }
            return Ok(product);
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            var product = await productRepository.Find(id);
            await productRepository.Remove(product);
            return Ok();
        }
    }
}

    

