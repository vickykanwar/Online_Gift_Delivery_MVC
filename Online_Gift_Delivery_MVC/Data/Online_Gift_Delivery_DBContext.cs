using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Gift_Delivery_MVC.Models;

namespace Online_Gift_Delivery_MVC.Data
{
    public class Online_Gift_Delivery_DBContext : DbContext
    {
        public Online_Gift_Delivery_DBContext (DbContextOptions<Online_Gift_Delivery_DBContext> options)
            : base(options)
        {
        }

        public DbSet<Online_Gift_Delivery_MVC.Models.Gift> Gift { get; set; }

        public DbSet<Online_Gift_Delivery_MVC.Models.GiftPurchase> GiftPurchase { get; set; }

        public DbSet<Online_Gift_Delivery_MVC.Models.GiftSender> GiftSender { get; set; }

        public DbSet<Online_Gift_Delivery_MVC.Models.ShippingMethod> ShippingMethod { get; set; }
    }
}
