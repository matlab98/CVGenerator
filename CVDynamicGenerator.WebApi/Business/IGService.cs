﻿using CVDynamicGenerator.WebApi.Entities.DTO;

namespace CVDynamicGenerator.WebApi.Business.ApplicationService
{
    public interface IGService
    {
        Task<DefaultResponse> CVGenerator(DefaultRequest request);
    }
}
