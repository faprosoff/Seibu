using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using ConexionABD.Models;

namespace GestionDeSeibu.Models
{
    public class Database
    {
        // ABRIR CONEXION DE LA BASE DE DATOS

        public static SqlConnection AbrirConexion()
        {
            SqlConnection conn;
            try
            {
                conn = new SqlConnection
                {
                    ConnectionString = "Server=.\\SQLEXPRESS;Database=Taller4;Integrated Security= true"
                };
                conn.Open();
            }
            catch (Exception e)
            {
                throw e;
            }
            return conn;
        }



        // CERRAR CONEXION DE LA BASE DE DATOS

        public void CerrarConexion(SqlConnection conn)
        {
            conn.Close();
        }

        // BUSCAR ALUMNO POR DNI

        public Alumno BuscarAlumnoPorDNI(int dni)
        {
            Alumno alumno = null;
            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.buscarAlumnoPorDNI", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@dni", dni));
                SqlDataReader reader = micomando.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        alumno = new Alumno
                        {
                            Id = Convert.ToInt32(reader["idAlumno"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = Convert.ToInt32(reader["dni"]),
                            Email = reader["email"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            EsSocio = Convert.ToBoolean(reader["esSocio"]),
                            NroSocio = Convert.ToInt32(reader["nroSocio"])
                        };
                    }
                }
                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return alumno;
        }

        // BUSCAR ALUMNO POR ID

        public Alumno BuscarAlumnoPorId(int id)
        {
            Alumno alumno = null;

            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.buscarAlumnoPorId", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = micomando.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        alumno = new Alumno
                        {
                            Id = Convert.ToInt32(reader["idAlumno"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = Convert.ToInt32(reader["dni"]),
                            Email = reader["email"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            EsSocio = Convert.ToBoolean(reader["esSocio"]),
                            NroSocio = Convert.ToInt32(reader["nroSocio"])
                        };
                    }
                }
                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return alumno;
        }

		public bool EliminarAlumnoDeCurso(Alumno alumno, Curso curso)
		{
			bool eliminado = false;
			try {
				SqlConnection conn = AbrirConexion();
				SqlCommand micomando = new SqlCommand("dbo.EliminarAlumnoDeCurso", conn)
				{
					CommandType = CommandType.StoredProcedure
				};
				micomando.Parameters.Add(new SqlParameter("@idAlumno", alumno.Id));
				micomando.Parameters.Add(new SqlParameter("@idCurso", curso.IdCurso));
				int result = micomando.ExecuteNonQuery();

				if(result > 0)
				{
					eliminado = true;
				}

			}
			catch (Exception e)
			{
				throw e;
			}
			return eliminado;
		}

		// BUSCAR ALUMNO POR NRO SOCIO

		public Alumno BuscarAlumnoPorNroSocio(int NroSocio)
        {
            Alumno alumno = null;
            if (NroSocio == 0)
            {
                return null;
            }
            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.BuscarAlumnoPorNroSocio", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@nroSocio", NroSocio));
                SqlDataReader reader = micomando.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        alumno = new Alumno
                        {
                            Id = Convert.ToInt32(reader["idAlumno"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = Convert.ToInt32(reader["dni"]),
                            Email = reader["email"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            EsSocio = Convert.ToBoolean(reader["esSocio"]),
                            NroSocio = Convert.ToInt32(reader["nroSocio"])
                        };
                    }
                }
                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return alumno;
        }

        // BUSCAR CURSO POR ID

