using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigPractice;

internal class FxConfigurationProvider : FileConfigurationProvider
{
    public FxConfigurationProvider(FileConfigurationSource source) : base(source)
    {
    }

    public override void Load(Stream stream)
    {
        throw new NotImplementedException();
    }
}
