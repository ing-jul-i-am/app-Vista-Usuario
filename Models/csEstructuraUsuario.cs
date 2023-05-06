using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace app_Vista_Usuario.Models
{
    public class csEstructuraUsuario
    {
        public class requestUsuario
        {
            public int idUsuario { get; set; }
            public string nombre { get; set; }
            public string contrasena { get; set; }
        }

        public class responseUsuario
        {
            public int respuesta { get; set; }
            public string descriptionRespuest { get; set; }
        }

        public class requestEliminarUsuario
        {
            public int idUsuario { get; set; }
        }
    }
}