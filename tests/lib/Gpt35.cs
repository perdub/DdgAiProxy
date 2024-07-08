using System.Diagnostics;
using DdgAiProxy;
using Xunit.Abstractions;

namespace lib;

public class Gpt35Tests(ITestOutputHelper helper)
{
    [Theory]
    [InlineData("Привет, кто ты? Я Алиска, а ты gpt3, да?")]
    public async Task Gpt35(string prompt)
    {
        DialogManager manager = new DialogManager(new CustomClient());
        await manager.Init(Model.Gpt3_5_turbo);
        string response = await manager.SendMessage(prompt);

        helper.WriteLine($"Model output: {response}");
    }
}