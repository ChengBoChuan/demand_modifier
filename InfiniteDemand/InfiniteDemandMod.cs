using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;

namespace InfiniteDemand;

/// <summary>
/// Infinite Demand Mod 主類別 - 實作 IMod 介面以整合到 Cities: Skylines 2
/// </summary>
public class InfiniteDemandMod : IMod
{
    /// <summary>
    /// 日誌記錄器 - 用於記錄 mod 的資訊和錯誤訊息
    /// SetShowsErrorsInUI(false) 防止錯誤訊息顯示在遊戲 UI 中
    /// </summary>
    public static ILog log = LogManager.GetLogger($"{nameof(InfiniteDemand)}.{nameof(InfiniteDemandMod)}").SetShowsErrorsInUI(false);

    /// <summary>
    /// 模組設定實例 - 提供全域存取的設定選項
    /// </summary>
    public static InfiniteDemandSettings? Settings { get; private set; }

    /// <summary>
    /// Mod 載入時執行 - 註冊 Harmony 補丁到遊戲系統
    /// </summary>
    /// <param name="updateSystem">遊戲更新系統</param>
    public void OnLoad(UpdateSystem updateSystem)
    {
        log.Info(nameof(OnLoad));

        // 建立並註冊模組設定
        Settings = new InfiniteDemandSettings(this);
        Settings.RegisterInOptionsUI();
        
        log.Info($"設定已載入 - 住宅需求: {Settings.EnableResidentialDemand}, 商業需求: {Settings.EnableCommercialDemand}, 工業需求: {Settings.EnableIndustrialDemand}");

        // 建立 Harmony 實例並自動套用所有標記的補丁
        var harmony = new HarmonyLib.Harmony("net.johnytoxic.infinitedemand");
        harmony.PatchAll();
        
        log.Info("Harmony 補丁已全部套用");
    }

    /// <summary>
    /// Mod 卸載時執行 - 清理資源
    /// </summary>
    public void OnDispose()
    {
        log.Info(nameof(OnDispose));
        
        // 儲存並清理設定
        if (Settings != null)
        {
            Settings.UnregisterInOptionsUI();
            Settings = null;
        }
    }
}
