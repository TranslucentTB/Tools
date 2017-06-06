using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class Bin2AccentPolicy
{
	internal enum AccentState
	{
		ACCENT_DISABLED = 0,
		ACCENT_ENABLE_GRADIENT = 1,
		ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
		ACCENT_ENABLE_BLURBEHIND = 3,
		ACCENT_INVALID_STATE = 4
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

			byte[] dumpedPolicy = new byte[16];
			using (FileStream dumpStream = File.OpenRead(dumpFile))
			{
				if (dumpStream.Length != 16)
				{
					throw new InvalidDataException();
				}
				else
				{
					dumpStream.Read(dumpedPolicy, 0, 16);
				}
			}

			IntPtr rawPolicy = Marshal.AllocHGlobal(16);
			Marshal.Copy(dumpedPolicy, 0, rawPolicy, 16);
			var finalPolicy = (AccentPolicy)Marshal.PtrToStructure(rawPolicy, (new AccentPolicy{}).GetType());
			Marshal.FreeHGlobal(rawPolicy);

			Console.WriteLine("Accent State   - " + finalPolicy.AccentState.ToString());
			Console.WriteLine("Accent Flags   - " + finalPolicy.AccentFlags.ToString());
			Console.WriteLine("Gradient Color - 0x" + finalPolicy.GradientColor.ToString("X"));
			Console.WriteLine("Animation Id   - " + finalPolicy.AnimationId.ToString());

			return 0;
		}
		catch (Exception error)
		{
			Console.WriteLine(error.Message);
			return 1;
		}
	}
}