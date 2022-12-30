using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BulkyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    //-------- GETALL: CoverType---------------
    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objCoverTypeList);
    }
    //--------------------------------------------------------------

    //-------- GET: CoverType/Create---------------
    public IActionResult Create()
    {
        return View();
    }
    // Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj)
    {
        //if (obj.Name == obj.DisplayOrder.ToString())
        //{
        //    ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name");
        //}
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type Created Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //-------------------------------------------------------------------------------------------

    //-------- Get: CoverType/Edit---------------
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var coverTypeDbFromFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

        if (coverTypeDbFromFirst == null)
        {
            return NotFound();
        }

        return View(coverTypeDbFromFirst);
    }
    // Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj)
    {
        //if (obj.Name == obj.DisplayOrder.ToString())
        //{
        //    ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name");
        //}
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type Updated Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //-------------------------------------------------------------------------------------------

    //-------- GET: CoverType/Delete---------------
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var coverTypeDbFromFirst = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
        if (coverTypeDbFromFirst == null)
        {
            return NotFound();
        }
        return View(coverTypeDbFromFirst);
    }
    // Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var obj = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
        if (id == null || id == 0)
        {
            return NotFound();
        }

        _unitOfWork.CoverType.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Cover Type Deleted Successfully";
        return RedirectToAction("Index");


    }

    //-------------------------------------------------------------------------------------------
}
