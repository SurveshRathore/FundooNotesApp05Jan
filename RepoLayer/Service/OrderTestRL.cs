using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class OrderTestRL: IOrderTestRL
    {
        private readonly FundooDBContext fundooDBContext;
        private readonly IConfiguration configuration;

        public OrderTestRL(FundooDBContext fundooDBContext, IConfiguration configuration)
        {
            this.fundooDBContext = fundooDBContext;
            this.configuration = configuration;
        }

        public OrdeTable AddOrder(OrderModel order)
        {
            try
            {
                OrdeTable ordeTable = new OrdeTable();
                ordeTable.ProductName = order.ProductName;
                ordeTable.Quantity = order.Quantity;
                ordeTable.UserID = order.UserID;

                fundooDBContext.ordeTables.Add(ordeTable);
                int result = fundooDBContext.SaveChanges();

                if(result != 0)
                {
                    return ordeTable;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
