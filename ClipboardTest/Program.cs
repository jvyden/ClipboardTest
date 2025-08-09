using System.Text;
using SDL;

Console.WriteLine("Clipboard test!");
SDL3.SDL_Init(SDL_InitFlags.SDL_INIT_VIDEO); // doesn't spawn a window
ThrowIfSdlError();


Console.WriteLine($"Clipboard has text? {(bool)SDL3.SDL_HasClipboardText()}");
ThrowIfSdlError();

if (SDL3.SDL_HasClipboardText())
    Console.WriteLine("\tClipboard text: " + SDL3.SDL_GetClipboardText());

Console.WriteLine("\tPrimary selection: " + SDL3.SDL_GetPrimarySelectionText());

Console.WriteLine($"Clipboard has image/png data? {(bool)SDL3.SDL_HasClipboardData("image/png")}");
ThrowIfSdlError();

SDLBool hasUriList = SDL3.SDL_HasClipboardData("text/uri-list");
Console.WriteLine($"Clipboard has text/uri-list data? {(bool)hasUriList}");

unsafe
{
    nuint len = 0;
    if (hasUriList)
    {
        IntPtr ptr = SDL3.SDL_GetClipboardData("text/uri-list", &len);
        ThrowIfSdlError();
        Span<byte> span = new((void*)ptr, (int)len);
        Console.WriteLine($"\tClipboard text: {Encoding.UTF8.GetString(span)}, len: {(int)len}");
    }
}

Console.WriteLine($"Clipboard has fake data? {(bool)SDL3.SDL_HasClipboardData("text/not-real-mime")}");

Console.WriteLine("Setting clipboard data...");
SDL3.SDL_SetClipboardText("Testy test");
ThrowIfSdlError();

Console.WriteLine($"\tClipboard text: {(bool)SDL3.SDL_HasClipboardText()}, {SDL3.SDL_GetClipboardText()}");
ThrowIfSdlError();

return;

void ThrowIfSdlError()
{
    string? error = SDL3.SDL_GetError();
    if (!string.IsNullOrEmpty(error))
        throw new Exception("SDL Init Error: " + error);
}