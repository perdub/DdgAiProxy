using Microsoft.AspNetCore.Mvc;

[Route("/base/api")]
public class BaseApiController(GlobalDialogManager globalDialogManager) : Controller
{
    [HttpGet]
    [Route("new")]
    public async Task<IActionResult> NewDialog()
    {
        var guid = globalDialogManager.CreateNew();
        HttpContext.Response.Headers.Append("ddg-ai-proxy-guid", guid.ToString());
        return Ok(guid);
    }
    [HttpGet]
    [Route("init")]
    public async Task<IActionResult> InitModel(Guid? guid = null, int model = -1, string modelName=""){
        if(guid == null || (model == -1 && string.IsNullOrWhiteSpace(modelName))){
            return BadRequest("no model or guid info.");
        }
        var dialog = globalDialogManager.GetManager(guid??throw new Exception());
        await dialog.Init((DdgAiProxy.Model)model);
        return Ok("we hope its works");
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