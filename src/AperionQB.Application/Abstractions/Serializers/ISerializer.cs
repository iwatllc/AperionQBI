namespace AperionQB.Application.Abstractions.Serializers;

internal interface ISerializer<TObj, TResult>
{
    public TResult serialize(TObj obj);
}
