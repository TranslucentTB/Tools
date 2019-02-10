using System;
using System.Runtime.InteropServices;

class ColorDumper
{
	[DllImport("uxtheme.dll", EntryPoint = "#95")]
	static extern uint GetImmersiveColorFromColorSetEx(uint dwImmersiveColorSet, uint dwImmersiveColorType, bool bIgnoreHighContrast, uint dwHighContrastCacheMode);

	[DllImport("uxtheme.dll", EntryPoint = "#96", CharSet = CharSet.Unicode)]
	static extern uint GetImmersiveColorTypeFromName(string name);

	[DllImport("uxtheme.dll", EntryPoint = "#98")]
	static extern uint GetImmersiveUserColorSetPreference(bool bForceCheckRegistry, bool bSkipCheckOnFail);

	[DllImport("uxtheme.dll", EntryPoint = "#100")]
	unsafe static extern char **GetImmersiveColorNamedTypeByIndex(uint dwIndex);

	unsafe static int Main()
	{
		try
		{
			uint i = 0;
			do
			{
				var typeNamePtr = GetImmersiveColorNamedTypeByIndex(i);
				if (typeNamePtr != null)
				{
					var typeName = new string(*typeNamePtr);

					var colorSet = GetImmersiveUserColorSetPreference(false, false);
					var colorType = GetImmersiveColorTypeFromName("Immersive" + typeName);
					var rawColor = GetImmersiveColorFromColorSetEx(colorSet, colorType, false, 0);

					Console.WriteLine("{0} - {1:X}", typeName, rawColor);
				}
			} while (++i != 0);

			return 0;
		}
		catch (Exception exc)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(exc);
			Console.ResetColor();
			return 1;
		}
	}
}