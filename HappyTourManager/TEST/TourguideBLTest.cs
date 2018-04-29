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
            //arranged in constructor
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
                    EndDate = new DateTime(2018,06,20),
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
            Connect(tourguides[1], languages[1]);
            Connect(tourguides[0], languages[0]);
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

        [TestCase(2018,04,10, 2018,05,10,0)] //before
        [TestCase(2018, 07, 21, 2018, 08, 09, 0)] //after
        [TestCase(2018, 06, 25, 2018, 07, 05, 0)] //between, in a gap
        [TestCase(2018, 04, 16, 2018, 05, 20, 1)] //start inside
        [TestCase(2018, 05, 18, 2018, 05, 29, 1)] //inside
        [TestCase(2018, 06, 10, 2018, 06, 30, 1)] //end inside
        [TestCase(2018, 06, 20, 2018, 07, 15, 2)] //start touching + end touching
        [TestCase(2018, 06, 18, 2018, 07, 18, 2)] //start inside + end inside
        [TestCase(2018, 05, 10, 2018, 07, 25, 2)] //enclosing
        public void WhenSearchingForTourguidesOnHolidayBetween2Dates_ThenGetTourguidesOnHoliday(
            int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay, int result)
        {
            //ARRANGE
            //arranged in  CreateTestdataArrays();
            //ACT
            IList<Tourguide> list = bl.Search(TourguideTerms.ISONHOLIDAY, new DateTime[] { new DateTime(startYear,startMonth,startDay), new DateTime(endYear,endMonth,endDay) });
            //ASSERT
            Assert.That(list.Count, Is.EqualTo(result));
        }

        private void Connect(Tourguide guide, OnHoliday holiday)
        {
            guide.OnHolidays.Add(holiday);
            holiday.Tourguide = guide;
        }

        private void Connect(Tourguide guide, Language language)
        {
            guide.Languages.Add(language);
            language.Tourguide = guide;
        }

        [TestCase(2018, 04, 10, 2018, 05, 10, 2)] //before
        [TestCase(2018, 07, 21, 2018, 08, 09, 2)] //after
        [TestCase(2018, 06, 25, 2018, 07, 05, 2)] //between, in a gap
        [TestCase(2018, 04, 16, 2018, 05, 20, 1)] //start inside
        [TestCase(2018, 05, 18, 2018, 05, 29, 1)] //inside
        [TestCase(2018, 06, 10, 2018, 06, 30, 1)] //end inside
        [TestCase(2018, 06, 20, 2018, 07, 15, 0)] //start touching + end touching
        [TestCase(2018, 06, 18, 2018, 07, 18, 0)] //start inside + end inside
        [TestCase(2018, 05, 10, 2018, 07, 25, 0)] //enclosing
        public void WhenSearchingForAvailableTourguidesBetween2Dates_ThenGetTourguidesWhoAreNotOnHoliday(
            int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay, int result)
        {
            //ARRANGE
            //arranged in  CreateTestdataArrays();
            //ACT
            IList<Tourguide> list = bl.Search(TourguideTerms.ISAVAILABLE, new DateTime[] { new DateTime(startYear, startMonth, startDay), new DateTime(endYear, endMonth, endDay) });
            //ASSERT
            Assert.That(list.Count, Is.EqualTo(result));

        }

        [Test]
        public void WhenSearchingForLanguage_ThenGetsTourguidesWhoSpeaksThisLanguage()
        {
            //ARRANGE
            //arranged in  CreateTestdataArrays();
            //ACT
            IList<Tourguide> list = bl.Search(TourguideTerms.LANGUAGE, "english");
            //ASSERT
            Assert.That(list.Contains(tourguides[0]), Is.True);
        }

        [Test]
        public void WhenSearchingForLanguage_ThenDoesntGetTourguidesNoOneSpeaksThisLanguage()
        {
            //ARRANGE
            //arranged in  CreateTestdataArrays();
            //ACT
            IList<Tourguide> list = bl.Search(TourguideTerms.LANGUAGE, "persian");
            //ASSERT
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [TestCase("Cirmos")]
        [TestCase("cIRMOS")]
        public void WhenSearchingForLastName_ThenWeFindATourguide(string lastName)
        {
            //ARRANGE
            //arranged in  CreateTestdataArrays();
            //ACT
            IList<Tourguide> list = bl.Search(TourguideTerms.LASTNAME, lastName);
            //ASSERT
            Assert.That(list.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenSearchingForTaxID_ThenWeFindATourguide()
        {
            //ARRANGE
            //arranged in  CreateTestdataArrays();
            //ACT
            IList<Tourguide> list = bl.Search(TourguideTerms.TAXIDENTIFICATION, 198600023);
            //ASSERT
            Assert.That(list.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenSearchingForDefault_ThenWeGetAllTourguides()
        {
            //ARRANGE
            //arranged in  CreateTestdataArrays();
            //ACT
            IList<Tourguide> list = bl.Search(TourguideTerms.DEFAULT, null);
            //ASSERT
            Assert.That(list.Count, Is.EqualTo(2));
        }
    }
}
