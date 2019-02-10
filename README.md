# Tools

These are some various tools used to reverse engineer some functions and API calls to aid in development of [TranslucentTB](https://github.com/TranslucentTB/TranslucentTB).

Compile them with the built-in C# compiler of the .NET Framework:

    C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /unsafe [filename]

## [Bin2AccentPolicy](https://github.com/TranslucentTB/Tools/blob/master/Bin2AccentPolicy.cs)

Unfortunately API Monitor is not aware of the AccentPolicy structure used by SetWindowCompositionAttribute:

![API Monitor](http://i.imgur.com/jQJRaTJ.png)

This simple tool when pointed to a dump of the hex buffer will write the correct values to the command prompt:

    C:\Users\Charles\Git\Tools>Bin2AccentPolicy.exe C:\Users\Charles\Desktop\test.bin
    Accent State   - ACCENT_ENABLE_TRANSPARENTGRADIENT
    Accent Flags   - 19
    Gradient Color - 0x9902129B
    Animation Id   - 0

### Usage

    Bin2AccentPolicy.exe [filename]

If filename is ignored, it will default to `dump.bin` in the current directory.

## [ColorDumper](https://github.com/TranslucentTB/Tools/blob/master/ColorDumper.cs)

Dumps all the colors accessible by the undocumented functions in `uxtheme.dll` to the command prompt:

    C:\Users\Charles\Git\Tools>ColorDumper.exe
    ApplicationBackground - FF000000
    ApplicationBackgroundDarkTheme - FF000000
    ApplicationBackgroundLightTheme - FFFFFFFF
    ApplicationText - FFFFFFFF
    ApplicationTextDarkTheme - FFFFFFFF
    ApplicationTextLightTheme - FF000000
    BootBackground - FFB26720
    BootConfirmationButton - DED6B147
    BootConfirmationButtonBackgroundDisabled - 0
    BootConfirmationButtonBackgroundHover - FFCF9454
    BootConfirmationButtonBackgroundPressed - FFFFFFFF
    BootConfirmationButtonBackgroundRest - FFB26720
    BootConfirmationButtonBorderDisabled - FFE0C2A6
    etc...
