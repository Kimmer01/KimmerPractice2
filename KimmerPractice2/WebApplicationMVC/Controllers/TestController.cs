using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers;

public class TestController : Controller
{
    public IActionResult Demo()
    {
        Student s = new Student(10, "test", 18, false);
        return View(s);
    }
}
