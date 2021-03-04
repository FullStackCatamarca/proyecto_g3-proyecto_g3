using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MisViajes.Models
{
    public class UsuarioModel
    {
        public string Id { get; set; }
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

            SqlCommand com = new SqlCommand("SELECT [Id],[Nombre],[Apellido],[Dni],[Domicilio],[Pais],[Provincia],[Departamento],[FechaNacimiento],[Sexo],[AspNetUser],[AvatarUrl],[Posicion],[Descripcion],[CodigoPostal],[Acerca],[ImgUrl] FROM Usuarios", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                UsuarioModel reg = new UsuarioModel
                {
                    // ToDO: Cambiar ID por Id
                    Id = registros["Id"].ToString(),
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

        public ApplicationUser RecuperarUsuario(string codigo)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("SELECT Id ,Email ,EmailConfirmed ,PasswordHash ,SecurityStamp ,PhoneNumber ,PhoneNumberConfirmed ,TwoFactorEnabled ,LockoutEndDateUtc ,LockoutEnabled ,AccessFailedCount ,UserName ,Nombre ,Apellido ,Dni ,Domicilio ,Pais ,Provincia ,Departamento ,FechaNacimiento ,Sexo ,AvatarUrl ,Posicion ,Descripcion ,CodigoPostal ,Acerca,ImgUrl FROM AspNetUsers where Id=@Id", con);
            comando.Parameters.Add("@Id", SqlDbType.NVarChar);
            comando.Parameters["@Id"].Value = codigo;
            con.Open();
            SqlDataReader registros = comando.ExecuteReader();
            ApplicationUser Usuario = new ApplicationUser();
            if (registros.Read())
            {
                Usuario.Id = registros["ID"].ToString();
                Usuario.Nombre = registros["Nombre"].ToString();
                Usuario.Apellido = registros["Apellido"].ToString();
                Usuario.Dni = registros["Dni"].ToString();
                Usuario.Domicilio = registros["Domicilio"].ToString();
                Usuario.Pais = ToNullableInt(registros["Pais"].ToString());
                Usuario.Provincia = ToNullableInt(registros["Provincia"].ToString());
                Usuario.Departamento = ToNullableInt(registros["Provincia"].ToString());
                Usuario.FechaNacimiento = registros["FechaNacimiento"].ToString();
                Usuario.Sexo = ToNullableInt(registros["Sexo"].ToString());
                Usuario.AvatarUrl = registros["AvatarUrl"].ToString();
                Usuario.Posicion = registros["Posicion"].ToString();
                Usuario.Descripcion = registros["Descripcion"].ToString();
                Usuario.CodigoPostal = registros["CodigoPostal"].ToString();
                Usuario.Acerca = registros["Acerca"].ToString();
                Usuario.ImgUrl = registros["ImgUrl"].ToString();
            }
            con.Close();
            return Usuario;
        }

        public int ModificarPerfil(UsuarioModel Usr)
        {
            Conectar();                                           
            SqlCommand comando = new SqlCommand("update AspNetUsers set Nombre=@Nombre,Apellido=@Apellido,Dni=@Dni ,Domicilio=@Domicilio ,Pais=@Pais ,Provincia=@Provincia ,Departamento=@Departamento ,FechaNacimiento=@FechaNacimiento ,Sexo=@Sexo ,Posicion=@Posicion ,Descripcion=@Descripcion ,CodigoPostal=@CodigoPostal ,Acerca=@Acerca where Id=@Id", con);

            // SqlCommand comando = new SqlCommand("UPDATE AspNetUsers SET Email = @Email, EmailConfirmed = @EmailConfirmed, PasswordHash = @PasswordHash, SecurityStamp = @SecurityStamp, PhoneNumber = @PhoneNumber, PhoneNumberConfirmed = @PhoneNumberConfirmed, TwoFactorEnabled = @TwoFactorEnabled, LockoutEndDateUtc = @LockoutEndDateUtc, LockoutEnabled = @LockoutEnabled, AccessFailedCount = @AccessFailedCount, UserName = @UserName, Nombre = @Nombre, Apellido = @Apellido, Dni = @Dni, Domicilio = @Domicilio, Pais = @Pais, Provincia = @Provincia, Departamento = @Departamento, FechaNacimiento = @FechaNacimiento, Sexo = @Sexo, AvatarUrl = @AvatarUrl, Posicion = @Posicion, Descripcion = @Descripcion, CodigoPostal = @CodigoPostal, Acerca = @Acerca, ImgUrl = @ImgUrl  WHERE (Id = @Id)");
            comando.Parameters.Add("@Id", SqlDbType.NVarChar, 128, "Id");
            comando.Parameters["@Id"].Value = checkDBNull(Usr.Id);
            comando.Parameters.Add("@Nombre", SqlDbType.NText, 0, "Nombre");
            comando.Parameters["@Nombre"].Value = checkDBNull(Usr.Nombre);
            comando.Parameters.Add("@Apellido", SqlDbType.NText, 0, "Apellido");
            comando.Parameters["@Apellido"].Value = checkDBNull(Usr.Apellido); 
            comando.Parameters.Add("@Dni", SqlDbType.NText, 0, "Dni");
            comando.Parameters["@Dni"].Value = checkDBNull(Usr.Dni);
            comando.Parameters.Add("@Domicilio", SqlDbType.NText, 0, "Domicilio");
            comando.Parameters["@Domicilio"].Value = checkDBNull(Usr.Domicilio);
            comando.Parameters.Add("@Pais", SqlDbType.Int, 0, "Pais");
            comando.Parameters["@Pais"].Value = checkDBNull(Usr.Pais);
            comando.Parameters.Add("@Provincia", SqlDbType.Int, 0, "Provincia");
            comando.Parameters["@Provincia"].Value = checkDBNull(Usr.Provincia);
            comando.Parameters.Add("@Departamento", SqlDbType.Int, 0, "Departamento");
            comando.Parameters["@Departamento"].Value = checkDBNull(Usr.Departamento);
            comando.Parameters.Add("@FechaNacimiento", SqlDbType.NText, 0, "FechaNacimiento");
            comando.Parameters["@FechaNacimiento"].Value = checkDBNull(Usr.FechaNacimiento);
            comando.Parameters.Add("@Sexo", SqlDbType.Int, 0, "Sexo");
            comando.Parameters["@Sexo"].Value = checkDBNull(Usr.Sexo);
            comando.Parameters.Add("@Posicion", SqlDbType.NText, 0, "Posicion");
            comando.Parameters["@Posicion"].Value = checkDBNull(Usr.Posicion);
            comando.Parameters.Add("@Descripcion", SqlDbType.NText, 0, "Descripcion");
            comando.Parameters["@Descripcion"].Value = checkDBNull(Usr.Descripcion);
            comando.Parameters.Add("@CodigoPostal", SqlDbType.NText, 0, "CodigoPostal");
            comando.Parameters["@CodigoPostal"].Value = checkDBNull(Usr.CodigoPostal);
            comando.Parameters.Add("@Acerca", SqlDbType.NText, 0, "Acerca");
            comando.Parameters["@Acerca"].Value = checkDBNull(Usr.Acerca);

            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }

        private dynamic checkDBNull(dynamic dato)
        {
            if (dato != null) return dato;
            else return DBNull.Value;
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

    public class UserRoleModel

    {
        public string UserID { get; set; }
        public string Nombre { get; set; }
        public string Roles { get; set; }
    }

}