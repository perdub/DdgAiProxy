# DdgAiProxy

DdgAiProxy is a libary to work with Duckduckgo AI and make request with it. Also, repo contains web server to work with libary.

## As Server

Not everyone familiar with C#(or other .Net languages), so, if you want, you can use it as web server with http api.

> [!WARNING]  
> Web Api now are in early stage of development and I not reccomend to use it in production, especially if you plan to use a lot of dialogs

### Docker
![Docker Image Size](https://img.shields.io/docker/image-size/perdub/ddg-ai-proxy) ![Docker Image Version](https://img.shields.io/docker/v/perdub/ddg-ai-proxy)


Faster way to run it - run in [docker container](https://hub.docker.com/r/perdub/ddg-ai-proxy) with
```
docker run -p 14532:5000 -d perdub/ddg-ai-proxy:latest
```
and after this you can make requests to api.

### Binarys Files

If you can\`t or don\`t want to use Docker, you can run it as standalone application. Compile it by yourself or use [autobuilded binarys from release page](https://github.com/perdub/DdgAiProxy/releases/latest).


## As Libary
![NuGet Version](https://img.shields.io/nuget/v/DdgAi?color=blue)  

### Adding
You can add nuget package to you project with
```
dotnet add package DdgAi
```
or go to [nuget package page](https://www.nuget.org/packages/DdgAi).

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

