using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    

    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
    }

    //-------- GETALL: Products---------------
    public IActionResult Index()
    {
        // IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
        //return View(objCategoryList);
        return View();
    }
    

    //-------- Get: Product/Edit/Insert(Create)---------------
    public IActionResult Upsert(int? id)
    {
        //Product product = new();
        ////----------------For Category DropDown---------------
        //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
        //    u=> new SelectListItem
        //    {
        //        Text = u.Name,
        //        Value = u.Id.ToString()
        //    });
        ////----------------For CoverType DropDown---------------
        //IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
        //    u => new SelectListItem
        //    {
        //        Text = u.Name,
        //        Value = u.Id.ToString()
        //    });

        Company company = new();           
        if (id == null || id == 0)
        {
            // Create
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CoverTypeList"] = CoverTypeList;
            return View(company);
        }
        else
        {
            //Update
            company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            return View(company);
        }

    }
    // Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Company obj)
    {
        
        if (ModelState.IsValid)
        {
            
            if(obj.Id == 0)
            {
                _unitOfWork.Company.Add(obj);
                TempData["success"] = "Company Created Successfully";
            }
            else
            {
                _unitOfWork.Company.Update(obj);
                TempData["success"] = "Company Updated Successfully";
            }
            
            _unitOfWork.Save();
            
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //-------------------------------------------------------------------------------------------





    //-------------------------------------------------------------------------------------------
    //-------API EndPoint For DataTable----------------------------

    #region API CALLS
    //-------- GETALL: Products---------------
    [HttpGet]
    public IActionResult GetAll()
    {
        var companyList = _unitOfWork.Company.GetAll();
        return Json(new { data = companyList });
    }

    // Delete Product
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);
        if (id == null)
        {
            return Json(new { success = false, message="Error while deleting" });
        }
        
        _unitOfWork.Company.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Successful" });
        
    }
    #endregion
    //-------------------------------------------------------------

}
