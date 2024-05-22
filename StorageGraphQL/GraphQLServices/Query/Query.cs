using HotChocolate.Types;
using StorageGraphQL.DTO;
using StorageGraphQL.Repo;
using System.Collections.Generic;

namespace StorageGraphQL.GraphQLServices.Query
{
    public class Query
    {
        public IEnumerable<int?> GetProducts([Service] ProductStorageRepo productRepo)
        {
            return productRepo.GetProducts();
        }
    }
}