using System;

namespace DdgAiProxy
{

    public enum Model
    {
        Gpt3_5_turbo = 0,
        Claude3_Haiku = 1,
        Llama3_70B = 2,
        Mixtral_8x7B = 3,
        Gpt4oMini = 4,
        Llama3_1_70B = 5
    }
    public static class ModelIds
    {
        public static string GetName(this Model model)
        {
            switch (model)
            {
                case Model.Gpt3_5_turbo:
                    return "gpt-3.5-turbo-0125";
                case Model.Claude3_Haiku:
                    return "claude-3-haiku-20240307";
                case Model.Llama3_70B:
                    return "meta-llama/Llama-3-70b-chat-hf";
                case Model.Mixtral_8x7B:
                    return "mistralai/Mixtral-8x7B-Instruct-v0.1";
                case Model.Gpt4oMini:
                    return "gpt-4o-mini";
                case Model.Llama3_1_70B:
                    return "meta-llama/Meta-Llama-3.1-70B-Instruct-Turbo";
            }
            return string.Empty;
        }
        public static Model GetModel(this string str)
        {
            switch (str)
            {
                case "gpt-3.5-turbo-0125":
                    return Model.Gpt3_5_turbo;
                case "claude-3-haiku-20240307":
                    return Model.Claude3_Haiku;
                case "meta-llama/Llama-3-70b-chat-hf":
                    return Model.Llama3_70B;
                case "mistralai/Mixtral-8x7B-Instruct-v0.1":
                    return Model.Mixtral_8x7B;
                case "gpt-4o-mini":
                    return Model.Gpt4oMini; 
                case "meta-llama/Meta-Llama-3.1-70B-Instruct-Turbo":
                    return Model.Llama3_1_70B;
                default:
                    throw new Exception("fall to GetModel");
            }
        }
    }
}