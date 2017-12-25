using System;
using System.IO;
using System.Runtime.InteropServices;

public class Bin2AccentPolicy
{
	internal enum AccentState
	{
		ACCENT_DISABLED = 0,
		ACCENT_ENABLE_GRADIENT = 1,
		ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
		ACCENT_ENABLE_BLURBEHIND = 3,
		ACCENT_ENABLE_FLUENT = 4,
		ACCENT_INVALID_STATE = 5
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct AccentPolicy
	{
		public AccentState AccentState;
		public int AccentFlags;
		public int GradientColor;
		public int AnimationId;
	}

	public static int Main(string[] Arguments)
	{
		try
		{
			string dumpFile;
			if (Arguments.Length > 0)
			{
				dumpFile = Arguments[0];
			}
			else
			{
				dumpFile = "dump.bin";
			}

			int size = Marshal.SizeOf<AccentPolicy>();

			byte[] dumpedPolicy = new byte[size];
			using (FileStream dumpStream = File.OpenRead(dumpFile))
			{
				if (dumpStream.Length != size)
				{
					throw new InvalidDataException();
				}
				else
				{
					dumpStream.Read(dumpedPolicy, 0, size);
				}
			}

			IntPtr rawPolicy = Marshal.AllocHGlobal(size);
			Marshal.Copy(dumpedPolicy, 0, rawPolicy, size);
			var finalPolicy = Marshal.PtrToStructure<AccentPolicy>(rawPolicy);
			Marshal.FreeHGlobal(rawPolicy);

			Console.WriteLine("Accent State   - " + finalPolicy.AccentState);
			Console.WriteLine("Accent Flags   - " + finalPolicy.AccentFlags);
			Console.WriteLine("Gradient Color - 0x" + finalPolicy.GradientColor.ToString("X"));
			Console.WriteLine("Animation Id   - " + finalPolicy.AnimationId);

			return 0;
		}
		catch (Exception error)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(error.Message);
			Console.ResetColor();
			return 1;
		}
	}
}