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

namespace _2501_Kon_Vol.Pages
{
    /// <summary>
    /// Логика взаимодействия для Content.xaml
    /// </summary>
    public partial class Content : Page
    {
        public Content()
        {
            InitializeComponent();
            var a = App.Context.Content.Select(c => c.Tarif1.Tarif_list).ToList();
            a.Insert(0, "все");
            CBFiltr.ItemsSource = a;
            //Sort.SelectedIndex = 0;
            CBFiltr.SelectedIndex = 0;
            Update();
        }
        public void Update()
        {
            var zxc = App.Context.Content.ToList();


            if (CBFiltr.SelectedIndex == 0)
            {
            
            }
            else
            {
               zxc = zxc.Where(p => p.Tarif1.Tarif_list == CBFiltr.SelectedItem.ToString()).ToList();
            }
            if (Sort.SelectedIndex == 0)
                zxc= zxc.OrderBy(p => p.Cena).ToList();
            else
                zxc = zxc.OrderByDescending(p => p.Cena).ToList();
            if (TBPoisk.Text.Length != 0)
            {
                zxc = zxc.Where(p => p.Operator.ToLower().Contains(TBPoisk.Text.ToLower()) || p.Tarif1.Tarif_list.ToLower().Contains(TBPoisk.Text.ToLower()) || p.Cena.ToString().Contains(TBPoisk.Text)).ToList();

            }


            LVmain.ItemsSource = zxc;
        }

        private void CBFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void TBPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
    }
}
