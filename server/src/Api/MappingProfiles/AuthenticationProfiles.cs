using Api.Contracts.Authentication;
using Application.Authentication.Commands.ChangePassword;
using Application.Authentication.Commands.ForgotPassword;
using Application.Authentication.Commands.Login;
using Application.Authentication.Commands.Register;
using Application.Authentication.Commands.ResetPassword;
using Application.Authentication.Commands.VerifyEmail;
using AutoMapper;

namespace Api.MappingProfiles;

public class AuthenticationProfiles : Profile
{
    public AuthenticationProfiles()
    {
        CreateMap<RegisterUserRequest, RegisterUserCommand>();
        CreateMap<LoginUserRequest, LoginUserCommand>();
        CreateMap<VerifyEmailRequest, VerifyEmailCommand>();
        CreateMap<ChangePasswordRequest, ChangeUserPasswordCommand>();
        CreateMap<ForgotPasswordRequest, ForgotPasswordCommand>();
        CreateMap<ResetPasswordRequest, ResetUserPasswordCommand>();
    }
}
