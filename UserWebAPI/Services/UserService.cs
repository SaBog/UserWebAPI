using AutoMapper;
using UserWebAPI.DTO;
using UserWebAPI.Models;
using UserWebAPI.Exceptions;
using UserWebAPI.Services.Interfaces;
using UserWebAPI.QueryExtensions.Responses;
using UserWebAPI.QueryExtensions.Params;
using UserWebAPI.Repository.Interfaces;

namespace UserWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _repository;
        private readonly IJwtService _JwtService;
        private readonly IMapper _mapper;

        public UserService(IRoleRepository roleRepository, 
            IUserRepository repository,
            IJwtService jwtService,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _JwtService = jwtService;
            _repository = repository;
            _mapper = mapper;
        }

        public UserQueryResponse GetAll(UserQueryParams queryParams)
        {
            var normalizedParams = FixQueryParams(queryParams);

            var collection = _repository
                .GetAll(normalizedParams)
                .Select(_mapper.Map<UserDto>)
                .ToList();

            return new(queryParams, collection);
        }

        public UserDto GetById(int id)
        {
            var user = _repository.GetById(id);
            return _mapper.Map<UserDto>(user);
        }

        public void Create(UserDto userDto)
        {
            ValidateModel(userDto);
            var user = Map(userDto);

            _repository.Create(user);
            _repository.Save();

            userDto.Id = user.Id;
        }

        public void Update(UserDto userDto)
        {
            ValidateModel(userDto);
            var user = Map(userDto);

            _repository.Update(user);
            _repository.Save();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }

        private User Map(UserDto userDto)
        {
            var ids = userDto.Roles.Select(r => r.Id).ToList();
            var roles = _roleRepository.GetAll(ids);

            var user = _mapper.Map<User>(userDto);
            user.Roles = roles.ToList();

            return user;
        }

        private void ValidateModel(UserDto model)
        {
            // null check 
            if (string.IsNullOrWhiteSpace(model.Name) &&
                string.IsNullOrWhiteSpace(model.Email))
            {
                throw new UserValidationModelException("Model has empty field name or (and) email");
            }

            if (model.Age <= 0)
            {
                throw new UserValidationModelException($"Model contains zero or negative age = {model.Age}");
            }

            // if not unique field
            var userWithSameEmail = _repository.ContainsEmail(model.Email);

            // if try to create user (skip on update)
            if (userWithSameEmail != null && userWithSameEmail.Id != model.Id)
            {
                throw new UserValidationModelException($"The user with same email = {model.Email} already exist");
            }
        }

        private static UserQueryParams FixQueryParams(UserQueryParams queryParams)
        {
            return queryParams is null ?
                UserQueryParams.NoFilterNoSort :
                queryParams.Fixed();

        }

        public string GetToken(string email)
        {
            var user = _repository.ContainsEmail(email);
            // password validation etc.

            if (user is null)
            {
                throw new UserNotFoundException($"User with email = {email} not found");
            }

            var userDto = _mapper.Map<UserDto>(user);
            return _JwtService.GetJwtToken(userDto);
        }

    }
}
