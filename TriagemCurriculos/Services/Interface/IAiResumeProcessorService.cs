using TriagemCurriculos.Domain.DTOs;

namespace TriagemCurriculos.Services.Interface
{
    public interface IAiResumeProcessorService
    {
        Task<AiCandidateEvaluation> EvaluateCandidateAsync(string resumeText, string jobRequirements);
    }
}
