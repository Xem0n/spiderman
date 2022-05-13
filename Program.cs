using spiderman;
using spiderman.Rendering;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

NativeWindowSettings nativeWindowSettings = new()
{
    Title = "Spider-Man",
    Size = Renderer.Resolution,
    NumberOfSamples = 16
};

using (Game game = new(GameWindowSettings.Default, nativeWindowSettings))
{
    game.Run();
}