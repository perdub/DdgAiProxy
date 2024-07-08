#if DEBUG
using System.Net.Http.Headers;
using System.Text;
using DdgAiProxy;

public static class TestRequest{
    public static async Task Test(){
        DialogManager dialogManager = new DialogManager(new CustomClient());
        await dialogManager.Init(Model.Claude3_Haiku);
        Console.WriteLine(await dialogManager.SendMessage("Привет! Я Алиска а как зовут тебя?"));
    }
}
#endif