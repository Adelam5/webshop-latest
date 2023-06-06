using Domain.Constants;
using FluentValidation;

namespace Application.Customers.Commands.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.UserData.FirstName).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.UserData.LastName).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.UserData.Email).NotEmpty().EmailAddress().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.Address.Street).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.Address.City).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.Address.State).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.Address.Zipcode).NotEmpty().Length(AddressConstants.ZipcodeLength).Matches(AddressConstants.ZipcodePattern);

    }
}

