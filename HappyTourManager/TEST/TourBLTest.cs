using BL;
using DATA;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEST.Fakes;

namespace TEST
{
    [TestFixture]
    class TourBLTest
    {
        private Tour[] tours;
        private Program[] programs;
        private Place[] places;
        private PLTCON[] pltcons;
        private PRTCON[] prtcons;
        private Tourguide[] tourguides;
        private TourBL bl;
        private FakeRepository<Tour> tourRepository;
        private FakeRepository<Program> programRepository;
        private FakeRepository<Place> placeRepository;
        private FakeRepository<PLTCON> pltconRepository;
        private FakeRepository<PRTCON> prtconRepository;

        public TourBLTest()
        {
            CreateTestdataArrays();
            this.tourRepository = new FakeRepository<Tour>(tours);
            this.programRepository = new FakeRepository<Program>(programs);
            this.placeRepository = new FakeRepository<Place>(places);
            this.pltconRepository = new FakeRepository<PLTCON>(pltcons);
            this.prtconRepository = new FakeRepository<PRTCON>(prtcons);
            bl = new TourBL(tourRepository, programRepository, placeRepository, pltconRepository, prtconRepository);
        }

        private void CreateTestdataArrays()
        {
            places = new[] {
                new Place{
                PlaceID = 1,
                Country = "Egypt",
                City = "Cairo"},
                new Place{PlaceID = 2,
                Country = "Egypt",
                City = "Luxor"},
                new Place{
                PlaceID = 3,
                Country = "France",
                City = "Paris"},
                new Place{
                PlaceID = 4,
                Country = "France",
                City = "Marseilles"}
            };
            programs = new[] {
                new Program{
                    ProgramID = 1,
                    ProgramType = "gourmet"
                },
                new Program{
                    ProgramID = 2,
                    ProgramType = "museum"
                },
                new Program{
                    ProgramID = 3,
                    ProgramType = "extreme sport"
                },
            };
            tours = new[]
            {
                new Tour{
                    TourID = 1,
                    TravelName = "wonderful east",
                    Description = "loremipsum",
                    AdultPrice = 121000,
                    ChildPrice = 89000,
                    MinNumber = 6,
                    MaxNumber = 20,
                    StartDate = new DateTime(2018,09,10),
                    EndDate = new DateTime(2018,9,23),
                    Transport = "bus",
                    TourguideID = 1
                },
                new Tour{
                    TourID = 2,
                    TravelName = "french travel",
                    Description = "loremipsum",
                    AdultPrice = 89000,
                    ChildPrice = 67000,
                    MinNumber = 10,
                    MaxNumber = 45,
                    StartDate = new DateTime(2018,06,30),
                    EndDate = new DateTime(2018,07,10),
                    Transport = "bus",
                    TourguideID = 2
                }
            };

            prtcons = new[]
            {
                new PRTCON{PRTCONID = 1, ProgramID=2, TourID = 1},
                new PRTCON{PRTCONID = 2, ProgramID=3, TourID = 1},
                new PRTCON{PRTCONID = 3, ProgramID=1, TourID = 2},
                new PRTCON{PRTCONID = 4, ProgramID=2, TourID = 2}
            };
            ConnectProgramsAndTours(programs[1], tours[0], prtcons[0]);
            ConnectProgramsAndTours(programs[2], tours[0], prtcons[1]);
            ConnectProgramsAndTours(programs[0], tours[1], prtcons[2]);
            ConnectProgramsAndTours(programs[1], tours[1], prtcons[3]);
            pltcons = new[]
            {
                new PLTCON{PLTCONID = 1, PlaceID=1, TourID = 1},
                new PLTCON{PLTCONID = 2, PlaceID=2, TourID = 1},
                new PLTCON{PLTCONID = 3, PlaceID=3, TourID = 2},
                new PLTCON{PLTCONID = 4, PlaceID=4, TourID = 2}
            };
            tourguides = new[] {
            new Tourguide{

                PersonID = 1,
                Person = new Person()
                {
                    FirstName = "Jani",
                    LastName = "Kormos",
                    BirthDate = new DateTime(1956, 01, 02),
                    IDNumber = 765678765,
                    IDType = "identity card",
                    AddressCity = "Pécs",
                    AddressCountry = "Hungary",
                    AddressFree = "Kossuth u 34",
                    AddressZip = "3200",
                    Phone = 063099912312,
                    ValidTo = new DateTime(2027, 02, 02)
                },
                Dailyallowance = 23000,
                Taxidentification = 195600001
            },
            new Tourguide{
                PersonID = 1,
                Person = new Person()
                {
                    FirstName = "Eliza",
                    LastName = "Cirmos",
                    BirthDate = new DateTime(1986, 03, 02),
                    IDNumber = 888888888,
                    IDType = "identity card",
                    AddressCity = "Budapest",
                    AddressCountry = "Hungary",
                    AddressFree = "Neumann u 34",
                    AddressZip = "3200",
                    Phone = 063099912555,
                    ValidTo = new DateTime(2019, 02, 02)
                },
                Dailyallowance = 20000,
                Taxidentification = 198600023
            } };
            
            ConnectPlacesAndTours(places[0], tours[0],pltcons[0]);
            ConnectPlacesAndTours(places[1], tours[0], pltcons[1]);
            ConnectPlacesAndTours(places[2], tours[1], pltcons[2]);
            ConnectPlacesAndTours(places[3], tours[1], pltcons[3]);

            ConnectToursAndGuides(tours[0], tourguides[0]);
            ConnectToursAndGuides(tours[1], tourguides[1]);
        }

