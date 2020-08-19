using System.Collections.Generic;
using DotNetBuild.Domain.Models;

namespace DotNetBuild.Services.Interfaces
{
    public interface ISampleService
{
    List<Sample> Get();

    Sample Get(int id);

    string Post(Sample data);

    string Put(Sample data);

    string Delete(int id);
}
}
