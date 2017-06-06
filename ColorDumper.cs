using System;
using System.Runtime.InteropServices;

static class ColorDumper
{
	[DllImport("uxtheme.dll", EntryPoint = "#95", CharSet = CharSet.Unicode)]
	internal static extern uint GetImmersiveColorFromColorSetEx(uint dwImmersiveColorSet, uint dwImmersiveColorType, bool bIgnoreHighContrast, uint dwHighContrastCacheMode);

	[DllImport("uxtheme.dll", EntryPoint = "#96", CharSet = CharSet.Unicode)]
	internal static extern uint GetImmersiveColorTypeFromName(string name);

	[DllImport("uxtheme.dll", EntryPoint = "#98", CharSet = CharSet.Unicode)]
	internal static extern uint GetImmersiveUserColorSetPreference(bool bForceCheckRegistry, bool bSkipCheckOnFail);

	[DllImport("uxtheme.dll", EntryPoint = "#100", CharSet = CharSet.Unicode)]
	internal static extern IntPtr GetImmersiveColorNamedTypeByIndex(uint dwIndex);

	public static int Main()
	{
		try
		{
			for (UInt32 colorIndex = 0; colorIndex < 0xFFF; colorIndex++)
			{
				IntPtr typeNamePtr = GetImmersiveColorNamedTypeByIndex(colorIndex);
				if (typeNamePtr != IntPtr.Zero)
				{
					IntPtr ptr_typeName = (IntPtr)Marshal.PtrToStructure(typeNamePtr, typeof (IntPtr));
					string typeName = Marshal.PtrToStringUni(ptr_typeName);

					var colorSet = GetImmersiveUserColorSetPreference(false, false);
					var colorType = GetImmersiveColorTypeFromName("Immersive" + typeName);
					var rawColor = GetImmersiveColorFromColorSetEx(colorSet, colorType, false, 0);

					Console.WriteLine(typeName + " - " + String.Format("{0:X}", rawColor));
				}
			}

			return 0;
		}
		catch (Exception error)
		{
			Console.WriteLine(error.Message);
			return 1;
		}
	}
}