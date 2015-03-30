namespace Cyclone2.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings) SettingsBase.Synchronized(new Settings()));

        [DefaultSettingValue("Vorze_USB,A10 Cyclone2"), DebuggerNonUserCode, ApplicationScopedSetting]
        public string cycloneDeviceNme
        {
            get
            {
                return (string) this["cycloneDeviceNme"];
            }
        }

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [ApplicationScopedSetting, DefaultSettingValue("一時停止"), DebuggerNonUserCode]
        public string labelPauseButtonDetault
        {
            get
            {
                return (string) this["labelPauseButtonDetault"];
            }
        }

        [DefaultSettingValue("再開"), DebuggerNonUserCode, ApplicationScopedSetting]
        public string labelPauseButtonPaused
        {
            get
            {
                return (string) this["labelPauseButtonPaused"];
            }
        }

        [DefaultSettingValue("プレイファイルの入力文字列が正しくありません。\r\n小数点の入った数字や英字は使えません。Ａ～Ｃ列のセル内には整数値のみを半角で入力してください。"), ApplicationScopedSetting, DebuggerNonUserCode]
        public string messageInvalidData
        {
            get
            {
                return (string) this["messageInvalidData"];
            }
        }

        [DebuggerNonUserCode, ApplicationScopedSetting, DefaultSettingValue("動画を選択してください。")]
        public string messageMovefileChoice
        {
            get
            {
                return (string) this["messageMovefileChoice"];
            }
        }

        [DefaultSettingValue("プレイファイルを選択してください。"), DebuggerNonUserCode, ApplicationScopedSetting]
        public string messagePlayfileChoice
        {
            get
            {
                return (string) this["messagePlayfileChoice"];
            }
        }

        [DefaultSettingValue("プレイファイルを指定してから動画を再生してください。"), ApplicationScopedSetting, DebuggerNonUserCode]
        public string messagePlayfileRequired
        {
            get
            {
                return (string) this["messagePlayfileRequired"];
            }
        }

        [ApplicationScopedSetting, DebuggerNonUserCode, DefaultSettingValue("16")]
        public int playtimesFontSize
        {
            get
            {
                return (int) this["playtimesFontSize"];
            }
        }

        [ApplicationScopedSetting, DebuggerNonUserCode, DefaultSettingValue("MS UI Gothic")]
        public string trimMessageFontName
        {
            get
            {
                return (string) this["trimMessageFontName"];
            }
        }

        [DebuggerNonUserCode, ApplicationScopedSetting, DefaultSettingValue("9")]
        public int trimMessageFontSize
        {
            get
            {
                return (int) this["trimMessageFontSize"];
            }
        }
    }
}

