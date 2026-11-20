# RetroBar Offline — RecoveryTechCoLtd

這是一份以使用者提供的 RetroBar `master` 原始碼封存（封存內檔案日期 2026-11-16）為基礎，製作的 **完整 Windows 桌面專案**，不是修改工具包。保留官方工作列邏輯、圖片、主題及翻譯資源，移除自動更新與會主動打開網頁的介面。專案可使用 Visual Studio 2026（或只用 .NET 10 SDK 命令列） 在 Windows 建置；目前尚未在 Windows 實際編譯或執行，請以編譯與實機測試結果為準。

## 已移除／改寫

- 刪除 `RetroBar/Utilities/Updater.cs`，並在 `App.xaml.cs`、`WindowManager.cs`、`Taskbar.xaml.cs` 移除呼叫和更新選單。
- 刪除 `Settings.CheckForUpdates` 及設定頁面的更新勾選項（舊設定 JSON 的同名欄位不會觸發任何更新）。
- 移除會下載 .NET 或向網站取得最新版本的原本 `installer.iss`；本分支不附網路安裝器。
- 移除設定視窗「開啟 GitHub」及「網路日期格式說明」連結，改用本機說明。
- 停用自動建立登錄表開機啟動項的 UI；原分支不會主動設定開機自啟。
- 停用從任意 AppData 或執行檔旁載入自訂 XAML；只容許來源封存中內建的 66 個主題／語言檔（SHA-256 檢查）；主題可能引用其他 XAML，因此載入前會檢查整組內建檔案。
- 移除原始 GitHub Actions、自動依賴更新、`Nerdbank.GitVersioning` 建置套件及原來的 publish profiles。
- 產出 `RetroBarOffline.exe`；設定改存 `%LOCALAPPDATA%\RetroBarOffline`，不覆蓋官方 RetroBar 設定。

## 編譯 Windows 11 版本

**必要軟體**：Windows 11、.NET 10 **SDK**（非僅 Runtime），可選擇 Visual Studio 2026（18.0 或更新）+ .NET 桌面開發。**不需要 Python**；專案附有 PowerShell 原始碼稽核，另外提供 Python 版本供跨平台查驗。

第一種方法：PowerShell 在專案根目錄執行：

```powershell
.\scripts\Audit-Offline.ps1
.\scripts\Build-Windows.ps1 -Runtime win-x64
```

如果 PowerShell 禁止執行腳本，可用當前視窗的 `Set-ExecutionPolicy -Scope Process Bypass`，不需改動全系統原則。

第二種方法：不使用 PowerShell，用 .NET CLI：

```powershell
dotnet restore .\RetroBar\RetroBar.csproj -r win-x64 --configfile NuGet.Config
dotnet publish .\RetroBar\RetroBar.csproj -c Release -f net10.0-windows -r win-x64 --self-contained true --no-restore -p:PublishSingleFile=false -p:PublishTrimmed=false -o .\out\win-x64
```

**輸出**：`out\win-x64\RetroBarOffline.exe` 與其相依 DLL、內建主題與語言。**整個 `out\win-x64` 都要保留**；不是只有 EXE 一個檔。

**重要**：第一次 `dotnet restore` 需連往 NuGet 取得原作者原本使用的 `ManagedShell` 0.0.372 與 `gong-wpf-dragdrop` 3.1.1，以及建置所需相依套件；**這只發生在編譯電腦，不是程式執行時的背景下載**。`--self-contained true` 使發布產物不需在使用者電腦下載 .NET Runtime。完成過一次 RID 對應的 restore 之後，使用 `Build-Windows.ps1 -NoRestore` 可避免再次還原，但 SDK/工作負載及 NuGet 快取必須完整。

## Windows 防火牆（在第一次啟動前）

以 **系統管理員**開啟 PowerShell，於專案根目錄執行：

```powershell
.\scripts\Block-Network.ps1 -Action Install -ExecutablePath ".\out\win-x64\RetroBarOffline.exe"
.\scripts\Block-Network.ps1 -Action Status
```

它會建立**指定 EXE 路徑**的進站、出站 `Block` 規則。若移動 EXE，需重新執行 `-Action Install` 更新路徑；若系統或組織防火牆原則無法套用規則，不能把它當成安全隔離。要移除規則：

```powershell
.\scripts\Block-Network.ps1 -Action Remove
```

**為什麼不能承諾 100% 不連網？** 原始碼掃描無法涵蓋所有 NuGet 二進位依賴、Windows 系統 DLL 或其他程式。RetroBar 是工作列，正常操作會啟動**其他**應用程式（例如瀏覽器）；擋住 `RetroBarOffline.exe` 的網路，不會擋住瀏覽器或 Windows 系統服務。若需要系統級絕對隔離，請在無網路的 VM／隔離環境使用，並於 Windows 實測封包流量。此專案已移除第一方程式碼的已知聯網入口，但並非已經通過獨立第三方安全驗證。

## 稽核

```powershell
.\scripts\Audit-Offline.ps1
```

或有 Python 3 時使用 `python .\security\audit_offline.py`。

會檢查 C# 中典型網路 API、網站開啟路徑、XAML 中的遠端資源、66 個內建資源的 SHA-256、是否仍留有 Updater／有連網的安裝器和 GitHub Actions。這是**靜態檢查**；Windows 上請另以資源監視器、TCPView / Wireshark 等實測。

## 上傳到你自己的 GitHub

請先在你的 GitHub 組織 `RecoveryTechCoLtd` 建立空白儲存庫 `RetroBar-Offline`。**不要在網頁建立 README 或 .gitignore**（這份專案已有）。在 ZIP 解壓縮後的**專案根目錄**執行：

```powershell
git init
git branch -M main
git add .
git commit -m "Initial offline RetroBar source fork"
git remote add origin https://github.com/RecoveryTechCoLtd/RetroBar-Offline.git
git push -u origin main
```

GitHub 會要求登入；HTTPS 推送請使用 Git Credential Manager 或 GitHub CLI 授權，不要把 GitHub 密碼／Token 寫進程式碼。如果 GitHub 組織不允許你建立儲存庫，需要先向組織管理員取得權限。

建議先設 `Private`，自行在 Windows 確認可編譯與執行後再決定是否公開。原作者 `dremin/RetroBar` 採 Apache License 2.0，保留 `LICENSE`、原來的權利聲明與 `UPSTREAM_README.md`，不能把程式宣稱成從零撰寫。

## 授權與來源

本儲存庫是基於 RetroBar 上游原始碼的衍生版本；上游原始碼與 README 保存在 `UPSTREAM_README.md`，著作權及授權詳見 `LICENSE`。本專案並不代表上游作者或得到其安全保證。
