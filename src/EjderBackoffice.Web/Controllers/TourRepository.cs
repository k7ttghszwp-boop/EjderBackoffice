namespace EjderBackoffice.Web.Models;

public static class TourRepository
{
    private static readonly List<Tour> Data = new()
    {
        new Tour { Id = 1, Name = "Küba Turu", IsActive = true },
        new Tour { Id = 2, Name = "Japonya-Kore", IsActive = true },
        new Tour { Id = 3, Name = "Latin Amerika", IsActive = true },
        new Tour { Id = 4, Name = "Brezilya", IsActive = true },
    };

    public static IEnumerable<Tour> GetAll(bool onlyActive = true)
        => (onlyActive ? Data.Where(x => x.IsActive) : Data).OrderBy(x => x.Name);

    public static Tour? GetById(int id) => Data.FirstOrDefault(x => x.Id == id);
}
