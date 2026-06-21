using System.Text.Json;
using OpenAI.Chat;
using TriagemCurriculos.Domain.DTOs;
using TriagemCurriculos.Services.Interface;

namespace TriagemCurriculos.Services
{
    public class AiResumeProcessorService : IAiResumeProcessorService
    {
        private readonly ChatClient _chatClient;

        public AiResumeProcessorService(ChatClient chatClient)
        {
            _chatClient = chatClient ?? throw new ArgumentNullException(nameof(chatClient));
        }

        public async Task<AiCandidateEvaluation> EvaluateCandidateAsync(string resumeText, string jobRequirements)
        {
            var systemPrompt = @"Persona: Você é um Tech Recruiter sênior com vasta experiência em mapeamento de competências e triagem técnica de candidatos.
Tarefa: Compare o texto do Currículo fornecido com os Requisitos da Vaga.
Critérios de Avaliação:
1. Hard Skills (Peso 50%): Avalie a correspondência exata ou conceitual das tecnologias exigidas (ex: se a vaga pede .NET e o candidato tem C#, há correspondência).
2. Tempo de Experiência (Peso 30%): Analise o histórico cronológico do candidato. Calcule se o tempo de atuação nas tecnologias exigidas atende ao nível da vaga (Júnior, Pleno, Sênior).
3. Soft Skills & Diferenciais (Peso 20%): Pontue se o candidato possui os diferenciais desejáveis listados na vaga (ex: inglês fluente, metodologias ágeis).
Regra de Output: Você deve responder estritamente um objeto JSON válido, sem textos introdutórios ou conclusivos fora do bloco JSON. O formato deve seguir estritamente o esquema técnico solicitado.";

            var userPrompt = $"Requisitos da Vaga:\n{jobRequirements}\n\nTexto do Currículo:\n{resumeText}";

            var options = new ChatCompletionOptions
            {
                ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                    jsonSchemaFormatName: "candidate_evaluation",
                    jsonSchema: BinaryData.FromString(@"
{
    ""type"": ""object"",
    ""properties"": {
        ""skills"": {
            ""type"": ""array"",
            ""items"": {
                ""type"": ""string""
            }
        },
        ""matchScore"": {
            ""type"": ""integer""
        },
        ""justification"": {
            ""type"": ""string""
        }
    },
    ""required"": [""skills"", ""matchScore"", ""justification""],
    ""additionalProperties"": false
}
"),
                    jsonSchemaIsStrict: true)
            };

            var messages = new List<ChatMessage>
            {
                new SystemChatMessage(systemPrompt),
                new UserChatMessage(userPrompt)
            };

            ChatCompletion completion = await _chatClient.CompleteChatAsync(messages, options);

            var jsonResult = completion.Content[0].Text;
            var evaluation = JsonSerializer.Deserialize<AiCandidateEvaluation>(jsonResult, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return evaluation ?? new AiCandidateEvaluation();
        }
    }
}
