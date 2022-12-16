using System.Runtime.CompilerServices;

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
    }
}
