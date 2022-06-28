using Nest;
using Newtonsoft.Json;

namespace SearchPokemons
{
    [ElasticsearchType(RelationName = "Pokemon")]
    public class Pokemon
    {
        [JsonProperty("#")]
        [Number(NumberType.Integer, Name = "#")]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }

        public int Total { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int SpAtk { get; set; }

        public int SpDef { get; set; }
        public int Defense { get; set; }

        public int Generation { get; set; }

        public string Legendary { get; set; }
    }



}
