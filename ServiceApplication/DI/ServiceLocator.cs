using ScanUtil.Models;
using ServiceApplication.Infrastructure;
using ServiceApplication.Infrastructure.Interfaces;

namespace ServiceApplication.DI;

public class ServiceLocator
{
    private static ServiceLocator? _instance;
    private IRepository? _repository;

    public static ServiceLocator GetInstance()
    {
        return _instance ??= new ServiceLocator();
    }

    public IRepository GetRepository()
    {
        return _instance!._repository ??= new Repository(new List<ScanResponseModel>());
    }
}