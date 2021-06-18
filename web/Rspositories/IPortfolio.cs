using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Rspositories
{
    public interface IPortfolio<Tentity>
    {
         IEnumerable<Tentity> GetAll();
        Tentity find(int id);

        void add(Tentity tentity);
        void delete(int id);
        void update(Tentity tentity);
    }
}
