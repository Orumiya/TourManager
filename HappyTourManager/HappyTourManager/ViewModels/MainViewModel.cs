namespace HappyTourManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BL;
    using DATA;
    using DATA.Repositoriees;
    using DATA.Repositories;

    class MainViewModel
    {
        // private CustomerRepository custRepository;
        // private CustomerBL custBL;
        CustomerRepository customerRepo;
        LanguageRepository languageRepo;
        OfficeRepository officeRepo;
        OnholidayRepository onHolidayRepo;
        OrderRepository orderRepo;
        PlaceRepository placeRepo;
        PLTCONRepository pltconRepo;
        ProgramRepository programRepo;
        PRTCONRepository prtconRepo;
        ReportRepository reportRepo;
        TourguideRepository tourguideRepo;
        TourRepository tourRepo;
        UserRepository userRepo;
        private string selectedPage;

        public MainViewModel(CustomerRepository customerRepo,
                            LanguageRepository languageRepo,
                            OfficeRepository officeRepo,
                            OnholidayRepository onHolidayRepo,
                            OrderRepository orderRepo,
                            PlaceRepository placeRepo,
                            PLTCONRepository pltconRepo,
                            ProgramRepository programRepo,
                            PRTCONRepository prtconRepo,
                            ReportRepository reportRepo,
                            TourguideRepository tourguideRepo,
                            TourRepository tourRepo
                                )
        {
            this.customerRepo = customerRepo;
            this.languageRepo = languageRepo;
            this.officeRepo = officeRepo;
            this.onHolidayRepo = onHolidayRepo;
            this.orderRepo = orderRepo;
            this.placeRepo = placeRepo;
            this.pltconRepo = pltconRepo;
            this.programRepo = programRepo;
            this.prtconRepo = prtconRepo;
            this.reportRepo = reportRepo;
            this.tourguideRepo = tourguideRepo;
            this.tourRepo = tourRepo;

        }

    }
}
