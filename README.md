# 📸 PicCaptionr

```
  ____  _       ____            _   _                  
 |  _ \(_) ___ / ___|__ _ _ __ | |_(_) ___  _ __  _ __ 
 | |_) | |/ __| |   / _` | '_ \| __| |/ _ \| '_ \| '__|
 |  __/| | (__| |__| (_| | |_) | |_| | (_) | | | | |   
 |_|   |_|\___|\____\__,_| .__/ \__|_|\___/|_| |_|_|   
                         |_|                           
```

**AI-powered tool to automatically generate Instagram-style captions for your photos – on Windows, Linux and macOS.**

![Header](/docs/header.png)

[![Build Console](https://github.com/tsjdev-apps/piccaptionr/actions/workflows/build-consoleapp.yml/badge.svg)](https://github.com/tsjdev-apps/piccaptionr/actions/workflows/build-consoleapp.yml)
[![Build WPF](https://github.com/tsjdev-apps/piccaptionr/actions/workflows/build-wpfapp.yml/badge.svg)](https://github.com/tsjdev-apps/piccaptionr/actions/workflows/build-wpfapp.yml)
[![Build Avalonia](https://github.com/tsjdev-apps/piccaptionr/actions/workflows/build-avaloniaapp.yml/badge.svg)](https://github.com/tsjdev-apps/piccaptionr/actions/workflows/build-avaloniaapp.yml)

## ✨ Features

- 🧠 Uses OpenAI's Vision API to understand and describe your photos
- 🌍 Multilingual output (English, German, Spanish)
- 🗞️ Output as `.txt` file
- 🖼️ Supports multiple images
- ⚙️ Flexible backend: OpenAI or Azure OpenAI
- 💻 Available as:
  - **Console App** (cross-platform)
  - **WPF Desktop App** (Windows only)
  - **Avalonia App** (modern cross-platform GUI for Windows, Linux & macOS)

## 🚀 Getting Started

### 🔧 Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- An API key from [OpenAI](https://platform.openai.com) or your Azure OpenAI deployment

### 🖥️ Console App

```bash
cd PicCaptionr.ConsoleApp
dotnet run
```

You will be prompted to enter:

- your host (OpenAI / Azure OpenAI)
- your API key, Deployment name and Endpoint (depending on the host)
- the path to the folder containing the images
- target language
- optional additional instructions
- the path to an output folder

Output file (`.txt`) will be saved in the given directory

### 🪟 WPF Desktop App

```bash
cd PicCaptionr.WPFApp
dotnet build
dotnet run
```

- Requires Windows and .NET Desktop Runtime
- Simple graphical interface with:
  - Folder pickers
  - Language and model selection
  - Progress bar and log viewer

### 🖼️ Avalonia App (Cross-platform GUI)

```bash
cd PicCaptionr.AvaloniaApp
dotnet build
dotnet run
```

- Works on Windows, Linux, and macOS
- Modern responsive UI using Avalonia UI
- Cross-platform folder picker and dynamic layout
- Provides the same functionality as the WPF app, but with platform-independent UI

> 💡 **macOS Notice**: You may need to allow the app to run via terminal:
>
> ```bash
> chmod +x PicCaptionr.AvaloniaApp
> xattr -d com.apple.quarantine PicCaptionr.AvaloniaApp
> ```

## 📦 CI/CD with GitHub Actions

All apps are built and published automatically via GitHub Actions:

- ✅ Console builds:
  - `win-x64`, `linux-x64`, `osx-x64`, `osx-arm64`
- ✅ WPF builds:
  - `win-x64` (single `.exe`)
- ✅ Avalonia builds:
  - `win-x64`, `linux-x64`, `osx-arm64` (self-contained)

  Artifacts are uploaded for each platform

## 🔐 Configuration

All variants require:

- An **API key** from OpenAI or Azure
- For Azure OpenAI:
  - **Endpoint URL**
  - **Deployment name**

Credentials are entered at runtime and never stored permanently.

## 🗂️ Folder Structure

```text
PicCaptionr.ConsoleApp     # Console-based UI (cross-platform)
PicCaptionr.WPFApp         # Windows-only WPF GUI
PicCaptionr.AvaloniaApp    # Cross-platform GUI (Avalonia)
PicCaptionr                # Shared models, services, and logic
```

## 📷 Screenshots

### Console App
  
![Console](/docs/console-01.png)

![Console](/docs/console-02.png)

### WPF App
  
![WPF](/docs/wpf-01.png)

### Avalonia App
  
![Avalonia](/docs/avalonia-01.png)

## 📄 Sample Output

![Sample Image](/docs/sample-image.jpg)

A typical output entry in JSON format might look like this:

```json
[
  {
    "ImageName": "tsjreiseblog_28-Jul-2025.jpg",
    "ImageMetaData": {
      "CaptureDate": null,
      "Latitude": null,
      "Longitude": null
    },
    "OpenAIResponse": {
      "RawContent": "Exploring the charming streets and historic sights of Civitavecchia, Italy 🇮🇹✨ From vibrant scooters to stunning architecture and scenic harbor views—every corner tells a story. Can't wait to see more of this beautiful port city! 🚤🏛️🌊\n\n#Civitavecchia #ItalyTravel #PortCity #HistoricItaly #TravelItaly #ItalianArchitecture #HarborViews #ScooterLife #MediterraneanVibes #TravelDestinations",
      "FormattedContent": "Exploring the charming streets and historic sights of Civitavecchia, Italy 🇮🇹✨ From vibrant scooters to stunning architecture and scenic harbor views—every corner tells a story. Can't wait to see more of this beautiful port city! 🚤🏛️🌊\r\n\r\n#Civitavecchia #ItalyTravel #PortCity #HistoricItaly #TravelItaly #ItalianArchitecture #HarborViews #ScooterLife #MediterraneanVibes #TravelDestinations",
      "InputTokens": 800,
      "OutputTokens": 94
    }
  }
]
```

## 📓 License

MIT License © [tsjdev-apps](https://github.com/tsjdev-apps)

## 💬 Feedback & Contributions

Found a bug? Want to improve the UI?  
Open an [Issue](https://github.com/tsjdev-apps/piccaptionr/issues) or submit a [Pull Request](https://github.com/tsjdev-apps/piccaptionr/pulls).  
We ❤️ feedback and contributions!
