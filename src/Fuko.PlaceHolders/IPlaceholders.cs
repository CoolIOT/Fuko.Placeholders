using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fuko.PlaceHolders
{
    public interface IPlaceholders
    {
        Task<string> ParseAsync(string template, IDictionary<string,string> placeholders);
    }
}