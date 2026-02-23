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
using System.IO;




namespace Documentos
{
    public partial class Documento2 : Form
    {
        public string id = "-1";
        public string id_referencia = "-1";
        string num_maniECU = "849";
        string num_maniCOL = "117";
        string num_maniPE = "1";
        bool existe_carta = false;
        

        string c2, c3, c4, c5, c6, c7, c8,
               c9, c10, c11, c12, c13,
               c14, c15, c16,
               c17, c18, c19, c20,
               c21, c22, c23, c24, c25,
               c26, c27, c28, c29, c30, c31,
               c32_1, c32_2, c33, c34, c37,
               c38, c40;

        private void richTextBox12_Leave(object sender, EventArgs e)
        {
            Base nueva = new Base();
            DataTable tabla = nueva.Consulta("Select c14,c15,c16,c17 from Conductores where c13='"+richTextBox13.Text+"'");
            if (tabla.Rows.Count != 0)
            {
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[0])))
                    richTextBox12.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[0]));
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[1])))
                    richTextBox11.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[1]));
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[2])))
                    richTextBox10.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[2]));
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[3])))
                    richTextBox9.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[3]));
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox1_Click(sender, e);
            if (!string.IsNullOrEmpty(richTextBox8.Text) && !string.IsNullOrEmpty(richTextBox13.Text) && !string.IsNullOrEmpty(richTextBox15.Text) && !string.IsNullOrEmpty(richTextBox31.Text) && (comboBox1.Text == "EC" || comboBox1.Text == "CO" || comboBox1.Text == "PE") && existe_carta)
                Abrir_Manifiestos(comboBox1.Text, numericUpDown1.Value.ToString());

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox1_Click(sender, e);
        }

        public void Abrir_Manifiestos(string pais, string num)
        {
            Form1 principal = Application.OpenForms.OfType<Form1>().SingleOrDefault();
            //Contenedor_controles principal = (Contenedor_controles)this;

            if (principal.panel2.Controls.Count > 0)
            {
                principal.panel2.Controls.RemoveAt(0);
            }

            Documento2 actual = new Documento2();
            //actual.id = id;
            //actual.id_referencia = refe;
            actual.numericUpDown1.Value = Convert.ToInt32(num);
            actual.comboBox1.Text = pais;
            actual.richTextBox22.Text = this.richTextBox22.Text;
            actual.richTextBox27.Text = this.richTextBox27.Text;
            actual.richTextBox26.Text = this.richTextBox26.Text;
            actual.richTextBox17.Text = this.richTextBox17.Text;
            actual.richTextBox18.Text = this.richTextBox18.Text;
            actual.richTextBox19.Text = this.richTextBox19.Text;
            actual.richTextBox20.Text = this.richTextBox20.Text;
            actual.richTextBox21.Text = this.richTextBox21.Text;
            actual.richTextBox36.Text = this.richTextBox36.Text;



            actual.TopLevel = false;
            actual.Dock = DockStyle.Fill;
            principal.panel2.Controls.Add(actual);
            principal.Tag = actual;
            actual.Show();
        }

        private void richTextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Base nueva = new Base();

            DataTable consulta_numECU = nueva.Consulta("SELECT manifiestos_de_carga.numero_manifiesto_pais"
            + " FROM cartas_de_porte INNER JOIN(cartas_final INNER JOIN(manifiestos_de_carga INNER JOIN manifiestos_final ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto) ON cartas_final.llave = manifiestos_final.id_carta_porte) ON cartas_de_porte.llave = cartas_final.id_carta"
            + " WHERE(([cartas_de_porte].[codigo_pais] = 'EC'))"
            + " ORDER BY manifiestos_de_carga.numero_manifiesto_pais ASC");
            DataTable consulta_numCOL = nueva.Consulta("SELECT manifiestos_de_carga.numero_manifiesto_pais"
            + " FROM cartas_de_porte INNER JOIN(cartas_final INNER JOIN(manifiestos_de_carga INNER JOIN manifiestos_final ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto) ON cartas_final.llave = manifiestos_final.id_carta_porte) ON cartas_de_porte.llave = cartas_final.id_carta"
            + " WHERE(([cartas_de_porte].[codigo_pais] = 'CO'))"
            + " ORDER BY manifiestos_de_carga.numero_manifiesto_pais ASC");
            DataTable consulta_numPE = nueva.Consulta("SELECT manifiestos_de_carga.numero_manifiesto_pais"
            + " FROM cartas_de_porte INNER JOIN(cartas_final INNER JOIN(manifiestos_de_carga INNER JOIN manifiestos_final ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto) ON cartas_final.llave = manifiestos_final.id_carta_porte) ON cartas_de_porte.llave = cartas_final.id_carta"
            + " WHERE(([cartas_de_porte].[codigo_pais] = 'PE'))"
            + " ORDER BY manifiestos_de_carga.numero_manifiesto_pais ASC");

            string empresaEmisor = "";string empresaReceptor = "";string campoDian = "";string campoDae = ""; string campoBodega = "";

            empresaEmisor = nueva.Quitar_espacios(nueva.Consulta("SELECT Organizaciones_y_direcciones.c2yc3"+
            " FROM Organizaciones_y_direcciones INNER JOIN(cartas_de_porte INNER JOIN Organizaciones_en_cartaportes ON cartas_de_porte.llave = Organizaciones_en_cartaportes.id_carta) ON Organizaciones_y_direcciones.id_organizacion = Organizaciones_en_cartaportes.id_organizacion"+
            " WHERE(([cartas_de_porte].[codigo_pais] = '"+comboBox1.Text+"') AND([cartas_de_porte].[numero_cartaporte] = "+numericUpDown1.Value.ToString()+") AND([Organizaciones_en_cartaportes].[papel_organizacion] = 'EMISOR'))").Rows[0].ItemArray[0].ToString()).Split('\n')[0];

            empresaReceptor = nueva.Quitar_espacios(nueva.Consulta("SELECT Organizaciones_y_direcciones.c2yc3" +
            " FROM Organizaciones_y_direcciones INNER JOIN(cartas_de_porte INNER JOIN Organizaciones_en_cartaportes ON cartas_de_porte.llave = Organizaciones_en_cartaportes.id_carta) ON Organizaciones_y_direcciones.id_organizacion = Organizaciones_en_cartaportes.id_organizacion" +
            " WHERE(([cartas_de_porte].[codigo_pais] = '" + comboBox1.Text + "') AND([cartas_de_porte].[numero_cartaporte] = " + numericUpDown1.Value.ToString() + ") AND([Organizaciones_en_cartaportes].[papel_organizacion] = 'RECEPTOR'))").Rows[0].ItemArray[0].ToString()).Split('\n')[0];

            try
            {

                campoDian = nueva.Quitar_espacios(nueva.Consulta("SELECT cartas_final.dian" +
                " FROM cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta" +
                " WHERE(([cartas_de_porte].[codigo_pais] = '" + comboBox1.Text + "') AND([cartas_de_porte].[numero_cartaporte] = " + numericUpDown1.Value.ToString() + ")) ").Rows[0].ItemArray[0].ToString().Split('\n')[0]);
            }
            catch (Exception hjkjhkj)
            {

            }

            try
            {
                campoDae = nueva.Quitar_espacios(nueva.Consulta("SELECT cartas_final.c18" +
            " FROM cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta" +
            " WHERE(([cartas_de_porte].[codigo_pais] = '" + comboBox1.Text + "') AND([cartas_de_porte].[numero_cartaporte] = " + numericUpDown1.Value.ToString() + "))").Rows[0].ItemArray[0].ToString()).Split('\n')[1];
            }
            catch(Exception esss)
            {

            }
            
            campoBodega = nueva.Quitar_espacios(nueva.Consulta("SELECT cartas_final.c21" +
            " FROM cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta" +
            " WHERE(([cartas_de_porte].[codigo_pais] = '" + comboBox1.Text + "') AND([cartas_de_porte].[numero_cartaporte] = " + numericUpDown1.Value.ToString() + "))").Rows[0].ItemArray[0].ToString()).Split('\n')[0];

            string numero = "";
            if (comboBox1.Text == "EC")
                numero = num_maniECU;
            else if (comboBox1.Text == "CO")
                numero = num_maniCOL;
            else
                numero = num_maniPE;

            if (id == "-1")
            {

            }
            else
            {

                string pais_actual = nueva.Quitar_espacios(Convert.ToString(nueva.Consulta("SELECT cartas_de_porte.codigo_pais"
                + " FROM manifiestos_de_carga INNER JOIN((cartas_de_porte INNER JOIN cartas_final " +
                "ON cartas_de_porte.llave = cartas_final.id_carta) INNER JOIN manifiestos_final " +
                "ON cartas_final.llave = manifiestos_final.id_carta_porte) ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto"
                + " WHERE(([manifiestos_final].[llave] = " + id + "))").Rows[0].ItemArray[0]));
                if (pais_actual == comboBox1.Text)
                {
                    numero = nueva.Quitar_espacios(Convert.ToString(nueva.Consulta("SELECT manifiestos_de_carga.numero_manifiesto_pais"
                    + " FROM manifiestos_de_carga INNER JOIN manifiestos_final ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto"
                    + " WHERE(([manifiestos_final].[llave] = " + id + "))").Rows[0].ItemArray[0]));

                }

            }





            DatosManifiesto datos = new DatosManifiesto();            
            datos.CodigoManifiesto = "000"+numero+" "+comboBox1.Text;
            datos.NumeroCodigo = numericUpDown1.Value;
            datos.CertificadoIdoneidad = richTextBox1.Text;
            datos.PermisoPrestacion = richTextBox2.Text;
            datos.MarcaVehiculo = richTextBox3.Text;
            datos.PlacaVehiculo = richTextBox8.Text;
            datos.AnioFabricacionVehiculo = richTextBox4.Text;
            datos.ChasisVehiculo = richTextBox7.Text;
            datos.CertificadosHabilitacion = richTextBox6.Text.Replace("\n"," ");
            datos.PlacaUnidadCarga = richTextBox15.Text;
            datos.MarcaUnidadCarga = richTextBox5.Text;
            datos.AnioFabricacionUnidadCarga = richTextBox16.Text;
            datos.ChasisUnidadCarga = richTextBox14.Text;
            datos.NombreConductorPrincipal = richTextBox13.Text;
            datos.DocIdentidadConductorPrincipal = richTextBox12.Text;
            datos.NacionalidadConductorPrincipal = richTextBox11.Text;
            datos.LicenciaConductorPrincipal = richTextBox10.Text;
            datos.LicenciaTripulanteTerrestreConductorPrincipal = richTextBox9.Text;
            datos.NombreConductorAuxiliar = richTextBox31.Text;
            datos.DocIdentidadConductorAuxiliar = richTextBox32.Text;
            datos.NacionalidadConductorAuxiliar = richTextBox30.Text;
            datos.LicenciaConductorAuxiliar = richTextBox29.Text;
            datos.LibretaTripulanteTerrestreConductorAuxiliar = richTextBox28.Text;
            datos.LugarCarga = richTextBox27.Text;
            datos.LugarDescarga = richTextBox26.Text;
            datos.APeligrosa = radioButton1.Checked;
            datos.BSustanciaQuimica= radioButton2.Checked;
            datos.CPerecible= radioButton4.Checked;
            datos.DOtra= radioButton3.Checked;
            datos.DOtraTexto = textBox1.Text;
            datos.NumeroIdentificacionContenedores = richTextBox24.Text.Replace("\n", " ");
            datos.NroCartaPorte = richTextBox22.Text;
            datos.DescripcionMercancias = saltotext(richTextBox21.Text);//+"REMITENTE:"+empresaEmisor+"\nDESTINATARIO:"+empresaReceptor+"\nDIAN:"+campoDian+"\n"+campoDae+"\nBODEGA:"+campoBodega;
            Console.WriteLine(datos.DescripcionMercancias.Split('\r')[0]);
            datos.NumeroPrecintosAduaneros = richTextBox23.Text.Replace("\n", " ");
            datos.CantidadBultos = richTextBox20.Text;
            datos.ClaseMarcaBultos = richTextBox19.Text;
            datos.PesoBruto = richTextBox18.Text;
            datos.PesoNeto = richTextBox17.Text;
            datos.TreintayTres = richTextBox40.Text;
            datos.PrecioMercancias = richTextBox39.Text;
            datos.AduanaCruceFrontera = richTextBox38.Text;
            datos.AduanaDestino = richTextBox37.Text;
            datos.FechaEmision = richTextBox36.Text;
            Form3 frm3 = new Form3();
            frm3.datos.Add(datos);
            frm3.Show();
        }

        public string saltotext(string text)
        {
            string t = text;
            int numlines = (text.Length/52)+1;
            int ns = contar(text);
            int espacios = 15 - numlines - ns-8;
            
            for(int i=1;i<=espacios;i++)
            {
                t += "\n";
            }
            return t;
        }

        public int contar(string t)
        {
            int con = 0;
            for(int i=0;i<t.Length-1;i++)
            {
                if (t[i] == '\n')
                {
                    con += 1;
                }
                    
            }
            return con;
        }
        private void richTextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(id == "-1")
            {
                string pais = comboBox1.Text;
                Base nueva = new Base();
                try
                {
                    string numerooo = (Convert.ToInt32(nueva.Consulta("SELECT manifiestos_de_carga.numero_manifiesto_pais " +
                    " FROM manifiestos_de_carga INNER JOIN((cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta) INNER JOIN manifiestos_final ON cartas_final.llave = manifiestos_final.id_carta_porte) ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto " +
                    " WHERE(([cartas_de_porte].[codigo_pais] = '" + pais + "')) order by manifiestos_de_carga.numero_manifiesto_pais desc").Rows[0].ItemArray[0].ToString()) + 1).ToString();
                    label34.Text = "NUEVO MANIFIESTO " + numerooo;
                }
                catch (Exception tryyu)
                {
                    label34.Text = "NUEVO MANIFIESTO ";
                }
            }
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox32_Leave(object sender, EventArgs e)
        {
            Base nueva = new Base();
            DataTable tabla = nueva.Consulta("Select c14,c15,c16,c17 from Conductores where c13='" + richTextBox31.Text + "'");
            if (tabla.Rows.Count != 0)
            {
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[0])))
                    richTextBox32.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[0]));
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[1])))
                    richTextBox30.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[1]));
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[2])))
                    richTextBox29.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[2]));
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[3])))
                    richTextBox28.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[3]));
            }
        }

        private void richTextBox15_Leave(object sender, EventArgs e)
        {
            Base nueva = new Base();
            DataTable tabla = nueva.Consulta("Select c9,c10,c12 from Unidades_de_Carga where c11='"+richTextBox15.Text+"'");
            if (tabla.Rows.Count != 0)
            {
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[0])))
                    richTextBox5.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[0]));
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[1])))
                    richTextBox16.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[1]));
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[2])))
                    richTextBox14.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[2]));
            }
        }

        private void richTextBox8_Leave(object sender, EventArgs e)
        {
            Base nueva = new Base();
            DataTable tabla = nueva.Consulta("Select c4,c5,c7,c8 from vehiculos where c6='"+richTextBox8.Text+"'");
            if(tabla.Rows.Count!=0)
            {
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[0])))
                    richTextBox3.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[0]));
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[1])))
                    richTextBox4.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[1]));
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[2])))
                    richTextBox7.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[2]));
                if (!String.IsNullOrEmpty(Convert.ToString(tabla.Rows[0].ItemArray[3])))
                    richTextBox6.Text = nueva.Quitar_espacios(Convert.ToString(tabla.Rows[0].ItemArray[3]));
            }
            
        }

        

        private void richTextBox32_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void richTextBox12_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            
        }

        private void richTextBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void richTextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {            
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void richTextBox13_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void richTextBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.NumerosEnteros(e);
        }

        private void richTextBox17_KeyPress(object sender, KeyPressEventArgs e)
        {

            
        }

        private void richTextBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
 
        }

        private void richTextBox31_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.NumerosEnteros(e);
        }

        private void richTextBox29_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.NumerosEnteros(e);
        }

        private void richTextBox28_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.NumerosEnteros(e);
        }

        private void richTextBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.NumerosEnteros(e);
        }

        private void richTextBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.NumerosEnteros(e);
        }

        private void richTextBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.NumerosEnteros(e);
        }

        private void richTextBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.NumerosEnteros(e);
        }

        private void richTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.NumerosEnteros(e);
        }

        public Documento2()
        {
            InitializeComponent();
            Base consultas = new Base();
            DataTable vehiculos = consultas.Consulta("select c6 from Vehiculos");
            DataTable unidades = consultas.Consulta("select c11 from Unidades_de_Carga");
            DataTable conductores = consultas.Consulta("select c13 from conductores");            
            

            string [] arreglo_vehiculos = new string [vehiculos.Rows.Count];
            for(int i=0;i<vehiculos.Rows.Count;i++)
            {
                arreglo_vehiculos[i]=consultas.Quitar_espacios(Convert.ToString(vehiculos.Rows[i].ItemArray[0]));
            }
            AutoCompleteStringCollection source1 = new AutoCompleteStringCollection();            
            source1.AddRange(arreglo_vehiculos);
            richTextBox8.AutoCompleteCustomSource = source1;

            string[] arreglo_unidades = new string[unidades.Rows.Count];
            for (int i = 0; i < unidades.Rows.Count; i++)
            {
                
                arreglo_unidades[i] = consultas.Quitar_espacios(Convert.ToString(unidades.Rows[i].ItemArray[0]));
            }
            AutoCompleteStringCollection source2 = new AutoCompleteStringCollection();
            source2.AddRange(arreglo_unidades);
            richTextBox15.AutoCompleteCustomSource = source2;

            string[] arreglo_conductores = new string[conductores.Rows.Count];
            for (int i = 0; i < conductores.Rows.Count; i++)
            {
                arreglo_conductores[i] = consultas.Quitar_espacios(Convert.ToString(conductores.Rows[i].ItemArray[0]));
            }
            AutoCompleteStringCollection source3 = new AutoCompleteStringCollection();
            source3.AddRange(arreglo_conductores);

            richTextBox13.AutoCompleteCustomSource = source3;
            richTextBox31.AutoCompleteCustomSource = source3;            

            
        }

        public void QuitarVacios()
        {
            foreach (Control ctrl in panel4.Controls)
            {
                if (ctrl is GroupBox)
                {
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        if (ctrl2 is TextBox && string.IsNullOrEmpty(ctrl2.Text))
                        {
                            TextBox text = ctrl2 as TextBox;
                            text.Text = " ";
                            if (ctrl2 == richTextBox4 || ctrl2 == richTextBox16 || ctrl2 == richTextBox23)
                            {
                                ctrl2.Text = "0";
                            }
                        }
                        else if (ctrl2 is RichTextBox && string.IsNullOrEmpty(ctrl2.Text))
                        {
                            RichTextBox text = ctrl2 as RichTextBox;
                            text.Text = " ";
                            if (ctrl2 == richTextBox4 || ctrl2 == richTextBox16 || ctrl2 == richTextBox23 )
                            {
                                ctrl2.Text = "0";
                            }
                        }
                        else if (ctrl2 is Panel)
                        {
                            foreach (Control ctrl3 in ctrl2.Controls)
                            {
                                if (ctrl3 == textBox1 && string.IsNullOrEmpty(ctrl3.Text))
                                    ctrl3.Text = " ";
                            }
                        }
                    }
                }

            }

            
            
        }

        public void VolverVacios()
        {
            foreach (Control ctrl in panel4.Controls)
            {
                if (ctrl is GroupBox)
                {
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        if (ctrl2 is TextBox && (ctrl2.Text==" " || ctrl2.Text=="0"))
                        {
                            TextBox text = ctrl2 as TextBox;
                            text.Text = "";                            
                        }
                        else if (ctrl2 is RichTextBox && (ctrl2.Text == " " || ctrl2.Text == "0"))
                        {
                            RichTextBox text = ctrl2 as RichTextBox;
                            text.Text = "";                            
                        }
                        else if (ctrl2 is Panel)
                        {
                            foreach (Control ctrl3 in ctrl2.Controls)
                            {
                                if (ctrl3 == textBox1 && ctrl3.Text==" ")
                                    ctrl3.Text = "";
                            }
                        }
                    }
                }

            }
        }

        public void refrescar_valores()
        {
            QuitarVacios();

            c2 = richTextBox1.Text.ToUpper();
            c3 = richTextBox2.Text.ToUpper();
            c4 = richTextBox3.Text.ToUpper();
            c5 = richTextBox4.Text.Replace("\n", "").Replace(" ", "");
            if (c5 == ".")
                c5 = "";
            c6 = richTextBox8.Text.ToUpper();
            c7 = richTextBox7.Text.ToUpper();
            c8 = richTextBox6.Text.ToUpper();
            c9 = richTextBox5.Text.ToUpper();
            c10 = richTextBox16.Text.Replace("\n", "").Replace(" ", "");
            if (c10 == ".")
                c10 = "";
            c11 = richTextBox15.Text.ToUpper();
            c12 = richTextBox14.Text.ToUpper();
            c13 = richTextBox13.Text.ToUpper();
            c14 = richTextBox12.Text;
            c15 = richTextBox11.Text.ToUpper();
            c16 = richTextBox10.Text;
            c17 = richTextBox9.Text;
            c18 = richTextBox31.Text.ToUpper();
            c19 = richTextBox32.Text.Replace("X","0");
            c20 = richTextBox30.Text.ToUpper();
            c21 = richTextBox29.Text.Replace("X", "0");
            c22 = richTextBox28.Text.Replace("X", "0");
            c23 = richTextBox27.Text.ToUpper();
            c24 = richTextBox26.Text.ToUpper();
            if (radioButton1.Checked)
                c25 = radioButton1.Text.Substring(2, radioButton1.Text.Length-2);
            else if (radioButton2.Checked)
                c25 = radioButton2.Text.Substring(2, radioButton2.Text.Length-2);
            else if (radioButton3.Checked)
                c25 = radioButton3.Text.Substring(2, radioButton3.Text.Length-2);
            else
                c25 = textBox1.Text;
            c26 = richTextBox24.Text;
            c27 = richTextBox23.Text.Replace("\n", "").Replace(" ","");
            if (c27 == ".")
                c27 = "";
            c28 = richTextBox22.Text.ToUpper();
            c29 = richTextBox21.Text.ToUpper();
            c30 = richTextBox20.Text.ToUpper();
            c31 = richTextBox19.Text.ToUpper();
            c32_1 = richTextBox18.Text;
            c32_2 = richTextBox17.Text;
            c33 = richTextBox40.Text;
            c34 = richTextBox39.Text;
            c37 = richTextBox38.Text.ToUpper();
            c38 = richTextBox37.Text.ToUpper();
            c40 = richTextBox36.Text.ToUpper();

            VolverVacios();

        }
        
        private void Limpiar()
        {
            foreach (Control ctrl in panel4.Controls)
            {
                if (ctrl is GroupBox)
                {
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        if (ctrl2 is TextBox)
                        {
                            TextBox text = ctrl2 as TextBox;
                            text.Clear();
                        }
                        else if (ctrl2 is RichTextBox)
                        {
                            RichTextBox text = ctrl2 as RichTextBox;
                            text.Clear();
                        }
                        else if (ctrl2 is Panel)
                        {
                            foreach (Control ctrl3 in ctrl2.Controls)
                            {
                                if (ctrl3 == textBox1)
                                    ctrl3.Text = "";
                            }
                        }
                    }
                }

            }
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            
        }        
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox31_TextChanged(object sender, EventArgs e)
        {

        }


        private void cargar_datos(string id_ref)
        {           
            if (id_ref != "-1")
            {
                Base nueva = new Base();

                DataTable consulta_3_datos = nueva.Consulta("SELECT cartas_de_porte.codigo_pais, cartas_de_porte.numero_cartaporte, manifiestos_de_carga.numero_manifiesto_pais" +
                " FROM manifiestos_de_carga INNER JOIN((cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta) INNER JOIN manifiestos_final ON cartas_final.llave = manifiestos_final.id_carta_porte) ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto" +
                " WHERE(([manifiestos_final].[llave] = " + id_ref + "))");

                string pais = nueva.Quitar_espacios(consulta_3_datos.Rows[0].ItemArray[0].ToString());
                string num_c = nueva.Quitar_espacios(consulta_3_datos.Rows[0].ItemArray[1].ToString());
                string num_m = nueva.Quitar_espacios(consulta_3_datos.Rows[0].ItemArray[2].ToString());

                if (id != "-1")
                {
                    label34.Text = "EDITANDO MANIFIESTO " + num_m + " DE CARTA PORTE " + pais + " " + num_c;
                }
                else
                {
                    try
                    {
                        string numerooo = (Convert.ToInt32(nueva.Consulta("SELECT manifiestos_de_carga.numero_manifiesto_pais "+
                        " FROM manifiestos_de_carga INNER JOIN((cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta) INNER JOIN manifiestos_final ON cartas_final.llave = manifiestos_final.id_carta_porte) ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto "+
                        " WHERE(([cartas_de_porte].[codigo_pais] = '"+pais+ "')) order by manifiestos_de_carga.numero_manifiesto_pais desc").Rows[0].ItemArray[0].ToString()) + 1).ToString();
                        label34.Text = "NUEVA MANIFIESTO " + numerooo;
                    }
                    catch (Exception tryyu)
                    {
                        label34.Text = "NUEVO MANIFIESTO ";
                    }
                }

                comboBox1.Text = pais;
                numericUpDown1.Value = Convert.ToInt32(num_c);

                /////////////////////////////////////
                DataTable consulta_C2_c3 = nueva.Consulta("SELECT manifiestos_de_carga.c2, manifiestos_de_carga.c3" +
                " FROM manifiestos_de_carga INNER JOIN manifiestos_final ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto" +
                " WHERE(([manifiestos_final].[llave] = " + id_ref + "))");

                richTextBox1.Text = nueva.Quitar_espacios(consulta_C2_c3.Rows[0].ItemArray[0].ToString());
                richTextBox2.Text = nueva.Quitar_espacios(consulta_C2_c3.Rows[0].ItemArray[1].ToString());

                /////////////////////////////////////////////
                ///VEHICULO
                ///////////////////////////////////////////////

                DataTable c_vehiculos = nueva.Consulta("SELECT Vehiculos.*" +
                    " FROM Vehiculos INNER JOIN(manifiestos_de_carga INNER JOIN manifiestos_final ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto) ON Vehiculos.c6 = manifiestos_de_carga.id_vehiculo" +
                    " WHERE(([manifiestos_final].[llave] = " + id_ref + "))");
                richTextBox7.Text = nueva.Quitar_espacios(c_vehiculos.Rows[0].ItemArray[0].ToString());
                richTextBox3.Text = nueva.Quitar_espacios(c_vehiculos.Rows[0].ItemArray[1].ToString());
                richTextBox4.Text = nueva.Quitar_espacios(c_vehiculos.Rows[0].ItemArray[2].ToString());
                richTextBox8.Text = nueva.Quitar_espacios(c_vehiculos.Rows[0].ItemArray[3].ToString());
                richTextBox6.Text = nueva.Quitar_espacios(c_vehiculos.Rows[0].ItemArray[4].ToString());

                /////////////////////////////////////////////
                ///UNIDAD CARGA
                ///////////////////////////////////////////////

                DataTable c_unidadescarga = nueva.Consulta("SELECT Unidades_de_Carga.*" +
                " FROM Unidades_de_Carga INNER JOIN(manifiestos_de_carga INNER JOIN manifiestos_final ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto) ON Unidades_de_Carga.c11 = manifiestos_de_carga.id_unidad" +
                " WHERE(([manifiestos_final].[llave] = " + id_ref + "))");
                richTextBox14.Text = nueva.Quitar_espacios(c_unidadescarga.Rows[0].ItemArray[0].ToString());
                richTextBox5.Text = nueva.Quitar_espacios(c_unidadescarga.Rows[0].ItemArray[1].ToString());
                richTextBox16.Text = nueva.Quitar_espacios(c_unidadescarga.Rows[0].ItemArray[2].ToString());
                richTextBox15.Text = nueva.Quitar_espacios(c_unidadescarga.Rows[0].ItemArray[3].ToString());

                ///////////////////////////////////////////////
                ///conductores
                ///////////////////////////////////////////////

                DataTable c_conductor_p = nueva.Consulta("SELECT Conductores.*" +
                " FROM Conductores INNER JOIN((manifiestos_de_carga INNER JOIN manifiestos_final ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto) INNER JOIN Conductores_en_manifiesto ON manifiestos_de_carga.llave = Conductores_en_manifiesto.id_manifiestos) ON Conductores.c13 = Conductores_en_manifiesto.id_conductor" +
                " WHERE(([manifiestos_final].[llave] = " + id_ref + ") AND([Conductores_en_manifiesto].[tipo_conductor] = 'PRINCIPAL'))");
                richTextBox12.Text = nueva.Quitar_espacios(c_conductor_p.Rows[0].ItemArray[0].ToString());
                richTextBox13.Text = nueva.Quitar_espacios(c_conductor_p.Rows[0].ItemArray[1].ToString());
                richTextBox11.Text = nueva.Quitar_espacios(c_conductor_p.Rows[0].ItemArray[2].ToString());
                richTextBox10.Text = nueva.Quitar_espacios(c_conductor_p.Rows[0].ItemArray[3].ToString());
                richTextBox9.Text = nueva.Quitar_espacios(c_conductor_p.Rows[0].ItemArray[4].ToString());

                DataTable c_conductor_a = nueva.Consulta("SELECT Conductores.*" +
                " FROM Conductores INNER JOIN((manifiestos_de_carga INNER JOIN manifiestos_final ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto) INNER JOIN Conductores_en_manifiesto ON manifiestos_de_carga.llave = Conductores_en_manifiesto.id_manifiestos) ON Conductores.c13 = Conductores_en_manifiesto.id_conductor" +
                " WHERE(([manifiestos_final].[llave] = " + id_ref + ") AND([Conductores_en_manifiesto].[tipo_conductor] = 'AYUDANTE'))");
                richTextBox32.Text = "XXXXXXXXXX";
                richTextBox29.Text = "XXXXXXXXXX";
                richTextBox28.Text = "XXXXXXXXXX";
                if (Convert.ToInt32(nueva.Quitar_espacios(c_conductor_a.Rows[0].ItemArray[0].ToString())) != 0)
                    richTextBox32.Text = nueva.Quitar_espacios(c_conductor_a.Rows[0].ItemArray[0].ToString());

                if (Convert.ToInt32(nueva.Quitar_espacios(c_conductor_a.Rows[0].ItemArray[3].ToString()))!=0)
                    richTextBox29.Text = nueva.Quitar_espacios(c_conductor_a.Rows[0].ItemArray[3].ToString());

                if(Convert.ToInt32(nueva.Quitar_espacios(c_conductor_a.Rows[0].ItemArray[4].ToString()))!=0)
                    richTextBox28.Text = nueva.Quitar_espacios(c_conductor_a.Rows[0].ItemArray[4].ToString());

                richTextBox31.Text = nueva.Quitar_espacios(c_conductor_a.Rows[0].ItemArray[1].ToString());
                richTextBox30.Text = nueva.Quitar_espacios(c_conductor_a.Rows[0].ItemArray[2].ToString());                                

                ////////////////////////////////
                ///RESTO MANIFIESTO
                /////////////////////////////////
                DataTable c_restomanifiesto = nueva.Consulta("SELECT *"+
                " FROM manifiestos_final"+
                " WHERE(([manifiestos_final].[llave] = "+ id_ref + "))");
                richTextBox27.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[3].ToString());
                richTextBox26.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[4].ToString());

                string l25= nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[5].ToString());


                if (radioButton1.Text.Substring(2, radioButton1.Text.Length - 2) == l25)
                    radioButton1.Checked = true;
                else if (radioButton2.Text.Substring(2, radioButton2.Text.Length - 2) == l25)
                    radioButton2.Checked = true;
                else if (radioButton4.Text.Substring(2, radioButton3.Text.Length - 2) == l25)
                    radioButton4.Checked = true;
                else
                {
                    radioButton3.Checked = true;
                    textBox1.Text = l25;
                }
                    

                richTextBox24.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[6].ToString());
                richTextBox23.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[7].ToString());
                richTextBox22.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[8].ToString());
                richTextBox21.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[9].ToString());
                richTextBox20.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[10].ToString());
                richTextBox19.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[11].ToString());
                richTextBox18.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[12].ToString());
                richTextBox17.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[13].ToString());
                richTextBox40.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[14].ToString());
                richTextBox39.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[15].ToString());
                richTextBox38.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[16].ToString());
                richTextBox37.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[17].ToString());
                richTextBox36.Text = nueva.Quitar_espacios(c_restomanifiesto.Rows[0].ItemArray[18].ToString());
            }
        }
        private void Documento2_Load(object sender, EventArgs e)
        {
            cargar_datos(id_referencia);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //numero_manifiesto_pais

            Base nueva = new Base();
            refrescar_valores();            
        
            if(nueva.Consulta("Select * from cartas_de_porte where numero_cartaporte =" + numericUpDown1.Value+ " and codigo_pais='"+comboBox1.Text+"'").Rows.Count!=0)
            {
                existe_carta = true;
            }
            else
            {
                existe_carta = false;
            }
            
            DataTable consulta_numECU = nueva.Consulta("SELECT manifiestos_de_carga.numero_manifiesto_pais"
            + " FROM cartas_de_porte INNER JOIN(cartas_final INNER JOIN(manifiestos_de_carga INNER JOIN manifiestos_final ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto) ON cartas_final.llave = manifiestos_final.id_carta_porte) ON cartas_de_porte.llave = cartas_final.id_carta"
            + " WHERE(([cartas_de_porte].[codigo_pais] = 'EC'))"
            + " ORDER BY manifiestos_de_carga.numero_manifiesto_pais ASC") ;
            DataTable consulta_numCOL = nueva.Consulta("SELECT manifiestos_de_carga.numero_manifiesto_pais"
            + " FROM cartas_de_porte INNER JOIN(cartas_final INNER JOIN(manifiestos_de_carga INNER JOIN manifiestos_final ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto) ON cartas_final.llave = manifiestos_final.id_carta_porte) ON cartas_de_porte.llave = cartas_final.id_carta"
            + " WHERE(([cartas_de_porte].[codigo_pais] = 'CO'))"
            + " ORDER BY manifiestos_de_carga.numero_manifiesto_pais ASC");
            DataTable consulta_numPE = nueva.Consulta("SELECT manifiestos_de_carga.numero_manifiesto_pais"
            + " FROM cartas_de_porte INNER JOIN(cartas_final INNER JOIN(manifiestos_de_carga INNER JOIN manifiestos_final ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto) ON cartas_final.llave = manifiestos_final.id_carta_porte) ON cartas_de_porte.llave = cartas_final.id_carta"
            + " WHERE(([cartas_de_porte].[codigo_pais] = 'PE'))"
            + " ORDER BY manifiestos_de_carga.numero_manifiesto_pais ASC");

            if(consulta_numECU.Rows.Count!=0)
            {
                num_maniECU = nueva.Quitar_espacios(Convert.ToString(Convert.ToInt32(consulta_numECU.Rows[consulta_numECU.Rows.Count-1].ItemArray[0])+1));
            }

            if (consulta_numCOL.Rows.Count != 0)
            {
                num_maniCOL = nueva.Quitar_espacios(Convert.ToString(Convert.ToInt32(consulta_numCOL.Rows[consulta_numCOL.Rows.Count - 1].ItemArray[0])+1));
            }

            if (consulta_numPE.Rows.Count != 0)
            {
                num_maniPE = nueva.Quitar_espacios(Convert.ToString(Convert.ToInt32(consulta_numPE.Rows[consulta_numPE.Rows.Count - 1].ItemArray[0])+1));
            }


            if (!string.IsNullOrEmpty(richTextBox8.Text) && !string.IsNullOrEmpty(richTextBox13.Text) && !string.IsNullOrEmpty(richTextBox15.Text) && !string.IsNullOrEmpty(richTextBox31.Text) && (comboBox1.Text == "EC" || comboBox1.Text == "CO" || comboBox1.Text == "PE") && existe_carta)
            {

                if (!nueva.Dato_en_consulta(nueva.Quitar_espacios(c13), "Select c13 from Conductores"))
                {
                    string comando1 = "INSERT INTO Conductores " +
                   "VALUES('" + c14 + "','" + c13 + "','" + c15 + "','" + c16 + "','" + c17 + "')";
                    nueva.comando(comando1);
                }
                else
                {
                    string comando = "UPDATE Conductores set c14='" + c14 + "',c15='" + c15 + "',c16='" + c16 + "',c17='" + c17 + "' Where c13='" + c13 + "'";
                    nueva.comando(comando);
                }

                if (!nueva.Dato_en_consulta(nueva.Quitar_espacios(c18), "Select c13 from Conductores"))
                {
                    string comando2 = "INSERT INTO Conductores " +
                    "VALUES('" + c19 + "','" + c18 + "','" + c20 + "','" + c21 + "','" + c22 + "')";
                    nueva.comando(comando2);
                }
                else
                {
                    string comando = "UPDATE Conductores set c14='" + c19 + "',c15='" + c20 + "',c16='" + c21 + "',c17='" + c22 + "' Where c13='" + c18 + "'";
                    nueva.comando(comando);
                }

                if (!nueva.Dato_en_consulta(nueva.Quitar_espacios(c11), "Select c11 from Unidades_de_Carga"))
                {
                    string comando3 = "INSERT INTO Unidades_de_Carga " +
                    "VALUES('" + c12 + "','" + c9 + "','" + c10 + "','" + c11 + "')";
                    nueva.comando(comando3);
                }
                else
                {
                    string comando = "UPDATE Unidades_de_Carga set c9='" + c9 + "',c10='" + c10 + "',c12='" + c12 + "' Where c11='" + c11 + "'";
                    nueva.comando(comando);
                }

                if (!nueva.Dato_en_consulta(nueva.Quitar_espacios(c6), "Select c6 from Vehiculos"))
                {
                    string comando4 = "INSERT INTO Vehiculos " +
                    "VALUES('" + c7 + "','" + c4 + "','" + c5 + "','" + c6 + "','" + c8 + "')";
                    nueva.comando(comando4);
                }
                else
                {
                    string comando = "UPDATE Vehiculos set c4='" + c4 + "',c5='" + c5 + "',c7='" + c7 + "',c8='" + c8 + "' Where c6='" + c6 + "'";
                    nueva.comando(comando);
                }



                if (id == "-1")
                {
                    string numero = "";
                    if (comboBox1.Text == "EC")
                        numero = num_maniECU;
                    else if (comboBox1.Text == "CO")
                        numero = num_maniCOL;
                    else
                        numero = num_maniPE;



                    string comando5 = "INSERT INTO manifiestos_de_carga(fecha_creacion, fecha_modificacion,numero_manifiesto_pais,c2,c3,id_vehiculo,id_unidad) " +
                    "VALUES(NOW(),NOW(),'"+numero+"','" + c2 + "','" + c3 + "','" + c6 + "','" + c11 + "')";
                    nueva.comando(comando5);
                }
                else
                {
                    string numero = "";
                    if (comboBox1.Text == "ECU")
                        numero = num_maniECU;
                    else if (comboBox1.Text == "COL")
                        numero = num_maniCOL;
                    else
                        numero = num_maniPE;
                    
                    string pais_actual = nueva.Quitar_espacios(Convert.ToString(nueva.Consulta("SELECT cartas_de_porte.codigo_pais"
                    +" FROM manifiestos_de_carga INNER JOIN((cartas_de_porte INNER JOIN cartas_final " +
                    "ON cartas_de_porte.llave = cartas_final.id_carta) INNER JOIN manifiestos_final " +
                    "ON cartas_final.llave = manifiestos_final.id_carta_porte) ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto"
                    +" WHERE(([manifiestos_final].[llave] = "+id+"))").Rows[0].ItemArray[0]));
                    if (pais_actual == comboBox1.Text)
                    {  
                        numero = nueva.Quitar_espacios(Convert.ToString(nueva.Consulta("SELECT manifiestos_de_carga.numero_manifiesto_pais"
                        +" FROM manifiestos_de_carga INNER JOIN manifiestos_final ON manifiestos_de_carga.llave = manifiestos_final.id_manifiesto"
                        +" WHERE(([manifiestos_final].[llave] = "+id+"))").Rows[0].ItemArray[0]));

                    }

                    DataTable id_minifiestos_carga = nueva.Consulta("Select manifiestos_final.id_manifiesto from manifiestos_final where manifiestos_final.llave = " + id );
                    string comando5 = "UPDATE manifiestos_de_carga SET fecha_modificacion=NOW(),numero_manifiesto_pais='"+numero+"',c2='" + c2 + "',c3='" + c3 + "',id_vehiculo='" + c6 + "'," +
                        "id_unidad='" + c11 + "' where llave=" + nueva.Quitar_espacios(Convert.ToString(id_minifiestos_carga.Rows[0].ItemArray[0])) + "";
                    nueva.comando(comando5);

                }
                DataTable manifiesto = nueva.Consulta("Select llave from manifiestos_de_carga");


                if (id == "-1")
                {
                    string comando6 = "INSERT INTO Conductores_en_manifiesto " +
                    "VALUES('" + c13 + "','" + manifiesto.Rows[manifiesto.Rows.Count - 1].ItemArray[0] + "','PRINCIPAL')";
                    nueva.comando(comando6);

                    string comando7 = "INSERT INTO Conductores_en_manifiesto " +
                        "VALUES('" + c18 + "','" + manifiesto.Rows[manifiesto.Rows.Count - 1].ItemArray[0] + "','AYUDANTE')";
                    nueva.comando(comando7);
                }
                else
                {
                    DataTable id_minifiestos_carga = nueva.Consulta("Select manifiestos_final.id_manifiesto from manifiestos_final where manifiestos_final.llave = " + id + "");
                    string comando6 = "UPDATE Conductores_en_manifiesto " +
                    "SET id_conductor='" + c13 + "' where id_manifiestos=" + nueva.Quitar_espacios(Convert.ToString(id_minifiestos_carga.Rows[0].ItemArray[0])) + " and tipo_conductor='PRINCIPAL'";
                    nueva.comando(comando6);

                    string comando7 = "UPDATE Conductores_en_manifiesto " +
                        "SET id_conductor='" + c18 + "' where id_manifiestos=" + nueva.Quitar_espacios(Convert.ToString(id_minifiestos_carga.Rows[0].ItemArray[0])) + " and tipo_conductor='AYUDANTE'";
                    nueva.comando(comando7);
                }


                string idcartafinal = nueva.Quitar_espacios(Convert.ToString(nueva.Consulta("SELECT cartas_final.llave FROM cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta " +
                        " WHERE(((cartas_de_porte.codigo_pais) = '" + comboBox1.Text + "' " +
                        " And ((cartas_de_porte.numero_cartaporte) = " + numericUpDown1.Value + ")))").Rows[0].ItemArray[0]));

                if (id == "-1")
                {                    
                    string comando8 = "INSERT INTO manifiestos_final(id_manifiesto,id_carta_porte,c23,c24,c25,c26,c27,c28,c29,c30,c31,c32_1,c32_2,c33,c34,c37,c38,c40) " +
                    "VALUES('" + manifiesto.Rows[manifiesto.Rows.Count - 1].ItemArray[0] + "','" +idcartafinal+"','"+
                    c23 + "','" + c24 + "','" + c25 + "','" + c26 + "','" +
                    c27 + "','" + c28 + "','" + c29 + "','" + c30 + "','" + c31 + "','" + c32_1 + "','" + c32_2 + "','" + c33 + "','" + c34 + "','" +
                    c37 + "','" + c38 + "','" + c40 + "')";
                    nueva.comando(comando8);

                    id = manifiesto.Rows[manifiesto.Rows.Count - 1].ItemArray[0].ToString();
                    label34.Text = "EDITANDO MANIFIESTO";
                }
                else
                {
                    string comando8 = "UPDATE manifiestos_final SET " + "id_carta_porte="+ idcartafinal+", "+
                    "c23='" + c23 + "',c24='" + c24 + "',c25='" + c25 + "',c26='" + c26 + "',c27='" +
                    c27 + "',c28='" + c28 + "',c29='" + c29 + "',c30='" + c30 + "',c31='" + c31 + "',c32_1='" + c32_1 + "',c32_2='" + c32_2 + "',c33='" + c33 + "',c34='" + c34 + "',c37='" +
                    c37 + "',c38='" + c38 + "',c40='" + c40 + "' where llave=" + id + "";
                    nueva.comando(comando8);
                }
                MessageBox.Show("Se ha guardado exitosamente");
            }
            else
            {
                MessageBox.Show("No puede dejar el campo 6, 11, 13 y 18 vacíos, además asegurese que ingrese el codigo y el número de una cartaporte existente", "Advertencia Ingreso de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           

        }
    }
}
