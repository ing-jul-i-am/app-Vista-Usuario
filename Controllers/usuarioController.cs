using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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
                url = $"http://localhost/API-USUARIO/rest/api/listarUsuarios";
            } else
            {
                url = $"http://localhost/API-USUARIO/rest/api/listarUsuariosPorID?idUsuario=" + idUsuario;
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
    }
}