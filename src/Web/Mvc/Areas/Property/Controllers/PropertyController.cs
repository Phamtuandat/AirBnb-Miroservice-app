using Microsoft.AspNetCore.Mvc;
using Mvc.Areas.Property.ViewModels;
using Mvc.Infrastructure;
using Mvc.Models;
using Newtonsoft.Json;

namespace Mvc.Areas.Property.Controllers
{
    [Area("property")]
    [Route("property/[action]")]
    public class PropertyController : Controller
    {
        internal string Message = string.Empty;
        internal string BaseUri = string.Empty;
        private readonly HttpClient _httpClient;
        public PropertyController(HttpClient httpClient)
        {
            BaseUri = "https://localhost:7074/api/Property/";
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index([FromForm] LocationViewModel? model, [FromQuery] int page = 1, [FromQuery] int pageSize = 12)
        {
            var location = model;
            if (page < 1)
            {
                return BadRequest();
            }
            var uri = API.Catalog.GetProperties(BaseUri, page, pageSize, null, null, null);


            var typeUri = API.Catalog.GetAllTypes("https://localhost:7074/api/");

            var typesResponseString = await _httpClient.GetStringAsync(typeUri);
            var typeList = JsonConvert.DeserializeObject<List<PlaceTypeViewModel>>(typesResponseString);
            if (location?.Search != null)
            {
                uri = API.Catalog.SearchProperties(BaseUri, page, pageSize, location.Search, location.Search);
            }
            var responseString = await _httpClient.GetStringAsync(uri);
            var catalog = JsonConvert.DeserializeObject<PaginatedItemsViewModel<PropertyItem>>(responseString);
            var propertyList = catalog.Data.Select(p => new PropertyViewModel()
            {
                Title = p.Title,
                HostId = p.HostId,
                CreateAt = DateTime.Parse(p.CreateAt),
                Description = p.Description,
                Id = p.Id,
                PricePerNight = Math.Round(p.PricePerNight),
                Amenities = p.Amenities,
                AverageRate = p.AverageRate,
                City = p.City,
                Country = p.Country,
                LocationDetail = p.LocationDetail,
                MaxGuests = p.MaxGuests,
                NumberOfBedroom = p.NumberOfBedroom,
                NumberOfBethroom = p.NumberOfBethroom,
                TypeId = p.TypeId,
                Medias = p.Medias ?? []
            }).ToList();
            if (propertyList.Count == 0)
            {
                ViewBag.title = "Not Found";
            }
            ViewBag.pagingModel = new PagingModel()
            {
                Countpages = (int)Math.Ceiling(catalog.Count / catalog.PageSize),
                Currentpage = page,
                GenerateUrl = (pageNumber) => Url.Action("Index", new
                {
                    page = pageNumber,
                    pageSize
                })
            };
            ViewBag.typeModel = new FilterViewModel()
            {
                Types = typeList,
                GenerateUrl = (typeId) => Url.Action("Index", new
                {
                    typeId
                })

            };

            return View(propertyList);
        }
        [HttpGet]
        public async Task<ActionResult> Detail([FromQuery] string id)
        {
            if (id == null)
            {
                return BadRequest("id parameter is required");
            }
            var uri = API.Catalog.GetPropertyById(BaseUri, id);
            var stringResult = await _httpClient.GetStringAsync(uri);
            var property = JsonConvert.DeserializeObject<PropertyViewModel>(stringResult);
            if (property == null)
            {
                ViewBag.Message = "Not Found!";
                return View();
            }
            else
            {

                return View(property);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, [FromQuery] string? typeId = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 12)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            if (page < 1)
            {
                return BadRequest();
            }
            var propertyUri = API.Catalog.GetProperties(BaseUri, page, pageSize, null, typeId, sortOrder);
            var typeUri = API.Catalog.GetAllTypes("https://localhost:7074/api/");
            var typesResponseString = await _httpClient.GetStringAsync(typeUri);
            var typeList = JsonConvert.DeserializeObject<List<PlaceTypeViewModel>>(typesResponseString);
            var propertyResponseString = await _httpClient.GetStringAsync(propertyUri);
            var catalog = JsonConvert.DeserializeObject<PaginatedItemsViewModel<PropertyItem>>(propertyResponseString);
            var propertyList = catalog.Data.Select(p => new PropertyViewModel()
            {
                Title = p.Title,
                HostId = p.HostId,
                CreateAt = DateTime.Parse(p.CreateAt),
                Description = p.Description,
                Id = p.Id,
                PricePerNight = Math.Round(p.PricePerNight),
                Amenities = p.Amenities,
                AverageRate = p.AverageRate,
                City = p.City,
                Country = p.Country,
                LocationDetail = p.LocationDetail,
                MaxGuests = p.MaxGuests,
                NumberOfBedroom = p.NumberOfBedroom,
                NumberOfBethroom = p.NumberOfBethroom,
                TypeId = p.TypeId,
                Medias = p.Medias ?? []
            }).ToList();
            ViewBag.pagingModel = new PagingModel()
            {
                Countpages = (int)Math.Ceiling(catalog.Count / catalog.PageSize),
                Currentpage = page,
                GenerateUrl = (pageNumber) => Url.Action("Index", new
                {
                    page = pageNumber,
                    pageSize
                })
            };
            ViewBag.typeModel = new FilterViewModel()
            {
                CurrentType = typeList.Where(t => t.Id == typeId).FirstOrDefault(),
                Types = typeList,
                GenerateUrl = (typeId) => Url.Action("Index", new
                {
                    typeId
                })
            };
            return View(propertyList);
        }

    }

}
