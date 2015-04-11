using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Synthesis;

namespace SpeechSynth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSpeak_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer syn = GetSynthesizer();
            syn.Speak(txtText.Text);
            syn.Dispose();
        }
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer syn = GetSynthesizer();
            syn.SetOutputToWaveFile(lblFileName.Content.ToString());
            syn.Speak(txtText.Text);
            syn.Dispose();
        }

        private SpeechSynthesizer GetSynthesizer()
        {
            SpeechSynthesizer syn = new SpeechSynthesizer();

            if( rbNeutral.IsChecked.HasValue && rbNeutral.IsChecked.Value)
            {
                syn.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.Adult);
            }
            else if(rbMale.IsChecked.HasValue && rbMale.IsChecked.Value)
            {
                syn.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);
            }
            else
            {
                syn.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            }
            syn.Rate = (int)sldRate.Value;
            return syn;
        }


        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Speech"; // Default file name
            dlg.DefaultExt = ".wav"; // Default file extension
            dlg.Filter = "Wave File (.wav)|*.wav"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                lblFileName.Content = dlg.FileName;
                btnSave.IsEnabled = true;
            }
        }
    }
}
