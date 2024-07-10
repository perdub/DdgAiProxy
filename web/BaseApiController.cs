using Microsoft.AspNetCore.Mvc;

[Route("/base/api")]
public class BaseApiController(GlobalDialogManager globalDialogManager) : Controller
{
    [HttpGet]
    [Route("init")]
    public async Task<IActionResult> InitModel( int model = -1, string modelName=""){
        var guid = globalDialogManager.CreateNew();
        HttpContext.Response.Headers.Append("ddg-ai-proxy-guid", guid.ToString());
        if(model == -1 && string.IsNullOrWhiteSpace(modelName)){
            return BadRequest("no model or guid info.");
        }
        var dialog = globalDialogManager.GetManager(guid);
        await dialog.Init((DdgAiProxy.Model)model);
        return Ok("we hope its works\ncheck headers to guid");
    }
    [HttpGet]
    [Route("talk")]
    public async Task<IActionResult> Talk(string message, Guid? guid = null){
        if(guid==null){
            return BadRequest("where you guid???");
        }
        var dialog = globalDialogManager.GetManager(guid??throw new Exception());
        string response = await dialog.SendMessage(message);
        return Ok(response);
    }
}