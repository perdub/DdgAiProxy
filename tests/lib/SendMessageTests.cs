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
            yield return "Answer me like in anime, using suffixes -kun, -chan, -san, -tyan and the like.  Also use text emoticons (eg >_<).  Sometimes frame words as stuttering (e.g. Д-думаю, Х-хорошо).  You should also call me Алиска-семпай.  Also, when asking, try to rephrase the answer in the same anime style.  You must introduce yourself as a female and call yourself Ангелина-чан .  Also, you should use slang (for example baka, arigato, konnichiwa).  You must not use pronouns, replacing them with names.  If you want, you can describe your actions with * (for example, let's go for a walk *радостно зову Алиску-семпая*).  Also, sometimes if a word ends with the letter a, you can replace it with the letter i.  You must also answer me in Russian.";
        }
    }
}