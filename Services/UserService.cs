using AutoMapper;
using SampleWebApi.DTOs;
using SampleWebApi.Entities;
using SampleWebApi.Repositories;

namespace SampleWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }

        public async Task<UserResponse?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? _mapper.Map<UserResponse>(user) : null;
        }

        public async Task<UserResponse?> GetUserDetailByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdWithOrdersAsync(id);
            return user != null ? _mapper.Map<UserResponse>(user) : null;
        }

        public async Task<IEnumerable<UserResponse>> GetUsersByDepartmentAsync(string department)
        {
            var users = await _userRepository.GetByDepartmentAsync(department);
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }

        public async Task<UserResponse> CreateUserAsync(UserCreateRequest request)
        {
            // 複合ユニーク制約の事前チェック
            if (await _userRepository.ExistsByDepartmentAndEmployeeCodeAsync(request.Department, request.EmployeeCode))
            {
                throw new InvalidOperationException($"部署'{request.Department}'に従業員コード'{request.EmployeeCode}'は既に存在します。");
            }

            // メールアドレスの重複チェック
            if (await _userRepository.ExistsByEmailAsync(request.Email))
            {
                throw new InvalidOperationException($"メールアドレス'{request.Email}'は既に使用されています。");
            }

            var user = _mapper.Map<User>(request);
            var createdUser = await _userRepository.CreateAsync(user);
            return _mapper.Map<UserResponse>(createdUser);
        }

        public async Task<UserResponse> UpdateUserAsync(UserUpdateRequest request)
        {
            var existingUser = await _userRepository.GetByIdAsync(request.Id);
            if (existingUser == null)
            {
                throw new ArgumentException("指定されたユーザーが見つかりません。");
            }

            // 複合ユニーク制約チェック（自分以外）
            if (await _userRepository.ExistsByDepartmentAndEmployeeCodeAsync(
                request.Department, request.EmployeeCode, request.Id))
            {
                throw new InvalidOperationException($"部署'{request.Department}'に従業員コード'{request.EmployeeCode}'は既に存在します。");
            }

            // メールアドレスの重複チェック（自分以外）
            if (await _userRepository.ExistsByEmailAsync(request.Email, request.Id))
            {
                throw new InvalidOperationException($"メールアドレス'{request.Email}'は既に使用されています。");
            }

            _mapper.Map(request, existingUser);
            var updatedUser = await _userRepository.UpdateAsync(existingUser);
            return _mapper.Map<UserResponse>(updatedUser);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            if (!await _userRepository.CanDeleteAsync(id))
            {
                throw new InvalidOperationException("関連するデータが存在するため削除できません。");
            }

            return await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> ValidateUniqueConstraintAsync(string department, string employeeCode, int? excludeId = null)
        {
            return !await _userRepository.ExistsByDepartmentAndEmployeeCodeAsync(department, employeeCode, excludeId);
        }
    }
}