        public Curso BuscarCursoPorId(int id)
        {
            Curso curso = null;

            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.buscarCursoPorId", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = micomando.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        curso = new Curso
                        {
                            IdCurso = Convert.ToInt32(reader["idCurso"]),
                            Nombre = reader["nombre"].ToString(),
                            Horario = (TimeSpan)reader["horario"],
                            PrecioNoSocio = Convert.ToInt32(reader["precioNoSocio"]),
                            PrecioSocio = Convert.ToInt32(reader["precioSocio"]),
                            Dia = reader["dia"].ToString(),
                            CantTopeInscriptos = Convert.ToInt32(reader["cantTopeInscriptos"]),
                            Profesor = BuscarProfesorPorId(Convert.ToInt32(reader["idProfesor"]))
                        };

                    }
                }
                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return curso;
        }

        // BUSCAR PROFESOR POR ID

        public Profesor BuscarProfesorPorId(int idProfesor)
        {
            Profesor profesor = null;
            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.buscarProfesorPorId", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@idProfesor", idProfesor));
                SqlDataReader reader = micomando.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        profesor = new Profesor
                        {
                            Id = Convert.ToInt32(reader["idProfesor"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = Convert.ToInt32(reader["dni"]),
                            Email = reader["email"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            PorcentajePago = Convert.ToDouble(reader["porcentajePago"])
                        };
                    }
                }
                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return profesor;
        }

        // BUSCAR PROFESOR POR DNI

        public Profesor BuscarProfesorPorDNI(int DNIProfesor)
        {
            Profesor profesor = null;
            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.buscarProfesorPorDNI", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@DNIProfesor", DNIProfesor));
                SqlDataReader reader = micomando.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        profesor = new Profesor
                        {
                            Id = Convert.ToInt32(reader["idProfesor"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = Convert.ToInt32(reader["dni"]),
                            Email = reader["email"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            PorcentajePago = Convert.ToDouble(reader["porcentajePago"])
                        };
                    }
                }
                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return profesor;
        }

        // INSERTAR ALUMNO

        public bool InsertarAlumno(Alumno alumno)
        {
            bool insertado = false;

            if (alumno.Telefono == null)
            {
                alumno.Telefono = "";
            }

            if (alumno.Email == null)
            {
                alumno.Email = "";
            }

            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.insertarAlumno", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@nombre", alumno.Nombre));
                micomando.Parameters.Add(new SqlParameter("@apellido", alumno.Apellido));
                micomando.Parameters.Add(new SqlParameter("@direccion", alumno.Direccion));
                micomando.Parameters.Add(new SqlParameter("@dni", alumno.Dni));
                micomando.Parameters.Add(new SqlParameter("@esSocio", alumno.EsSocio));
                micomando.Parameters.Add(new SqlParameter("@nroSocio", alumno.NroSocio));
                micomando.Parameters.Add(new SqlParameter("@email", alumno.Email));
                micomando.Parameters.Add(new SqlParameter("@telefono", alumno.Telefono));

                if (micomando.ExecuteNonQuery() > 0)
                {
                    insertado = true;
                }

                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return insertado;
        }

        // INSERTAR CURSO

        public bool InsertarCurso(Curso curso)
        {
            bool insertado = false;
            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.insertarCurso", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@nombre", curso.Nombre));
                micomando.Parameters.Add(new SqlParameter("@dia", curso.Dia));
                micomando.Parameters.Add(new SqlParameter("@horario", curso.Horario));
                micomando.Parameters.Add(new SqlParameter("@precioSocio", curso.PrecioSocio));
                micomando.Parameters.Add(new SqlParameter("@precioNoSocio", curso.PrecioNoSocio));
                micomando.Parameters.Add(new SqlParameter("@cantTopeInscriptos", curso.CantTopeInscriptos));
                micomando.Parameters.Add(new SqlParameter("@idProfesor", curso.Profesor.Id));

                if (micomando.ExecuteNonQuery() > 0)
                {
                    insertado = true;
                }

                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return insertado;
        }

        // INSERTAR PROFESOR

        public bool InsertarProfesor(Profesor profesor)
        {
            bool insertado = false;

            if (profesor.Telefono == null)
            {
                profesor.Telefono = "";
            }

            if (profesor.Email == null)
            {
                profesor.Email = "";
            }

            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.insertarProfesor", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@nombre", profesor.Nombre));
                micomando.Parameters.Add(new SqlParameter("@apellido", profesor.Apellido));
                micomando.Parameters.Add(new SqlParameter("@direccion", profesor.Direccion));
                micomando.Parameters.Add(new SqlParameter("@dni", profesor.Dni));
                micomando.Parameters.Add(new SqlParameter("@email", profesor.Email));
                micomando.Parameters.Add(new SqlParameter("@telefono", profesor.Telefono));
                micomando.Parameters.Add(new SqlParameter("@porcentajePago", profesor.PorcentajePago));

                if (micomando.ExecuteNonQuery() > 0)
                {
                    insertado = true;
                }

                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return insertado;
        }

        // INSERTAR RELACION CURSO ALUMNO

        public bool InsertarRelacionCursoAlumno(Curso curso, Alumno alumno)
        {
            bool insertado = false;
            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.insertarRelacionCursoAlumno", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@idCurso", curso.IdCurso));
                micomando.Parameters.Add(new SqlParameter("@idAlumno", alumno.Id));

                if (micomando.ExecuteNonQuery() > 0)
                {
                    insertado = true;
                }

                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return insertado;
        }

        // MODIFICAR ALUMNO

        public void ModificarAlumno(Alumno alumno)
        {
            if (alumno.Telefono == null)
            {
                alumno.Telefono = "";
            }

            if (alumno.Email == null)
            {
                alumno.Email = "";
            }

            try
            {

                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.modificarAlumno", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@nombre", alumno.Nombre));
                micomando.Parameters.Add(new SqlParameter("@apellido", alumno.Apellido));
                micomando.Parameters.Add(new SqlParameter("@direccion", alumno.Direccion));
                micomando.Parameters.Add(new SqlParameter("@dni", alumno.Dni));
                micomando.Parameters.Add(new SqlParameter("@email", alumno.Email));
                micomando.Parameters.Add(new SqlParameter("@esSocio", alumno.EsSocio));
                micomando.Parameters.Add(new SqlParameter("@nroSocio", alumno.NroSocio));
                micomando.Parameters.Add(new SqlParameter("@telefono", alumno.Telefono));
                micomando.Parameters.Add(new SqlParameter("@idAlumno", alumno.Id));
                micomando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // MODIFICAR CURSO

        public void ModificarCurso(Curso curso)
        {
            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.modificarCurso", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@idCurso", curso.IdCurso));
                micomando.Parameters.Add(new SqlParameter("@idProfesor", curso.Profesor.Id));
                micomando.Parameters.Add(new SqlParameter("@nombre", curso.Nombre));
                micomando.Parameters.Add(new SqlParameter("@dia", curso.Dia));
                micomando.Parameters.Add(new SqlParameter("@horario", curso.Horario));
                micomando.Parameters.Add(new SqlParameter("@precioSocio", curso.PrecioSocio));
                micomando.Parameters.Add(new SqlParameter("@precioNoSocio", curso.PrecioNoSocio));
                micomando.Parameters.Add(new SqlParameter("@cantTopeInscriptos", curso.CantTopeInscriptos));
                micomando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // MODIFICAR PROFESOR

        public void ModificarProfesor(Profesor profesor)
        {
            if (profesor.Telefono == null)
            {
                profesor.Telefono = "";
            }

            if (profesor.Email == null)
            {
                profesor.Email = "";
            }

            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.modificarProfesor", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@nombre", profesor.Nombre));
                micomando.Parameters.Add(new SqlParameter("@apellido", profesor.Apellido));
                micomando.Parameters.Add(new SqlParameter("@direccion", profesor.Direccion));
                micomando.Parameters.Add(new SqlParameter("@dni", profesor.Dni));
                micomando.Parameters.Add(new SqlParameter("@telefono", profesor.Telefono));
                micomando.Parameters.Add(new SqlParameter("@email", profesor.Email));
                micomando.Parameters.Add(new SqlParameter("@porcentajePago", profesor.PorcentajePago));
                micomando.Parameters.Add(new SqlParameter("@idProfesor", profesor.Id));
                micomando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // OBTENER ALUMNOS DE UN CURSO

        public List<Alumno> ObtenerAlumnosDelCurso(int idCurso)
        {
            List<Alumno> alumnos = new List<Alumno>();
            Alumno alumno;
            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.obtenerAlumnosDelCurso", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@id", idCurso));
                SqlDataReader reader = micomando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        alumno = new Alumno
                        {
                            Id = Convert.ToInt32(reader["idAlumno"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = Convert.ToInt32(reader["dni"]),
                            Email = reader["email"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            EsSocio = Convert.ToBoolean(reader["esSocio"]),
                            NroSocio = Convert.ToInt32(reader["nroSocio"])
                        };
                        alumnos.Add(alumno);
                    }
                }

                CerrarConexion(conn);

            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return alumnos;

        }

        // OBTENER PAGOS DE UN ALUMNO

        public List<Pago> ObtenerPagosDelAlumno(int idAlumno)
        {
            List<Pago> pagos = new List<Pago>();
            Pago pago;
            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.obtenerPagosDelAlumno", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@id", idAlumno));
                SqlDataReader reader = micomando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pago = new Pago
                        {
                            Id = Convert.ToInt32(reader["idAlumno"]),
                            NroFactura = Convert.ToInt32(reader["nroFactura"]),
                            Detalle = reader["detalle"].ToString(),
                            Cobro = Convert.ToDouble(reader["cobro"]),
                            MontoPago = Convert.ToDouble(reader["montoPago"]),
                            Fecha = Convert.ToDateTime(reader["fecha"]),
                            Saldo = Convert.ToDouble(reader["montoPago"])
                        };
                        pagos.Add(pago);
                    }
                }
                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
            return pagos;
        }

        // OBTENER LA CANTIDAD DE ALUMNOS DE UN CURSO

        public int ObtenerCantAlumnosCurso(int idCurso)
        {
            int total_alumnos = 0;
            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.obtenerCantAlumnosCurso", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@id", idCurso));
                total_alumnos = (int)micomando.ExecuteScalar();

                CerrarConexion(conn);

            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return total_alumnos;

        }

        // OBTENER TODOS LOS ALUMNOS

        public List<Alumno> ObtenerTodosLosAlumnos()
        {
            List<Alumno> alumnos = new List<Alumno>();
            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.obtenerTodosLosAlumnos", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader reader = micomando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Alumno alumno = new Alumno
                        {
                            Id = Convert.ToInt32(reader["idAlumno"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = Convert.ToInt32(reader["dni"]),
                            Email = reader["email"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            EsSocio = Convert.ToBoolean(reader["esSocio"]),
                            NroSocio = Convert.ToInt32(reader["nroSocio"])
                        };
                        alumnos.Add(alumno);
                    }
                }
                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return alumnos;
        }

        // OBTENER TODOS LOS PROFESORES

        public List<Profesor> ObtenerTodosLosProfesores()
        {
            List<Profesor> profesores = new List<Profesor>();
            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.obtenerTodosLosProfesores", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader reader = micomando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Profesor profesor = new Profesor
                        {
                            Id = Convert.ToInt32(reader["idProfesor"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = Convert.ToInt32(reader["dni"]),
                            Email = reader["email"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            PorcentajePago = Convert.ToDouble(reader["porcentajePago"])
                        };
                        profesores.Add(profesor);
                    }
                }
                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return profesores;
        }

        // OBTENER TODOS LOS CURSOS

        public List<Curso> ObtenerTodosLosCursos()
        {
            List<Curso> cursos = new List<Curso>();

            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.obtenerTodosLosCursos", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader reader = micomando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Curso curso = new Curso
                        {
                            IdCurso = Convert.ToInt32(reader["idCurso"]),
                            Nombre = reader["nombre"].ToString(),
                            Dia = reader["dia"].ToString(),
                            Horario = (TimeSpan)reader["horario"],
                            PrecioSocio = Convert.ToDouble(reader["precioSocio"].ToString()),
                            PrecioNoSocio = Convert.ToDouble(reader["precioNoSocio"].ToString()),
                            CantTopeInscriptos = Convert.ToInt32(reader["cantTopeInscriptos"]),
                            Profesor = BuscarProfesorPorId(Convert.ToInt32(reader["idProfesor"]))
                        };

                        cursos.Add(curso);
                    }
                }
                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return cursos;
        }

        // INSERTAR PAGO

        public bool InsertarPago(Pago pago)
        {
            bool insertado = false;
            double saldo;

            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.insertarPago", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                saldo = DameSaldoAlumno(pago.Id);
                saldo += (pago.Cobro - pago.MontoPago);

                micomando.Parameters.Add(new SqlParameter("@idAlumno", pago.Id));
                micomando.Parameters.Add(new SqlParameter("@nroFactura", pago.NroFactura));
                micomando.Parameters.Add(new SqlParameter("@detalle", pago.Detalle));
                micomando.Parameters.Add(new SqlParameter("@cobro", pago.Cobro));
                micomando.Parameters.Add(new SqlParameter("@montoPago", pago.MontoPago));
                micomando.Parameters.Add(new SqlParameter("@fecha", pago.Fecha));
                micomando.Parameters.Add(new SqlParameter("@saldo", saldo));

                if (micomando.ExecuteNonQuery() > 0)
                {
                    insertado = true;
                }

                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
            return insertado;
        }

        // DAME SALDO DEL ALUMNO

        public double DameSaldoAlumno(int idAlumno)
        {
            double saldo = 0;

            try
            {
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand("dbo.dameSaldoAlumno", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                micomando.Parameters.Add(new SqlParameter("@id", idAlumno));
                saldo = Convert.ToDouble(micomando.ExecuteScalar());

                CerrarConexion(conn);

            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
            return saldo;
        }

        // BUSCAR ALUMNOS POR NOMBRE O APELLIDO O DNI

        public List<Alumno> BuscarAlumnosPorCriterios(Alumno alumno)
        {
            List<Alumno> alumnos = new List<Alumno>();
            try
            {
                String query = "SELECT nombre,apellido,dni,direccion,telefono,email,esSocio,nroSocio,idAlumno FROM dbo.Alumnos WHERE 1 = 1";
                if (alumno.Nombre != null)
                {
                    query += " AND LOWER(nombre) LIKE '%" + alumno.Nombre.ToLower() + "%'";
                }
                if (alumno.Apellido != null)
                {
                    query += " AND LOWER(apellido) LIKE '%" + alumno.Apellido.ToLower() + "%'";
                }
                if (alumno.Dni != 0)
                {
                    query += " AND dni LIKE '%" + alumno.Dni + "%'";
                }
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand(query, conn)
                {
                    CommandType = CommandType.Text
                };

                Alumno a;
                SqlDataReader reader = micomando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        alumno = new Alumno
                        {
                            Id = Convert.ToInt32(reader["idAlumno"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = Convert.ToInt32(reader["dni"]),
                            Email = reader["email"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            EsSocio = Convert.ToBoolean(reader["esSocio"]),
                            NroSocio = Convert.ToInt32(reader["nroSocio"])
                        };
                        alumnos.Add(alumno);
                    }
                }
                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
            return alumnos;
        }


        // BUSCAR ALUMNOS POR NOMBRE O APELLIDO O DNI

        public List<Curso> BuscarCursosPorCriterios(String nomProf, String nomCurso, TimeSpan horario)
        {
            List<Curso> cursos = new List<Curso>();
            try
            {
                String query = "SELECT C.idCurso, P.idProfesor, C.nombre, C.dia, C.horario, C.precioSocio, C.precioNoSocio, C.cantTopeInscriptos FROM dbo.Cursos AS C INNER JOIN dbo.Profesores AS P ON P.idProfesor = C.idProfesor WHERE 1=1";
                if (nomCurso != "")
                {
                    query += " AND LOWER(C.nombre) LIKE '%" + nomCurso.ToLower() + "%'";
                }
                if (nomProf != "")
                {
                    query += " AND LOWER(P.nombre) LIKE '%" + nomProf.ToLower() + "%'";
                }
                if (horario != TimeSpan.Zero)
                {
                    query += " AND C.horario LIKE '%" + horario + "%'";
                }
                SqlConnection conn = AbrirConexion();
                SqlCommand micomando = new SqlCommand(query, conn)
                {
                    CommandType = CommandType.Text
                };

                Curso c;
                SqlDataReader reader = micomando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        c = new Curso
                        {
                            IdCurso = Convert.ToInt32(reader["idCurso"]),
                            Nombre = reader["nombre"].ToString(),
                            Dia = reader["dia"].ToString(),
                            Horario = (TimeSpan)reader["horario"],
                            PrecioSocio = Convert.ToDouble(reader["precioSocio"].ToString()),
                            PrecioNoSocio = Convert.ToDouble(reader["precioNoSocio"].ToString()),
                            CantTopeInscriptos = Convert.ToInt32(reader["cantTopeInscriptos"]),
                            Profesor = BuscarProfesorPorId(Convert.ToInt32(reader["idProfesor"]))
                        };
                        cursos.Add(c);
                    }
                }
                CerrarConexion(conn);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
            return cursos;
        }



    }
}