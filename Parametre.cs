using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace VeritabaniIslem
{
    public class Parametre
    {
        private List<OleDbParameter> parametre = new List<OleDbParameter>();


        public Parametre()
        {

        }

        public Parametre(string veri)
        {
            this.Ekle(veri);
        }

        public Parametre(params string[] veriDizi)
        {
            foreach (var veri in veriDizi)
            {
                this.Ekle(veri);
            }
        }

        public void Ekle(string _deger)
        {
            parametre.Add( new OleDbParameter("@",_deger));
        }

        public OleDbParameter[] Listele()
        {
            return parametre.ToArray();
        }

    }
}
