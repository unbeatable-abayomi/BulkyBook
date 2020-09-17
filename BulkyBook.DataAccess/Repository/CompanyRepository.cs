using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
	public class CompanyRepository : Repository<Company>, ICompanyRepository
	{
		private readonly ApplicationDbContext _db;
		public CompanyRepository( ApplicationDbContext db): base(db)
		{
			_db = db;
		}

		public void Update(Company company)
		{

			_db.Update(company);
			//var objFromDb = _db.Companies.FirstOrDefault(s => s.Id == company.Id);

			//if (objFromDb != null)
			//{

			//	objFromDb.Name = company.Name;
			//	objFromDb.State = company.State;
			//	objFromDb.StreetAddress = company.StreetAddress;
			//	objFromDb.PhoneNumber = company.PhoneNumber;
			//	objFromDb.PostalCode = company.PostalCode;
			//	objFromDb.IsAuthorizedCompany = company.IsAuthorizedCompany;




			//}
		}
	}
}
