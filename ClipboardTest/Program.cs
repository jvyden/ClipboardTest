using SDL;

Console.WriteLine("Clipboard test!");
SDL3.SDL_Init(SDL_InitFlags.SDL_INIT_VIDEO); // doesn't spawn a window
ThrowIfSdlError();


Console.WriteLine($"Clipboard has text? {(bool)SDL3.SDL_HasClipboardText()}");
ThrowIfSdlError();

if (SDL3.SDL_HasClipboardText())
    Console.WriteLine("\tClipboard text: " + SDL3.SDL_GetClipboardText());

Console.WriteLine($"Clipboard has image/png data? {(bool)SDL3.SDL_HasClipboardData("image/png")}");
ThrowIfSdlError();

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