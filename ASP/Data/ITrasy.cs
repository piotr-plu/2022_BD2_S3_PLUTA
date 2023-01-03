using Narciarze_v_2.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narciarze_v_2.Data
{
    public interface ITrasy
    {
        Task<IEnumerable<Stoki>> GetStoki();

    }
}
