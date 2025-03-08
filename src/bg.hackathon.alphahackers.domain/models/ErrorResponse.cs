using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bg.hackathon.alphahackers.domain.models
{
    public class ErrorResponse
    {
        /// <summary>
        /// Código de estado HTTP
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Identificador de la traza
        /// </summary>
        public string TraceId { get; set; }

        /// <summary>
        /// Mensaje general del error
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Lista de errores detallados
        /// </summary>
        public List<ErrorDetail> Errors { get; set; } 

        public ErrorResponse()
        {
            Errors = new List<ErrorDetail>();
        }
    }

}
