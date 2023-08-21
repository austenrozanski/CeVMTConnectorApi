using CeVMTConnectorApi.Bll;
using Microsoft.AspNetCore.Mvc;

namespace CeVMTConnectorApi.Presentation;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly ILogger<AdminController> _logger;
    private readonly DataTransferService _dataTransferService;
    
    public AdminController(DataTransferService dataTransferService)
    {
        _dataTransferService = dataTransferService;
    }

    [HttpPost("transfer-data")]
    public async Task<ActionResult<IEnumerable<dynamic>>> TransferData()
    {
        try
        {
            await _dataTransferService.TransferData();
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred in POST admin/transfer-data. Error: {ex}");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<dynamic>>> TransferData(int id)
    {
        try
        {
            var result = await _dataTransferService.ExampleGet(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred in GET admin/{id}. Error: {ex}");
            return StatusCode(500, "Internal server error");
        }
    }
}