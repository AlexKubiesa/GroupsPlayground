using System;
using GroupsPlayground.Persistence.Framework;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GroupsPlayground.Persistence.Groups.Model
{
    [JsonObject]
    [JsonConverter(typeof(PermutationToStringConverter))]
    public class Permutation
    {
        public Permutation(string expression)
        {
            Expression = expression;
        }

        public string Expression { get; private set; }
    }
}