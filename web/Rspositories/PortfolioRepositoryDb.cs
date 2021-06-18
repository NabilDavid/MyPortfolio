using core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Rspositories
{
    public class PortfolioRepositoryDb : IPortfolio<portfolioItem>
    {
        private readonly DataContext db;

        public PortfolioRepositoryDb( DataContext dataContext)
        {
            this.db = dataContext;
        }

      

        public void add(portfolioItem tentity)
        {
            db.PortfolioItem.Add(tentity);
            db.SaveChanges();
        }

        public void delete(int id)
        {
            var item = find(id);
            db.PortfolioItem.Remove(item);
            db.SaveChanges();
        }

        public portfolioItem find(int id)
        {
            var item= db.PortfolioItem.Find(id);
            return item;
        }

        public IEnumerable<portfolioItem> GetAll()
        {
            return db.PortfolioItem.ToList();
        }

        public void update(portfolioItem  tentity)
        {
            db.Update(tentity);
            db.SaveChanges();
        }
    }
}
