using CommonLayer.Model;
using ManagerLayer.Interface;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Service
{
    public class OrdertestBL:IOrderTestBL
    {
        private readonly IOrderTestRL orderTestRL;

        public OrdertestBL (IOrderTestRL orderTestRL)
        {
            this.orderTestRL = orderTestRL;
        }

        public OrdeTable AddNewOrder(OrderModel orderModel)
        {
            try
            {
                return this.orderTestRL.AddOrder(orderModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
