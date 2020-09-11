using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
	public class ProductVm
	{
		public Product Product { get; set; }

		public IEnumerable<SelectListItem> CategoryList { get; set; }
		public IEnumerable<SelectListItem> CoverTypeList { get; set; }


	}
}
