namespace System.Runtime.CompilerServices
{
	/// <summary>
	/// C# 9.0 adds the "record" type but init-only properties need .NET 5.0 OR this class.
	/// </summary>
	/// <seealso href="https://stackoverflow.com/a/64749403/98903"/>
	internal static class IsExternalInit { }
}
