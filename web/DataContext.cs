using core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace web
{
    public class DataContext: DbContext
    {
       
   
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }



        public DbSet<Owner> Owner { get; set; }
        public DbSet<portfolioItem> PortfolioItem { get; set; }

        


    }
}
