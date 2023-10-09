using AutoMapper;
using UserWebAPI.DTO;
using UserWebAPI.Models;
using UserWebAPI.QueryExtensions.Params;
using UserWebAPI.QueryExtensions.Responses;
using UserWebAPI.Repository.Interfaces;
using UserWebAPI.Services.Interfaces;

namespace UserWebAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _repository = roleRepository;
            _mapper = mapper;
        }

        public RoleQueryResponse GetAll(RoleQueryParams queryParams)
        {
            var normalizedParams = FixQueryParams(queryParams);

            var collection = _repository
                .GetAll(normalizedParams)
                .Select(_mapper.Map<RoleDto>)
                .ToList();

            return new(queryParams, collection);
        }

        public RoleDto GetById(int id)
        {
            var user = _repository.GetById(id);
            return _mapper.Map<RoleDto>(user);
        }

        public void Create(RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);

            _repository.Create(role);
            _repository.Save();
        }

        public void Update(RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);

            _repository.Update(role);
            _repository.Save();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }

        private static RoleQueryParams FixQueryParams(RoleQueryParams queryParams)
        {
            return queryParams is null ?
                RoleQueryParams.NoFilterNoSort :
                queryParams.Fixed();

        }
    }
}
