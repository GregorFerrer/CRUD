using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IngresoTienda
{ 
    public partial class Form2 : Form
    {
        Query qr = new Query();
        Conexión cn = new Conexión();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();

        public Form2()
        {
            InitializeComponent();
        }
        public void Consultar()
        {
            SqlCommand consulta2; // declaracion de la variable donde se guarda la consulta sql ejecutada

            consulta2 = new SqlCommand("select * from Usuarios", cn.Conectar()); // escritura de consulta sql

            consulta2.CommandType = CommandType.Text; // se especifica el tipo de consulta

            consulta2.ExecuteNonQuery(); // ejecuta la consulta sql

            ds.Clear(); // impia el dataset para evitar la dup´licacion de la informacion al presionar varias veces el boton consultar

            SqlDataAdapter da = new SqlDataAdapter(consulta2); // paso de la variable consulta2 como parametro del data adapter

            da.Fill(ds, "Usuarios"); // llenar el dataset llamando el metodo fill a traves de la instancia del
            //sqlDataAdapter, pasando como parametro la instancia creada de la clase dataset y el nombre de la tabla

            try
            {
                // mostrar los registros de la tabla usuarios en el control datagridview
                dataGridView1.DataMember = ("Usuarios"); // en el metodo datamenber se especifica el nombre de la tabla que se va a llenar el datagridview

                dataGridView1.DataSource = ds; // aqui se especifica el objeto dataset y el nombre de la tabla
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        public void Modificar (int idUsuarios, string contraseñaUsuarios)
        {
            SqlCommand actualiza; // variable donde se almacenará la consulta sql que se ejecutará para realizar la actualización del registro

            actualiza = new SqlCommand("update Usuarios set contraseñaUsuarios=@contraseñaUsuarios where idUsuarios=@idUsuarios", cn.Conectar());
            // escritura de la consulta sql que se desea ejecutar

            // relación de parámetros del método con los campos de la tabla Usuario
            try
            {
                actualiza.Parameters.AddWithValue("@idUsuarios", idUsuarios); actualiza.Parameters.AddWithValue("@contraseñaUsuarios", contraseñaUsuarios);
                actualiza.ExecuteNonQuery(); // Ejecución de la consulta sql

                ds.Clear(); // Limpiar el DataGridView para evitar duplicar registros al presionar varias veces el botón Actualizar

                Consultar(); // Llamando del método Consultar dentro del método Actualizar para que la modificación quede realizada automáticamente en el DataGridView
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                cn.Cerrar();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //Especificar el TextBox1 como control de solo lectura: En la propiedad ReadOnly establecer el valor true
            //Para que en el se asigne de forma automática el valor de la celda seleccionada en el DataGridView
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Modificar(int.Parse(textBox3.Text), textBox4.Text); 
            // los parámetros definidos en el método se relacionan con los controles TextBox1 y
            // TextBox2 respectivamente.
        }

        public void InsertarRegistros(int idUsuarios, string contraseñaUsuarios, string tipoUsuarios)
        {
            SqlCommand insert; // variable que almacena la consulta sql para crear el registro de usuario

            try
            {
                insert = new SqlCommand(" insert into Usuarios (idUsuarios, contraseñaUsuarios, tipoUsuarios) Values (@idUsuarios,@contraseñaUsuarios,@tipoUsuarios)", cn.Conectar());
                // escritura de la consulta sql para registrar nuevos usuarios

                insert.CommandType = CommandType.Text; // especificacion de tipo de consulta

                //relacionar los parametros del metodo con los campos de la tabla usuarios y sus respectivos tipos de datos
                insert.Parameters.AddWithValue("@idUsuarios", SqlDbType.Int).Value = idUsuarios;

                insert.Parameters.AddWithValue("@contraseñaUsuarios", SqlDbType.NVarChar).Value = contraseñaUsuarios;

                insert.Parameters.AddWithValue("@tipoUsuarios", SqlDbType.NVarChar).Value = tipoUsuarios;

                insert.ExecuteNonQuery(); // ejecutar la consulta sql

                textBox1.Text = ""; //, limpia la caja de texto del id despues de registrar
                textBox2.Text = ""; //, limpia la caja de texto de la contraseña despues de registrar

                MessageBox.Show("Registro exitoso"); // mensaje que se mostrara cuando se inserte el registro
            }

            catch (Exception e)
            {
                MessageBox.Show("El usuario ya existe"); // se mostrara este mensaje cuando intente registrar mas de una vez un usuario existente

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertarRegistros(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()),

                dataGridView1.CurrentRow.Cells[1].Value.ToString(),
                dataGridView1.CurrentRow.Cells[1].Value.ToString());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            InsertarRegistros(int.Parse(textBox1.Text), textBox2.Text,
            comboBox1.SelectedItem.ToString());
            // en el combobox se captura el valor seleccionado (encargado / administrador)

            Consultar(); // se llama el metodo consultar para que el registro se muestre en el datagrid
        }

        public void Eliminar(int idUsuarios)
        {
            try
            {
                SqlCommand eliminar; // variable que guardara la consulta sql de eliminar registro

                eliminar = new SqlCommand("Delete from Usuarios Where idUsuarios=@idUsuarios", 
                    cn.Conectar());
                //consulta para eliminar usuario

                eliminar.CommandType = CommandType.Text; // se especifica el tipo de consulta 

                eliminar.Parameters.AddWithValue("@idUsuarios",
                    SqlDbType.Int).Value = idUsuarios; // especifica parametro

                eliminar.ExecuteNonQuery(); // ejecuta la consulta

                MessageBox.Show("El usuario ha sido eliminado exitosamente");
                //se mostrara este mensae si el usuario es borrado exitosamente

                Consultar();// llama el metodo consultar para actualizar el datagrid con el registro eliminado
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Eliminar(int.Parse(textBox3.Text));
            // en este textBox se almacena el valor del id de usuario seleccionado en el datagrid
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
        }
    }
}