        private void ConnectPlacesAndTours(Place place, Tour tour, PLTCON pltcon)
        {
            tour.PLTCONs.Add(pltcon);
            place.PLTCONs.Add(pltcon);
            pltcon.Place = place;
            pltcon.Tour = tour;
        }

        private void ConnectProgramsAndTours(Program program, Tour tour, PRTCON prtcon)
        {
            tour.PRTCONs.Add(prtcon);
            program.PRTCONs.Add(prtcon);
            prtcon.Program = program;
            prtcon.Tour = tour;
        }

        private void ConnectToursAndGuides(Tour tour, Tourguide guide)
        {
            guide.Tours.Add(tour);
            tour.Tourguide = guide;
        }
        [Test]
        public void WhenCreatingTourBL_ThenTourBLIsNotNull()
        {
            // ARRANGE - ACT
            //arranged in constructor
            // ASSERT
            Assert.That(bl, Is.Not.Null);
        }

        [TestCase("France", 1)]
        [TestCase("franCE", 1)]
        [TestCase("USA", 0)]
        [TestCase("fra", 0)] //can't search for substring yet
        public void WhenSearchingForACountry_ThenGetsToursToThatCountry(string country, int result)
        {
            //ARRANGE --> Testarray
            //ACT
            IList<Tour> list = bl.Search(TourTerms.COUNTRY, country);

            //ASSERT
            Assert.That(list.Count, Is.EqualTo(result));
        }

        [TestCase("Cairo", 1)]
        [TestCase("caIRO", 1)]
        [TestCase("Bukarest", 0)]
        [TestCase("cai", 0)] //can't search for substring yet
        public void WhenSearchingForACity_ThenGetsToursToThatCity(string city, int result)
        {
            //ARRANGE --> Testarray
            //ACT
            IList<Tour> list = bl.Search(TourTerms.CITY, city);

            //ASSERT
            Assert.That(list.Count, Is.EqualTo(result));
        }

        [TestCase("museum", 2)]
        [TestCase("MUseUM", 2)]
        [TestCase("beach", 0)]
        [TestCase("mus", 0)] //can't search for substring yet
        public void WhenSearchingForAProgram_ThenGetsToursWithThisProgram(string program, int result)
        {
            //ARRANGE --> Testarray
            //ACT
            IList<Tour> list = bl.Search(TourTerms.PROGRAM, program);

            //ASSERT
            Assert.That(list.Count, Is.EqualTo(result));
        }
    }
}
