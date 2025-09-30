using RateLimitMinimalApi.Core.App.DTOs;
using RateLimitMinimalApi.Core.Domain.Entities;
using RateLimitMinimalApi.Core.Domain.Interfaces.Repos;

namespace RateLimitMinimalApi.Core.App.Services;

public interface IUserService
{
    Task<ApiResponse<IEnumerable<UserResponse>>> GetAllUsersAsync();
    Task<ApiResponse<UserResponse?>> GetUserByIdAsync(int id);
    Task<ApiResponse<UserResponse>> CreateUserAsync(UserCreateRequest request);
}

public class UserService : IUserService
{
    private readonly IUserRepo _userRepository;

    public UserService(IUserRepo userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<IEnumerable<UserResponse>>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        var userResponses = users.Select(u => new UserResponse(
            u.Id, u.Username, u.Email, u.CreatedAt
        ));

        return new ApiResponse<IEnumerable<UserResponse>>(
            "Users retrieved successfully",
            userResponses,
            userResponses.Count()
        );
    }

    public async Task<ApiResponse<UserResponse?>> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            return new ApiResponse<UserResponse?>("User not found", null);
        }

        var userResponse = new UserResponse(
            user.Id, user.Username, user.Email, user.CreatedAt
        );

        return new ApiResponse<UserResponse?>("User found", userResponse);
    }

    public async Task<ApiResponse<UserResponse>> CreateUserAsync(UserCreateRequest request)
    {
        var user = new User(request.Username, request.Email);
        var createdUser = await _userRepository.CreateAsync(user);

        var userResponse = new UserResponse(
            createdUser.Id, createdUser.Username, createdUser.Email, createdUser.CreatedAt
        );

        return new ApiResponse<UserResponse>("User created successfully", userResponse);
    }
}
