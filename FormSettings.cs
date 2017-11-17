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

        /*
        private void buttonActivatePoloniexKey_Click(object sender, EventArgs e)
        {
            if (textBoxPoloniexSecret.Text == "")
            {
                MessageBox.Show("You need to fill API Key and Secret fields");
                return;
            }
            selSettings.poloniexkey = AppCrypt.EncryptData(textBoxPoloniexKey.Text);
            selSettings.poloniexsecret = AppCrypt.EncryptData(textBoxPoloniexSecret.Text);
            MessageBox.Show("Done! Save you profile to apply changes");
        }

        private void buttonActivateBittrexKey_Click(object sender, EventArgs e)
        {
            if (textBoxBittrexSecret.Text == "")
            {
                MessageBox.Show("You need to fill API Key and Secret fields");
                return;
            }
            selSettings.bittrexkey = AppCrypt.EncryptData(textBoxBittrexKey.Text);
            selSettings.bittrexsecret = AppCrypt.EncryptData(textBoxBittrexSecret.Text);
            MessageBox.Show("Done! Save you profile to apply changes");

        }
        */
    }
}
