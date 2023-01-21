using CoolParking.BL.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Net;

namespace CoolParking.WebAPI.Extentions
{
    public static class ControllerExtension
    {
        public static ActionResult HandlerResponse<T>(this ControllerBase controllerBase, ResponseHendler<T> response) =>
            response.StatusCode switch
            {
                HttpStatusCode.OK => controllerBase.StatusCode(200, response.Data),
                HttpStatusCode.NoContent => controllerBase.NoContent(),
                HttpStatusCode.NotFound => controllerBase.StatusCode(404, response.Error),
                HttpStatusCode.BadRequest => controllerBase.StatusCode(400, response.Error),
                HttpStatusCode.Created => controllerBase.StatusCode(201, response.Data),
                _ => controllerBase.StatusCode(500, "Internal server error")
            };
    }
}
