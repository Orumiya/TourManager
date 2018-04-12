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
        private TourguideBL bl;
        private FakeRepository<Tourguide> tourguideRepository;
        private FakeRepository<OnHoliday> onHolidayRepository;
        private FakeRepository<Language> languageRepository;
        //private Tour[] tours;
        //private Program[] programs;
        //private Place[] places;
        //private PLTCON[] pltcon;
        //private PRTCON[] prtcon;

        public TourguideBLTest()
        {
            CreateTestdataArrays();
            tourguideRepository = new FakeRepository<Tourguide>(tourguides);
            onHolidayRepository = new FakeRepository<OnHoliday>(onholidays);
            languageRepository = new FakeRepository<Language>(languages);
            bl = new TourguideBL(tourguideRepository, languageRepository, onHolidayRepository);
        }

        [Test]
        public void WhenCreatingTourguideBL_ThenTourguideBLIsNotNull()
        {
            // ARRANGE - ACT
            
            // ASSERT
            Assert.That(bl, Is.Not.Null);
        }
        
        private void CreateTestdataArrays()
        {
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

            Connect(tourguides[0], onholidays[1]);
            Connect(tourguides[1], onholidays[0]);
        }

        [Test]
        public void WhenCreatingNewTourguide_ThenTourguideIsSaved()
        {
            // ARRANGE 
            Tourguide tg = new Tourguide
            {
                PersonID = 3,
                Person = new Person()
                {
                    FirstName = "Mirabella",
                    LastName = "Nemesis",
                    BirthDate = new DateTime(1997, 10, 02),
                    IDNumber = 333333338,
                    IDType = "identity card",
                    AddressCity = "Wien",
                    AddressCountry = "Austria",
                    AddressFree = "Neumann u 34",
                    AddressZip = "3200",
                    Phone = 063099912333,
                    ValidTo = new DateTime(2024, 02, 02)
                },
                Dailyallowance = 25000,
                Taxidentification = 198600555
            };
            ///ACT
            bl.Save(tg);

            ///ASSERT
            Assert.That(tourguideRepository.SavedObjects.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenSearchingForTourguidesOnHolidayBetween2Dates_ThenGetTourguidesOnHoliday()
        {
            //ARRANGE

            //ACT
            IList<Tourguide> list = bl.Search(TourguideTerms.IsOnHoliday, new DateTime[] { new DateTime(2018,05,18), new DateTime(2018,05,24) });
            //ASSERT
            Assert.That(list.Count, Is.EqualTo(1));
        }

        private void Connect(Tourguide guide, OnHoliday holiday)
        {
            guide.OnHolidays.Add(holiday);
            holiday.Tourguide = guide;
        }
    }
}
