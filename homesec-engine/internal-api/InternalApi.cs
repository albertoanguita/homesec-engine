using Microsoft.AspNetCore.Mvc;

namespace TestWebAPI1.internal_api;

[Route("internal/event")]
[ApiController]
public class InternalApi : ControllerBase
{
    //[HttpGet("{id}")]
    [HttpGet()]
    public ActionResult<string> Get()
    {
        return "hello";
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto>> Post([FromBody] EventDto eventDto)
    {
        System.Console.WriteLine(eventDto);
        return Ok(new ResponseDto()
        {
            timestamp2 = 3456, event_type2 = "fuck!"
        });
    }
}