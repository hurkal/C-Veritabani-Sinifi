using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace VeritabaniIslem
{
    public class Veritabani 
    {
        OleDbConnection baglanti;
        OleDbDataAdapter daKomut;
        OleDbCommand komut;
        DataTable dt;
        public static string cumle;

        public Veritabani(string _cumle)
        {
            Veritabani.cumle = _cumle;
            baglanti = new OleDbConnection(Veritabani.cumle);
            baglanti.Open();
        }

        ~Veritabani()
        {
            try
            {
                baglanti.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public DataTable TabloCek(string _komut, Parametre _param=null)
        {
            dt = new DataTable();
            daKomut = new OleDbDataAdapter(_komut, baglanti);

            if (_param!=null)
                daKomut.SelectCommand.Parameters.AddRange(_param.Listele());

            daKomut.Fill(dt);
            return dt;
        }

        public string TekVeriCek(string _komut, Parametre _param=null)
        {
            komut = new OleDbCommand(_komut, baglanti);

            if (_param!=null)
                komut.Parameters.AddRange(_param.Listele());

            return komut.ExecuteScalar().ToString();
        }

        public int KomutCalistir(string _komut,Parametre _param=null)
        {
            komut = new OleDbCommand(_komut, baglanti);

            if (_param!=null)
                komut.Parameters.AddRange(_param.Listele());

            return komut.ExecuteNonQuery();
        }

        public OleDbDataReader SatirOku(string sqlCumle, Parametre _param = null)
        {
            komut = new OleDbCommand(sqlCumle, baglanti);

            if (_param != null)
                komut.Parameters.AddRange(_param.Listele());

            return komut.ExecuteReader();
        }

        public object ProsedurCalistir(string _komut, Parametre _param = null, bool degerDondur=false)
        {
            if (degerDondur)
                _komut += ",? OUTPUT";

            komut = new OleDbCommand(_komut, baglanti);          

            if (_param != null)
                komut.Parameters.AddRange(_param.Listele());

            if (degerDondur)
            {
                OleDbParameter donus = new OleDbParameter("@id", OleDbType.Integer);
                donus.Direction = ParameterDirection.Output;
                komut.Parameters.Add(donus);

                komut.ExecuteNonQuery();

                return donus.Value;
            } else
            {
                komut.ExecuteNonQuery();

                return null;
            }
        }
    }
}
