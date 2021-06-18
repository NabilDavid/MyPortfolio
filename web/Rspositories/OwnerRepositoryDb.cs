using core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Rspositories
{
    public class OwnerRepositoryDb : IPortfolio<Owner>
    {
        private readonly DataContext db;

        public OwnerRepositoryDb(DataContext dataContext)
        {
            this.db = dataContext;
        }

        public void add(Owner tentity)
        {
            db.Add(tentity);
            db.SaveChanges();
        }

        public void delete(int id)
        {
            var owner = find(id);
            db.Remove(owner);
            db.SaveChanges();
        }

        public Owner find(int id)
        {
           
            //var owner = db.Owner.Include(a => a.address).Where(o => (Object)o.Id == id).ToList();
            return db.Owner.Find(id);
        }

        public IEnumerable<Owner> GetAll()
        {
            return db.Owner.ToList();
        }

        public void update(Owner tentity)
        {
            var owner = find(tentity.Id);
            db.Update(owner);
            db.SaveChanges();
        }
    }
}
