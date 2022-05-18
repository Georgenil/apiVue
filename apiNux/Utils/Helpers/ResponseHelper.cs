using apiNux.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SeguroViagemApi.Application.Utils
{
    public class ResponseHelper : ControllerBase
    {

        public IActionResult CreateResponse(ResponseModel response) 
        {
            return response.StatusCode switch
            {
                200 => Ok(response),
                500 => StatusCode(500, response),
                422 => UnprocessableEntity(response),
                409 => Conflict(response),
                401 => Unauthorized(response),
                404 => NotFound(response),
                _ => throw new NotImplementedException(),
            };
        }
    }
}