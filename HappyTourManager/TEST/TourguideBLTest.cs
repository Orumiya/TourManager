using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using DATA;
using DATA.Repositoriees;
using NUnit.Framework;


namespace TEST
{
    //[TestFixture]
    public class TourguideBLTest
    {
        [Test]
        public void WhenCreatingTourguideBL_ThenTourguideBLIsNotNull()
        {
            // ARRANGE - ACT
            HappyTourDatabaseEntities entities = new HappyTourDatabaseEntities();
            TourguideRepository tourguideRepository = new TourguideRepository(entities);
            LanguageRepository languageRepository = new LanguageRepository(entities);
            OnholidayRepository onHolidayRepository = new OnholidayRepository(entities);
            TourguideBL bl = new TourguideBL(tourguideRepository, languageRepository, onHolidayRepository);

            // ASSERT
            Assert.That(bl, Is.Not.Null);
        }

        [Test]
        public void WhenCreatingNewTourguide_ThenTourguideIsSaved()
        {
            // ARRANGE 
            //TourguideBL bl = new TourguideBL(tourguideRepository, languageRepository, onHolidayRepository);
            ///ACT
        }
    }
}
