using ScanUtil.Models;

namespace ServiceApplication.Infrastructure.Interfaces;

public interface IRepository
{
    public Task Save(ScanResponseModel model);
    public Task<ScanResponseModel> Find(Guid id);
}