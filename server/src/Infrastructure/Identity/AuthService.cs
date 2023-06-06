using Application.Authentication.Commands.ChangePassword;
using Application.Authentication.Commands.ForgotPassword;
using Application.Authentication.Commands.Login;
using Application.Authentication.Commands.Register;
using Application.Authentication.Commands.ResetPassword;
using Application.Authentication.Queries.GetCurrentUser;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Constants;
using Domain.Errors;
using Domain.Primitives.Result;
using Infrastructure.Authentication;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Infrastructure.Identity;
public sealed class AuthService : IAuthService
{
    private readonly IMapper mapper;
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly ICookieService cookieService;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;


    public AuthService(IMapper mapper, IJwtTokenGenerator jwtTokenGenerator, ICookieService cookieService, UserManager<User> userManager,
            SignInManager<User> signInManager)
    {
        this.mapper = mapper;
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.cookieService = cookieService;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    public async Task<Result<string>> Register(RegisterUserCommand registerData, CancellationToken cancellationToken = default)
    {
        var existingUser = await userManager.FindByEmailAsync(registerData.Email);

        if (existingUser != null)
            return Result.Failure<string>(Errors.Authentication.DuplicateEmail);

        var user = mapper.Map<User>(registerData);
        user.UserName = user.Email;

        var result = await userManager.CreateAsync(user, registerData.Password);

        if (!result.Succeeded)
            return Result.Failure<string>(new Error("User.NotSaved", result.Errors.First().Description));

        await userManager.AddToRoleAsync(user, Roles.User);

        return Result.Success(user.Id);
    }

    public async Task<Result<string>> Login(LoginUserCommand loginData, CancellationToken cancellationToken = default)
    {
        var (email, password) = loginData;

        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
            return Result.Failure<string>(Errors.Authentication.InvalidCredentials);

        var result = await signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

        if (!result.Succeeded)
            return Result.Failure<string>(Errors.Authentication.InvalidCredentials);

        var token = jwtTokenGenerator.GenerateToken(user);

        cookieService.SetTokenInCookie(token);

        return Result.Success(user.Id);
    }

    public async Task<GetCurrentUserResponse?> GetCurrentUser(string userId, CancellationToken cancellationToken = default)
    {
        return await userManager
            .Users
            .Where(x => x.Id == userId)
            .ProjectTo<GetCurrentUserResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Result<bool>> VerifyEmail(string token, string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure<bool>(Errors.User.NotFound);

        var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

        var result = await userManager.ConfirmEmailAsync(user, code);

        if (!result.Succeeded)
            return Result.Failure<bool>(new Error(result.Errors.First().Code, result.Errors.First().Description));

        return Result.Success(true);

    }

    public async Task<Result<bool>> ChangePassword(string userId, ChangeUserPasswordCommand password)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure<bool>(Errors.Authentication.NotAuthenticated);

        var result = await userManager
            .ChangePasswordAsync(user, password.CurrentPassword, password.NewPassword);

        if (!result.Succeeded)
            return Result.Failure<bool>(new Error(
                code: result.Errors.First().Code,
                message: result.Errors.First().Description));

        return Result.Success(true);
    }

    public async Task<PasswordResetDetailsDto?> GetPasswordResetDetails(string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
            return null;

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        return new PasswordResetDetailsDto(user.Id, token);
    }

    public async Task<Result<bool>> ResetPassword(ResetUserPasswordCommand request)
    {
        var user = await userManager.FindByIdAsync(request.UserId);

        if (user is null)
            return Result.Failure<bool>(Errors.User.NotFound);

        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

        var result = await userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);

        if (!result.Succeeded)
            return Result.Failure<bool>(new Error(
                code: result.Errors.First().Code,
                message: result.Errors.First().Description));

        return Result.Success(true);
    }

    public void Logout()
    {
        cookieService.RemoveTokenFromCookie();
    }
}
