using System.Collections.Generic;

namespace Master.ViewModels
{
    public class QueryResponse<TResource>
    {
        public IEnumerable<TResource> Data { get; set; }

        public int ItemsCount { get; set; }
    }
}
