using Domain.Constants;
using FluentValidation;

namespace Application.Customers.Commands.UpdateAddress;

public class UpdateCustomerAddressCommandValidator : AbstractValidator<UpdateCustomerAddressCommand>
{
    public UpdateCustomerAddressCommandValidator()
    {
        RuleFor(x => x.Street).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.City).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.State).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.Zipcode).NotEmpty().Length(AddressConstants.ZipcodeLength).Matches(AddressConstants.ZipcodePattern);
    }
}
