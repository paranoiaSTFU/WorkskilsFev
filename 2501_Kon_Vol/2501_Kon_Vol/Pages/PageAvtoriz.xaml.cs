using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace _2501_Kon_Vol.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAvtoriz.xaml
    /// </summary>
    public partial class PageAvtoriz : Page
    {
        string a ="";



        public PageAvtoriz()
        {
            
            InitializeComponent();
            int q = PartTwo();
            if (q == 0)
            {
                CapthaIMG.Visibility = Visibility.Hidden;
                CapthaInput.Visibility = Visibility.Hidden;
                BTNCapthaInput.Visibility = Visibility.Hidden;
            }
            else 
            {

                CapthaIMG.Visibility = Visibility.Visible;
                CapthaIMG.Width = 200;
                CapthaIMG.Height = 100;
                string txt = ClassCaptha.CreateTXT();
                a = txt;
                ClassCaptha captha = new ClassCaptha();
                Bitmap z = captha.CreateIMG(200, 100, txt);
                CapthaIMG.Source = ClassCaptha.ConvertBitMapToIMG(z);
                Timer(q);
    

            }
        }

        private void BTNLogin_Click(object sender, RoutedEventArgs e)
        {
            if (TBLogin.Text.Length != 0 & TBPassword.Text.Length != 0)
            {
                if (App.Context.Login.Where(p => p.Login1 == TBLogin.Text & p.Password == TBPassword.Text).Count() == 1)
                {
                    NavigationService.Navigate(new Pages.Content());
                }
                else
                { 
                    CapthaIMG.Visibility = Visibility.Visible;
                    CapthaInput.Visibility = Visibility.Visible;
                    BTNCapthaInput.Visibility = Visibility.Visible;
                    CapthaIMG.Width = 200;
                    CapthaIMG.Height = 100;
                    string txt = ClassCaptha.CreateTXT();
                    a = txt;
                    ClassCaptha captha = new ClassCaptha();
                    Bitmap z =  captha.CreateIMG(200, 100, txt);
                    CapthaIMG.Source = ClassCaptha.ConvertBitMapToIMG(z);
                    BTNLogin.Visibility = Visibility.Hidden;

                }
            }
        }

        private void BTNCapthaInput_Click(object sender, RoutedEventArgs e)
        {
            if (CapthaInput.Text == a)
            {
                CapthaIMG.Visibility = Visibility.Hidden;
                CapthaInput.Visibility = Visibility.Hidden;
                BTNCapthaInput.Visibility = Visibility.Hidden;
                BTNLogin.Visibility = Visibility.Visible;
            }
            else
            {
                string txt = ClassCaptha.CreateTXT();
                a = txt;
                ClassCaptha captha = new ClassCaptha();
                Bitmap z = captha.CreateIMG(200, 100, txt);
                CapthaIMG.Source = ClassCaptha.ConvertBitMapToIMG(z);
                BTNCapthaInput.Visibility = Visibility.Hidden;
                PartOne(15);
                Timer(PartTwo());

                /*
                if (PartTwo() == 0)
                {
                    CapthaIMG.Visibility = Visibility.Hidden;
                    CapthaInput.Visibility = Visibility.Hidden;
                    BTNCapthaInput.Visibility = Visibility.Hidden;
                }
                else
                {
                    CapthaIMG.Visibility = Visibility.Visible;
                    CapthaInput.Visibility = Visibility.Visible;
                    BTNCapthaInput.Visibility = Visibility.Visible;
                    Timer();
                }
                */
            }

        }

        public void PartOne(int a)
        {
            if (a <= 0)
            {

            }
            else
            {
               
                    RegistryKey registryKey = Registry.CurrentUser;
                    registryKey.OpenSubKey("HelloKey");
                    registryKey.SetValue("TimeTick", a - 1);
                    registryKey.Close();
                
            }

        }
        public int PartTwo()
        {
            RegistryKey registryKey = Registry.CurrentUser;
            registryKey.OpenSubKey("HelloKey");
            int a = int.Parse(registryKey.GetValue("TimeTick").ToString());
            registryKey.Close();
            return a;
        }

        async Task Timer(int a)
        {

            BTNLogin.Visibility = Visibility.Hidden;
            CapthaInput.Visibility = Visibility.Hidden;
            BTNCapthaInput.Visibility = Visibility.Hidden;

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            
            timer.Tick += new EventHandler(timerTick);

            timer.Interval = new TimeSpan(0, 0, a);
            await Task.Run(() => timer.Start());

            Task.Delay(1000);
            await Task.Run(() => q());
                //await Task.Run(() =>  TimerForTimer());

                //BTNLogin.IsEnabled = false;
                //Task.Delay(10000);
                //BTNLogin.IsEnabled = true;
                //await Task.Run(() => PartOne(new TimeSpan(0,0,timer.Interval.Seconds).Seconds));
            }
        public void q()
        {
            for (int i = 0; i < PartTwo(); i++)
            {
                PartOne(PartTwo() - 1);
                Thread.Sleep(1000);
            }
        }
        private void TimerForTimer()
        {
            for (int i = 0; i < PartTwo(); i++)
            {
                System.Windows.Threading.DispatcherTimer tick = new System.Windows.Threading.DispatcherTimer();
                tick.Interval = new TimeSpan(0, 0, 1);
                tick.Tick += new EventHandler(timerTickForTick);
                tick.Start();
            }
        }
        
        private void timerTick(object sender, EventArgs e)
        {
            BTNCapthaInput.Visibility = Visibility.Visible;
            CapthaInput.Visibility = Visibility.Visible;
        }
        private void timerTickForTick(object sender, EventArgs e)
        {
            PartOne(PartTwo());
        }
    }
}
