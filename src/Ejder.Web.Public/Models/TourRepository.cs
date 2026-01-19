namespace Ejder.Web.Public.Models;

public static class TourRepository
{
    private static readonly List<Tour> Data = new()
    {
        new Tour
        {
            Id = 1,
            Title = "Küba Turu",
            Country = "Küba",
            Days = 7,
            PriceTry = 79900,
            Summary = "Havana, Varadero ve kültür dolu bir Küba deneyimi.",
            ImageUrl = "https://images.unsplash.com/photo-1542370285-b8eb8317691c?q=80&w=1200&auto=format&fit=crop",
            Highlights = new() { "Havana şehir turu", "Klasik araba deneyimi", "Varadero sahilleri" },
            Included = new() { "Konaklama", "Rehberlik", "Havalimanı transferleri" },
            NotIncluded = new() { "Uçak bileti", "Vize/seyahat sigortası", "Kişisel harcamalar" },
            Program = new()
            {
                "Varış & karşılama – Otele transfer",
                "Havana şehir turu – Eski Havana",
                "Klasik araba ile gezi – Kültürel duraklar",
                "Serbest gün / opsiyonel turlar",
                "Varadero – deniz & dinlenme",
                "Alışveriş & serbest zaman",
                "Dönüş"
            },
            Dates = new()
            {
                new TourDate { Id = 101, StartDate = new DateTime(2026, 3, 10), EndDate = new DateTime(2026, 3, 16), PriceTry = 79900, Capacity = 25, Reserved = 12 },
                new TourDate { Id = 102, StartDate = new DateTime(2026, 4, 7),  EndDate = new DateTime(2026, 4, 13), PriceTry = 82900, Capacity = 25, Reserved = 25 }, // dolu
                new TourDate { Id = 103, StartDate = new DateTime(2026, 5, 12), EndDate = new DateTime(2026, 5, 18), PriceTry = 84900, Capacity = 25, Reserved = 19 },
            },

        },
        new Tour
        {
            Id = 2,
            Title = "Japonya & Kore",
            Country = "Japonya / Kore",
            Days = 10,
            PriceTry = 129900,
            Summary = "Tokyo, Kyoto ve Seul rotasında premium Asya turu.",
            ImageUrl = "https://images.unsplash.com/photo-1492571350019-22de08371fd3?q=80&w=1200&auto=format&fit=crop",
            Highlights = new() { "Tokyo & Kyoto", "Seul şehir turu", "Gastronomi deneyimi" },
            Included = new() { "Konaklama", "Rehberlik", "Şehir turları" },
            NotIncluded = new() { "Uçak bileti", "Vize/seyahat sigortası", "Kişisel harcamalar" },
            Program = new()
            {
                "Varış – Tokyo",
                "Tokyo turu",
                "Kyoto – tapınaklar",
                "Osaka – serbest zaman",
                "Seul – varış",
                "Seul şehir turu",
                "DMZ (opsiyonel)",
                "Alışveriş & serbest zaman",
                "Serbest gün",
                "Dönüş"
            },
            Dates = new()
            {
                new TourDate { Id = 201, StartDate = new DateTime(2026, 3, 20), EndDate = new DateTime(2026, 3, 29), PriceTry = 129900, Capacity = 20, Reserved = 9 },
                new TourDate { Id = 202, StartDate = new DateTime(2026, 4, 15), EndDate = new DateTime(2026, 4, 24), PriceTry = 134900, Capacity = 20, Reserved = 20 }, // dolu
            },

        }
    };

    public static List<Tour> GetAll() => Data;

    public static Tour? GetById(int id) => Data.FirstOrDefault(x => x.Id == id);
}
