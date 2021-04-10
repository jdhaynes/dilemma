using Microsoft.AspNetCore.Mvc;
using DilemmaSvc.Application.Queries.GetDilemma.DTOs;

namespace DilemmaSvc.RestApi.Controllers
{
    [Route("api/[controller]")]
    public class DilemmaController : Controller
    {
        [HttpGet("{id}")]
        public Application.Queries.GetDilemma.DTOs.Dilemma Get(string id)
        {
            return View();
        }
    }
}