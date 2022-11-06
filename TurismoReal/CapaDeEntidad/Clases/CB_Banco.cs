using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CB_Banco
    {
        private int _IdBanco;
        private string _BancoName;

        public int IdBanco { get => _IdBanco; set => _IdBanco = value; }
        public string BancoName { get => _BancoName; set => _BancoName = value; }
    }
}
