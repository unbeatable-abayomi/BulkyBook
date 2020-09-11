using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _hostEnvironment;

		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_hostEnvironment = hostEnvironment;
		}


		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Upsert(int? id)
		{
			ProductVm productVm = new ProductVm()
			{
				Product = new Product(),
				CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem {
				  Text = i.Name,
				  Value = i.Id.ToString()
				
				}),
				CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString()

				})

			};
			//Product product = new Product();
			if (id == null)
			{
				//this is for create 
				return View(productVm);
			}
			//this is for edit

			//category = _unitOfWork.Category.Get((int)id);  
			productVm.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());
			if (productVm.Product == null)
			{

				return NotFound();
			}
			return View(productVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Upsert(ProductVm productVm)
		{

			if (ModelState.IsValid)
			{
				string webRootPath = _hostEnvironment.WebRootPath;
				var files = HttpContext.Request.Form.Files;

				if(files.Count > 0)
				{
					string fileName = Guid.NewGuid().ToString();
					var uploads = Path.Combine(webRootPath, @"images\products");
					var extension = Path.GetExtension(files[0].FileName);
					if(productVm.Product.ImageUrl != null)
					{
						//this is an edit we have to remove old image
						var imagePath = Path.Combine(webRootPath, productVm.Product.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(imagePath))
						{
							System.IO.File.Delete(imagePath);
						}
					}

					using (var fileStreams = new FileStream(Path.Combine(uploads,fileName+extension), FileMode.Create))
					{
						files[0].CopyTo(fileStreams);
					}
					productVm.Product.ImageUrl = @"\images\products\" + fileName + extension;
				}
				else
				{
					//update when they do not change the image;
					if(productVm.Product.Id != 0)
					{
						Product objFromDb = _unitOfWork.Product.Get(productVm.Product.Id);
						productVm.Product.ImageUrl = objFromDb.ImageUrl;
					}
				}
				if (productVm.Product.Id == 0)
				{
					_unitOfWork.Product.Add(productVm.Product);
				}
				else
				{
					_unitOfWork.Product.Update(productVm.Product);
				}
				_unitOfWork.Save();
				return RedirectToAction(nameof(Index));
			}
			return View(productVm.Product);
		}

		#region API CALLS

		[HttpGet]
		public IActionResult GetAll()
		{
			var allObj = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
			return Json(new { data = allObj });
		}

		[HttpDelete]

		public IActionResult Delete(int id)
		{
			var objFromDb = _unitOfWork.Product.Get(id);
			if (objFromDb == null)
			{
				return Json(new { success = false, message = "Error While Deleting" });
			}
			_unitOfWork.Product.Remove(objFromDb);
			_unitOfWork.Save();
			return Json(new { success = true, message = "SuccessFully Deleted" });
		}
		#endregion
	}
}
