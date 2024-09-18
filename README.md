
# StaX.Domain

**[`StaX.Domain`](https://www.nuget.org/packages/StaX.Domain)** is a core library built on [Avalonia](https://github.com/AvaloniaUI) and [Fluent Avalonia](https://github.com/amwx/FluentAvalonia), designed for plugin development and integration within the [StaX](https://github.com/Psynapsis/StaX) application. It provides essential abstractions and a flexible architecture that allows developers to create custom plugins for the StaX platform.

## Features

- **Plugin Development Support**: [`StaX.Domain`](https://www.nuget.org/packages/StaX.Domain) provides a base class [`UiState`](https://github.com/Psynapsis/StaX.Domain/blob/main/Source/StaX.Domain/UiState.cs), allowing developers to build plugins that can be easily integrated into the main StaX application.
- **Modular Architecture**: Each plugin is developed independently and can be placed in the `Plugins` folder of the StaX application for dynamic loading.
- **Flexible UI Design**: Plugins can fully control their UI, making them adaptable to various use cases.

## Installation

You can install the **[`StaX.Domain`](https://www.nuget.org/packages/StaX.Domain)** NuGet package using the .NET CLI or the NuGet Package Manager in Visual Studio.

### .NET CLI

```bash
dotnet add package StaX.Domain
```

### Package Manager Console

```bash
Install-Package StaX.Domain
```

## Usage

### Creating a Plugin

To create a plugin for the StaX application, follow these steps:

1. Install the **[`StaX.Domain`](https://www.nuget.org/packages/StaX.Domain)** package in your project.
2. Create a class that inherits from [`UiState`](https://github.com/Psynapsis/StaX.Domain/blob/main/Source/StaX.Domain/UiState.cs), which defines the UI and behavior of your plugin.

Here is a simple example:

```csharp
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Controls;
using MyStaxPlugin.ViewModels;
using MyStaxPlugin.Views;
using StaX.Domain;

namespace MyStaxPlugin;

public partial class MyState : UiState
{
    public override string StateName { get; protected set; } = "MyStaxPlugin";
    public override string ToolTip { get; protected set; } = "MyStaxPlugin";
    public override Symbol? Icon { get; protected set; } = Symbol.ShareAndroid;

    public MyState()
    {
        StateView = new MyView();
        StateViewModel = new MyViewModel();
    }

    public override void Initialize() => AvaloniaXamlLoader.Load(this);
}
```

3. Assemble your project to create a `.dll` file.
4. Copy the created `.dll` file in the folder with the name of your plugin to the `Plugins` folder of the StaX application. The plugin will be loaded automatically when you start StaX.

## Contributing

Contributions are welcome! If you'd like to help improve **[`StaX.Domain`](https://www.nuget.org/packages/StaX.Domain)** or add features, feel free to fork the repository and submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for more details.
