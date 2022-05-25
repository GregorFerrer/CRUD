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
    class Ingreso
    {
        SqlConnection con;
        public SqlConnection Conectar()
        {
            try  // el bloque Try Catch se usa para el manejo de excepciones en la conexión
            {
                con = new SqlConnection("data Source=DESKTOP - AEMLS35\\SQLEXPRESS; " +
                "Initial Catalog = BD TIENDA; Integrated Security = true");  //aquí se creó la cadena de conexión.
                con.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message); // mensaje que se mostrará al usuario indicando las excepciones que puedan surgir
            }
            return con; // variable que se retorna en la función, ella contiene la cadena de conexión.
        }

        public void Cerrar()
        {
            con.Close();
        }
    }
}
