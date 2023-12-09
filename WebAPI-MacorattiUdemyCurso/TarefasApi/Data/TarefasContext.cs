using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TarefasApi.Data
{
    public class TarefasContext
    {
        public delegate Task<IDbConnection> GetConnection();
    }
}