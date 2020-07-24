using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Documentos
{
    public partial class Menu : Form
    {
        string pais_carta = "";
        string numero_carta = "";

        string pais_carta_manifiesto = "";
        string numero_carta_manifiesto = "";
        string numero_manifiesto = "";

        public Menu()
        {
            InitializeComponent();
            
        }

        public void Recargar()

        {
            Form1 principal = Application.OpenForms.OfType<Form1>().SingleOrDefault();
            //Contenedor_controles principal = (Contenedor_controles)this;

            if (principal.panel2.Controls.Count > 0)
            {
                principal.panel2.Controls.RemoveAt(0);
            }

            Menu actual = new Menu();
            actual.TopLevel = false;
            actual.Dock = DockStyle.Fill;
            principal.panel2.Controls.Add(actual);
            principal.Tag = actual;
            actual.Show();
        }
        public void Actualizar_Cartaportes(string id, string refe)

        {
            Form1 principal = Application.OpenForms.OfType<Form1>().SingleOrDefault();
            //Contenedor_controles principal = (Contenedor_controles)this;

            if (principal.panel2.Controls.Count > 0)
            {
                principal.panel2.Controls.RemoveAt(0);
            }

            Inventario_CP actual = new Inventario_CP();
            actual.id = id;
            actual.id_referencial = refe;
            actual.TopLevel = false;
            actual.Dock = DockStyle.Fill;
            principal.panel2.Controls.Add(actual);
            principal.Tag = actual;
            actual.Show();
        }

        public void Actualizar_Manifiestos(string id, string refe)
        {
            Form1 principal = Application.OpenForms.OfType<Form1>().SingleOrDefault();
            //Contenedor_controles principal = (Contenedor_controles)this;

            if (principal.panel2.Controls.Count > 0)
            {
                principal.panel2.Controls.RemoveAt(0);
            }

            Documento2 actual = new Documento2();
            actual.id = id;
            actual.id_referencia = refe;
            actual.TopLevel = false;
            actual.Dock = DockStyle.Fill;
            principal.panel2.Controls.Add(actual);
            principal.Tag = actual;
            actual.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            Base nueva = new Base();

            string consultar= "SELECT cartas_de_porte.codigo_pais AS PAIS, cartas_de_porte.numero_cartaporte AS NUMERO, cartas_de_porte.fecha_creacion AS CREADO, cartas_de_porte.fecha_modificacion AS [ULTIMA MODIFICACIÓN], Organizaciones_y_direcciones.c2yc3 AS REMITENTE, Organizaciones_y_direcciones_1.c2yc3 AS RECEPTOR"+
            " FROM Organizaciones_y_direcciones AS Organizaciones_y_direcciones_1 INNER JOIN Organizaciones_en_cartaportes AS Organizaciones_en_cartaportes_1 ON Organizaciones_y_direcciones_1.id_organizacion = Organizaciones_en_cartaportes_1.id_organizacion, Organizaciones_y_direcciones INNER JOIN((cartas_de_porte INNER JOIN Organizaciones_en_cartaportes ON cartas_de_porte.llave = Organizaciones_en_cartaportes.id_carta) INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta) ON Organizaciones_y_direcciones.id_organizacion = Organizaciones_en_cartaportes.id_organizacion"+
            " WHERE(((Organizaciones_en_cartaportes.papel_organizacion) = 'EMISOR') AND((Organizaciones_en_cartaportes_1.id_carta) =[cartas_de_porte].[llave]) AND((Organizaciones_en_cartaportes_1.papel_organizacion) = 'RECEPTOR')) ORDER BY cartas_de_porte.fecha_modificacion DESC ";
            DataTable tabla = nueva.Consulta(consultar);
            dataGridView1.DataSource = tabla;
            dataGridView1.AutoResizeColumns();
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[4].Width = 342;
            dataGridView1.Columns[5].Width = 342;
            if (tabla.Rows.Count!=0)
            {
                pais_carta = nueva.Quitar_espacios(dataGridView1.Rows[0].Cells[0].Value.ToString());
                numero_carta = nueva.Quitar_espacios(dataGridView1.Rows[0].Cells[1].Value.ToString());
            }
            


            string consultar2 = "SELECT cartas_de_porte.codigo_pais AS PAIS, cartas_de_porte.numero_cartaporte AS CPI, manifiestos_de_carga.numero_manifiesto_pais AS [NUMERO MANIFIESTO], manifiestos_de_carga.fecha_creacion AS CREADO, manifiestos_de_carga.fecha_modificacion AS [ULTIMA MODIFICACIÓN], Conductores.c13 AS[CONDUCTOR]" +
            " FROM cartas_de_porte INNER JOIN(cartas_final INNER JOIN ((manifiestos_de_carga INNER JOIN (Conductores INNER JOIN Conductores_en_manifiesto ON Conductores.c13 = Conductores_en_manifiesto.id_conductor) ON manifiestos_de_carga.llave = Conductores_en_manifiesto.id_manifiestos) INNER JOIN manifiestos_final ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto) ON cartas_final.llave = manifiestos_final.id_carta_porte) ON cartas_de_porte.llave = cartas_final.id_carta"+
            " WHERE(([Conductores_en_manifiesto].[tipo_conductor] = 'PRINCIPAL')) ORDER BY manifiestos_de_carga.fecha_modificacion DESC";
            DataTable tabla2 = nueva.Consulta(consultar2);
            dataGridView2.DataSource = tabla2;
            dataGridView2.AutoResizeColumns();            
            dataGridView2.Columns[3].Width = 200;
            dataGridView2.Columns[4].Width = 200;
            dataGridView2.Columns[5].Width = 438;//455

            if(tabla2.Rows.Count!=0)
            {
                pais_carta_manifiesto = nueva.Quitar_espacios(dataGridView2.Rows[0].Cells[0].Value.ToString());
                numero_carta_manifiesto = nueva.Quitar_espacios(dataGridView2.Rows[0].Cells[1].Value.ToString());
                numero_manifiesto = nueva.Quitar_espacios(dataGridView2.Rows[0].Cells[2].Value.ToString());
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                Base nueva = new Base();
                pais_carta = nueva.Quitar_espacios(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                numero_carta = nueva.Quitar_espacios(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Base nueva = new Base();
                pais_carta_manifiesto = nueva.Quitar_espacios(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                numero_carta_manifiesto = nueva.Quitar_espacios(dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString());
                numero_manifiesto = nueva.Quitar_espacios(dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString());
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(pais_carta!="" && numero_carta!="")
            {
                Base nueva = new Base();
                string id_carta_De_porte = nueva.Quitar_espacios(nueva.Consulta("SELECT cartas_final.llave" +
                " FROM cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta" +
                " WHERE(([cartas_de_porte].[codigo_pais] = '"+pais_carta+"') AND([cartas_de_porte].[numero_cartaporte] = "+numero_carta+"))").Rows[0].ItemArray[0].ToString());
                Actualizar_Cartaportes(id_carta_De_porte, id_carta_De_porte);
            }
            else
            {
                MessageBox.Show("Seleccione la Carta de Porte que desea editar","Editar Carta de Porte",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (pais_carta_manifiesto != "" && numero_carta_manifiesto != "" && numero_manifiesto!="")
            {
                Base nueva = new Base();
                string id_del_manifiesto = nueva.Quitar_espacios(nueva.Consulta("SELECT manifiestos_final.llave"+
                " FROM manifiestos_de_carga INNER JOIN(cartas_de_porte INNER JOIN(cartas_final INNER JOIN manifiestos_final ON cartas_final.llave = manifiestos_final.id_carta_porte) ON cartas_de_porte.llave = cartas_final.id_carta) ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto"+
                " WHERE(([cartas_de_porte].[codigo_pais] = '"+pais_carta_manifiesto+"') AND([cartas_de_porte].[numero_cartaporte] = "+numero_carta_manifiesto+") AND([manifiestos_de_carga].[numero_manifiesto_pais] = "+numero_manifiesto+"))").Rows[0].ItemArray[0].ToString());
                Actualizar_Manifiestos(id_del_manifiesto, id_del_manifiesto);
            }
            else
            {
                MessageBox.Show("Seleccione el Manifiesto de Carga que desea editar", "Editar Manifiesto de Carga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (pais_carta != "" && numero_carta != "")
            {
                Base nueva = new Base();
                string id_carta_De_porte = nueva.Quitar_espacios(nueva.Consulta("SELECT cartas_final.llave" +
                " FROM cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta" +
                " WHERE(([cartas_de_porte].[codigo_pais] = '" + pais_carta + "') AND([cartas_de_porte].[numero_cartaporte] = " + numero_carta + "))").Rows[0].ItemArray[0].ToString());
                Actualizar_Cartaportes("-1", id_carta_De_porte);
            }
            else
            {
                MessageBox.Show("Seleccione la Carta de Porte que desea tomar como plantilla", "Creat Carta de Porte", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (pais_carta_manifiesto != "" && numero_carta_manifiesto != "" && numero_manifiesto != "")
            {
                Base nueva = new Base();
                string id_del_manifiesto = nueva.Quitar_espacios(nueva.Consulta("SELECT manifiestos_final.llave" +
                " FROM manifiestos_de_carga INNER JOIN(cartas_de_porte INNER JOIN(cartas_final INNER JOIN manifiestos_final ON cartas_final.llave = manifiestos_final.id_carta_porte) ON cartas_de_porte.llave = cartas_final.id_carta) ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto" +
                " WHERE(([cartas_de_porte].[codigo_pais] = '" + pais_carta_manifiesto + "') AND([cartas_de_porte].[numero_cartaporte] = " + numero_carta_manifiesto + ") AND([manifiestos_de_carga].[numero_manifiesto_pais] = " + numero_manifiesto + "))").Rows[0].ItemArray[0].ToString());
                Actualizar_Manifiestos("-1", id_del_manifiesto);
            }
            else
            {
                MessageBox.Show("Seleccione el Manifiesto de Carga que desea tomar como plantilla", "Crear Manifiesto de Carga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (pais_carta != "" && numero_carta != "")
            {
                Base nueva = new Base();
                string id_carta_De_porte = nueva.Quitar_espacios(nueva.Consulta("SELECT cartas_final.llave" +
                " FROM cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta" +
                " WHERE(([cartas_de_porte].[codigo_pais] = '" + pais_carta + "') AND([cartas_de_porte].[numero_cartaporte] = " + numero_carta + "))").Rows[0].ItemArray[0].ToString());

                DialogResult respuesta = new DialogResult();
                respuesta = MessageBox.Show("¿Seguro que desea eliminar la carta de porte " + pais_carta + numero_carta + " ? También se eliminarán todos los manifiestos que tenga.", "Eliminar Carta de Porte", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                DataTable id_manifiestos = nueva.Consulta("SELECT DISTINCT (manifiestos_de_carga.llave) AS Expr1" +
                " FROM(manifiestos_de_carga INNER JOIN((cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta) INNER JOIN manifiestos_final ON cartas_final.llave = manifiestos_final.id_carta_porte) ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto) INNER JOIN Conductores_en_manifiesto ON manifiestos_de_carga.llave = Conductores_en_manifiesto.id_manifiestos" +
                " WHERE(((cartas_final.llave) = " + id_carta_De_porte + "))");

                if (respuesta.ToString() == "Yes")
                {
                    ////////////////////////////
                    string el_organi_carta = "DELETE " +
                    " FROM Organizaciones_en_cartaportes " +
                    " WHERE [Organizaciones_en_cartaportes].[id_carta] =" + id_carta_De_porte;
                    nueva.comando(el_organi_carta);

                    //////////////////////////////////
                    for (int i = 0; i < id_manifiestos.Rows.Count; i++)
                    {
                        string comando = "DELETE" +
                        " FROM Conductores_en_manifiesto" +
                        " where[Conductores_en_manifiesto].[id_manifiestos] =" + nueva.Quitar_espacios(id_manifiestos.Rows[i].ItemArray[0].ToString());
                        nueva.comando(comando);
                    }


                    //////////////////////////////////
                    for (int i = 0; i < id_manifiestos.Rows.Count; i++)
                    {
                        string comando = "DELETE" +
                        " FROM manifiestos_final" +
                        " where[manifiestos_final].[llave] =" + nueva.Quitar_espacios(id_manifiestos.Rows[i].ItemArray[0].ToString());
                        nueva.comando(comando);
                    }

                    //////////////////////////////////
                    for (int i = 0; i < id_manifiestos.Rows.Count; i++)
                    {
                        string comando = "DELETE" +
                        " FROM manifiestos_de_carga" +
                        " where[manifiestos_de_carga].[llave] =" + nueva.Quitar_espacios(id_manifiestos.Rows[i].ItemArray[0].ToString());
                        nueva.comando(comando);
                    }


                    string el_cartas_final = "DELETE FROM cartas_final where [cartas_final].[llave] =  " + id_carta_De_porte;
                    string el_cartas_porte = "DELETE FROM cartas_de_porte where [cartas_de_porte].[llave] =  " + id_carta_De_porte;

                    nueva.comando(el_cartas_final);
                    nueva.comando(el_cartas_porte);

                    Recargar();

                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (pais_carta_manifiesto != "" && numero_carta_manifiesto != "" && numero_manifiesto != "")
            {
                Base nueva = new Base();
                string id_del_manifiesto = nueva.Quitar_espacios(nueva.Consulta("SELECT manifiestos_final.llave" +
                " FROM manifiestos_de_carga INNER JOIN(cartas_de_porte INNER JOIN(cartas_final INNER JOIN manifiestos_final ON cartas_final.llave = manifiestos_final.id_carta_porte) ON cartas_de_porte.llave = cartas_final.id_carta) ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto" +
                " WHERE(([cartas_de_porte].[codigo_pais] = '" + pais_carta_manifiesto + "') AND([cartas_de_porte].[numero_cartaporte] = " + numero_carta_manifiesto + ") AND([manifiestos_de_carga].[numero_manifiesto_pais] = " + numero_manifiesto + "))").Rows[0].ItemArray[0].ToString());

                DialogResult respuesta = new DialogResult();
                respuesta = MessageBox.Show("¿Seguro que desea eliminar el manifiesto " + numero_manifiesto + " de la carta de porte " + pais_carta_manifiesto + numero_carta_manifiesto, "Eliminar Manifiesto", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta.ToString() == "Yes")
                {

                    //////////////////////////////////
                    string comando = "DELETE" +
                    " FROM Conductores_en_manifiesto" +
                    " where[Conductores_en_manifiesto].[id_manifiestos] =" + id_del_manifiesto;
                    nueva.comando(comando);



                    //////////////////////////////////
                    string comando1 = "DELETE" +
                    " FROM manifiestos_final" +
                    " where[manifiestos_final].[llave] =" + id_del_manifiesto;
                    nueva.comando(comando1);


                    //////////////////////////////////
                    string comando2 = "DELETE" +
                    " FROM manifiestos_de_carga" +
                    " where[manifiestos_de_carga].[llave] =" + id_del_manifiesto;
                    nueva.comando(comando2);


                    Recargar();

                }

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
