using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace RetroBar.Utilities
{
    public class DictionaryManager : IDisposable
    {
        private const string DICT_DEFAULT = "System";
        private const string DICT_EXT = "xaml";

        private const string LANG_DEFAULT = DICT_DEFAULT;
        private const string LANG_FALLBACK = "English";
        private const string LANG_FOLDER = "Languages";
        private const string LANG_EXT = DICT_EXT;

        public const string THEME_DEFAULT = DICT_DEFAULT;
        private const string THEME_FOLDER = "Themes";
        private const string THEME_EXT = DICT_EXT;

        private ResourceDictionary _selectedTheme;

        public DictionaryManager()
        {
            Settings.Instance.PropertyChanged += Settings_PropertyChanged;
        }

        public void SetThemeFromSettings()
        {
            // App.xaml supplies the base theme for every selection.
            if (_selectedTheme != null)
            {
                GetMergedDictionaries().Remove(_selectedTheme);
                _selectedTheme = null;
            }

            if (Settings.Instance.Theme.StartsWith(THEME_DEFAULT))
            {
                SetSystemThemeParams();
            }
            else
            {
                ClearSystemThemeParams();
            }

            if (Settings.Instance.Theme != THEME_DEFAULT)
            {
                SetTheme(Settings.Instance.Theme);
            }
        }

        private void SetSystemThemeParams()
        {
            Application.Current.Resources["GlobalFontFamily"] = SystemFonts.CaptionFontFamily;
        }

        private void ClearSystemThemeParams()
        {
            Application.Current.Resources.Remove("GlobalFontFamily");
        }

        private void SetTheme(string theme)
        {
            SetDictionary(theme, THEME_FOLDER, THEME_DEFAULT, THEME_EXT, 0);
        }

        private static Collection<ResourceDictionary> GetMergedDictionaries()
        {
            return Application.Current.Resources.MergedDictionaries;
        }

        public void SetLanguageFromSettings()
        {
            SetLanguage(LANG_FALLBACK);
            if (Settings.Instance.Language == LANG_DEFAULT)
            {
                var currentUICulture = System.Globalization.CultureInfo.CurrentUICulture;
                string systemLanguageParent = currentUICulture.Parent.NativeName;
                string systemLanguage = currentUICulture.NativeName;
                ManagedShell.Common.Logging.ShellLogger.Info
                    ($"Loading system language (if available): {systemLanguageParent}, {systemLanguage}");
                SetLanguage(systemLanguageParent);
                SetLanguage(systemLanguage);
            }
            else
            {
                SetLanguage(Settings.Instance.Language);
            }
        }

        private void SetLanguage(string language)
        {
            SetDictionary(language, LANG_FOLDER, LANG_FALLBACK, LANG_EXT, 1);
        }

        private void SetDictionary(string dictionary, string dictFolder, string dictDefault, string dictExtension, int dictType)
        {
            Uri source;

            if (dictionary == dictDefault)
            {
                // The original defaults are compiled application XAML resources.
                source = new Uri($"{dictFolder}/{dictDefault}.{dictExtension}", UriKind.Relative);
            }
            else
            {
                // NO arbitrary XAML from AppData, remote URI, or manually copied files.
                // All accepted files must match the SHA256 of an original bundled resource.
                if (!TrustedDictionaries.VerifyEveryBundledFile() ||
                    !TrustedDictionaries.TryGetVerifiedFile(dictFolder, dictionary, out string filePath))
                {
                    ManagedShell.Common.Logging.ShellLogger.Warning("Bundled XAML integrity check failed; external XAML was not loaded.");
                    return;
                }

                source = new UriBuilder(Uri.UriSchemeFile, string.Empty)
                {
                    Path = filePath
                }.Uri;
            }

            try
            {
                var loaded = new ResourceDictionary { Source = source };
                GetMergedDictionaries().Add(loaded);
                if (dictType == 0)
                    _selectedTheme = loaded;
            }
            catch (Exception e)
            {
                ManagedShell.Common.Logging.ShellLogger.Error($"Error loading bundled dictionary: {e.Message}");
            }
        }

        public List<string> GetThemes()
        {
            var themes = new List<string> { THEME_DEFAULT };
            themes.AddRange(TrustedDictionaries.GetNames(THEME_FOLDER)
                .Where(name => name != THEME_DEFAULT));
            return themes;
        }

        public List<string> GetLanguages()
        {
            var languages = new List<string> { LANG_DEFAULT, LANG_FALLBACK };
            languages.AddRange(TrustedDictionaries.GetNames(LANG_FOLDER)
                .Where(name => name != LANG_FALLBACK && name != LANG_DEFAULT));
            return languages;
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.Language))
            {
                SetLanguageFromSettings();
            }
            if (e.PropertyName == nameof(Settings.Theme))
            {
                SetThemeFromSettings();
            }
        }

        public void Dispose()
        {
            Settings.Instance.PropertyChanged -= Settings_PropertyChanged;
        }
    }
}