using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace Documentos
{
    class Base
    {
        public void crear()
        {
            //CREAMOS LA BASE DE DATOS CON ACCESS
            ADOX.Catalog cat = new ADOX.Catalog();

            cat.Create("Provider=Microsoft.Jet.OLEDB.4.0;" +
            "Data Source = documentos.accdb;" +
            "Jet OLEDB:Engine Type=5");

            //CREAMOS Y ABRIMOS LA CONEXION

            OleDbConnectionStringBuilder documentos = new OleDbConnectionStringBuilder();
            documentos.DataSource = "documentos.accdb";
            documentos.Provider = "Microsoft.Jet.OLEDB.4.0";
            OleDbConnection conexion = new OleDbConnection(documentos.ToString());
            conexion.Open();

            //CREAMOS LOS COMANDOS

            List<String> comandos = new List<string>();

            string c1 = "CREATE TABLE Organizaciones_y_direcciones" +
                        "(id_organizacion AUTOINCREMENT PRIMARY KEY ," +
                        "c2yc3 char(200))";

            string c2 = "CREATE TABLE cartas_de_porte " +
                "(llave AUTOINCREMENT PRIMARY KEY," +
                "fecha_creacion DATE," +
                "fecha_modificacion DATE," +
                "codigo_pais char(3)," +
                "numero_cartaporte int," +                
                "c4 char(200)," +
                "c5 char(200)," +
                "c6 char(50)," +
                "c7 char(50)," +
                "c8 char(50)," +
                "c9 char(100)," +
                "c10 char(160)," +
                "c11 char(200)," +
                "c12 MEMO)";

            string c3 = "CREATE TABLE Organizaciones_en_cartaportes" +
                        "(id_carta int not null constraint FK_carta1 references cartas_de_porte (llave)," +
                        "id_organizacion int not null constraint FK_organizacion references Organizaciones_y_direcciones (id_organizacion)," +
                        "papel_organizacion char(12))";

            

            string c4 = "CREATE TABLE cartas_final"+
                "(llave AUTOINCREMENT PRIMARY KEY," +
                "id_carta int not null constraint FK_carta2 references cartas_de_porte (llave)," +
                "c13_1 char(12)," +
                "c13_2 char(12)," +
                "c14 float," +
                "c15 char(10)," +
                "c16 char(50)," +
                "c17_1 float," +
                "c17_2 char(3)," +
                "c17_3 float," +
                "c17_4 char(3)," +
                "c17_5 float," +
                "c17_6 char(3)," +
                "c17_7 float," +
                "c17_8 char(8)," +
                "c17_9 float," +
                "c17_10 char(3)," +
                "c17_11 float," +
                "c17_12 char(3)," +
                "c18 char(100)," +
                "c19 char(50)," +
                "c21 char(165)," +
                "c22 char(165)," +
                "dian char(45))";

            //Manifiestos
            string c5 = "CREATE TABLE Conductores" +                        
                        "(c14 char(10) ," +
                        "c13 char(45) PRIMARY KEY," +
                        "c15 char(20)," +
                        "c16 char(10)," +
                        "c17 char(20))";

            string c6 = "CREATE TABLE Unidades_de_Carga" +
                        "(c12 char(25) ," +
                        "c9 char(42)," +
                        "c10 int," +
                        "c11 char(45) PRIMARY KEY)";

                        

            string c7 = "CREATE TABLE Vehiculos" +
                        "(c7 char(25) ," +
                        "c4 char(45)," +
                        "c5 int," +
                        "c6 char(45) Primary key," +
                        "c8 char(45))";

            string c8 = "CREATE TABLE manifiestos_de_carga" +
                "(llave AUTOINCREMENT PRIMARY KEY," +
                "fecha_creacion DATE," +
                "fecha_modificacion DATE," +
                "numero_manifiesto_pais int," +
                "c2 char(45)," +
                "c3 char(90)," +
                "id_vehiculo char(45) not null constraint FK_vehiculo references Vehiculos (c6)," +
                "id_unidad char(45) not null constraint FK_unidad references Unidades_de_Carga (c11))";

            string c9 = "CREATE TABLE Conductores_en_manifiesto" +
                "(id_conductor char(45) not null constraint FK_conductor references Conductores (c13)," +
                "id_manifiestos int not null constraint FK_manifiestos1 references manifiestos_de_carga (llave)," +
                "tipo_conductor char(9))";

            string c10 = "CREATE TABLE manifiestos_final"+
                "(llave AUTOINCREMENT PRIMARY KEY," +
                "id_manifiesto int not null constraint FK_manfiestos2 references manifiestos_de_carga (llave)," +
                "id_carta_porte int not null constraint FK_cartaporte references cartas_final (llave)," +
                "c23 char(20)," +
                "c24 char(20)," +
                "c25 char(45)," +
                "c26 char(45)," +
                "c27 char(125)," +
                "c28 char(25)," +
                "c29 MEMO," +
                "c30 char(180)," +
                "c31 char(180)," +
                "c32_1 char(180)," +
                "c32_2 char(180)," +
                "c33 char(180)," +
                "c34 char(45)," +
                "c37 char(45)," +
                "c38 char(45)," +
                "c40 char(45))";


            comandos.Add(c1);
            comandos.Add(c2);
            comandos.Add(c3);
            comandos.Add(c4);
            comandos.Add(c5);
            comandos.Add(c6);
            comandos.Add(c7);
            comandos.Add(c8);
            comandos.Add(c9);
            comandos.Add(c10);

            //EJECUTAMOS LOS COMANDOS
            foreach (string comando in comandos)
            {
                OleDbCommand cmd = new OleDbCommand(comando, conexion);
                cmd.ExecuteNonQuery();
            }            

            //CERRAMOS LA CONEXIÓN            
            conexion.Close();
        }

        public void comando( string c)
        {

            OleDbConnectionStringBuilder documentos = new OleDbConnectionStringBuilder();
            documentos.DataSource = "documentos.accdb";
            documentos.Provider = "Microsoft.Jet.OLEDB.4.0";
            OleDbConnection conexion = new OleDbConnection(documentos.ToString());
            conexion.Open();
            OleDbCommand cmd = new OleDbCommand(c, conexion);
            cmd.ExecuteNonQuery();
            cmd.ToString();
            conexion.Close();
        }

        public DataTable Consulta( string c)
        {
            DataTable dt = new DataTable();
            //try
            //{
                
                OleDbDataAdapter da = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                OleDbConnectionStringBuilder documentos = new OleDbConnectionStringBuilder();
                documentos.DataSource = "documentos.accdb";
                documentos.Provider = "Microsoft.Jet.OLEDB.4.0";
                OleDbConnection conexion = new OleDbConnection(documentos.ToString());
                da.SelectCommand = new OleDbCommand(c, conexion);

                da.Fill(ds);

                dt = ds.Tables[0];

                //label5.Text =Convert.ToString(dt.Rows[0].ItemArray[6]);
                //label5.Text = Convert.ToString(dt.Rows.Count);                
            //}
            //catch(System.Data.OleDb.OleDbException)
            //{
            //    MessageBox.Show("Por favor cierre el archivo documentos.accdb", "Advertencia Ingreso de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            return dt;
        }

        public bool Dato_en_consulta(string valor, string consulta)
        {
            bool se_encuentra=false;
            Base nueva = new Base();
            DataTable tabla= nueva.Consulta(consulta);

            for( int i =0;i<tabla.Rows.Count;i++)
            {
                if(valor==nueva.Quitar_espacios(Convert.ToString(tabla.Rows[i].ItemArray[0])))
                {
                    se_encuentra = true;
                    break;
                }
            }
            return se_encuentra;
        }

        public string Quitar_espacios(string dato)
        {
            /*string nuevo = "";
            int count = 1;
            foreach(char i in dato)
            {
                if(!Char.IsWhiteSpace(i))
                {
                    nuevo = dato.Substring(0, count);
                }
                count++;
            }
            return nuevo;*/
            return dato.Trim();
        }
    }
}
