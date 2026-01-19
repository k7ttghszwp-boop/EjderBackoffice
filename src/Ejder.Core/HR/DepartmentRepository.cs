namespace Ejder.Core.HR;

public static class DepartmentRepository
{
    private static readonly List<Department> Data = new()
    {
        new() { Id = 1, Name = "IT" },
        new() { Id = 2, Name = "Satış" },
        new() { Id = 3, Name = "Müşteri Hizmetleri" }
    };

    public static IEnumerable<Department> GetAll() => Data;

    public static Department? GetById(int id)
        => Data.FirstOrDefault(x => x.Id == id);
}
