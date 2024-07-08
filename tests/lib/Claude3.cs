using System.Diagnostics;
using DdgAiProxy;
using Xunit.Abstractions;

namespace lib;

public class Claude3Tests(ITestOutputHelper helper)
{
    [Theory]
    [InlineData("Привет, кто ты? Я Алиска, а ты gpt3, да?")]
    public async Task Claude3(string prompt)
    {
        DialogManager manager = new DialogManager(new CustomClient());
        await manager.Init(Model.Claude3_Haiku);
        string response = await manager.SendMessage(prompt);

        helper.WriteLine($"Model output: {response}");
    }
}