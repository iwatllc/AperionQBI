using AperionQB.Application.Mappings;
namespace AperionQB.Application.Mappings;

public class ListMapper<T, U> : IMapper<IEnumerable<T>, List<U>> {
    private readonly IMapper<T, U> _mapper;

    public ListMapper(IMapper<T, U> mapper) {
        _mapper = mapper;
    }

    public List<U> Map(IEnumerable<T> objs) {
        List<U> toTypes = new List<U>();
        foreach (var obj in objs)
        {
            toTypes.Add(_mapper.Map(obj));
        }
        return toTypes;
    }
}
