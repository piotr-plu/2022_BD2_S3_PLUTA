using Narciarze_v_2.Models;
using Microsoft.EntityFrameworkCore;

namespace Narciarze_v_2.Data
{
    public class TrasySql : ITrasy
    {
        private Narty_V2Context Narty_V2Context { get; }
        public TrasySql(Narty_V2Context narty_V2Context) 
        { 
            Narty_V2Context = narty_V2Context;
        }
        public async Task<IEnumerable<Stoki>> GetStoki()
        {
            return await Narty_V2Context.Stokis.ToListAsync();
        }
    }
}
