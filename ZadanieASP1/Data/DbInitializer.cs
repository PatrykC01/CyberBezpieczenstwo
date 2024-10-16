using Microsoft.CodeAnalysis;
using ZadanieASP1.Models;


namespace ZadanieASP1.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TravelAgencyContext context)
        {
            context.Database.EnsureCreated();

            if (context.Customers.Any())
            {
                return;
            }

            var customers = new Customer[]
            {
                new Customer {FirstName="Anna", LastName="Nowak", PhoneNumber="123-456-789"},
                new Customer { FirstName = "Jan", LastName = "Kowalski", PhoneNumber = "987-654-321" },
                new Customer { FirstName = "Ewa", LastName = "Wiśniewska", PhoneNumber = "555-123-456" },
                new Customer { FirstName = "Piotr", LastName = "Nowicki", PhoneNumber = "111-222-333" },
                new Customer { FirstName = "Magdalena", LastName = "Dąbrowska", PhoneNumber = "444-555-666" }
            };

            context.Customers.AddRange(customers);
           
            context.SaveChanges();


            var trips = new Trip[]
            {
                new Trip {TripID=1, Title="Delfiny i snorkling przy wyspie Mnemba, żółwie wodne i zachód słońca", Description="Jest to wycieczka, na której zobaczymy wyspę Mnemba. Będziemy mieli okazję nurkować z rurką  przy najładniejszych rafach w pobliżu Zanzibaru. Zatrzymamy się przy słynnym sand banku – jest to  znikająca piękna plaża na oceanie. Przy odrobinie szczęścia zobaczymy delfiny i będziemy mieli okazję z nimi popływać. Następnie możemy pływać wśród żółwi wodnych i je karmić, wycieczka kończy się zachodem słońca na plaży Kendwa Rock.", Price=289.0f, Duration="6h", TripDate=DateTime.Parse("2024-06-05 12:00")},
                new Trip
    {
        TripID = 2,
        Title = "Wulkan Etna: Wspinaczka na szczyt",
        Description = "Ta wycieczka umożliwia zdobycie szczytu Wulkanu Etna, najwyższego czynnego wulkanu w Europie. Podziwiaj widoki na krajobraz lawowy i odkryj tajemnice tego majestatycznego wulkanu.",
        Price = 350.0f,
        Duration = "8h",
        TripDate = DateTime.Parse("2024-07-15 9:00")
    },
    new Trip
    {
        TripID = 3,
        Title = "Rajskie Jeziora Plitwickie: Spacer po drewnianych kładkach",
        Description = "Odkryj urok Plitwickich Jezior - malowniczych jezior i wodospadów, które tworzą niepowtarzalny krajobraz. Spaceruj po drewnianych kładkach i ciesz się pięknem natury.",
        Price = 220.0f,
        Duration = "6h",
        TripDate = DateTime.Parse("2024-04-05 11:00")
    },
    new Trip
    {
        TripID = 4,
        Title = "Santorini: Odkrywanie białych domów i niebieskich kopuł",
        Description = "Ta wycieczka zabierze cię na malowniczą Wyspę Santorini, gdzie odkryjesz białe domy, niebieskie kopuły i zapierające dech w piersiach widoki na Morze Egejskie.",
        Price = 420.0f,
        Duration = "10h",
        TripDate = DateTime.Parse("2024-08-25 10:00")
    },
    new Trip
    {
        TripID = 5,
        Title = "Safari w Serengeti: Spotkanie z dzikimi zwierzętami",
        Description = "Wybierz się na niezapomniane safari w Parku Narodowym Serengeti w Tanzanii. Obserwuj lwy, słonie, żyrafy i inne dzikie zwierzęta w ich naturalnym środowisku.",
        Price = 550.0f,
        Duration = "12h",
        TripDate = DateTime.Parse("2024-05-05 11:00")
    }
            };

            foreach (Trip t in trips)
            {

                context.Trips.Add(t);

            }
            context.SaveChanges();

            var reservations = new Reservation[]
 {
    new Reservation { TripID = 1, CustomerID = 1, ReservationDate = DateTime.Parse("2024-02-12 23:05"), DateOfDeparture = DateTime.Parse("2024-02-15"), DateOfReturn = DateTime.Parse("2024-02-20") },
    new Reservation { TripID = 3, CustomerID = 3, ReservationDate = DateTime.Parse("2024-02-22 21:05"), DateOfDeparture = DateTime.Parse("2024-02-25"), DateOfReturn = DateTime.Parse("2024-03-05") },
    new Reservation { TripID = 3, CustomerID = 4, ReservationDate = DateTime.Parse("2024-03-02 20:20"), DateOfDeparture = DateTime.Parse("2024-03-10"), DateOfReturn = DateTime.Parse("2024-03-15") },
    new Reservation { TripID = 2, CustomerID = 2, ReservationDate = DateTime.Parse("2024-01-29 20:15"), DateOfDeparture = DateTime.Parse("2024-02-01"), DateOfReturn = DateTime.Parse("2024-02-05") },
    new Reservation { TripID = 4, CustomerID = 5, ReservationDate = DateTime.Parse("2024-02-10 13:05"), DateOfDeparture = DateTime.Parse("2024-02-15"), DateOfReturn = DateTime.Parse("2024-02-25") }
 };


            foreach (Reservation r in reservations)
            {
                context.Reservations.Add(r);
            }
            context.SaveChanges();
        }
    }
}
