namespace Samwise.Abstractions.Parsers
{
    public interface IParseData<in TDataInput, out TResult>
    {
        TResult ParseData(TDataInput data);
    }
}