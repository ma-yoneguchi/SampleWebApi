using AutoMapper;
using SampleWebApi.DTOs;
using SampleWebApi.Entities;
using SampleWebApi.Repositories;

namespace SampleWebApi.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserRoleService(
            IUserRoleRepository userRoleRepository,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserRoleResponse>> GetAllUserRolesAsync()
        {
            var userRoles = await _userRoleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserRoleResponse>>(userRoles);
        }

        public async Task<UserRoleResponse?> GetUserRoleByIdAsync(int userId, int roleId)
        {
            var userRole = await _userRoleRepository.GetByIdAsync(userId, roleId);
            return userRole != null ? _mapper.Map<UserRoleResponse>(userRole) : null;
        }

        public async Task<IEnumerable<UserRoleResponse>> GetUserRolesByUserIdAsync(int userId)
        {
            var userRoles = await _userRoleRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<UserRoleResponse>>(userRoles);
        }

        public async Task<IEnumerable<UserRoleResponse>> GetUserRolesByRoleIdAsync(int roleId)
        {
            var userRoles = await _userRoleRepository.GetByRoleIdAsync(roleId);
            return _mapper.Map<IEnumerable<UserRoleResponse>>(userRoles);
        }

        public async Task<UserRoleResponse> CreateUserRoleAsync(UserRoleCreateRequest request)
        {
            if (await _userRoleRepository.ExistsAsync(request.UserId, request.RoleId))
            {
                throw new InvalidOperationException("このユーザーには既に同じロールが割り当てられています。");
            }

            if (!await _userRepository.ExistsAsync(request.UserId))
            {
                throw new ArgumentException("指定されたユーザーが存在しません。");
            }

            if (!await _roleRepository.ExistsAsync(request.RoleId))
            {
                throw new ArgumentException("指定されたロールが存在しません。");
            }

            var userRole = _mapper.Map<UserRole>(request);
            var createdUserRole = await _userRoleRepository.CreateAsync(userRole);

            var userRoleWithRelations = await _userRoleRepository.GetByIdAsync(createdUserRole.UserId, createdUserRole.RoleId);
            return _mapper.Map<UserRoleResponse>(userRoleWithRelations);
        }

        public async Task<bool> DeleteUserRoleAsync(int userId, int roleId)
        {
            return await _userRoleRepository.DeleteAsync(userId, roleId);
        }
    }
}
