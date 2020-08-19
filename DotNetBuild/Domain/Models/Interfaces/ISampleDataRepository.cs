using System.Collections.Generic;

namespace DotNetBuild.Domain.Models.Interfaces
{
     public interface ISampleDataRepository
{
    List<Sample> Get();

    Sample Get(int id);

    string Post(Sample data);

    string Put(Sample data);

    string Delete(int id);
}
}
