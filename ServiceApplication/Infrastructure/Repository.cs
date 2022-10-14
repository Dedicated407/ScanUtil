using ScanUtil.Models;
using ServiceApplication.Infrastructure.Interfaces;

namespace ServiceApplication.Infrastructure;

public class Repository : IRepository
{
    private readonly IList<ScanResponseModel> _responseModels;
    
    public Repository(IList<ScanResponseModel> responseModels)
    {
        _responseModels = responseModels;
    }

    public Task Save(ScanResponseModel model)
    {
        _responseModels.Add(model);
        
        return Task.CompletedTask;
    }

    public Task<ScanResponseModel> Find(Guid id)
    {
        return Task.FromResult(_responseModels.First(model => model.Id == id));
    }
}