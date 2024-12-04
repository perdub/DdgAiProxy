﻿using DdgAiProxy;
DialogManager dialogManager = new DialogManager(new CustomClient());
await dialogManager.Init(Model.Gpt4oMini);
string response = await dialogManager.SendMessage("Hello! How are you? And who are you?");
Console.WriteLine(response);
