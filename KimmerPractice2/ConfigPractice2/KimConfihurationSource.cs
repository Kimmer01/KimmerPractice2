using Microsoft.Extensions.Configuration;

namespace ConfigPractice2;

internal class KimConfihurationSource : FileConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        EnsureDefaults(builder);
        return new KimConfigurationProvider(this);
    }
}
