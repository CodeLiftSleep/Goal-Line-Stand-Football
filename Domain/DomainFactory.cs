using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class DomainFactory
    {
        private static FootballEntities _footballEntities = new FootballEntities();

        public static FootballEntities DB () { return _footballEntities; }
    }
}
