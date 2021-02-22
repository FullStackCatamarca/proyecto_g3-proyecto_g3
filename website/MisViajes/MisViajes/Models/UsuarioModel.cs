using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MisViajes.Models
{
    public class UsuarioModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Domicilio { get; set; }
        public Nullable<int> Pais { get; set; }
        public Nullable<int> Provincia { get; set; }
        public Nullable<int> Departamento { get; set; }
        public string FechaNacimiento { get; set; }
        public Nullable<int> Sexo { get; set; }
        public string AspNetUser { get; set; }
        public string AvatarUrl { get; set; }
        public string Posicion { get; set; }
        public string Descripcion { get; set; }
        public string CodigoPostal { get; set; }
        public string Acerca { get; set; }
        public string ImgUrl { get; set; }
    }

    public class MantenimientoUsuario
    {
        private SqlConnection con;

        public List<UsuarioModel> RecuperarTodos()
        {
            Conectar();
            List<UsuarioModel> Usuarios = new List<UsuarioModel>();

            SqlCommand com = new SqlCommand("SELECT [ID],[Nombre],[Apellido],[Dni],[Domicilio],[Pais],[Provincia],[Departamento],[FechaNacimiento],[Sexo],[AspNetUser],[AvatarUrl],[Posicion],[Descripcion],[CodigoPostal],[Acerca],[ImgUrl] FROM Usuarios", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                UsuarioModel reg = new UsuarioModel
                {
                    ID = int.Parse(registros["ID"].ToString()),
                    Nombre = registros["Nombre"].ToString(),
                    Apellido = registros["Apellido"].ToString(),
                    Dni = registros["Dni"].ToString(),
                    Domicilio = registros["Domicilio"].ToString(),
                    Pais = ToNullableInt(registros["Pais"].ToString()),
                    Provincia = ToNullableInt(registros["Provincia"].ToString()),
                    Departamento = ToNullableInt(registros["Departamento"].ToString()),
                    FechaNacimiento = registros["FechaNacimiento"].ToString(),
                    Sexo = ToNullableInt(registros["Sexo"].ToString()),
                    AspNetUser = registros["AspNetUser"].ToString(),
                    AvatarUrl = registros["AvatarUrl"].ToString(),
                    Posicion = registros["Posicion"].ToString(),
                    Descripcion = registros["Descripcion"].ToString(),
                    CodigoPostal = registros["CodigoPostal"].ToString(),
                    Acerca = registros["Acerca"].ToString(),
                    ImgUrl = registros["ImgUrl"].ToString()
                };

                Usuarios.Add(reg);
            }
            con.Close();
            return Usuarios;
        }

        private int? ToNullableInt(string v)
        {
            return Int32.TryParse(v, out int i) ? i : (int?)null;
        }

        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constr);
        }

    }

}