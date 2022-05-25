using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace IngresoTienda
{
    
    class Query
    {
      
        Conexión c = new Conexión();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        Boolean Estado_conexion = false;
        public static string tipoUsuario;

        public Boolean IniciarSesion(string idusuario, string contraseña) 
        
        {    
            SqlCommand consulta;
            consulta = new SqlCommand("select idUsuarios, contraseñaUsuarios, tipoUsuarios from Usuarios where idUsuarios = @idUsuarios and contraseñaUsuarios = @contraseñaUsuarios", c.Conectar());
            consulta.CommandType = CommandType.Text;
            consulta.Parameters.AddWithValue("@idUsuarios", idusuario);
            consulta.Parameters.AddWithValue("@contraseñaUsuarios", contraseña);
            consulta.ExecuteNonQuery();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(consulta);
                da.Fill(ds, "Usuarios");
                DataRow dr;
                dr = ds.Tables["Usuarios"].Rows[0];

                if (idusuario == dr["idUsuarios"].ToString() & contraseña == dr["contraseñaUsuarios"].ToString() & "Administrador" == dr["tipoUsuarios"].ToString())
                {
                    Form2 fmr = new Form2();
                    fmr.Show();
                    Estado_conexion = true;
                    tipoUsuario = "Administrador";
                    
                }
                else
                {
                    if (idusuario == dr["idUsuarios"].ToString() & contraseña == dr["contraseñaUsuarios"].ToString() & "Cliente" == dr["tipoUsuarios"].ToString())
                    {
                        Form4 fr = new Form4();
                        fr.Show();
                        Estado_conexion = true;
                        tipoUsuario = "Cliente";
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                c.Cerrar();
            }
            return Estado_conexion;
        }
    }
}
