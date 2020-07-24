using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Documentos
{
    public partial class Inventario_CP : Form
    {
        string c2, c3, c4, c5, c6, c7, c8,
               c9, c10, c11, c12, c13_1,
               c13_2, c14, c15, c16,
               c17_1, c17_2, c17_3,
               c17_4, c17_5, c17_6, c17_7,
               c17_8, c17_9, c17_10,
               c17_11, c17_12, c18, c19,
               c21, c22,dian;

        public string id = "-1";
        public string id_referencial = "-1";

        string numeroEC = "413";
        string numeroCOL = "80";
        string numeroPE = "1";

        private void richTextBox33_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox33_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n')
            {
                e.Handled = true;
            }

        }

        private void richTextBox34_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n')
            {
                e.Handled = true;
            }
        }

        private void richTextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            
        }

        string numero_final_para_enviar_al_manifiesto = "";


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox1_Click(sender, e);
            if (!string.IsNullOrEmpty(richTextBox1.Text) && !string.IsNullOrEmpty(richTextBox2.Text) && (comboBox1.Text == "EC" || comboBox1.Text == "CO" || comboBox1.Text == "PE"))
                Abrir_Manifiestos(comboBox1.Text, numero_final_para_enviar_al_manifiesto);

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox1_Click(sender, e);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            QuitarVacios();
            DatosCartaPorte datos = new DatosCartaPorte();

            Base nueva = new Base();            
            string numero = "";
            string busquedaEcu = "Select numero_cartaporte from cartas_de_porte where codigo_pais='EC' Order By numero_cartaporte asc";
            string busquedaCOL = "Select numero_cartaporte from cartas_de_porte where codigo_pais='CO' Order By numero_cartaporte asc";
            string busquedaPE = "Select numero_cartaporte from cartas_de_porte where codigo_pais='PE' Order By numero_cartaporte asc";
            if (nueva.Consulta(busquedaEcu).Rows.Count != 0)
            {
                numeroEC = Convert.ToString(Convert.ToInt32(nueva.Consulta(busquedaEcu).Rows[nueva.Consulta(busquedaEcu).Rows.Count - 1].ItemArray[0]) + 1);
            }
            if (nueva.Consulta(busquedaCOL).Rows.Count != 0)
            {
                numeroCOL = Convert.ToString(Convert.ToInt32(nueva.Consulta(busquedaCOL).Rows[nueva.Consulta(busquedaCOL).Rows.Count - 1].ItemArray[0]) + 1);
            }
            if (nueva.Consulta(busquedaPE).Rows.Count != 0)
            {
                numeroPE = Convert.ToString(Convert.ToInt32(nueva.Consulta(busquedaPE).Rows[nueva.Consulta(busquedaPE).Rows.Count - 1].ItemArray[0]) + 1);
            }
            if (id == "-1")
            {
                
                if (comboBox1.Text == "EC")
                    numero = numeroEC;
                else if (comboBox1.Text == "CO")
                    numero = numeroCOL;
                else
                    numero = numeroPE;
                
            }
            else
            {                                

                DataTable codigopais = nueva.Consulta("Select cartas_de_porte.codigo_pais from cartas_final inner join cartas_de_porte on cartas_final.llave=cartas_de_porte.llave where cartas_final.llave = " + id + "");
                if (nueva.Quitar_espacios(Convert.ToString(codigopais.Rows[0].ItemArray[0])) == comboBox1.Text)
                {
                    DataTable numero_cartaporte = nueva.Consulta("Select cartas_de_porte.numero_cartaporte from cartas_final inner join cartas_de_porte on cartas_final.llave=cartas_de_porte.llave where cartas_final.llave = " + id + "");
                    numero = nueva.Quitar_espacios(Convert.ToString(numero_cartaporte.Rows[0].ItemArray[0]));                    
                }
            }

            string pais = comboBox1.Text;
            
            datos.numero = richTextBox33.Text+"\nCEC"+richTextBox34.Text;
            datos.codigo = "000"+numero + " "+comboBox1.Text;
            datos.nomDirRemitente = richTextBox1.Text;
            datos.nomDirDestinatario= richTextBox2.Text;
            datos.nomDirConsignatario = richTextBox3.Text;
            datos.notificar = richTextBox4.Text;
            datos.lugarRecibe = richTextBox5.Text;
            datos.lugarEmbarque = richTextBox6.Text;
            datos.lugarEntrega = richTextBox11.Text;
            datos.Condiciones = richTextBox13.Text;
            datos.CantidadBultos = richTextBox7.Text;
            datos.MarcasBultos = richTextBox10.Text;
            datos.DescripcionMercancia = richTextBox12.Text;
            datos.PesoNeto = richTextBox32.Text;
            datos.PesoBruto = richTextBox8.Text;
            datos.VolumenMetros = richTextBox15.Text;
            datos.OtrasUnidades = richTextBox14.Text;
            datos.PrecioMercancias = richTextBox9.Text;
            datos.GastosValorFlete = richTextBox16.Text.Replace(".",",");
            datos.GastosMonedaFlete = richTextBox21.Text;
            datos.GastosCargoDestinatarioFlete = richTextBox24.Text.Replace(".", ",");
            datos.GastosMonedaCargoDestinatarioFlete = richTextBox27.Text;
            datos.GastosValorSeguro = richTextBox17.Text.Replace(".", ",");
            datos.GastosMonedaSeguro = richTextBox20.Text;
            datos.GastosCargoDestinatarioSeguro = richTextBox23.Text.Replace(".", ",");
            datos.GastosMonedaCargoDestinatarioSeguro = richTextBox26.Text;
            datos.GastosValorOtros = richTextBox18.Text.Replace(".", ",");
            datos.GastosMonedaOtros = richTextBox19.Text;
            datos.GastosCargoDestinatarioOtros = richTextBox22.Text.Replace(".", ",");
            datos.GastosMonedaCargoDestinatarioOtros = richTextBox25.Text;
            datos.DocumentosRecibidos = richTextBox29.Text;
            datos.LugarEmision = richTextBox30.Text;
            datos.InstruccionesTransportista = richTextBox31.Text;
            datos.ObservacionesTransportista = richTextBox28.Text;            
            decimal sumar = Convert.ToDecimal(datos.GastosValorFlete)+ Convert.ToDecimal(datos.GastosValorSeguro)+ Convert.ToDecimal(datos.GastosValorOtros);            
            datos.SumaRemitente = sumar.ToString();
            double sumard = Convert.ToDouble(datos.GastosCargoDestinatarioFlete) + Convert.ToDouble(datos.GastosCargoDestinatarioSeguro) + Convert.ToDouble(datos.GastosCargoDestinatarioOtros);
            datos.SumaDestinatario = sumard+"";            

            Form2 frm2 = new Form2();
            frm2.datos.Add(datos);
            frm2.Show();

            VolverVacios();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (id == "-1")
            {
                string pais = comboBox1.Text;
                Base nueva = new Base();
                try
                {
                    string numerooo = (Convert.ToInt32(nueva.Consulta("Select numero_cartaporte FROM cartas_de_porte where codigo_pais='" + pais + "' order by numero_cartaporte desc").Rows[0].ItemArray[0].ToString()) + 1).ToString();
                    label34.Text = "NUEVA CARTA DE PORTE " + numerooo;
                }
                catch (Exception tryyu)
                {
                    label34.Text = "NUEVA CARTA DE PORTE ";
                }
            }
              
        }

        

        private void groupBox4_Enter(object sender, EventArgs e)
        {
            
        }

        

        private void richTextBox21_Leave(object sender, EventArgs e)
        {
            string moneda = richTextBox21.Text;
            richTextBox19.Text = moneda;
            richTextBox20.Text = moneda;
            richTextBox25.Text = moneda;
            richTextBox26.Text = moneda;
            richTextBox27.Text = moneda;
        }


        public void Abrir_Manifiestos(string pais,string num)
        {
            Base nueva = new Base();
            string numero = "";
            string busquedaEcu = "Select numero_cartaporte from cartas_de_porte where codigo_pais='EC' Order By numero_cartaporte asc";
            string busquedaCOL = "Select numero_cartaporte from cartas_de_porte where codigo_pais='CO' Order By numero_cartaporte asc";
            string busquedaPE = "Select numero_cartaporte from cartas_de_porte where codigo_pais='PE' Order By numero_cartaporte asc";
            if (nueva.Consulta(busquedaEcu).Rows.Count != 0)
            {
                numeroEC = Convert.ToString(Convert.ToInt32(nueva.Consulta(busquedaEcu).Rows[nueva.Consulta(busquedaEcu).Rows.Count - 1].ItemArray[0]) + 1);
            }
            if (nueva.Consulta(busquedaCOL).Rows.Count != 0)
            {
                numeroCOL = Convert.ToString(Convert.ToInt32(nueva.Consulta(busquedaCOL).Rows[nueva.Consulta(busquedaCOL).Rows.Count - 1].ItemArray[0]) + 1);
            }
            if (nueva.Consulta(busquedaPE).Rows.Count != 0)
            {
                numeroPE = Convert.ToString(Convert.ToInt32(nueva.Consulta(busquedaPE).Rows[nueva.Consulta(busquedaPE).Rows.Count - 1].ItemArray[0]) + 1);
            }
            if (id == "-1")
            {

                if (comboBox1.Text == "EC")
                    numero = numeroEC;
                else if (comboBox1.Text == "COL")
                    numero = numeroCOL;
                else
                    numero = numeroPE;

            }
            else
            {

                DataTable codigopais = nueva.Consulta("Select cartas_de_porte.codigo_pais from cartas_final inner join cartas_de_porte on cartas_final.llave=cartas_de_porte.llave where cartas_final.llave = " + id + "");
                if (nueva.Quitar_espacios(Convert.ToString(codigopais.Rows[0].ItemArray[0])) == comboBox1.Text)
                {
                    DataTable numero_cartaporte = nueva.Consulta("Select cartas_de_porte.numero_cartaporte from cartas_final inner join cartas_de_porte on cartas_final.llave=cartas_de_porte.llave where cartas_final.llave = " + id + "");
                    numero = nueva.Quitar_espacios(Convert.ToString(numero_cartaporte.Rows[0].ItemArray[0]));
                }
            }            


            

            Documento2 actual = new Documento2();
            //actual.id = id;
            //actual.id_referencia = refe;
            
            string aduanaCruce = "";
            string aduanaDestino = "";

            if(comboBox1.Text=="EC")
            {
                aduanaCruce = "TULCAN    ECUADOR";
                aduanaDestino = "IPIALES    COLOMBIA";
            }
            else if (comboBox1.Text == "CO")
            {
                aduanaCruce = "IPIALES    COLOMBIA";
                aduanaDestino = "TULCAN    ECUADOR";
            }
            else if(comboBox1.Text=="PE")
            {
                aduanaCruce = "HUAQUILLAS    ECUADOR";
                aduanaDestino = "AGUASU    PERU";
            }

            List<string> sobrepasa = new List<string>();
            
            actual.richTextBox38.Text = aduanaCruce;
            actual.richTextBox37.Text = aduanaDestino;
            actual.numericUpDown1.Value = Convert.ToInt32(num);
            actual.comboBox1.Text = pais;
            actual.richTextBox22.Text = "000"+numero+" "+pais;
            string n27 = "";
            try
            {
                n27=this.richTextBox6.Text.Split(',')[0];
                if (n27[0] == '\n')
                    n27 = n27.Substring(1);
            }catch(Exception sghhg) { }
            
            
            if (seAcepta(actual.richTextBox27, n27))
                actual.richTextBox27.Text = n27;
            else
                sobrepasa.Add("[7]");


            if (seAcepta(actual.richTextBox26, this.richTextBox11.Text))
                actual.richTextBox26.Text = this.richTextBox11.Text;
            else
                sobrepasa.Add("[8]");

            if (seAcepta(actual.richTextBox21, this.richTextBox12.Text))
                actual.richTextBox21.Text = this.richTextBox12.Text;
            else
                sobrepasa.Add("[12]");

            if (seAcepta(actual.richTextBox20, this.richTextBox7.Text.Split('\n')[0]))
                actual.richTextBox20.Text = this.richTextBox7.Text.Split('\n')[0];
            else
                sobrepasa.Add("[10]");
            
            try
            {
                if (seAcepta(actual.richTextBox19, this.richTextBox7.Text.Split('\n')[1]))
                    actual.richTextBox19.Text = this.richTextBox7.Text.Split('\n')[1];
                else
                    sobrepasa.Add("[10]");
                
            }
            catch (Exception e)
            {
                actual.richTextBox19.Text = "";
            }

            if (seAcepta(actual.richTextBox18, this.richTextBox32.Text))
                actual.richTextBox18.Text = this.richTextBox32.Text;
            else
                sobrepasa.Add("[13.1]");

            if (seAcepta(actual.richTextBox17, this.richTextBox8.Text))
                actual.richTextBox17.Text = this.richTextBox8.Text;
            else
                sobrepasa.Add("[13.2]");

            if (seAcepta(actual.richTextBox36, this.richTextBox30.Text))
                actual.richTextBox36.Text = this.richTextBox30.Text;
            else
                sobrepasa.Add("[19]");

            if (seAcepta(actual.richTextBox39, this.richTextBox9.Text.Replace("\n",",")))
                actual.richTextBox39.Text = this.richTextBox9.Text.Replace("\n", " ");
            else
                sobrepasa.Add("[16]");  
            
            if(sobrepasa.Count==0)
            {
                Form1 principal = Application.OpenForms.OfType<Form1>().SingleOrDefault();
                //Contenedor_controles principal = (Contenedor_controles)this;

                if (principal.panel2.Controls.Count > 0)
                {
                    principal.panel2.Controls.RemoveAt(0);
                }
                actual.TopLevel = false;
                actual.Dock = DockStyle.Fill;
                principal.panel2.Controls.Add(actual);
                principal.Tag = actual;
                actual.Show();
            }
            else
            {
                DialogResult respuesta = new DialogResult();
                string textmani = "";
                for(int i =0;i<sobrepasa.Count;i++)
                {
                    textmani += sobrepasa[i];
                }
                respuesta = MessageBox.Show("Los siguientes campos son muy grandes para los campos del manifiesto:"+textmani+"\n ¿Desea continuar sin cargar estos campos?", "Abrir Nuevo Manifiesto ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (respuesta.ToString() == "Yes")
                {
                    Form1 principal = Application.OpenForms.OfType<Form1>().SingleOrDefault();
                    //Contenedor_controles principal = (Contenedor_controles)this;

                    if (principal.panel2.Controls.Count > 0)
                    {
                        principal.panel2.Controls.RemoveAt(0);
                    }
                    actual.TopLevel = false;
                    actual.Dock = DockStyle.Fill;
                    principal.panel2.Controls.Add(actual);
                    principal.Tag = actual;
                    actual.Show();
                }
            }
            
        }

        public bool seAcepta(RichTextBox rich, string texto)
        {
            bool f = true;

            if (rich.MaxLength < texto.Length)
            {
                f= false;
            }
            return f;
        }

        private void richTextBox21_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox22_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' && richTextBox22.Text.Contains("."))
            {
                e.Handled = true;
            }
            else
            {
                Validar.NumerosYPuntoSinEspacio(e);
            }

        }

        private void richTextBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' && richTextBox23.Text.Contains("."))
            {
                e.Handled = true;
            }
            else
            {
                Validar.NumerosYPuntoSinEspacio(e);
            }
        }

        private void richTextBox24_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' && richTextBox24.Text.Contains("."))
            {
                e.Handled = true;
            }
            else
            {
                Validar.NumerosYPuntoSinEspacio(e);
            }
        }

        private void richTextBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' && richTextBox18.Text.Contains("."))
            {
                e.Handled = true;
            }
            else
            {
                Validar.NumerosYPuntoSinEspacio(e);
            }
        }

        private void richTextBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' && richTextBox17.Text.Contains("."))
            {
                e.Handled = true;
            }
            else
            {
                Validar.NumerosYPunto(e);
            }
        }

        private void richTextBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' && richTextBox16.Text.Contains("."))
            {
                e.Handled = true;
            }
            else
            {
                Validar.NumerosYPuntoSinEspacio(e);
            }
        }

        private void richTextBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' && richTextBox32.Text.Contains("."))
            {
                e.Handled = true;
            }
            else
            {
                Validar.NumerosYPuntoSinEspacio(e);
            }
        }

        private void richTextBox8_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void richTextBox32_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            
        }

        public Inventario_CP()
        {
            InitializeComponent();
            
            Base consultas = new Base();
            DataTable organizacion = consultas.Consulta("select c2yc3 from Organizaciones_y_direcciones");            


            string[] arreglo = new string[organizacion.Rows.Count];
            for (int i = 0; i < organizacion.Rows.Count; i++)
            {
                arreglo[i] = (Convert.ToString(organizacion.Rows[i].ItemArray[0]).Replace("  ", ""));
            }
            var source1 = new AutoCompleteStringCollection();
            source1.AddRange(arreglo);

        }

        private void QuitarVacios()
        {
            foreach (Control ctrl in panel3.Controls)
            {
                if (ctrl is GroupBox)
                {
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        if ((ctrl2 is TextBox || ctrl2 is RichTextBox) && string.IsNullOrEmpty(ctrl2.Text))
                        {                            
                            ctrl2.Text = " ";
                            if (ctrl2 == richTextBox32 ||
                                ctrl2 == richTextBox8 ||
                                ctrl2 == richTextBox15 ||
                                ctrl2 == richTextBox16 ||
                                ctrl2 == richTextBox17 ||
                                ctrl2 == richTextBox18 ||
                                ctrl2 == richTextBox22 ||
                                ctrl2 == richTextBox23 ||
                                ctrl2 == richTextBox24 || 
                                ctrl2.Text.Replace(" ","")==".")
                                ctrl2.Text = "0";
                        }                        
                    }
                }
            }
        }

        private void VolverVacios()
        {
            foreach (Control ctrl in panel3.Controls)
            {
                if (ctrl is GroupBox)
                {
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        if ((ctrl2 is TextBox || ctrl2 is RichTextBox) && (ctrl2.Text==" " || ctrl2.Text=="0"))
                        {                            
                            ctrl2.Text = "";                            
                        }
                    }
                }
            }
        }
        private void Limpiar()
        {
            foreach (Control ctrl in panel3.Controls)
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
                    }
                }

            }
        }

        private void refrescar_valores()
        {
            QuitarVacios();

            c2 = richTextBox1.Text.ToUpper();
            c3 = richTextBox2.Text.ToUpper();
            c4 = richTextBox3.Text.ToUpper();
            c5 = richTextBox4.Text.ToUpper();
            c6 = richTextBox5.Text.ToUpper();
            c7 = richTextBox6.Text.ToUpper();
            c8 = richTextBox11.Text.ToUpper();
            c9 = richTextBox13.Text.ToUpper();
            c10 = richTextBox7.Text.ToUpper();
            c11 = richTextBox10.Text.ToUpper();
            c12 = richTextBox12.Text.ToUpper();
            c13_1 = richTextBox32.Text.Replace("\n", "");
            if (c13_1 == ".")
                c13_1 = "";
            c13_2 = richTextBox8.Text.Replace("\n", "");
            if (c13_2 == ".")
                c13_2 = "";
            c14 = richTextBox15.Text.Replace("\n", "");
            if (c14 == ".")
                c14 = "";
            c15 = richTextBox14.Text;
            c16 = richTextBox9.Text.ToUpper();
            c17_1 = richTextBox16.Text.Replace("\n", "").Replace('.', ',');
            if (c17_1 == ".")
                c17_1 = "";
            c17_2 = richTextBox21.Text.ToUpper();
            c17_3 = richTextBox24.Text.Replace("\n", "").Replace('.', ',');
            if (c17_3 == ".")
                c17_3 = "";
            c17_4 = richTextBox27.Text.ToUpper();
            c17_5 = richTextBox17.Text.Replace("\n", "").Replace('.',',');
            if (c17_5 == ".")
                c17_5 = "";
            c17_6 = richTextBox20.Text.ToUpper();
            c17_7 = richTextBox23.Text.Replace("\n", "").Replace('.', ',');
            if (c17_7 == ".")
                c17_7 = "";
            c17_8 = richTextBox26.Text.ToUpper();
            c17_9 = richTextBox18.Text.Replace("\n", "").Replace('.', ',');
            if (c17_9 == ".")
                c17_9 = "";
            c17_10 = richTextBox19.Text.ToUpper();
            c17_11 = richTextBox22.Text.Replace("\n", "").Replace('.', ',');
            if (c17_11 == ".")
                c17_11 = "";
            c17_12 = richTextBox25.Text.ToUpper();
            c18 = richTextBox29.Text.ToUpper();
            c19 = richTextBox30.Text.ToUpper();
            c21 = richTextBox31.Text.ToUpper();
            c22 = richTextBox28.Text.ToUpper();
            dian = richTextBox33.Text+"\n"+richTextBox34.Text;

            VolverVacios();
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Limpiar();
        }        
        

        private void cargar_datos ( string re)
        {


                   

            if (re != "-1")
            {

                Base nueva = new Base();

                string pais = nueva.Quitar_espacios(nueva.Consulta("SELECT cartas_de_porte.codigo_pais"
                + " FROM cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta"
                + " WHERE(([cartas_final].[llave] = " + re + "))").Rows[0].ItemArray[0].ToString());

                string nu = nueva.Quitar_espacios(nueva.Consulta("SELECT cartas_de_porte.numero_cartaporte"
                + " FROM cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta"
                + " WHERE(([cartas_final].[llave] = " + re + "))").Rows[0].ItemArray[0].ToString());

                if (id != "-1")
                {
                    label34.Text = "EDITANDO " + pais + " " + nu;
                }
                else
                {
                    try
                    {
                        string numerooo = (Convert.ToInt32(nueva.Consulta("Select numero_cartaporte FROM cartas_de_porte where codigo_pais='" + pais + "' order by numero_cartaporte desc").Rows[0].ItemArray[0].ToString()) + 1).ToString();
                        label34.Text = "NUEVA CARTA DE PORTE " + numerooo;
                    }
                    catch (Exception tryyu) { }
                    
                }

                comboBox1.Text = pais;

                richTextBox1.Text = nueva.Quitar_espacios(nueva.Consulta("SELECT Organizaciones_y_direcciones.c2yc3"
                +" FROM Organizaciones_y_direcciones INNER JOIN((cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta) INNER JOIN Organizaciones_en_cartaportes ON cartas_de_porte.llave = Organizaciones_en_cartaportes.id_carta) ON Organizaciones_y_direcciones.id_organizacion = Organizaciones_en_cartaportes.id_organizacion"
                + " WHERE(([cartas_final].[llave] = " + re + ") AND([Organizaciones_en_cartaportes].[papel_organizacion] = 'EMISOR'))").Rows[0].ItemArray[0].ToString());

                richTextBox2.Text = nueva.Quitar_espacios(nueva.Consulta("SELECT Organizaciones_y_direcciones.c2yc3"
                + " FROM Organizaciones_y_direcciones INNER JOIN((cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta) INNER JOIN Organizaciones_en_cartaportes ON cartas_de_porte.llave = Organizaciones_en_cartaportes.id_carta) ON Organizaciones_y_direcciones.id_organizacion = Organizaciones_en_cartaportes.id_organizacion"
                + " WHERE(([cartas_final].[llave] = " + re + ") AND([Organizaciones_en_cartaportes].[papel_organizacion] = 'RECEPTOR'))").Rows[0].ItemArray[0].ToString());

                DataTable consulta_carta_pote = nueva.Consulta("SELECT cartas_de_porte.*"
                +" FROM cartas_de_porte INNER JOIN cartas_final ON cartas_de_porte.llave = cartas_final.id_carta"
                +" WHERE(([cartas_final].[llave] = "+re+"))");

                richTextBox3.Text=nueva.Quitar_espacios(consulta_carta_pote.Rows[0].ItemArray[5].ToString());
                richTextBox4.Text = nueva.Quitar_espacios(consulta_carta_pote.Rows[0].ItemArray[6].ToString());
                richTextBox5.Text = nueva.Quitar_espacios(consulta_carta_pote.Rows[0].ItemArray[7].ToString());
                richTextBox6.Text = nueva.Quitar_espacios(consulta_carta_pote.Rows[0].ItemArray[8].ToString());
                richTextBox11.Text = nueva.Quitar_espacios(consulta_carta_pote.Rows[0].ItemArray[9].ToString());
                richTextBox13.Text = nueva.Quitar_espacios(consulta_carta_pote.Rows[0].ItemArray[10].ToString());
                richTextBox7.Text = nueva.Quitar_espacios(consulta_carta_pote.Rows[0].ItemArray[11].ToString());
                richTextBox10.Text = nueva.Quitar_espacios(consulta_carta_pote.Rows[0].ItemArray[12].ToString());
                richTextBox12.Text = nueva.Quitar_espacios(consulta_carta_pote.Rows[0].ItemArray[13].ToString());


                DataTable consulta_carta_final = nueva.Consulta("SELECT *"
                +" FROM cartas_final"
                +" WHERE(([cartas_final].[llave] = +"+re+"))");
                richTextBox32.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[2].ToString());
                richTextBox8.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[3].ToString());
                richTextBox15.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[4].ToString());
                richTextBox14.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[5].ToString());
                richTextBox9.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[6].ToString());
                richTextBox16.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[7].ToString()).Replace(',', '.');
                richTextBox21.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[8].ToString());
                richTextBox24.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[9].ToString()).Replace(',', '.');
                richTextBox27.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[10].ToString());
                richTextBox17.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[11].ToString()).Replace(',', '.');
                richTextBox20.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[12].ToString());
                richTextBox23.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[13].ToString()).Replace(',', '.');
                richTextBox26.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[14].ToString());
                richTextBox18.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[15].ToString()).Replace(',', '.');
                richTextBox19.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[16].ToString());
                richTextBox22.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[17].ToString()).Replace(',', '.');
                richTextBox25.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[18].ToString());
                richTextBox29.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[19].ToString());
                richTextBox30.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[20].ToString());
                richTextBox31.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[21].ToString());
                richTextBox28.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[22].ToString());
                try { richTextBox33.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[23].ToString().Split('\n')[0]); } catch (Exception cc) { }
                try { richTextBox34.Text = nueva.Quitar_espacios(consulta_carta_final.Rows[0].ItemArray[23].ToString().Split('\n')[1]); }catch(Exception dfd) { }

                //richTextBox2.Text = nueva.Quitar_espacios(nueva.Consulta("").Rows[0].ItemArray[0].ToString());
            }
        }
        private void Inventario_CP_Load(object sender, EventArgs e)
        {
            cargar_datos(id_referencial);
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {                                  
            Base nueva = new Base();
            refrescar_valores();
            
            string busquedaEcu = "Select numero_cartaporte from cartas_de_porte where codigo_pais='EC' Order By numero_cartaporte asc";
            string busquedaCOL = "Select numero_cartaporte from cartas_de_porte where codigo_pais='CO' Order By numero_cartaporte asc";
            string busquedaPE = "Select numero_cartaporte from cartas_de_porte where codigo_pais='PE' Order By numero_cartaporte asc";

            if(nueva.Consulta(busquedaEcu).Rows.Count!=0)
            {
                numeroEC = Convert.ToString(Convert.ToInt32(nueva.Consulta(busquedaEcu).Rows[nueva.Consulta(busquedaEcu).Rows.Count - 1].ItemArray[0]) + 1);
            }

            if (nueva.Consulta(busquedaCOL).Rows.Count != 0)
            {
                numeroCOL = Convert.ToString(Convert.ToInt32(nueva.Consulta(busquedaCOL).Rows[nueva.Consulta(busquedaCOL).Rows.Count - 1].ItemArray[0]) + 1);
            }

            if (nueva.Consulta(busquedaPE).Rows.Count != 0)
            {
                numeroPE = Convert.ToString(Convert.ToInt32(nueva.Consulta(busquedaPE).Rows[nueva.Consulta(busquedaPE).Rows.Count - 1].ItemArray[0]) + 1);
            }


            if (!string.IsNullOrEmpty(richTextBox1.Text) && !string.IsNullOrEmpty(richTextBox2.Text) && (comboBox1.Text=="EC" || comboBox1.Text == "CO" || comboBox1.Text == "PE"))
            {
                if (!nueva.Dato_en_consulta(nueva.Quitar_espacios(c2), "Select c2yc3 from Organizaciones_y_direcciones"))
                {
                    string comando1 = "INSERT INTO Organizaciones_y_direcciones(c2yc3) " +
                    "VALUES('" + c2 + "')";
                    nueva.comando(comando1);
                }
                string codigo_emisor = nueva.Quitar_espacios(Convert.ToString(nueva.Consulta("Select id_organizacion from Organizaciones_y_direcciones where c2yc3 ='" + c2 + "'").Rows[0].ItemArray[0]));

                if (!nueva.Dato_en_consulta(nueva.Quitar_espacios(c3), "Select c2yc3 from Organizaciones_y_direcciones"))
                {
                    string comando2 = "INSERT INTO Organizaciones_y_direcciones(c2yc3) " +
                    "VALUES('" + c3 + "')";
                    nueva.comando(comando2);
                }
                string codigo_receptor = nueva.Quitar_espacios(Convert.ToString(nueva.Consulta("Select id_organizacion from Organizaciones_y_direcciones where c2yc3 ='" + c3 + "'").Rows[0].ItemArray[0]));

                if (id == "-1")
                {
                    string numero = "";
                    if (comboBox1.Text == "EC")
                        numero = numeroEC;
                    else if (comboBox1.Text == "CO")
                        numero = numeroCOL;
                    else
                        numero = numeroPE;

                    string comando3 = "INSERT INTO cartas_de_porte (fecha_creacion,fecha_modificacion,codigo_pais,numero_cartaporte,c4,c5,c6,c7,c8,c9,c10,c11,c12) " +
                    "VALUES(NOW(), NOW(), '" + comboBox1.Text + "', '" + numero + "', '" + c4 + "', '" + c5 + "', '" +
                    c6 + "','" + c7 + "','" + c8 + "','" + c9 + "','" + c10 + "','" + c11 + "','" + c12 + "')";
                    nueva.comando(comando3);
                    numero_final_para_enviar_al_manifiesto = numero;
                }
                else
                {
                    string codigo_pais = comboBox1.Text;
                    string numero = "";                    
                    if (comboBox1.Text == "EC")
                        numero = numeroEC;
                    else if (comboBox1.Text == "CO")
                        numero = numeroCOL;
                    else
                        numero = numeroPE;

                    DataTable codigopais = nueva.Consulta("Select cartas_de_porte.codigo_pais from cartas_final inner join cartas_de_porte on cartas_final.llave=cartas_de_porte.llave where cartas_final.llave = " + id + "");
                    if(nueva.Quitar_espacios(Convert.ToString(codigopais.Rows[0].ItemArray[0]))==comboBox1.Text)
                    {                    
                        DataTable numero_cartaporte = nueva.Consulta("Select cartas_de_porte.numero_cartaporte from cartas_final inner join cartas_de_porte on cartas_final.llave=cartas_de_porte.llave where cartas_final.llave = " + id + "");
                        numero = nueva.Quitar_espacios(Convert.ToString(numero_cartaporte.Rows[0].ItemArray[0]));
                        codigo_pais=nueva.Quitar_espacios(Convert.ToString(codigopais.Rows[0].ItemArray[0]));
                    }

                    DataTable id_cartaporte = nueva.Consulta("Select cartas_de_porte.llave from cartas_final inner join cartas_de_porte on cartas_final.llave=cartas_de_porte.llave where cartas_final.llave = " + id );
                    string comando = "UPDATE cartas_de_porte SET fecha_modificacion=NOW(),codigo_pais='"+codigo_pais+"',numero_cartaporte='"+numero+"',c4='" + c4 + "',c5='" + c5 + "',c6='" + c6 + "',c7='"+c7+"',C8='" + c8 + "',c9='" + c9 + "" +
                        "',c10='" + c10 + "',c11='" + c11 + "',c12='" + c12 + "' WHERE llave=" + nueva.Quitar_espacios(Convert.ToString(id_cartaporte.Rows[0].ItemArray[0])) + "";
                    nueva.comando(comando);

                    numero_final_para_enviar_al_manifiesto = numero;
                }
                DataTable cartas = nueva.Consulta("Select llave from cartas_de_porte");

                if (id == "-1")
                {

                    string comando4 = "INSERT INTO Organizaciones_en_cartaportes " +
                        "VALUES('" + nueva.Quitar_espacios(Convert.ToString(cartas.Rows[cartas.Rows.Count - 1].ItemArray[0])) + "','" + codigo_emisor + "','EMISOR')";
                    nueva.comando(comando4);

                    string comando5 = "INSERT INTO Organizaciones_en_cartaportes " +
                   "VALUES('" + nueva.Quitar_espacios(Convert.ToString(cartas.Rows[cartas.Rows.Count - 1].ItemArray[0])) + "','" + codigo_receptor + "','RECEPTOR')";
                    nueva.comando(comando5);

                }
                else
                {
                    DataTable id_cartaporte = nueva.Consulta("Select cartas_de_porte.llave from cartas_final inner join cartas_de_porte on cartas_final.llave=cartas_de_porte.llave  where cartas_final.llave = " + id );
                    string comando4 = "UPDATE Organizaciones_en_cartaportes SET id_organizacion='" + codigo_emisor + "' Where id_carta =" + id_cartaporte.Rows[0].ItemArray[0] + " and" +
                        " papel_organizacion='EMISOR'";
                    nueva.comando(comando4);

                    string comando5 = "UPDATE Organizaciones_en_cartaportes SET id_organizacion='" + codigo_receptor + "' Where id_carta =" + id_cartaporte.Rows[0].ItemArray[0] + " and" +
                        " papel_organizacion='RECEPTOR'";
                    nueva.comando(comando5);
                }

                if (id == "-1")
                {
                    string comando6 = "INSERT INTO cartas_final (id_carta, c13_1,c13_2,c14,c15,c16,c17_1,c17_2,c17_3,c17_4,c17_5,c17_6,c17_7,c17_8,c17_9,c17_10,c17_11,c17_12,c18,c19,c21,c22,dian) " +
                    " VALUES('" + cartas.Rows[cartas.Rows.Count - 1].ItemArray[0] + "','" + c13_1 + "','" + c13_2 + "','" +
                    c14 + "','" + c15 + "','" + c16 + "','" + c17_1 + "','" + c17_2 + "','" + c17_3 + "','" + c17_4 + "','" + c17_5 + "','" + c17_6 + "','" +
                    c17_7 + "','" + c17_8 + "','" + c17_9 + "','" + c17_10 + "','" + c17_11 + "','" + c17_12 + "','" + c18 + "','" + c19 + "','" + c21 + "','" + c22 + "','"+dian+"')";
                    nueva.comando(comando6);

                    id = cartas.Rows[cartas.Rows.Count - 1].ItemArray[0].ToString();
                    label34.Text = "EDITANDO CARTA DE PORTE";
                }
                else
                {
                    string comando6 = "UPDATE cartas_final SET c13_1='" + c13_1 + "',dian='" + dian + "',c13_2='" + c13_2 + "',c14='" + c14 + "'," +
                        "c15='" + c15 + "',c16='" + c16 + "',c17_1='" + c17_1 + "',c17_2='" + c17_2 + "',c17_3='" + c17_3 + "',c17_4='" + c17_4 + "'," +
                        "c17_5='" + c17_5 + "',c17_6='" + c17_6 + "',c17_7='" + c17_7 + "',c17_8='" + c17_8 + "',c17_9='" + c17_9 + "',c17_10='" + c17_10 + "'" +
                        ",c17_11='" + c17_11 + "',c17_12='" + c17_12 + "',c18='" + c18 + "',c19='" + c19 + "',c21='" + c21 + "',c22='" + c22 + "'" +
                        "WHERE llave=" + id;
                    nueva.comando(comando6);

                }
                MessageBox.Show("Se ha guardado exitosamente");
            }
            else
            {
                MessageBox.Show("No puede dejar el campo 2 y 3 vacíos, además se tiene que ingresar el código del país ","Advertencia Ingreso de datos",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            
            
        }
    }
}
