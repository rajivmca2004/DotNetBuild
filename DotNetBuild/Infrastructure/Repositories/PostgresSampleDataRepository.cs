using DotNetBuild.Domain.Models.Interfaces;
using  DotNetBuild.Domain.Models;
using DotNetBuild.Infrastructure.Initialization;
using System.Collections.Generic;
using System.Linq;

namespace DotNetBuild.Infrastructure.Repositories
{
  public class PostgresSampleDataRepository : ISampleDataRepository
{
    protected readonly InitializeContext _InitializeContext;
    public PostgresSampleDataRepository(InitializeContext initializeContext)
    {
        _InitializeContext = initializeContext;
    }

    public List<Sample> Get()
    {
        return _InitializeContext.Sample.ToList();
    }

    public Sample Get(int id)
    {
        return _InitializeContext.Sample.Where(x => x.Id == id).FirstOrDefault();
    }

    public string Post(Sample data)
    {
        if (!_InitializeContext.Sample.Any(x => x.Id == data.Id))
        {
            _InitializeContext.Sample.Add(data);
            _InitializeContext.SaveChanges();
            return $"Data Inserted Succesfully with ID: {data.Id}";
        }
        else
        {
            return $"Data already exists with ID: {data.Id}. Please try Put Operation";
        }
    }

    public string Put(Sample data)
    {
        Sample ExistingData = _InitializeContext.Sample.Where(x => x.Id == data.Id).FirstOrDefault();
        if (ExistingData != null && ExistingData.Id != 0)
        {
            ExistingData.Name = data.Name;
            ExistingData.Details = data.Details;
            _InitializeContext.SaveChanges();
            return $"Data Updated Succesfully with ID: {data.Id}";
        }
        else
        {
            return $"Data doesnot exists with ID: {data.Id}. Please try Post Operation";
        }
    }

    public string Delete(int id)
    {
        Sample ExistingData = _InitializeContext.Sample.Where(x => x.Id == id).FirstOrDefault();
        if (ExistingData != null && ExistingData.Id != 0)
        {
            _InitializeContext.Remove(ExistingData);
            _InitializeContext.SaveChanges();
            return $"Data Deleted Succesfully with ID: {id}";
        }
        else
        {
            return $"Data doesnot exists with ID: {id}. Please try Post Operation";
        }
    }
}
}
