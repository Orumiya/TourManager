using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using DATA;
using DATA.Repositoriees;
using NUnit.Framework;
using TEST.Fakes;

namespace TEST
{
    [TestFixture]
    internal class TourguideBLTest
    {
        private Tourguide[] tourguides;
        private Language[] languages;
        private OnHoliday[] onholidays;
        //private Tour[] tours;
        //private Program[] programs;
        //private Place[] places;
        //private PLTCON[] pltcon;
        //private PRTCON[] prtcon;

        [Test]
        public void WhenCreatingTourguideBL_ThenTourguideBLIsNotNull()
        {
            // ARRANGE - ACT

            CreateTestdataArrays();
            var tourguideRepository = new FakeRepositoryImpl(tourguides);
            var onHolidayRepository = new FakeRepository<OnHoliday>(onholidays);
            var languageRepository = new FakeRepository<Language>(languages);
            
            TourguideBL bl = new TourguideBL(tourguideRepository, languageRepository, onHolidayRepository);

            // ASSERT
            Assert.That(bl, Is.Not.Null);
        }

        private void CreateTestdataArrays()
        {
            //places = new[] { 
            //    new Place{
            //    PlaceID = 1,
            //    Country = "Egypt",
            //    City = "Cairo"},
            //    new Place{PlaceID = 2,
            //    Country = "Egypt",
            //    City = "Luxor"},
            //    new Place{
            //    PlaceID = 3,
            //    Country = "France",
            //    City = "Paris"},
            //    new Place{
            //    PlaceID = 4,
            //    Country = "France",
            //    City = "Marseilles"}
            //};
            //programs = new[] {
            //    new Program{
            //        ProgramID = 1,
            //        ProgramType = "gourmet"
            //    },
            //    new Program{
            //        ProgramID = 2,
            //        ProgramType = "museum"
            //    },
            //    new Program{
            //        ProgramID = 3,
            //        ProgramType = "extreme sport"
            //    },
            //};
            //tours = new[]
            //{
            //    new Tour{
            //        TourID = 1,
            //        TravelName = "wonderful east",
            //        Description = "loremipsum",
            //        AdultPrice = 121000,
            //        ChildPrice = 89000,
            //        MinNumber = 6,
            //        MaxNumber = 20,
            //        StartDate = new DateTime(2018,09,10),
            //        EndDate = new DateTime(2018,9,23),
            //        Transport = "bus",
            //        TourguideID = 0
            //    },
            //    new Tour{
            //        TourID = 2,
            //        TravelName = "french travel",
            //        Description = "loremipsum",
            //        AdultPrice = 89000,
            //        ChildPrice = 67000,
            //        MinNumber = 10,
            //        MaxNumber = 45,
            //        StartDate = new DateTime(2018,06,30),
            //        EndDate = new DateTime(2018,07,10),
            //        Transport = "bus",
            //        TourguideID = 0
            //    }
            //};
            
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
            languages = new[]
            {
                new Language
                {
                    LanguageID = 1,
                    Language1 = "english",
                    Tourguide = tourguides[0]
                },
                new Language
                {
                    LanguageID = 2,
                    Language1 = "french",
                    Tourguide = tourguides[1]
                },
            };
            onholidays = new[]
            {
                new OnHoliday
                {
                    OnHolidayID = 1,
                    StartDate = new DateTime(2018,05,15),
                    EndDate = new DateTime(2018,08,20),
                    Tourguide = tourguides[1]
                },
                new OnHoliday
                {
                    OnHolidayID = 2,
                    StartDate = new DateTime(2018,07,15),
                    EndDate = new DateTime(2018,07,20),
                    Tourguide = tourguides[0]
                }
            };
            //prtcon = new[]
            //{
            //    new PRTCON{PRTCONID = 1, ProgramID=2, TourID = 1},
            //    new PRTCON{PRTCONID = 2, ProgramID=3, TourID = 1},
            //    new PRTCON{PRTCONID = 3, ProgramID=1, TourID = 2},
            //    new PRTCON{PRTCONID = 4, ProgramID=2, TourID = 2}
            //};
        }

        //private void ConnectTours(Tourguide guide, Tour tour)
        //{
        //    guide.Tours.Add(tour);
        //    tour.Tourguide = guide;
        //}
        //private void ConnectPrograms(Program program, Tour tour)
        //{
            
        //}

        [Test]
        public void WhenCreatingNewTourguide_ThenTourguideIsSaved()
        {
            // ARRANGE 
            //TourguideBL bl = new TourguideBL(tourguideRepository, languageRepository, onHolidayRepository);
            ///ACT
        }
    }
}
