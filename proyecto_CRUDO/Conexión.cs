using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace IngresoTienda
{
    class Conexión
    {
        SqlConnection con;
        public SqlConnection Conectar()
        {
            try  
            {
                con = new SqlConnection("data Source= CNGAPRCIPFSD063\\SQLEXPRESS; " +
                "Initial Catalog = BD_login; Integrated Security = true");  
                con.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return con; 
        }

        public void Cerrar()
        {
            con.Close();
        }

    }
}
