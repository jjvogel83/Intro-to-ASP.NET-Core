//LEVEL 1
//Challenge 1
Creating the View
Views/Home/Index.cshtml
@model String
<h2>Equipment</h2>

<div>
  <ul>
    <li>@Model</li>
  </ul>
</div>

Controllers/HomeController.cs
using Microsoft.AspNet.Mvc;
using System;

namespace CharacterSheetApp.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      var name = "Shield";
      return View("Index", name);
    }
  }
}

Challenge 2 (Set Up Form Input)
Models/Equipment.cs

namespace CharacterSheetApp.Models
{
   public class Equipment
   {
      public string Name;
   }



Controllers/HomeController.cs

using Microsoft.AspNet.Mvc;
using CharacterSheetApp.Models;
using System;

namespace CharacterSheetApp.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      var model = new CharacterSheetApp.Models.Equipment();
      model.Name= "Shield";
      return View("Index", model);
      
    }
  }
}

Views/Home/Index.cshtml

@model CharacterSheetApp.Models.Equipment
<h2>Equipment</h2>

<div>
  <ul><li>@Model.Name</li>  </ul>
</div>

LEVEL 2

Controllers/HomeController.cs
using Microsoft.AspNet.Mvc;
using CharacterSheetApp.Models;
using System;

namespace CharacterSheetApp.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      Equipment model = new Equipment();
      model.Name = "Shield";
      return View("Index", model);
    }
     
    //create our Create Action here
     public IActionResult Create(string EquipmentName)
       {
       var model = new Equipment();
       model.Name = EquipmentName;
       return View("Index", model);
     }
     
  } 
}

Views/Home/Index.cshtml
@model CharacterSheetApp.Models.Equipment
<h2>Equipment:</h2>

<form asp-action="Create"  class="form-inline mbm">
  <div class="form-group">
    <input name="EquipmentName"/> 
    <input type="submit" value="Add Equipment" class="form-control" />
  </div>
</form>

<h4>Equipment:</h4>

<div>
  <ul>
    <li>
      <label>@Model.Name</label>
    </li>
  </ul>
</div>



Level 3 (Set Our View to Use Lists)
Views/Home/Index.cshtml
@model List<CharacterSheetApp.Models.Equipment>
<h2>Equipment:</h2>

<form asp-action="Create" class="form-inline mbm">
  <div class="form-group">
    <input name="EquipmentName" class="form-control" />
    <input class="btn" type="submit" value="Add Equipment" />
  </div>
</form>

<h4>Equipment:</h4>

<div>
  <ul>
     @foreach (var item in Model){
    <li>
      
      <label>@item.Name</label>
      
    </li>
    }
  </ul>
</div>

GlobalVariables.cs (Created our Global Variable)
using System.Collections.Generic;
using CharacterSheetApp.Models;
using System;

namespace CharacterSheetApp
{
  public static class GlobalVariables
  {
    public static List<Equipment> Equipment {get; set;} = new List<Equipment>();  
  }
}


Models/Equipment.cs (Set Up Our Model)
using System.Collections.Generic;
using System;

namespace CharacterSheetApp.Models
{
  public class Equipment
  {
  
    public string Name;
    public static void Create(string EquipmentName)
    {
      var equipment = new Equipment();
      equipment.Name = EquipmentName;
      GlobalVariables.Equipment.Add(equipment);
    }
    public static List<Equipment> GetAll()
      {
      return GlobalVariables.Equipment;
    }
  }
}


Controllers/HomeController.cs (Setting Up Our Controller)
using Microsoft.AspNet.Mvc;
using CharacterSheetApp.Models;
using System;

namespace CharacterSheetApp.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      var name = "Shield";
      
      return View(Equipment.GetAll());
    }

    public IActionResult Create(string EquipmentName)
    {
      Equipment.Create(EquipmentName);
      return RedirectToAction("Index");
    }
  }
}



