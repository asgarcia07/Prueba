using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceRest_TBTB.Models.Data;
using ServiceRest_TBTB.Models.Request;
using ServiceRest_TBTB.Models.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRest_TBTB.Database
{
    public class TransaccionBaseDatos
    {
        public static void ConsultarMedicoPorDocumento(String numeroDocumento, ref Response response, IConfiguration _configuration)
        {
            try
            {

                StringBuilder sqlQuery = new StringBuilder();
                sqlQuery.AppendLine("Select top 1 * from Transaccion.Medico Where numeroDocumento = @numeroDocumento order by id");

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@numeroDocumento", numeroDocumento)
                };

                using (Prueba_TBTBContext dbo = new Prueba_TBTBContext(_configuration))
                {
                    var medicoFound = dbo.Medicos.FromSqlRaw(sqlQuery.ToString(), sqlParameters).AsEnumerable().FirstOrDefault();

                    if (medicoFound != null)
                    {
                        response.codigo = 102;
                        response.mensaje = "Medico registrado previamente";
                        response.resultado = "Error";
                    }
                    else
                    {
                        response.codigo = 200;
                    }
                }
            }
            catch (Exception ex)
            {
                response.codigo = 500;
                response.mensaje = "Error al consultar medico " + ex.Message;
                response.resultado = "Error";

            }
        }

        public static ResponseBD RegistrarMedicoBD(RequestRegistrarMedico request, IConfiguration _configuration)
        {
            ResponseBD response = new ResponseBD();
            try
            {
                using (Prueba_TBTBContext dbo = new Prueba_TBTBContext(_configuration))
                {
                    try
                    {
                        using (var dbContextTransaction = dbo.Database.BeginTransaction())
                        {
                            var t = new Medico();

                            t.Nombre = request.nombre;
                            t.Apellido = request.apellido;
                            t.NumeroDocumento = request.numeroDocumento;
                            dbo.Add(t);
                            dbo.SaveChanges();
                            response.idMedico = t.Id;

                            var esp = new MedicoEspecialidad();                            
                            esp.IdMedico = response.idMedico;
                            esp.IdEspecialidad = Int32.TryParse(request.especialidad, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out int especialidad) ? especialidad : 6;
                            dbo.Add(esp);
                            dbo.SaveChanges();

                            dbo.Database.CommitTransaction();
                            ValidarMedicoRegistrado(_configuration, ref response);
                        }

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                
                }

                    return response;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void ValidarMedicoRegistrado(IConfiguration _configuration, ref ResponseBD response)
        {
            try
            {
                using (Prueba_TBTBContext dbo = new Prueba_TBTBContext(_configuration))
                {
                    var responseMedico = dbo.Medicos.FromSqlRaw($"SELECT TOP 1 * FROM Transaccion.Medico WHERE id = @id_medico",
                        new SqlParameter[]
                        {
                            new SqlParameter("@id_medico", response.idMedico),
                        }).FirstOrDefault();


                    if (responseMedico != null)
                    {
                        response.codigo = 200;
                        response.mensaje = "Médico Registrado Satisfactoriamente";
                        response.resultado = "Procesado";
                    }
                    else
                    {
                        response.codigo = 500;
                        response.mensaje = "No se encontró registro";
                        response.resultado = "Error";
                    }
                }
            }
            catch (Exception ex)
            {
                response.codigo = 103;
                response.mensaje = Utils.UtilitiesString.dCodeMessageEquivalence[response.codigo];
                response.resultado = "Error";
            }
        }

        public static void ConsultarMedicoID(String id, ref ResponseConsultar response, IConfiguration _configuration)
        {
            try
            {

                using (SqlConnection conexion = new SqlConnection(_configuration["BaseDeDatos"]))
                {
                    using (SqlCommand cmd = new SqlCommand("Transaccion.sp_get_medico", conexion))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        conexion.Open();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            response.codigo = 200;
                            response.mensaje = "Se retornan datos del medico";
                            response.resultado = "Exitoso";
                            Models.Documents.Medico medico = new Models.Documents.Medico();
                            medico.nombre = dt.Rows[0]["nombre"].ToString();
                            medico.apellido = dt.Rows[0]["apellido"].ToString();
                            medico.numeroDocumento = dt.Rows[0]["numeroDocumento"].ToString();
                            medico.especialidad = dt.Rows[0]["especialidad"].ToString();
                            medico.descripcion = dt.Rows[0]["descripcion"].ToString();
                            response.medico = medico;
                        }
                        else
                        {
                            response.codigo = 102;
                            response.mensaje = "id medico no encontrado";
                            response.resultado = "Error";
                            response.medico = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.codigo = 500;
                response.mensaje = "Error al consultar medico " + ex.Message;
                response.resultado = "Error";

            }
        }
        
        public static void ActualizarMedicoBD(RequestActualizarMedico request, IConfiguration _configuration, ref Response response)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_configuration["BaseDeDatos"]))
                {
                    using (SqlCommand cmd = new SqlCommand("Transaccion.sp_upd_medico", conexion))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@idmedico", Int32.Parse(request.idMedico)));
                        cmd.Parameters.Add(new SqlParameter("@nombre", request.nombre));
                        cmd.Parameters.Add(new SqlParameter("@apellido", request.apellido));
                        cmd.Parameters.Add(new SqlParameter("@numeroDocumento", request.numeroDocumento));
                        cmd.Parameters.Add(new SqlParameter("@especialidad", Int32.Parse(request.especialidad)));
                        cmd.Parameters.Add(new SqlParameter("@descripcion", !String.IsNullOrEmpty(request.descripcion) ? request.descripcion : ""));

                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        conexion.Open();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            response.codigo = 102;
                            response.mensaje = "id medico no encontrado";
                            response.resultado = "Error";
                        }
                        else
                        {
                            response.codigo = 200;
                            response.mensaje = "Médico Actualizado Satisfactoriamente";
                            response.resultado = "Procesado";
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                response.codigo = 103;
                response.mensaje = Utils.UtilitiesString.dCodeMessageEquivalence[response.codigo];
                response.resultado = "Error";
            }
        }

        public static void EliminarMedicoID(String id, ref Response response, IConfiguration _configuration)
        {
            try
            {

                using (SqlConnection conexion = new SqlConnection(_configuration["BaseDeDatos"]))
                {
                    using (SqlCommand cmd = new SqlCommand("Transaccion.sp_del_medico", conexion))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@idmedico", id));
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        conexion.Open();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            response.codigo = 102;
                            response.mensaje = "id medico no encontrado";
                            response.resultado = "Error";
                        }
                        else
                        {
                            response.codigo = 200;
                            response.mensaje = "Registro de Médico Eliminado Satisfactoriamente";
                            response.resultado = "Procesado";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.codigo = 500;
                response.mensaje = "Error al consultar medico " + ex.Message;
                response.resultado = "Error";

            }
        }

        public static void ConsultarListaMedicos(ref ResponseConsultarListaMedicos response, IConfiguration _configuration)
        {
            try
            {

                using (SqlConnection conexion = new SqlConnection(_configuration["BaseDeDatos"]))
                {
                    using (SqlCommand cmd = new SqlCommand("Transaccion.sp_get_listaMedicos", conexion))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        conexion.Open();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            response.codigo = 200;
                            response.mensaje = "Se retornan lista de medicos";
                            response.resultado = "Exitoso";
                            List<Models.Documents.Medico> lmedicos = new List<Models.Documents.Medico>();

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Models.Documents.Medico medico = new Models.Documents.Medico();
                                medico.nombre = dt.Rows[i]["nombre"].ToString();
                                medico.apellido = dt.Rows[i]["apellido"].ToString();
                                medico.numeroDocumento = dt.Rows[i]["numeroDocumento"].ToString();
                                medico.especialidad = dt.Rows[i]["especialidad"].ToString();
                                medico.descripcion = dt.Rows[i]["descripcion"].ToString();
                                lmedicos.Add(medico);
                                
                            }
                            response.medicos = lmedicos;
                        }
                        else
                        {
                            response.codigo = 102;
                            response.mensaje = "No se encontraron registros";
                            response.resultado = "Error";
                            response.medicos = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.codigo = 500;
                response.mensaje = "Error al consultar registros " + ex.Message;
                response.resultado = "Error";

            }
        }
    }
}
