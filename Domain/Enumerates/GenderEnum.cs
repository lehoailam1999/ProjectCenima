using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Enumerates
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GenderEnum
    {
        UnKnown = 0,
        Male = 1,
        Female = 2
    }
}
