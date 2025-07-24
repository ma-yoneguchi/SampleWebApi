using AutoMapper;
using SampleWebApi.DTOs;
using SampleWebApi.Entities;
using SampleWebApi.Repositories;

namespace SampleWebApi.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleResponse>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleResponse>>(roles);
        }

        public async Task<RoleResponse?> GetRoleByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            return role != null ? _mapper.Map<RoleResponse>(role) : null;
        }

        public async Task<RoleResponse> CreateRoleAsync(RoleCreateRequest request)
        {
            if (await _roleRepository.ExistsByNameAsync(request.Name))
            {
                throw new InvalidOperationException($"ロール名'{request.Name}'は既に存在します。");
            }

            var role = _mapper.Map<Role>(request);
            var createdRole = await _roleRepository.CreateAsync(role);
            return _mapper.Map<RoleResponse>(createdRole);
        }

        public async Task<RoleResponse> UpdateRoleAsync(RoleUpdateRequest request)
        {
            var existingRole = await _roleRepository.GetByIdAsync(request.Id);
            if (existingRole == null)
            {
                throw new ArgumentException("指定されたロールが見つかりません。");
            }

            if (await _roleRepository.ExistsByNameAsync(request.Name, request.Id))
            {
                throw new InvalidOperationException($"ロール名'{request.Name}'は既に存在します。");
            }

            _mapper.Map(request, existingRole);
            var updatedRole = await _roleRepository.UpdateAsync(existingRole);
            return _mapper.Map<RoleResponse>(updatedRole);
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            if (!await _roleRepository.CanDeleteAsync(id))
            {
                throw new InvalidOperationException("関連するユーザーが存在するため削除できません。");
            }

            return await _roleRepository.DeleteAsync(id);
        }
    }
}
