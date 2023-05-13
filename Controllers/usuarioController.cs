using app_Vista_Usuario.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using static app_Vista_Usuario.Models.csEstructuraUsuario;

namespace app_Vista_Usuario.Controllers
{
    public class usuarioController : Controller
    {
        // GET: usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuario(string idUsuario)
        {
            DataSet dsi = new DataSet();
            var url = "";

            //La direccion de nuestra API
            if (idUsuario==null)
            {
                url = $"http://JULIAN/API-USUARIO/rest/api/listarUsuarios";
            } else
            {
                url = $"http://JULIAN/API-USUARIO/rest/api/listarUsuariosPorID?idUsuario=" + idUsuario;
            }
            

            //La variable que guarda la peticion al API
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            //Aqui se pedirian los permisos y autenticacion con la API
            request.Accept = "application/json";

            string responseBody;

            //Aqui se ejecuta la peticion a la API que nos devuelve un json
            //inicialmente lo guardamos en el string 'responseBody'
            //y por ultimo lo convertimos en un dataset 'dsi' ya que tiene la estructura json
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        using(StreamReader objReader = new StreamReader(strReader))
                        {
                            responseBody = objReader.ReadToEnd();
                        }
                    }
                    dsi = JsonConvert.DeserializeObject<DataSet>(responseBody);
                }
            }
            catch (Exception ex)
            {
                
            }

            return View(dsi);
        }

        public ActionResult newUsuario()
        {
            return View();
        }

        public ActionResult Guardar(FormCollection formCollection)
        {
            string json, resultJson;
            Byte[] reqString, resByte;
            requestUsuario insertar = new requestUsuario();
            insertar.idUsuario = Convert.ToInt32(formCollection["idUsuario"]);
            insertar.nombre = formCollection["nombre"];
            insertar.contrasena = formCollection["contrasena"];

            json = JsonConvert.SerializeObject(insertar);

            WebClient webClient = new WebClient();
            string url = $"http://JULIAN/API-USUARIO/rest/api/insertarUsuario";
            var request = (HttpWebRequest)WebRequest.Create(url);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            webClient.Headers["content-type"] = "application/json";
            reqString = Encoding.UTF8.GetBytes(json);
            resByte = webClient.UploadData(request.Address.ToString(), "post", reqString);
            resultJson = Encoding.UTF8.GetString(resByte);

            responseUsuario result = new responseUsuario();
            result = JsonConvert.DeserializeObject<responseUsuario>(resultJson);
            webClient.Dispose();

            if (result.respuesta == 1)
            {
                return RedirectToAction("Usuario", "Usuario");
            }
            return RedirectToAction("newUsuario", "Usuario");
        }

        public ActionResult ActualizarUsuario(string idUsuario)
        {
            DataSet dsi = new DataSet();
            var url = "";

            //La direccion de nuestra API
            if (idUsuario == null)
            {
                url = $"http://JULIAN/API-USUARIO/rest/api/listarUsuarios";
            }
            else
            {
                url = $"http://JULIAN/API-USUARIO/rest/api/listarUsuariosPorID?idUsuario=" + idUsuario;
            }


            //La variable que guarda la peticion al API
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            //Aqui se pedirian los permisos y autenticacion con la API
            request.Accept = "application/json";

            string responseBody;

            //Aqui se ejecuta la peticion a la API que nos devuelve un json
            //inicialmente lo guardamos en el string 'responseBody'
            //y por ultimo lo convertimos en un dataset 'dsi' ya que tiene la estructura json
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            responseBody = objReader.ReadToEnd();
                        }
                    }
                    dsi = JsonConvert.DeserializeObject<DataSet>(responseBody);
                }
            }
            catch (Exception ex)
            {

            }

            return View(dsi);
        }

        public ActionResult Actualizar(FormCollection formCollection)
        {
            string json, resultJson;
            Byte[] reqString, resByte;
            requestUsuario insertar = new requestUsuario();
            insertar.idUsuario = Convert.ToInt32(formCollection["idUsuario"]);
            insertar.nombre = formCollection["nombre"];
            insertar.contrasena = formCollection["contrasena"];

            json = JsonConvert.SerializeObject(insertar);

            WebClient webClient = new WebClient();
            string url = $"http://JULIAN/API-USUARIO/rest/api/actualizarUsuario";
            var request = (HttpWebRequest)WebRequest.Create(url);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            webClient.Headers["content-type"] = "application/json";
            reqString = Encoding.UTF8.GetBytes(json);
            resByte = webClient.UploadData(request.Address.ToString(), "post", reqString);
            resultJson = Encoding.UTF8.GetString(resByte);

            responseUsuario result = new responseUsuario();
            result = JsonConvert.DeserializeObject<responseUsuario>(resultJson);
            webClient.Dispose();

            if (result.respuesta == 1)
            {
                return RedirectToAction("Usuario", "Usuario");
            }
            return RedirectToAction("ActualizarUsuario", "Usuario");
        }

        public ActionResult EliminarUsuario(string idUsuario)
        {
            string json, resultJson;
            Byte[] reqString, resByte;
            
            WebClient webClient = new WebClient();
            string url = $"http://JULIAN/API-USUARIO/rest/api/eliminarUsuario";
            var request = (HttpWebRequest)WebRequest.Create(url);

            requestEliminarUsuario eliminar = new requestEliminarUsuario();
            eliminar.idUsuario = Convert.ToInt32(idUsuario);

            json = JsonConvert.SerializeObject(eliminar);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            webClient.Headers["content-type"] = "application/json";

            reqString = Encoding.UTF8.GetBytes(json);
            resByte = webClient.UploadData(request.Address.ToString(), "post", reqString);
            resultJson = Encoding.UTF8.GetString(resByte);

            responseUsuario result = new responseUsuario();
            result = JsonConvert.DeserializeObject<responseUsuario>(resultJson);
            webClient.Dispose();

            return RedirectToAction("Usuario", "Usuario");
                        
        }

        //Este es el codigo para generar el reporte y enviarlo al usuario como PDF.
        public ActionResult Reporte()
        {
            //Se llama al metodo Usuario de esta misma clase
            //Este metodo devuelve un string con todo lo que manda el API
            DataTable dsiTable = new DataTable();
            DataSet dsi = GetListaDeUsuarios();
            dsiTable = dsi.Tables[0];

            //Creamos el objeto de la clase reporte
            ReportClass reporte = new ReportClass();

            //Le decimos que reporte ser:
            reporte.FileName = Server.MapPath("/Reports/rptListarUsuarios.rpt");
            reporte.Load();

            //Le decimos con que datos llenar el reporte
            reporte.SetDataSource(dsiTable);

            //Creamos el PDF
            Stream stream = reporte.ExportToStream(ExportFormatType.PortableDocFormat);

            //Se cierra el reporte
            reporte.Dispose();
            reporte.Close();

            //retornamos el PDF
            return new FileStreamResult(stream, "application/pdf");
        }

        //Este es el codigo para hacer la peticion a la API de los usuarios
        //y guardarlos en un data set
        public DataSet GetListaDeUsuarios()
        {
            DataSet dsi=new DataSet();
            string url;

            url = $"http://JULIAN/API-USUARIO/rest/api/listarUsuarios";

            

            //La variable que guarda la peticion al API
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            //Aqui se pedirian los permisos y autenticacion con la API
            request.Accept = "application/json";

            string responseBody=null;

            //Aqui se ejecuta la peticion a la API que nos devuelve un json
            //inicialmente lo guardamos en el string 'responseBody'
            //y por ultimo lo convertimos en un dataset 'dsi' ya que tiene la estructura json
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    
                    using (Stream strReader = response.GetResponseStream())
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            responseBody = objReader.ReadToEnd();
                        }
                    }
                    dsi = JsonConvert.DeserializeObject<DataSet>(responseBody);
                }
            }
            catch (Exception ex)
            {

            }

            return dsi;
        }
    }
}