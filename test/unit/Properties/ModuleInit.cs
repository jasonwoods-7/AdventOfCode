using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AoC.Tests.Properties;

public static class ModuleInit
{
    [ModuleInitializer]
    public static void Init()
    {
        ClipboardAccept.Enable();

        DerivePathInfo((sourceFile, _, type, method) =>
            new PathInfo(
                Path.Combine(Path.GetDirectoryName(sourceFile)!, "VerifyResults"),
                type.IsNested
                    ? $"{type.ReflectedType!.Name}.{type.Name}"
                    : type.Name,
                method.Name));

        Configuration = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) => config.AddUserSecrets(typeof(ModuleInit).Assembly))
            .Build()
            .Services
            .GetRequiredService<IConfiguration>();
    }

    public static IConfiguration Configuration { get; private set; } = null!;
}
