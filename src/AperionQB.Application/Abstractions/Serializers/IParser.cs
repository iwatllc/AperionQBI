namespace AperionQB.Application.Abstractions.Serializers;

internal interface IParser<TObj, TData>
{
    TObj Parse(TData data);
}
