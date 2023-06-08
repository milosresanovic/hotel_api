using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_hotel.DataAccess;
using project_hotel.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project_hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialDataController : ControllerBase
    {
        private HotelContext _context;

        public InitialDataController(HotelContext context)
        {
            _context = context;
        }
        // POST api/<InitialDataController>
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                var faker = new Faker();
                Random random = new Random();

                var roomTypes = new List<RoomType>
                {
                new RoomType
                {
                    Name = "Terasa",
                },
                new RoomType
                {
                    Name = "Kuhinja"
                },
                new RoomType
                {
                    Name = "Kupatilo"
                },
                new RoomType
                {
                    Name = "Spavaća soba"
                },
                new RoomType
                {
                    Name = "Hodnik"
                }
                };

                var images = new List<Image>
                {
                    new Image
                    {
                        Path = faker.Image.PicsumUrl()
                    },
                    new Image
                    {
                        Path = faker.Image.PicsumUrl()
                    },
                    new Image
                    {
                        Path = faker.Image.PicsumUrl()
                    },
                    new Image
                    {
                        Path = faker.Image.PicsumUrl()
                    },
                    new Image
                    {
                        Path = faker.Image.PicsumUrl()
                    },
                    new Image
                    {
                        Path = faker.Image.PicsumUrl()
                    },
                    new Image
                    {
                        Path = faker.Image.PicsumUrl()
                    },
                    new Image
                    {
                        Path = faker.Image.PicsumUrl()
                    }
                };

                var equipments = new List<Equipment>
            {
                new Equipment
                {
                    Name = "Fen za kosu"
                },
                new Equipment
                {
                    Name = "Aparat za kafu"
                },
                new Equipment
                {
                    Name = "Toster"
                },
                new Equipment
                {
                    Name = "Kuvalo za vodu"
                },
                new Equipment
                {
                    Name = "Đakuzi"
                },
                new Equipment
                {
                    Name = "TV"
                },
                new Equipment
                {
                    Name = "Frižider"
                },
                new Equipment
                {
                    Name = "Šporet"
                }
            };

                var categories = new List<Category>
            {
                new Category
                {
                    Name = "Delux",
                    ParentId = null
                },
                new Category
                {
                    Name = "Jednosoban",
                    ParentId = null
                },
                new Category
                {
                    Name = "Dvosoban",
                    ParentId = null
                },
                new Category
                {
                    Name = "Trosoban",
                    ParentId =null
                },
                new Category
                {
                    Name = "Pretsednički",
                    ParentId = null
                },
                new Category
                {
                    Name = "Studio",
                    ParentId = null
                },
            };

                var users = new List<User>
                {
                    new User
                    {
                        FirstName = "Anonimous",
                        LastName = "Anonimous",
                        Username = "Anonimous",
                        Email = "anonimous@gmail.com",
                        Password = "$2a$11$Ahl8NDzdxDryw1hhJdck/OSJk8W6fZtCiiZbpDt2nRjyDdmHTJLyW",
                        Image = images.ElementAt(0)
                    },
                new User
                {
                    FirstName = "Milos",
                    LastName = "Resanovic",
                    Username = "somiresa",
                    Email = "somi@gmail.com",
                    Password = "$2a$11$Ahl8NDzdxDryw1hhJdck/OSJk8W6fZtCiiZbpDt2nRjyDdmHTJLyW",
                    Image = images.ElementAt(1)
                },
                new User
                {
                    FirstName = "David",
                    LastName = "Stevic",
                    Username = "dacha02",
                    Email = "dacha02@gmail.com",
                    Password = "$2a$11$Ahl8NDzdxDryw1hhJdck/OSJk8W6fZtCiiZbpDt2nRjyDdmHTJLyW",
                    Image = images.ElementAt(2)
                },
                new User
                {
                    FirstName = "Nenad",
                    LastName = "Jevtic",
                    Username = "jevta01",
                    Email = "jevta01@gmail.com",
                    Password = "$2a$11$Ahl8NDzdxDryw1hhJdck/OSJk8W6fZtCiiZbpDt2nRjyDdmHTJLyW",
                    Image = images.ElementAt(3)
                },
                new User
                {
                    FirstName = "Jovan",
                    LastName = "Vasic",
                    Username = "jocava",
                    Email = "jocava@gmail.com",
                    Password = "$2a$11$Ahl8NDzdxDryw1hhJdck/OSJk8W6fZtCiiZbpDt2nRjyDdmHTJLyW",
                    Image = images.ElementAt(4)
                },
                new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Username = "admin",
                    Email = "admin@gmail.com",
                    Password = "$2a$11$Ahl8NDzdxDryw1hhJdck/OSJk8W6fZtCiiZbpDt2nRjyDdmHTJLyW",
                    Image = images.ElementAt(5)
                }
            };

                var apartments = new List<Apartment>
            {
                new Apartment
                {
                    Name = "Apartman 1",
                    Description = faker.Lorem.Paragraph(),
                    AverageRating = (float)random.NextDouble() * 4 + 1,
                    Category = categories.ElementAt(1),
                    MaxPersons = (int)Math.Round(random.NextDouble() * 3 +1),
                },
                new Apartment
                {
                    Name = "Apartman 2",
                    Description = faker.Lorem.Paragraph(),
                    AverageRating = (float)random.NextDouble() * 4 + 1,
                    Category = categories.ElementAt(2),
                    MaxPersons = (int)Math.Round(random.NextDouble() * 3 +1),
                },
                new Apartment
                {
                    Name = "Apartman 3",
                    Description = faker.Lorem.Paragraph(),
                    AverageRating = (float)random.NextDouble() * 4 + 1,
                    Category = categories.ElementAt(3),
                    MaxPersons = (int)Math.Round(random.NextDouble() * 3 +1),
                },
                new Apartment
                {
                    Name = "Apartman 4",
                    Description = faker.Lorem.Paragraph(),
                    AverageRating = (float)random.NextDouble() * 4 + 1,
                    Category = categories.ElementAt(4),
                    MaxPersons = (int)Math.Round(random.NextDouble() * 3 +1),
                },
            };

                var apartmentRooms = new List<ApartmentRoom>
            {
                new ApartmentRoom
                {
                    RoomType = roomTypes.ElementAt(0),
                    Apartment = apartments.ElementAt(0),
                    Area = 10
                },
                new ApartmentRoom
                {
                    RoomType = roomTypes.ElementAt(1),
                    Apartment = apartments.ElementAt(0),
                    Area = 10
                },
                new ApartmentRoom
                {
                    RoomType = roomTypes.ElementAt(2),
                    Apartment = apartments.ElementAt(0),
                    Area = 10
                },

                new ApartmentRoom
                {
                    RoomType = roomTypes.ElementAt(1),
                    Apartment = apartments.ElementAt(1),
                    Area = 10
                },
                new ApartmentRoom
                {
                    RoomType = roomTypes.ElementAt(2),
                    Apartment = apartments.ElementAt(1),
                    Area = 10
                },
                new ApartmentRoom
                {
                    RoomType = roomTypes.ElementAt(3),
                    Apartment = apartments.ElementAt(1),
                    Area = 10
                },

                new ApartmentRoom
                {
                    RoomType = roomTypes.ElementAt(1),
                    Apartment = apartments.ElementAt(2),
                    Area = 10
                },
                new ApartmentRoom
                {
                    RoomType = roomTypes.ElementAt(2),
                    Apartment = apartments.ElementAt(2),
                    Area = 10
                },
                new ApartmentRoom
                {
                    RoomType = roomTypes.ElementAt(3),
                    Apartment = apartments.ElementAt(2),
                    Area = 10
                },

                new ApartmentRoom
                {
                    RoomType = roomTypes.ElementAt(1),
                    Apartment = apartments.ElementAt(3),
                    Area = 10
                },
                new ApartmentRoom
                {
                    RoomType = roomTypes.ElementAt(2),
                    Apartment = apartments.ElementAt(3),
                    Area = 10
                },
                new ApartmentRoom
                {
                    RoomType = roomTypes.ElementAt(3),
                    Apartment = apartments.ElementAt(3),
                    Area = 10
                }
            };

                var reservations = new List<Reservation>
            {
                new Reservation
                {
                    User = users.ElementAt(1),
                    Apartment = apartments.ElementAt(0),
                    DateFrom = DateTime.UtcNow,
                    DateTo = DateTime.UtcNow.AddDays(5),
                    GuestsNumber = 2,
                    TotalPrice = 50M
                },
                new Reservation
                {
                    User = users.ElementAt(1),
                    Apartment = apartments.ElementAt(1),
                    DateFrom = DateTime.UtcNow,
                    DateTo = DateTime.UtcNow.AddDays(3),
                    GuestsNumber = 3,
                    TotalPrice = 50M
                },
                new Reservation
                {
                    User = users.ElementAt(2),
                    Apartment = apartments.ElementAt(2),
                    DateFrom = DateTime.UtcNow,
                    DateTo = DateTime.UtcNow.AddDays(7),
                    GuestsNumber = 3,
                    TotalPrice = 50M
                },
                new Reservation
                {
                    User = users.ElementAt(1),
                    Apartment = apartments.ElementAt(0),
                    DateFrom = DateTime.UtcNow.AddDays(10),
                    DateTo = DateTime.UtcNow.AddDays(15),
                    GuestsNumber = 2,
                    TotalPrice = 50M
                }
            };

                var apartmentEquipments = new List<ApartmentEquipment>
            {
                new ApartmentEquipment
                {
                    Apartment = apartments.ElementAt(1),
                    Equipment = equipments.ElementAt(0)
                },
                new ApartmentEquipment
                {
                    Apartment = apartments.ElementAt(1),
                    Equipment = equipments.ElementAt(1)
                },
                new ApartmentEquipment
                {
                    Apartment = apartments.ElementAt(1),
                    Equipment = equipments.ElementAt(2)
                },
                new ApartmentEquipment
                {
                    Apartment = apartments.ElementAt(1),
                    Equipment = equipments.ElementAt(3)
                },
                new ApartmentEquipment
                {
                    Apartment = apartments.ElementAt(0),
                    Equipment = equipments.ElementAt(0)
                },
                new ApartmentEquipment
                {
                    Apartment = apartments.ElementAt(0),
                    Equipment = equipments.ElementAt(1)
                },
                new ApartmentEquipment
                {
                    Apartment = apartments.ElementAt(0),
                    Equipment = equipments.ElementAt(2)
                },
                new ApartmentEquipment
                {
                    Apartment = apartments.ElementAt(0),
                    Equipment = equipments.ElementAt(3)
                },
                new ApartmentEquipment
                {
                    Apartment = apartments.ElementAt(2),
                    Equipment = equipments.ElementAt(0)
                },
                new ApartmentEquipment
                {
                    Apartment = apartments.ElementAt(2),
                    Equipment = equipments.ElementAt(1)
                },
                new ApartmentEquipment
                {
                    Apartment = apartments.ElementAt(3),
                    Equipment = equipments.ElementAt(2)
                },
                new ApartmentEquipment
                {
                    Apartment = apartments.ElementAt(3),
                    Equipment = equipments.ElementAt(3)
                },
            };

                var apartmentImages = new List<ApartmentImage>
            {
                new ApartmentImage
                {
                    Apartment = apartments.ElementAt(0),
                    Image = images.ElementAt(0)
                },
                new ApartmentImage
                {
                    Apartment = apartments.ElementAt(0),
                    Image = images.ElementAt(1)
                },
                new ApartmentImage
                {
                    Apartment = apartments.ElementAt(1),
                    Image = images.ElementAt(2)
                },
                new ApartmentImage
                {
                    Apartment = apartments.ElementAt(2),
                    Image = images.ElementAt(3)
                },
                new ApartmentImage
                {
                    Apartment = apartments.ElementAt(3),
                    Image = images.ElementAt(4)
                },
                new ApartmentImage
                {
                    Apartment = apartments.ElementAt(3),
                    Image = images.ElementAt(5)
                }
            };

                var prices = new List<Price>
            {
                new Price
                {
                    Apartment = apartments.ElementAt(0),
                    StartDate = DateTime.UtcNow.AddMonths(-1),
                    Cost = Convert.ToDecimal(faker.Commerce.Price(30, 150, 2))
                },
                new Price
                {
                    Apartment = apartments.ElementAt(1),
                    StartDate = DateTime.UtcNow.AddMonths(-1),
                    Cost = Convert.ToDecimal(faker.Commerce.Price(30, 150, 2))
                },
                new Price
                {
                    Apartment = apartments.ElementAt(2),
                    StartDate = DateTime.UtcNow.AddMonths(-1),
                    Cost = Convert.ToDecimal(faker.Commerce.Price(30, 150, 2))
                },
                new Price
                {
                    Apartment = apartments.ElementAt(3),
                    StartDate = DateTime.UtcNow.AddMonths(-1),
                    Cost = Convert.ToDecimal(faker.Commerce.Price(30, 150, 2))
                },
                new Price
                {
                    Apartment = apartments.ElementAt(3),
                    StartDate = DateTime.UtcNow.AddDays(-1),
                    Cost = Convert.ToDecimal(faker.Commerce.Price(30, 150, 2))
                },
            };

                var useCases = new List<UseCase>
            {
                new UseCase
                {
                    Name = "Inicijalizacija podataka",
                    Description = "U bazi se popunjavaju tabele"
                },
                new UseCase
                {
                    Name = "Kreiranje rezervacije",
                    Description = "Kreira se rezervacija u tabeli"
                },
                new UseCase
                {
                    Name = "Kreiranje opreme u apartmanu",
                    Description = "Kreira se oprema tipa (ves masina, aparat za kafu itd)"
                },
                new UseCase
                {
                    Name = "Brisanje rezervacije",
                    Description = "Postavlja deletedAt na trenutno vreme."
                },
                new UseCase
                {
                    Name = "Dodavanje apartmana",
                    Description = "Dodaje se apartman u bazu podataka"
                },
                new UseCase
                {
                    Name = "Registracija korisnika",
                    Description = "Registracija novog korisnika."
                },
                new UseCase
                {
                    Name = "Pretraga audit loga",
                    Description = "Pretrazuje se auditLog tbela u bazi"
                },
                new UseCase
                {
                    Name = "Pretraga apartmana",
                    Description = "Pretraga apartmana u bazi podataka"
                },
                new UseCase
                {
                    Name = "Pronalazenje apartmana",
                    Description = "Pronalazi se apartmana sa zadatim identifikatorom kroz query string"
                },
                new UseCase
                {
                    Name = "Azuriranje apartmana",
                    Description = "Azuriraju se podaci vezani za apartman"
                },
                new UseCase
                {
                    Name = "Brisanje apartmana",
                    Description = "Apartman se deaktivira tako da ne moze vise da se rezervise."
                },
                new UseCase
                {
                    Name = "Brisanje korisnika",
                    Description = "Korisniku se kolona IsActive setuje na false."
                },
                new UseCase
                {
                    Name = "Dohvatanje korisnika",
                    Description = "Dohvataju se korisnici filtrirani kroz parametre iz query stringa"
                },
                new UseCase
                {
                    Name = "Azuriranje korisnika",
                    Description = "Promena Username FirstName i LastName u tabeli Users"
                },
                new UseCase
                {
                    Name = "Kreiranje komentara",
                    Description = "Kreira se komentar za odredjeni apartman"
                },
                new UseCase
                {
                    Name = "Kreiranje cene",
                    Description = "Kreira se cena sa datumom pocetka za apartman"
                },
                new UseCase
                {
                    Name = "Pretraga rezervacija",
                    Description = "Pretraga rezervacija po odredjenim kriterijumima."
                },
                new UseCase
                {
                    Name = "Brisanje komentara",
                    Description = "Brise se odredjeni komentar ciji se Id posalje kroz query string."
                }
            };

                var userUseCases = new List<UserUseCase>
            {
                new UserUseCase
                {
                    User = users.ElementAt(0),
                    UseCase = useCases.ElementAt(0)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(0)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(1)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(2)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(3)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(4)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(5)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(6)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(7)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(8)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(9)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(10)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(11)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(12)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(13)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(14)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(15)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(16)
                },
                new UserUseCase
                {
                    User = users.ElementAt(5),
                    UseCase = useCases.ElementAt(17)
                },
            };

                var comments = new List<Comment>
            {
                new Comment
                {
                    User = users.ElementAt(2),
                    Apartment = apartments.ElementAt(0),
                    Text = "Ovo je bas dobar apartmen!",
                    StarNumber = 5
                },
                new Comment
                {
                    User = users.ElementAt(3),
                    Apartment = apartments.ElementAt(0),
                    Text = "Ovo je bas dobar apartmen dajem ocenu 5!",
                    StarNumber = 5
                },
                new Comment
                {
                    User = users.ElementAt(2),
                    Apartment = apartments.ElementAt(0),
                    Text = "Nije nista specijalno...!",
                    StarNumber = 3
                }
            };

                _context.Users.AddRange(users);
                _context.RoomTypes.AddRange(roomTypes);
                _context.Equipments.AddRange(equipments);
                _context.Images.AddRange(images);
                _context.UseCases.AddRange(useCases);
                _context.Categories.AddRange(categories);
                _context.Apartments.AddRange(apartments);
                _context.Comments.AddRange(comments);
                _context.Prices.AddRange(prices);
                _context.ApartmentEquipments.AddRange(apartmentEquipments);
                _context.UserUceCases.AddRange(userUseCases);
                _context.ApartmentImages.AddRange(apartmentImages);
                _context.ApartmentRooms.AddRange(apartmentRooms);
                _context.Reservations.AddRange(reservations);

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, new {messag = "Unexpected server error..."});
            }
        }
    }
}
