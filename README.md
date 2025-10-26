# Cities: Skylines 2 - Demand Control (.NET 8)

[English](#english) | [繁體中文](#繁體中文)

---

## English

Manage the demand in your city.

**This is a reimplementation using .NET 8 framework**

### Project Description

This project is a demand control mod repository for Cities: Skylines 2. It uses the Harmony library to intercept the game's demand system update cycles, dynamically modifying demand values for residential, commercial, industrial, and other building types. Through reflection techniques, it accesses the game's private fields and sets demand values to maximum (255) during each simulation cycle, allowing players to develop their cities without restrictions.

This repository contains the following mods:
- [Infinite Demand](./InfiniteDemand/) - Sets all demand types to infinite

### .NET 8 Version Features

This project is a .NET 8 port of the original .NET Framework 4.7.2 version, featuring the following improvements:

#### Technical Improvements
- Uses .NET 8 target framework
- Enables latest C# language features
- Uses modern C# syntax (file-scoped namespaces, collection expressions)
- Enables nullable reference types
- Enables implicit using statements
- Removes legacy test dependencies (Microsoft.QualityTools.Testing.Fakes)

#### Code Improvement Examples
- Uses file-scoped namespaces (`namespace InfiniteDemand;`)
- Uses collection expressions (`[element1, element2, ...]`)
- Better code readability and maintainability

### Prerequisites

1. **CSII_TOOLPATH Environment Variable**: Must be set (user-level) pointing to the CS2 mod tools directory
2. **.NET 8 SDK**: Requires .NET 8 SDK to build the project
3. **Cities: Skylines 2**: Game must be installed

### Building

```powershell
# Standard build
msbuild CSL2DemandControl_NET8.sln /p:Configuration=Release

# Debug build
msbuild CSL2DemandControl_NET8.sln /p:Configuration=Debug
```

### Troubleshooting

This mod is in early development stages. There may be some bugs or unhandled edge cases. The demand "system" in Cities: Skylines 2 is much more complex than in 1. If you find any bugs or missing features, please report them here: https://github.com/ChengBoChuan/demand_modifier/issues

### Differences from Original Version

This version has identical functionality to the original .NET Framework 4.7.2 version but is built using the modern .NET 8 framework and C# language features.

### License

Please refer to the original project's license information.

---

## 繁體中文

管理您城市中的需求。

**這是使用 .NET 8 框架重新實作的版本**

## 專案說明

本專案是 Cities: Skylines 2 的需求控制 mod 儲存庫，使用 Harmony 函式庫攔截遊戲的需求系統更新循環，動態修改住宅、商業、工業等各類建築的需求值。透過反射技術存取遊戲私有欄位，在每個模擬週期將需求值設定為最大值 (255)，讓玩家可以不受限制地發展城市。

此儲存庫包含以下 mod：
- [Infinite Demand](./InfiniteDemand/) - 將所有需求類型設置為無限

## .NET 8 版本特點

本專案是原始 .NET Framework 4.7.2 版本的 .NET 8 移植版本，具有以下改進：

### 技術改進
- 使用 .NET 8 目標框架
- 啟用 C# 最新語言功能
- 使用現代 C# 語法（file-scoped namespaces、collection expressions）
- 啟用可空參考型別（Nullable reference types）
- 啟用隱式 using 語句
- 移除舊版測試相依性（Microsoft.QualityTools.Testing.Fakes）

### 程式碼改進範例
- 使用 file-scoped namespaces（`namespace InfiniteDemand;`）
- 使用 collection expressions（`[element1, element2, ...]`）
- 更好的程式碼可讀性和維護性

## 先決條件

1. **CSII_TOOLPATH 環境變數**：必須設置（使用者層級）指向 CS2 mod 工具目錄
2. **.NET 8 SDK**：需要安裝 .NET 8 SDK 來建構專案
3. **Cities: Skylines 2**：遊戲必須安裝

## 建構

```powershell
# 標準建構
msbuild CSL2DemandControl_NET8.sln /p:Configuration=Release

# 偵錯建構
msbuild CSL2DemandControl_NET8.sln /p:Configuration=Debug
```

## 疑難排解

本 mod 處於早期開發階段。可能存在一些錯誤或尚未處理的邊緣情況。Cities: Skylines 2 中的需求「系統」比 1 中要複雜得多。如果您發現任何錯誤或缺少功能，請在此處報告：https://github.com/ChengBoChuan/demand_modifier/issues

## 與原始版本的差異

此版本與原始 .NET Framework 4.7.2 版本功能完全相同，但使用現代 .NET 8 框架和 C# 語言功能建構。

## 授權

請參閱原始專案的授權資訊。
