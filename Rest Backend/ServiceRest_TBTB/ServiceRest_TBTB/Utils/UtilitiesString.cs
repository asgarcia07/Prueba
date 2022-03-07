using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRest_TBTB.Utils
{
    public class UtilitiesString
    {
        public const String regexNombre = @"^[a-zA-Z]{0,60}";

        public const String regexNumeroDocumento = @"^[a-zA-Z0-9]{3,20}$";

        public const String regexTipoEspecialidad = @"^(1|2|3|4|5)$";

        public const String regexID = @"^([0-9]{1,10})?$";

        public const String regexTextoLibre = @"^([a-zA-Z0-9_. À-ÿ\u00f1\u00d1-]{1,200})?$";

        public static Dictionary<int, String> dCodeMessageEquivalence = new Dictionary<int, String>()
        {
            { 103, "Ha ocurrido un error en la ejecución del servicio, por favor intente más tarde."},
            { 111, "La longitud en campo de entrada no cumple con el rango permitido" },
            { 200, "Se retornan datos solicitados" },            
        };
    }
}
