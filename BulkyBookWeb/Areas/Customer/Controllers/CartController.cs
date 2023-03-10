using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM  ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM = new ShoppingCartVM()
            {
                //ListCart = _unitOfWork.ShoppingCart.GetAll(u=> u.ApplicationUserId == claims.Value, includeProperties:"Product")
                ListCart = (IEnumerable<BulkyBook.Models.ViewModels.ShoppingCart>)_unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claims.Value, includeProperties: "Product")
            };
            return View(ShoppingCartVM);
        }
    }
}
