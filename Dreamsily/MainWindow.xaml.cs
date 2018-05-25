using System;
using System.Threading.Tasks;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace Dreamsily
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

        private void shutdown(string args)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "shutdown";
            startInfo.Arguments = args;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.Start();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton.IsChecked == true)
            {
                shutdown("/s /t 3600");
            }
            else if (radioButton1.IsChecked == true)
            {
                shutdown("/s /t 7200");
            }
            else if (radioButton2.IsChecked == true)
            {
                shutdown("/s /t 10800");
            }
            else if (radioButton3.IsChecked == true)
            {
                surprise.Visibility = Visibility.Visible;

                var player = new SoundPlayer(Dreamsily.Properties.Resources.Surprise);
                player.Play();
                Task.Delay(1200).ContinueWith(_ => { shutdown("/s /t 0"); } );
            }
            else if (radioButton4.IsChecked == true)
            {
                if (!textBox.Text.Equals(""))
                {
                    int time = int.Parse(textBox.Text);
                    time = time * 3600;
                    shutdown("/s /t " + time);
                }
                else if (!textBox.Text.Equals("0"))
                {
                    MessageBox.Show("0 is not allowed!");
                }
                else
                {
                    MessageBox.Show("try entering a number next time");
                }
            }
            else
            {
                MessageBox.Show("try clicking an option next time");
            }
        }
        // button for canceling shutdown
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            shutdown("/a");
        }

        // if the textbox is selected, check the corresponding radiobutton
        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            radioButton4.IsChecked = true;
        }

        // activate the timer if enter is pressed in the custom box
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                button_Click(sender, e);
            }
        }

        // only allow numbers in the textbox
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex reg = new Regex("[^0-9]+");
            e.Handled = reg.IsMatch(e.Text);
        }
    }
}
