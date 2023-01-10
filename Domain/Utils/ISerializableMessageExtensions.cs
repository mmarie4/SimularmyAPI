using Domain.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Domain.Utils;
public static class ISerializableMessageExtensions
{
    public static string ToMessage(this ISerializableMessage message)
        => JsonConvert.SerializeObject(message, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() } });
}
