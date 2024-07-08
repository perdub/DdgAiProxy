namespace DdgAiProxy;

public enum Model{
    Gpt3_5_turbo = 0,
    Claude3_Haiku = 1,
    Llama3_70B = 2,
    Mixtral_8x7B = 3
}
public static class ModelIds{
    public static string GetName(this Model model){
        switch(model){
            case Model.Gpt3_5_turbo:
                return "gpt-3.5-turbo-0125";
            case Model.Claude3_Haiku:
                return "claude-3-haiku-20240307";
            case Model.Llama3_70B:
                return "meta-llama/Llama-3-70b-chat-hf";
            case Model.Mixtral_8x7B:
                return "mistralai/Mixtral-8x7B-Instruct-v0.1";
        }
        return string.Empty;
    }
}