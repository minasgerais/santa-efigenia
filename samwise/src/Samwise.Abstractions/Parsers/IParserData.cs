namespace Samwise.Abstractions.Parsers
{
    public interface IParserData<in TDataInput, out TResult>
    {
        TResult ParterData(TDataInput data);
    }
}