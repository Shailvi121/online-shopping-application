using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Online_Shopping_Application.API.Models;
using Online_Shopping_Application.Areas.Admin.Model;
using Online_Shopping_Application.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shopping_Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly HttpAPIWrapper _httpApiWrapper;
        private readonly IConfiguration _configuration;

        public CategoryController(IConfiguration configuration, HttpAPIWrapper httpApiWrapper)
        {
            _httpApiWrapper = httpApiWrapper;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var endpoint = Constants.Category.GetAllCategories;
            var response = await _httpApiWrapper.GetAsync<List<UICategoryViewModel>>(endpoint);

            if (response.IsSuccess)
            {
                var categories = response.data;
                return View(categories);
            }
            else
            {
                return View("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var endpoint = Constants.Category.GetCategoryById;
            var response = await _httpApiWrapper.GetByIdAsync<UICategoryViewModel>(endpoint, id);
            
           
                return Json(response);
            
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UICategoryViewModel category)
        {
            var endpoint = Constants.Category.Create;
            if (ModelState.IsValid)
            {
                var response = await _httpApiWrapper.CreateAsync(category, endpoint);

                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var endpoint = Constants.Category.GetCategoryById;
            var response = await _httpApiWrapper.GetByIdAsync<UICategoryViewModel>(endpoint, id);

            if (response.IsSuccess)
            {
                var category = response.data;
                return View(category);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UICategoryViewModel category)
        {
            var endpoint = Constants.Category.Edit;
            var response = await _httpApiWrapper.PostByID<UICategoryViewModel>(endpoint, id, category);

            if (response.IsSuccess)
            {

                return RedirectToAction("Index");
            }


            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Deleted(int id)
        {
            var endpoint = Constants.Category.Delete;

            var response = await _httpApiWrapper.DeleteAsync<UICategoryViewModel>(id, endpoint);


            return Json(response);


        }
    }
}
