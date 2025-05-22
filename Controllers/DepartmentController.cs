using CareNet_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CareNet_System.Models;
using CareNet_System.Repostatory;

namespace CareNet_System.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _deptRepo;

        public DepartmentsController(IDepartmentRepository deptRepo)
        {
            _deptRepo = deptRepo;
        }

        public IActionResult Index()
        {
            var deptList = _deptRepo.GetAll();
            return View("~/Views/Department/All.cshtml", deptList); 
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(Department newDept)
        {
            if (ModelState.IsValid)
            {
                _deptRepo.Add(newDept);
                return RedirectToAction("Index");
            }
            return View(newDept);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dept = _deptRepo.GetById(id);
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }

        [HttpPost]
        public IActionResult Edit(Department updatedDept)
        {
            if (ModelState.IsValid)
            {
                _deptRepo.Update(updatedDept);
                return RedirectToAction("Index");
            }
            return View(updatedDept);
        }

        public IActionResult Delete(int id)
        {
            _deptRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}