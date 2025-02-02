// inject HolidaysApiService and call the GetHolidaus method

using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IHolidaysApiService _holidaysApiService;

    public HomeController(IHolidaysApiService holidaysApiService)
    {
        _holidaysApiService = holidaysApiService;
    }

    public async Task<IActionResult> Index(string countryCode, int year)
    {
        List<HolidayModel> holidays = new List<HolidayModel>();
        // make sure that the website doesn't crash when a code or year isn't given
        if (!string.IsNullOrEmpty(countryCode) && year > 0)
        {
            holidays = await _holidaysApiService.GetHolidays(countryCode, year);
        }
        return View(holidays);
    }
}
