using AutoMapper;

namespace ProjectStructure.BL.Services.AbstractClasses
{
    public abstract class BaseService
    {
        private protected readonly IMapper _mapper;

        public BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
