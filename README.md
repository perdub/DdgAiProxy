# DdgAiProxy

DdgAiProxy is a libary to work with Duckduckgo AI and make request with it. Also, repo contains web server to work with libary.

## As Libary
![NuGet Version](https://img.shields.io/nuget/v/DdgAi?color=blue)  

### Adding
You can add nuget package to you project with
```
dotnet add package DdgAi
```
or go to [nuget package page](https://www.nuget.org/packages/DdgAi)

### Usage
Base conservation example:
```csharp
DialogManager dialogManager = new DialogManager(new CustomClient());
await dialogManager.Init(Model.Gpt3_5_turbo);
string response = await dialogManager.SendMessage("Hello! How are you? And who are you?");
```
Also, you can find this project at [examples folder](https://github.com/perdub/DdgAiProxy/tree/main/examples/Base).    
So, what happends here? For first, we create a ```DialogManager``` and ```CustomClient``` instancs. ```CustomClient``` is a derivative from ```HttpClient``` class, which helps us to make requests to DuckDuckGo Ai servers. With base usage like this you can don`t care about it, but if you app means usages of 2 or more ```DialogManager```, ```CustomClient``` maybe and should be a singeltone.

After this, with ```CustomClient``` instance, we can create our ```DialogManager``` to send request to LLM. After creating, we should init dialog with ```Init()``` method. As params, it`s takes ```Model``` enum with model, which we want to use.

Now we are ready to send and response: call ```SendMessages(string text)``` and pass prompt to LLM, function will return LLM response(if all staff are not broken in this moment). 

## As Server

Not everyone familiar with C#(or other .Net languages), so, if you want, you can use it as web server with http api. 