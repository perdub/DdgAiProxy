using System.Diagnostics;
using DdgAiProxy;
using Xunit.Abstractions;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
namespace lib
{

    public class SendMessageTests
    {
        private ITestOutputHelper helper;
        public SendMessageTests(ITestOutputHelper helper)
        {
            this.helper = helper;
        }
        [Theory]
        [MemberData(nameof(getParams))]
        public async Task SendMessage(Model model, string prompt)
        {
            DialogManager manager = new DialogManager(new CustomClient());
            await manager.Init(model);
            string response = await manager.SendMessage(prompt);

            helper.WriteLine($"Model output: {response}");
        }
        public static IEnumerable<object[]> getParams(){
            for(int i = 0; i<4;i++){
                foreach(var prompt in getPrompts()){
                    yield return new object[]{(Model)i, prompt};
                }
            }
        }
        public static IEnumerable<string> getPrompts(){
            yield return "Привет! Я Алиска, а тебя как зовут?";
            yield return "Сложи 23 и 45";
        }
    }
}