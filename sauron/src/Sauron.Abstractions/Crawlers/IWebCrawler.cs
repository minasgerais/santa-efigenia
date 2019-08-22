using System.Threading.Tasks;

namespace Sauron.Abstractions.Crawlers
{
    public interface IWebCrawler<TResult>
        where TResult : class
    {
        Task<TResult> ExtractAsync(string source, IFilter filter);
    }
}
