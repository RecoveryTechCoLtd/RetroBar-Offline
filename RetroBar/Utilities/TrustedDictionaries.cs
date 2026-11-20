// GENERATED FROM THE USER-SUPPLIED RetroBar-master.zip SOURCE FILES.
// Any changed or new theme/language XAML will be rejected by default.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace RetroBar.Utilities
{
    internal static class TrustedDictionaries
    {
        private static readonly Dictionary<string, string> Sha256ByResource = new(StringComparer.Ordinal)
        {
            ["Themes/System Vista.xaml"] = "3686a26d52f01516038302997eb4615ed6e15b35147d3925ed9edce699b33f51",
            ["Themes/System XP.xaml"] = "0706d62085b56c20d90f604dc7949772a1d771e2b30dfcb9b3929df5ea02fc62",
            ["Themes/System.xaml"] = "78e2eebd7dcd95ad74f3054a3a03d15c21e6d59e928ca2ff9f170ee7604a4290",
            ["Themes/Watercolor.xaml"] = "2c0daf6aca10ad7dd5e67f382fa76230f8d73603aa311415e39f2085164e09c4",
            ["Themes/Windows 2000.xaml"] = "6e36824662ab41ff291f19bd0876cf26eb7a50f235172b4f11b32eb5328a283a",
            ["Themes/Windows 95-98.xaml"] = "516d2925d3c090099af7aac5ff527b2265b420e575bb641014a728fb094d82c8",
            ["Themes/Windows Longhorn Aero.xaml"] = "0c63ed716b1a21834e90a00f5cf7ce61957a189d1d86cfed3ff4e0d5195dbb14",
            ["Themes/Windows Me.xaml"] = "5cdf5578abb597651cdfc651a5c7f84fbfba5e0fa5e47dd923eb9cc41e941618",
            ["Themes/Windows Vista Aero.xaml"] = "924405b441bc7add3fd6264e1c2588d51ef05e89dcbb43b01190b4833709a9b9",
            ["Themes/Windows Vista Basic.xaml"] = "b50d93531fc2bcb37412099a39ee5aaa6b5d65436512b9095ac92cef6aabfd38",
            ["Themes/Windows Vista Classic.xaml"] = "c2f902e284d021111aefb37a37835da4306befb3260c99536c923e177616aa13",
            ["Themes/Windows XP Blue.xaml"] = "a9a1acc62939b14aabadc95ea9e4e25e59c9ee94fc7b1fefb5d17211a569ca55",
            ["Themes/Windows XP Classic.xaml"] = "476a809a6fdd6ede62749f501e9d37a274ec157917a4a8f6509041a23c9d0e5b",
            ["Themes/Windows XP Embedded Style.xaml"] = "610a679298d933213fa99b4f7251b02921edad1de3a02406b5d827e1d400406e",
            ["Themes/Windows XP Olive Green.xaml"] = "993944746add197f90c33685f4aa7f18410211c1f089b8e0e61563b44ec73ef9",
            ["Themes/Windows XP Royale Noir.xaml"] = "426de4e43c7907e309572d12e61b26e657929ea576baf45557bc00d11f074f91",
            ["Themes/Windows XP Royale.xaml"] = "595f6e9d189b0c60e3b96a6470821894fbc9610ae8b78e64537449606c72b39c",
            ["Themes/Windows XP Silver.xaml"] = "1079ff90ca4c9d30765e962cc5874b5ce3db89875d39ee4092e10646942c3ca7",
            ["Themes/Windows XP Zune Style.xaml"] = "caa2d2505723126b1995dd958621878bba72792edf93f8dd2ecf00fda0f99e8e",
            ["Languages/#U00edslenska.xaml"] = "c2e18ae31595580e1b429daadb675c5fac588e9f1ea6e42f40cd51324d7986b6",
            ["Languages/#U010de#U0161tina.xaml"] = "2782a5824f0eeef0f840aa011e1f38e10a731e10aa8c38e34e4e2145c6596f36",
            ["Languages/#U03b5#U03bb#U03bb#U03b7#U03bd#U03b9#U03ba#U03ac.xaml"] = "6259b686131efbf72cdfdf03bac764be264b7e127f31cae47a757759c80ccd05",
            ["Languages/#U041c#U0430#U043a#U0435#U0434#U043e#U043d#U0441#U043a#U0438.xaml"] = "4f815dc9e1bf69f838240d3c79b68760517c5ee4de146fe25cf93ed6ffa486a3",
            ["Languages/#U0431#U0435#U043b#U0430#U0440#U0443#U0441#U043a#U0430#U044f.xaml"] = "58f5337d2def0d2f0ac949c50d6ab00fe1bf03df450a68b7366031da2a6e242e",
            ["Languages/#U0431#U044a#U043b#U0433#U0430#U0440#U0441#U043a#U0438.xaml"] = "3bee2cf7d06a324a1ea956dc700872b84a42f051645c8bc879d703b855656c97",
            ["Languages/#U0440#U0443#U0441#U0441#U043a#U0438#U0439.xaml"] = "a79301bb5f1726b180e4abb0bf3376ed8d39aa597dece1a7b51dda6086a1ec97",
            ["Languages/#U0441#U0440#U043f#U0441#U043a#U0438.xaml"] = "6f2ad200d020fdc9ffd4ff27490dbd1b4b76d8c8308647182fc5cc609368f74a",
            ["Languages/#U0443#U043a#U0440#U0430#U0457#U043d#U0441#U044c#U043a#U0430.xaml"] = "36b6d2dcf187c721f7e6d23235c8d83ec6eea0b87aa87589330b4dc611edee3e",
            ["Languages/#U05e2#U05d1#U05e8#U05d9#U05ea.xaml"] = "bdc2db5a7c08deefa061d1e7fe0d373691992d4763002584963e3017cf485814",
            ["Languages/#U0627#U0644#U0639#U0631#U0628#U064a#U0629.xaml"] = "f17a04b8df23fecaa465a1c0a36404ee1cd1a876b327a8b44911da04c945f360",
            ["Languages/#U0641#U0627#U0631#U0633#U06cc.xaml"] = "5d992b1da18a2f5376d54dce7003ab42fa6704f6d9e0a77d6a49617d7fd32544",
            ["Languages/#U0e44#U0e17#U0e22.xaml"] = "39dd19b33e52eda6ee88f8e4be5d8c142853b753d2b58f59d5a11002f534d5b8",
            ["Languages/#U4e2d#U6587(#U7b80#U4f53).xaml"] = "a123237560f3d2bb6bfbaeba8bbacc672aabba910b7bd9c64bfc522ae0ab5d44",
            ["Languages/#U4e2d#U6587(#U7e41#U9ad4).xaml"] = "f4710885360ffccf4844c659e57cf7748642948fb5b52cf8796eaf8bdd0d6689",
            ["Languages/#U65e5#U672c#U8a9e.xaml"] = "e41f211b09fbc17ba34e9e0d3d5d47577abc27da67d127647a0001a155309d3a",
            ["Languages/#Ud55c#Uad6d#Uc5b4.xaml"] = "d2d1793212fba81e751114fff7f15508476216c8b8a45f6869e293659fb42eb9",
            ["Languages/Cymraeg.xaml"] = "a47a1d39a529a5c21ca01c9644811adc8344ea22f6c70a84f4de072a0a0b71c0",
            ["Languages/Deutsch.xaml"] = "9e27e0a80e7e1858afb83946e469f480eab9f6e419674e926e91c664587fafe2",
            ["Languages/English (United Kingdom).xaml"] = "fdc948a7ea6db89c482db6f24a6b47cbb7045c5d65211d19c2136af419c22a29",
            ["Languages/English.xaml"] = "ecb1f8b1fee7fec26de8e4c0924b9473556326fa894415c3c436d8515c538e40",
            ["Languages/Indonesia.xaml"] = "015b1f5168a2946e95c11f854827ba7a642485b30dd39fd57f647d2dbff1b4f8",
            ["Languages/L#U00ebtzebuergesch.xaml"] = "d1b831553392de89254165ca7b84282dbf4450fe01355f62cde6c6342ec92726",
            ["Languages/Malti.xaml"] = "aa659ceb90a37a4c7079bb00e55cc314cff43527a30f0ce3cace2f5cf2236da5",
            ["Languages/Melayu.xaml"] = "1abb5c9cf6676a62765402c4a7e3769cf8979a63fe126ab2307450ee2f9046b0",
            ["Languages/Nederlands.xaml"] = "d8f02f2582009c738655e4e642aac786e694cdd24864d38082a70c7860698725",
            ["Languages/Norsk (bokm#U00e5l).xaml"] = "17dc7010dffb13ca9c6e00e9d943cdd526c3c3adc8d544f1529ae5f763dde077",
            ["Languages/Suomi.xaml"] = "e21138bd372de3117b612f780bde094b4f398567fa7a1afd469d7a6edacebe95",
            ["Languages/T#U00fcrk#U00e7e.xaml"] = "a00847a4dc349fe0fbae7b0f381156da02918c8a1c3401ca3aef550a0829d1a2",
            ["Languages/Ti#U1ebfng Vi#U1ec7t.xaml"] = "32d048c0c83c71e1b47b5e4bc593380ed53938b817f4399a5049d91aa316f780",
            ["Languages/catal#U00e0.xaml"] = "01f0102d6c7bb478fdbad6d712a1f47a9ee23462c4ee1b3fdf7a3a71708aaf4c",
            ["Languages/dansk.xaml"] = "cd90156354700267a285605044a3351d89e2122ef22fe290ae83b74934d3484a",
            ["Languages/espa#U00f1ol.xaml"] = "90ec516c414131defa634fb74a56c16cb73d2ddd4334a6227dd3c291c8b757e2",
            ["Languages/euskara.xaml"] = "22529054ca18fc666516c41d2ac749c7925ce55d2c176613e539924aa6d7f828",
            ["Languages/fran#U00e7ais.xaml"] = "4512bad752e39134dfa8b7645b27f3051d478a76f681d7969ce7546f362a93cb",
            ["Languages/hrvatski.xaml"] = "1dca5b55b9c082ef5b9cd0ca7d65698cb6c5d8b7a950f117468fc212cd5b3cb6",
            ["Languages/italiano.xaml"] = "838731d5eeeb60b2f250a883fe6194243110bc81820e9cfe0a54baee18b427cb",
            ["Languages/latvie#U0161u.xaml"] = "07a067ab4a3ca109194eefa08f21df198f86380ea73c3406360880dca447db11",
            ["Languages/lietuvi#U0173.xaml"] = "dacc9f0d095e5083e45f95acb0e502c58fe8d153e68f75b8fc020d41482faf00",
            ["Languages/magyar.xaml"] = "49a043ca99f58f16ed13d3f3e22f9339360a7ba7dcae81bad37ec97a4107c27f",
            ["Languages/polski.xaml"] = "992de2a667500ef46d7d07c77e93559dcb21e2be0be4f4decd39051b21415f21",
            ["Languages/portugu#U00eas.xaml"] = "928423f0208ddfa19de81bbc74b6e86386ac59967adca5e523ea44df6e0a1fc9",
            ["Languages/rom#U00e2n#U0103.xaml"] = "edf0a345479d00d677c182a5494f3b88034725c75b3b1e132312d33afa32d6c7",
            ["Languages/sloven#U010dina.xaml"] = "e1dfbf3f6b9794494cd85117f9daf70898aa0397e66b4b27403383326652afa8",
            ["Languages/sloven#U0161#U010dina.xaml"] = "5c1ad48fe5f4b1fcc2f0385993d4f557522961a25aa1fcf8f3d8374e289c5b6a",
            ["Languages/srpski.xaml"] = "1187669c21bd1e51efc985fd37f9aac52f7f2f6e91c714ef25ede907810b632e",
            ["Languages/svenska.xaml"] = "8d11380958bc4067bf72605df379ef6a448ae081914b9ffe1b5edceaa1a9538a",
        };

        // WPF theme files can import other XAML using ResourceDictionary.Source.
        // Verify the ENTIRE built-in chain before loading any loose dictionary,
        // otherwise a modified nested resource could bypass a per-file check.
        public static bool VerifyEveryBundledFile()
        {
            foreach (string key in Sha256ByResource.Keys)
            {
                // These two defaults are compiled into the WPF assembly as BAML.
                if (key == "Themes/System.xaml" || key == "Languages/English.xaml")
                    continue;

                int slash = key.IndexOf('/');
                string folder = key.Substring(0, slash);
                string name = Path.GetFileNameWithoutExtension(key);
                if (!TryGetVerifiedFile(folder, name, out _))
                    return false;
            }

            return true;
        }

        public static List<string> GetNames(string folder)
        {
            return Sha256ByResource.Keys
                .Where(key => key.StartsWith(folder + "/", StringComparison.Ordinal))
                .Select(key => Path.GetFileNameWithoutExtension(key))
                .OrderBy(name => name, StringComparer.CurrentCulture)
                .ToList();
        }

        public static bool TryGetVerifiedFile(string folder, string name, out string fullPath)
        {
            fullPath = null;
            if (string.IsNullOrEmpty(name) || name != Path.GetFileName(name))
                return false;

            string key = folder + "/" + name + ".xaml";
            if (!Sha256ByResource.TryGetValue(key, out string expectedHash))
                return false;

            string candidate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder, name + ".xaml");
            if (!File.Exists(candidate))
                return false;

            try
            {
                using var stream = File.OpenRead(candidate);
                string actualHash = Convert.ToHexString(SHA256.HashData(stream)).ToLowerInvariant();
                if (!string.Equals(actualHash, expectedHash, StringComparison.Ordinal))
                    return false;

                fullPath = Path.GetFullPath(candidate);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
    }
}
