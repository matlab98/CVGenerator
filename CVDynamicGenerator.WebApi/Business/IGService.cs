using CVDynamicGenerator.WebApi.Entities.DTO;

namespace CVDynamicGenerator.WebApi.Business.ApplicationService
{
    public interface IGService
    {
        Task<DefaultResponse> CVEnGenerator(DefaultRequest request);
        Task<DefaultResponse> CVEsGenerator(DefaultRequest request);
    }
}
