using FreeDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Application.Model
{
    public class PaginationModel<T>
    {
        public List<T> Results { get; set; }
        public int TotalDocs { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrev { get; set; }
        public void FormatPagination(List<T> results, int totalDocs, int page, int limit)
        {
            this.Results = results;
            this.TotalDocs = totalDocs;
            this.Page = page;
            this.TotalPages = totalDocs / limit + (totalDocs % limit > 0 ? 1 : 0);
            this.HasNext = page < this.TotalPages;
            this.HasPrev = page > 1 && page <= this.TotalPages;
        }
    }
}
