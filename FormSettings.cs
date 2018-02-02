using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BitWhiskey
{
    public partial class FormSettings : Form
    {
        public Form parent;
        public AppSettingsManager settingsManager = new AppSettingsManager();
        MySettings selSettings =null;

        public FormSettings()
        {
            InitializeComponent();

            LoadProfileList();
            comboBoxProfile.Text = Global.settingsInit.currentprofile;
        }
    private void TestSaveStandart()
        {
            MySettings settings = new MySettings();
            string settingsPath = settingsManager.GetSettingsFilePath("Standart", "settings.json");
            settings.Save(settingsPath);
        }
        private void LoadProfileList()
        {
            List<string> profiles=settingsManager.GetProfiles();
            comboBoxProfile.Items.Clear();
            comboBoxProfile.Items.AddRange(profiles.ToArray());
        }
        
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //            TestSaveStandart();return;

            string settingsPath = settingsManager.GetSettingsFilePath(comboBoxProfile.Text, "settings.json");
            selSettings.defaultlimitorders = checkBoxDefLimitTrade.Checked;
            ActivatePoloniexKey();
            ActivateBittrexKey();
            ActivateYobitKey();
            selSettings.Save(settingsPath);
            Global.settingsMain = selSettings;
            Global.settingsInit.currentprofile = comboBoxProfile.Text;
            Global.settingsInit.Save(Path.Combine(ApplicationPath.directoryAppBin, "init.json"));
            Close();
        }
        private void comboBoxProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            string settingsPath = settingsManager.GetSettingsFilePath(comboBoxProfile.Text, "settings.json");
            selSettings = MySettings.Load(settingsPath);
            if (selSettings.poloniexkey == "")
            {
                textBoxPoloniexKey.Text = "Empty";
                textBoxPoloniexSecret.Text = "Empty";
            }
            else
            {
                textBoxPoloniexKey.Text = "******";
                textBoxPoloniexSecret.Text = "******";
            }

            if (selSettings.bittrexkey == "")
            {
                textBoxBittrexKey.Text = "Empty";
                textBoxBittrexSecret.Text = "Empty";
            }
            else
            {
                textBoxBittrexKey.Text = "******";
                textBoxBittrexSecret.Text = "******";
            }

            if (selSettings.yobitkey == "")
            {
                textBoxYobitKey.Text = "Empty";
                textBoxYobitSecret.Text = "Empty";
            }
            else
            {
                textBoxYobitKey.Text = "******";
                textBoxYobitSecret.Text = "******";
            }

            checkBoxDefLimitTrade.Checked= selSettings.defaultlimitorders;
        }

        private void FormSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.Activate();
            if (parent.WindowState == FormWindowState.Minimized)
                parent.WindowState = FormWindowState.Normal;
        }
        private void ActivatePoloniexKey()
        {
            if (textBoxPoloniexKey.Text != "******" && textBoxPoloniexKey.Text != "Empty")
            {
              selSettings.poloniexkey = AppCrypt.EncryptData(textBoxPoloniexKey.Text);
              selSettings.poloniexsecret = AppCrypt.EncryptData(textBoxPoloniexSecret.Text);
            }
        }
        private void ActivateBittrexKey()
        {
            if (textBoxBittrexKey.Text != "******" && textBoxBittrexKey.Text != "Empty")
            {
                selSettings.bittrexkey = AppCrypt.EncryptData(textBoxBittrexKey.Text);
                selSettings.bittrexsecret = AppCrypt.EncryptData(textBoxBittrexSecret.Text);
            }
        }
        private void ActivateYobitKey()
        {
            if (textBoxYobitKey.Text != "******" && textBoxYobitKey.Text != "Empty")
            {
                selSettings.yobitkey = AppCrypt.EncryptData(textBoxYobitKey.Text);
                selSettings.yobitsecret = AppCrypt.EncryptData(textBoxYobitSecret.Text);
            }
        }

    }
}
