using Microsoft.Extensions.DependencyInjection;
using PainterLib;

var services = new ServiceCollection();

// Choose which tool to inject
services.AddTransient<IPaintTool, PaintBrush>();
services.AddTransient<Painter>();

var provider = services.BuildServiceProvider();
var painter = provider.GetRequiredService<Painter>();

Console.WriteLine(painter.StartPainting("wall"));
