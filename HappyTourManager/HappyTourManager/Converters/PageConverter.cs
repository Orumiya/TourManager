using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA;
using DATA.Interfaces;
using DATA.Repositoriees;
using DATA.Repositories;
using HappyTourManager.Pages;

namespace HappyTourManager
{
    public class PageConverter : ValueConverter<PageConverter>
    {
        private HappyTourDatabaseEntities entities;
        private IRepository<Order> orderRepository;
        private IRepository<Customer> customerRepository;
        private IRepository<Tour> tourRepository;
        private IRepository<Program> programRepository;
        private IRepository<Place> placeRepository;
        private IRepository<PLTCON> pltconRepository;
        private IRepository<PRTCON> prtconRepository;
        private IRepository<Report> reportRepository;
        private IRepository<Tourguide> tourguideRepository;
        private IRepository<Language> languageRepository;
        private IRepository<OnHoliday> onHolidayRepository;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            this.entities = new HappyTourDatabaseEntities();
            this.orderRepository = new OrderRepository(entities);
            this.customerRepository = new CustomerRepository(entities);
            this.tourRepository = new TourRepository(entities);
            this.programRepository = new ProgramRepository(entities);
            this.placeRepository = new PlaceRepository(entities);
            this.pltconRepository = new PLTCONRepository(entities);
            this.prtconRepository = new PRTCONRepository(entities);
            this.reportRepository = new ReportRepository(entities);
            this.tourguideRepository = new TourguideRepository(entities);
            this.languageRepository = new LanguageRepository(entities);
            this.onHolidayRepository = new OnholidayRepository(entities);

            switch ((string)value)
            {
                case "LoginPage":
                    return new LoginPage();
                case "CustomerPage":
                    return new CustomerMainPage();
                case "OrderPage":
                    return new OrderMainPage(orderRepository, customerRepository, tourRepository, programRepository, placeRepository, pltconRepository, prtconRepository);
                default:
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
