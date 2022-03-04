using Microsoft.AspNetCore.Mvc;
using ResturantScheduling.Application.Commands;
using ResturantScheduling.Application.Queries;

namespace ResturantScheduling.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ResturantSchedulingController : ControllerBase
    {
        private  IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

       

        
        [Produces("application/json")]
        [HttpPost, Route("GetOpenHours")]
        public async Task<ActionResult<bool>> GetOpenHours([FromBody] OpenCloseRequest Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   // _logger.LogInformation("Model is Valid");
                   // var Querry = new GetOpenningAndClosingHoursQuerry(Model);
                    var result = await Mediator.Send(Model);

                    if (result == null)
                    {
                        return new OkObjectResult(result);
                    }

                    return BadRequest();

                }
                return BadRequest(ModelState);

            }
            catch
            {
                return BadRequest();

            }
        }
    }
}