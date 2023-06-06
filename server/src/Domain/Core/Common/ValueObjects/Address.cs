using Domain.Constants;
using Domain.Exceptions.Address;
using Domain.Primitives;
using System.Text.RegularExpressions;

namespace Domain.Core.Common.ValueObjects;

public class Address : ValueObject
{
    private Address(string street, string city, string state, string zipcode)
    {
        Street = street;
        City = city;
        State = state;
        Zipcode = zipcode;
    }

    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zipcode { get; set; }

    internal static Address Create(string street, string city, string state, string zipcode)
    {
        if (string.IsNullOrWhiteSpace(street) || street.Length < CommonConstants.StringMinLength || street.Length > CommonConstants.StringMaxLength)
            throw new InvalidStreetException();

        if (string.IsNullOrWhiteSpace(city) || city.Length < CommonConstants.StringMinLength || city.Length > CommonConstants.StringMaxLength)
            throw new InvalidCityException();

        if (string.IsNullOrWhiteSpace(state) || state.Length < CommonConstants.StringMinLength || state.Length > CommonConstants.StringMaxLength)
            throw new InvalidStateException();

        if (string.IsNullOrWhiteSpace(zipcode) || zipcode.Length != AddressConstants.ZipcodeLength || !Regex.IsMatch(zipcode, AddressConstants.ZipcodePattern))
            throw new InvalidZipcodeException();

        return new Address(street, city, state, zipcode);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return Zipcode;
    }
}
