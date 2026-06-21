using System.Text.Json.Serialization;

namespace TriagemCurriculos.Domain.DTOs
{
    public class AiCandidateEvaluation
    {
        [JsonPropertyName("skills")]
        public List<string> Skills { get; set; } = new List<string>();

        [JsonPropertyName("matchScore")]
        public int MatchScore { get; set; }

        [JsonPropertyName("justification")]
        public string Justification { get; set; } = string.Empty;
    }
}
