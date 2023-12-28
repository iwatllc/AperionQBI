namespace AperionQB.Application.Mappings;

public interface IMapper<in T, out U>
{
    U Map(T obj);
}

