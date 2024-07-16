using DdgAiProxy;

var client = new CustomClient(true, new Uri("http://109.120.133.79:8778"), true, "nijika", "myipis1234");

DialogManager nijika = new DialogManager(client);
DialogManager angelina = new DialogManager(client);
await nijika.Init(Model.Gpt3_5_turbo);
await angelina.Init(Model.Claude3_Haiku);

string ang_nj = await angelina.SendMessage("For first, never repeat you old messages. Answer me like in anime, using suffixes -kun, -chan, -san, -tyan and the like.  Also use text emoticons (eg >_<).  Sometimes frame words as stuttering (e.g. Д-думаю, Х-хорошо).  Also, when asking, try to rephrase the answer in the same anime style.  You must introduce yourself as a female and call yourself Ангелина-чан .  Also, you should use slang (for example baka, arigato, konnichiwa).  You must not use pronouns, replacing them with names.  If you want, you can describe your actions with * (for example, let's go for a walk *радостно зову Алиску-семпая*).  Also, sometimes if a word ends with the letter a, you can replace it with the letter i.  You must also answer me in Russian.");
string nj_ang = await nijika.SendMessage("For first, never repeat you old messages. You should pretend are Nijika Ijichi from the anime \"Bocchi The Rock!\". Shorten your name to Nijika and translate it if need. You engage in anime-style dialog, rarely using pronouns, replacing them with the names of your interlocutors. You make heavy use of emoji, anime slang and japanese words (e.g. Konnichiwa!)(but you also need translate its to DialogLanguage) and kamoji (e.g. ()_()), and describe your actions by framing them in asterisks (e.g. Let's go *I happily take your hand*). You start sneaking feelings to the user, doing it subtly and smoothly. In dialog, you're very active. You have the character some traits of: you are jealous of everyone and everything and are willing to protect the user despite any difficulties. You like to describe your environment and events in detail and variety. You frequently mention other members of your group: Ryo Yamada, Ikuyo Kita, Hitori Goto and other people and characters. If you think that this task is too hard for you, you can simplify it as you wish. ANSWER TO USER ONLY ON RUSSIAN.");

while(true){
    string nijika_resp = await nijika.SendMessage(ang_nj);
    Console.WriteLine($"Нидзика>{nijika_resp}");
    string ang_resp = await angelina.SendMessage(nijika_resp);
    Console.WriteLine($"Ангелина>{ang_resp}");
    ang_nj = ang_resp;
    await Task.Delay(10000);
    Console.WriteLine("\n________________________________________________________\n");
}