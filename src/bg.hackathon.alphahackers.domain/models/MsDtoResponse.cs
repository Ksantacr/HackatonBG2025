
using System.Net;

namespace bg.hackathon.alphahackers.domain.models
{
    public class MsDtoResponse<T>
    {
        public int code { get; set; }
        public string traceId { get; set; }
        public T data { get; set; } 

        // Constructor
        public MsDtoResponse(string TraceId, T Data)
        {
            this.code = (int) HttpStatusCode.OK;
            this.traceId = TraceId;
            this.data = Data;
        }
    }
}

