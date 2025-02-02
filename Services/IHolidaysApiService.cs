// create the interface that will be user input
public interface IHolidaysApiService
{
    Task<List<HolidayModel>> GetHolidays(string countryCode, int year);
}