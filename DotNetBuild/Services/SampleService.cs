using DotNetBuild.Domain.Models.Interfaces;
using DotNetBuild.Domain.Models;
using DotNetBuild.Services.Interfaces;
using System.Collections.Generic;

namespace DotNetBuild.Services
{
   public class SampleService : ISampleService
{
    protected readonly ISampleDataRepository _postgresDataOperations;
    public SampleService(ISampleDataRepository postgresDataOperations)
    {
        _postgresDataOperations = postgresDataOperations;
    }

    public List<Sample> Get()
    {
        return _postgresDataOperations.Get();
    }

    public Sample Get(int id)
    {
        return _postgresDataOperations.Get(id);
    }

    public string Post(Sample data)
    {
        return _postgresDataOperations.Post(data);
    }

    public string Put(Sample data)
    {
        return _postgresDataOperations.Put(data);
    }

    public string Delete(int id)
    {
        return _postgresDataOperations.Delete(id);
    }
}
}
