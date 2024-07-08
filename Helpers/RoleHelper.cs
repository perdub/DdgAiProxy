namespace DdgAiProxy;

public enum Role{
    User=0,
    AI = 1
}
public static class RoleHelper{
    public static string GetParam(this Role model){
        switch(model){
            case Role.User:
                return "user";
            case Role.AI:
                return "assistant";
        }
        return string.Empty;
    }
}