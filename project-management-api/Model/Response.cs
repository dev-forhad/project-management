
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_api.Model
{
    public class Response
    {
        public string Status { get; set; }
        public  HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }

    public enum HttpStatusCode
    {
        Accepted = 202,
        BadRequest= 400,
        Created = 201,
        OK = 200,
        NotFound = 404,
        InternalServerErrorException = 500,
        Conflict = 409
    }
}
