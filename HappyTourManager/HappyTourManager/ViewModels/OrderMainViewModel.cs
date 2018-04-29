using BL;
using DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyTourManager
{
    public class OrderMainViewModel : Bindable
    {
        private IList<Order> orderList;
        private OrderBL orderBL;

        public OrderMainViewModel()
        {
            this.orderBL = new OrderBL()
            this.RefreshOrderList();
        }

        public void RefreshOrderList()
        {
            OnPropertyChanged(nameof(OrderList));
        }

        public IList<Order> OrderList
        {
            get { return orderList; }
            set { orderList = value; }
        }

    }
}
