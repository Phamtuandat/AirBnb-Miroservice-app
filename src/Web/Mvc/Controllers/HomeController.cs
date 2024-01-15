using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mvc.Areas.Property.ViewModels;
using Mvc.Infrastructure;
using Mvc.Models;
using Newtonsoft.Json;

namespace Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpClient;

    public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {

        var typeUri = API.Catalog.GetAllTypes("https://localhost:7074/api/");

        var typesResponseString = await _httpClient.GetStringAsync(typeUri);
        var typeList = JsonConvert.DeserializeObject<List<PlaceTypeViewModel>>(typesResponseString);
        ViewBag.typeModel = new FilterViewModel()
        {
            Types = typeList,
            GenerateUrl = (typeId) => Url.Action("Index", new
            {
                type = typeId
            })

        };
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
