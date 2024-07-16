using System.Diagnostics;
using DdgAiProxy;
using Xunit.Abstractions;
using System.Threading.Tasks;
using Xunit;

namespace lib
{

    public class Gpt35Tests
    {
        private ITestOutputHelper helper;
        public Gpt35Tests(ITestOutputHelper helper)
        {
            this.helper = helper;
        }
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
}