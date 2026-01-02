# Project 100

A visual programming environment built with Godot 4.5 and C# (.NET 8.0) that implements a drag-and-drop block-based coding system.

## 📋 Project Overview

**Project 100** is a visual code block editor that allows users to create programs by connecting and arranging code blocks. This educational tool provides an intuitive interface for learning programming concepts through visual representation.

### Key Features

- 🧩 **Block-Based Programming**: Drag-and-drop code blocks to build programs
- 🎨 **Visual Canvas**: Zoomable and pannable canvas for organizing code blocks
- 🔄 **Real-time Execution**: Execute visual code and see results in an integrated console
- 📦 **Block Categories**: Organized system with multiple block types (Statement, Variable, Control, Expression)
- 🎮 **Built with Godot**: Leverages Godot Engine's robust 2D capabilities

## 🛠️ Technology Stack

- **Engine**: Godot 4.5
- **Language**: C# (.NET 8.0 / .NET 9.0 for Android)
- **Target Platforms**: Windows, Android (Mobile)
- **Dependencies**:
  - Godot.NET.Sdk 4.5.1
  - Firebelley.GodotUtilities 5.0.0

## 📁 Project Structure

```
Project 100/
├── addons/                      # Godot editor plugins
│   ├── AsepriteWizard/         # Aseprite integration for sprite import
│   └── inspector_tabs/          # Inspector UI enhancements
│
├── assets/                      # Game assets and resources
│   ├── aseprite/               # Source sprite files
│   ├── font/                   # Font resources
│   └── textures/               # Texture assets
│
├── scripts/                     # C# source code
│   └── code_blocks/
│       ├── base_block_types/   # Core block classes
│       ├── blocks/             # Specific block implementations
│       └── ui/                 # User interface components
│
├── scenes/                      # Godot scene files
│   └── code_blocks/
│
├── resources/                   # Godot resources
│   └── code_blocks/
│
├── build/                       # Build outputs
│   ├── android/                # Android APK
│   └── windows/                # Windows executable
│
├── Project 100.csproj          # C# project file
├── Project 100.sln             # Visual Studio solution
└── project.godot               # Godot project configuration
```

## 🧱 Code Block System

### Block Types

The system implements several types of code blocks:

#### Base Block Types

- **CodeBlock**: Abstract base class for all code blocks
- **StatementBlock**: Executable code statements
- **ExpressionBlock**: Value-returning expressions
- **VariableBlock**: Variable declarations and references
- **ConditionBlock**: Conditional logic blocks
- **MathCalculationBlock**: Mathematical operations

#### Specific Block Implementations

- **PrintBlock**: Output text to console
- **IntBlock**: Integer value blocks
- **IfBlock**: Conditional control flow

### Block Components

Each code block includes:

- **DragAreaComponent**: Enables drag-and-drop functionality
- **DropAreaComponent**: Defines drop zones for connecting blocks
- **TemplateEditor**: Customizes block appearance and parameters
- **BlockDefinition**: Resource-based block configuration

### UI Components

- **BlockCanvas**: Main editing canvas with zoom and pan controls
- **BlockPicker**: Block selection and spawning interface
- **BlockCategoryContainer**: Organizes blocks by category
- **Console**: Output display for executed code
- **DragManager**: Handles global drag-and-drop state

## 🎯 Core Features

### Visual Programming Interface

- Drag and drop blocks from the picker onto the canvas
- Connect blocks together to form programs
- Zoom and pan to navigate large programs
- Visual feedback for block connections

### Execution System

- Asynchronous block execution (`async Task Execute()`)
- Block value retrieval and propagation
- Console output for debugging and results

### Block Management

- Dynamic block spawning via `BlockSpawner`
- Resource-based block definitions for easy customization
- Global constants for block type management

## 🚀 Getting Started

### Prerequisites

- Godot Engine 4.5 or later
- .NET 8.0 SDK (or .NET 9.0 for Android builds)
- Visual Studio 2022 or JetBrains Rider (recommended for C# development)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/KimTRM/PROJECT-100.git
   ```

2. Open the project in Godot Engine:

   - Launch Godot 4.5
   - Import the `project.godot` file

3. Build the C# solution:
   - Open the project in Godot, which will generate necessary files
   - Or manually open `Project 100.sln` in your IDE

### Running the Project

1. **In Godot Editor**: Press F5 or click the "Run Project" button
2. **From Command Line**:
   ```bash
   dotnet build
   ```

## 🎮 Usage

1. **Select a Block**: Click on a block in the Block Picker
2. **Place on Canvas**: Drag the block onto the Block Canvas
3. **Connect Blocks**: Drop blocks into compatible drop zones
4. **Execute**: Run the visual program to see results in the console
5. **Navigate**: Use zoom controls and middle-mouse drag to navigate the canvas

## 🔧 Development

### Building

Use the provided build task or run:

```bash
dotnet build /property:GenerateFullPaths=true /consoleloggerparameters:NoSummary
```

### Platform-Specific Builds

- **Windows**: Builds to `build/windows/`
- **Android**: Builds to `build/android/` (requires .NET 9.0)

### Editor Plugins

This project uses two custom Godot plugins:

- **AsepriteWizard**: Streamlines sprite import from Aseprite files
- **Inspector Tabs**: Enhances the Godot inspector with tabbed interface

## 📝 Configuration

### Display Settings

- Base Resolution: 640x360
- Window Override: 1280x720
- Stretch Mode: Viewport
- Aspect: Expand

### Project Settings

- Assembly Name: `Project 100`
- Root Namespace: `Project100`

## 🤝 Contributing

This is a personal development project. If you're interested in contributing or have suggestions:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is currently in development. License information to be added.

## 🔗 Links

- **Repository**: [KimTRM/PROJECT-100](https://github.com/KimTRM/PROJECT-100)
- **Current Branch**: Dev-1.25
- **Godot Engine**: [godotengine.org](https://godotengine.org/)

## 📧 Contact

**Developer**: KimTRM

---

_Last Updated: January 2, 2026_
