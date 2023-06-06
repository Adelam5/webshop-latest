using Domain.Constants;
using Domain.Primitives;

namespace Domain.Core.Common.ValueObjects;

public sealed class Photo : ValueObject
{
    public Photo(string url, string name)
    {
        Url = url;
        Name = name;
    }

    public string Url { get; private set; }
    public string Name { get; private set; }

    public static Photo Create(
        string url = PhotoConstants.DefaultPhotoUrl,
        string name = PhotoConstants.DefaultPhotoName)
    {
        return new Photo(url, name);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Url;
        yield return Name;
    }
}
