using System.Diagnostics;
using DdgAiProxy;
using Xunit.Abstractions;
using System.Threading.Tasks;
using Xunit;
namespace lib
{

    public class Llama3_70B_Tests
    {
        private ITestOutputHelper helper;
        public Llama3_70B_Tests(ITestOutputHelper helper)
        {
            this.helper = helper;
        }
        [Theory]
        [InlineData("Привет, кто ты? Я Алиска, а ты gpt3, да?")]
        public async Task Llama3(string prompt)
        {
            DialogManager manager = new DialogManager(new CustomClient());
            await manager.Init(Model.Llama3_70B);
            string response = await manager.SendMessage(prompt);

            helper.WriteLine($"Model output: {response}");
        }
    }
}