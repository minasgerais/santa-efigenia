using System.Threading.Tasks;

namespace Samwise.Abstractions.Services
{
    public interface IParseService<TDataInput, TResult>
    {
        Task ExecuteParseAsync();
    }
}