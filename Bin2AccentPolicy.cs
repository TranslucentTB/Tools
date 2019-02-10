using System;
using System.IO;
using System.Runtime.InteropServices;

class Bin2AccentPolicy
{
	enum ACCENT_STATE : int
	{
		ACCENT_DISABLED = 0,
		ACCENT_ENABLE_GRADIENT = 1,
		ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
		ACCENT_ENABLE_BLURBEHIND = 3,
		ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
		ACCENT_ENABLE_HOSTBACKDROP = 5,
		ACCENT_INVALID_STATE = 6
	}

	[StructLayout(LayoutKind.Sequential)]
	struct ACCENT_POLICY
	{
		public ACCENT_STATE AccentState;
		public uint AccentFlags;
		public uint GradientColor;
		public int AnimationId;
	}

	unsafe static int Main(string[] args)
	{
		try
		{
			byte *array = stackalloc byte[sizeof(ACCENT_POLICY)];
			using (var stream = File.OpenRead(args.Length > 0 ? args[0] : "dump.bin"))
			{
				if (stream.Length != sizeof(ACCENT_POLICY))
				{
					throw new InvalidDataException();
				}

				stream.CopyTo(new UnmanagedMemoryStream(array, 0, sizeof(ACCENT_POLICY), FileAccess.Write));
			}

			ACCENT_POLICY *policy = (ACCENT_POLICY *)array;

			Console.WriteLine("Accent State:   {0}", policy->AccentState);
			Console.WriteLine("Accent Flags:   {0}", policy->AccentFlags);
			Console.WriteLine("Gradient Color: 0x{0:X}", policy->GradientColor.ToString("X"));
			Console.WriteLine("Animation Id:   {0}", policy->AnimationId);

			return 0;
		}
		catch (Exception exc)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(exc.Message);
			Console.ResetColor();
			return 1;
		}
	}
}