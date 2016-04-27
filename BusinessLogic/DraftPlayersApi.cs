using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class DraftPlayersApi
    {
        private FootballEntities _db;

        public DraftPlayersApi()
        {
            _db = DomainFactory.DB();
        }

        public IQueryable<DraftPlayer> GetAllDraftPlayers()
        {
            return _db.DraftPlayers.Select(dp => dp);
        }
    }
}
