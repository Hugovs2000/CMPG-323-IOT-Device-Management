using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Linq;

namespace DeviceManagement_WebApp.Repository
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(ConnectedOfficeContext context) : base(context) 
        {
        }

        public Category GetMostRecentCategory() 
        {
            return _context.Category.OrderByDescending(category => category.DateCreated).FirstOrDefault();
        }

        public Category GetOldestCategory()
        {
            return _context.Category.OrderBy(category => category.DateCreated).LastOrDefault();
        }
    }
}
