using backend.factory.DTOs;
using backend.factory.ServiceManager;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DataController : ControllerBase
{
    private readonly DataBLL _dataBLL;

    public DataController(DataBLL dataBLL)
    {
        _dataBLL = dataBLL;
    }


    [HttpGet("get-data")]
    public async Task<IActionResult> GetData()
    {
            var result = await _dataBLL.GetDataAsync();
            return Ok(result);
    }

    [HttpPost("add-data")]
    public async Task<IActionResult> AddData([FromBody] DataModelDto data)
    {
            var result = await _dataBLL.AddDataAsync(data);
            return Ok(result);
    }

    [HttpPut("update-data/{id}")]
    public async Task<IActionResult> UpdateData(int id, [FromBody] DataModelDto data)
    {
        
            var result = await _dataBLL.UpdateDataAsync(id, data);
            return Ok(result);

    }

    [HttpDelete("delete-data/{id}")]
    public async Task<IActionResult> DeleteData(int id)
    {
            var result = await _dataBLL.DeleteDataAsync(id);
            return Ok(result);
    }
}