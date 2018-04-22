using BL.Interfaces;
using DATA;
using DATA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class OrderBL : ISearcheable<Order>, IOrderList
    {
        private readonly IRepository<Order> orderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderBL"/> class.
        /// creates the OrderBL
        /// </summary>
        /// <param name="orderRepository">input repository</param>
        public OrderBL(IRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        /// <inheritdoc />
        public event EventHandler OrderListChanged;

        /// <inheritdoc />
        public void Delete(Order order)
        {
            try
            {
                this.orderRepository.Delete(order);
            }
            finally
            {
                this.OnOrderListChanged();
            }
        }

        /// <inheritdoc />
        public void Save(Order order)
        {
            try
            {
                this.orderRepository.Create(order);
            }
            finally
            {
                this.OnOrderListChanged();
            }
        }

        /// <inheritdoc />
        public IList<Order> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void ThrowIfExists(Order order)
        {
            this.orderRepository.ThrowIfExists(order);
        }

        /// <summary>
        /// notifies the outside about any collection change manually
        /// </summary>
        private void OnOrderListChanged()
        {
            this.OrderListChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
