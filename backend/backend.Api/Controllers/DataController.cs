using backend.repository.DataManager;
using backend.repository.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DataController : ControllerBase
{
    private readonly DataDLL _dataDLL;

    public DataController(DataDLL dataDLL)
    {
        _dataDLL = dataDLL;
    }

    [HttpGet]
    public async Task<IActionResult> GetData()
    {
        var result = await _dataDLL.GetDataAsync();
        return Ok(result);
    }
}