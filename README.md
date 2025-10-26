# Cities: Skylines 2 - Demand Control (.NET 8)

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
