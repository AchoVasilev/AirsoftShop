namespace AirsoftShop.Services.Services.Field;

using AirsoftShop.Common.Models;
using Common;
using Models.Field;

public interface IFieldService : ITransientService
{
    Task<OperationResult<CreatedFieldResultServiceModel>> Create(string dealerId, CreateFieldServiceModel model);

    Task<OperationResult<FieldDetailsServiceModel>> Details(int fieldId);
}