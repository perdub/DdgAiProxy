using System.Diagnostics;
using DdgAiProxy;
using Xunit.Abstractions;

namespace lib;

public class Mixtral_8x7BTests(ITestOutputHelper helper)
{
    [Theory]
    [InlineData("Привет, кто ты? Я Алиска, а ты gpt3, да?")]
    public async Task Mixtral_8x7B(string prompt)
    {
        DialogManager manager = new DialogManager(new CustomClient());
        await manager.Init(Model.Mixtral_8x7B);
        string response = await manager.SendMessage(prompt);

        helper.WriteLine($"Model output: {response}");
    }
}