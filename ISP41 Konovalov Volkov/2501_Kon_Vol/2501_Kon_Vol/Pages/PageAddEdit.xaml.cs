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
    /// Логика взаимодействия для PageAddEdit.xaml
    /// </summary>
    public partial class PageAddEdit : Page
    {
        private DB.Content _current = null;
        public PageAddEdit()
        {
            InitializeComponent();
            var a = App.Context.Tarif.Select(c => c.Tarif_list).ToList();
            CB_Tarif.ItemsSource = a;
        }
        public PageAddEdit(DB.Content content)
        {
            InitializeComponent();
            Title = "Редактирование";
            var a = App.Context.Tarif.Select(c => c.Tarif_list).ToList();
            CB_Tarif.ItemsSource = a;

            _current = content;
            TB_Oper.Text = _current.Operator.ToString();
            TB_cena.Text = _current.Cena.ToString();
            CB_Tarif.SelectedItem = _current.Tarif1.Tarif_list.ToString();
            

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_current == null)
            {

                if (TB_Oper.Text.Length != 0 && TB_cena.Text.Length != 0 && CB_Tarif.Text.Length != 0)
                {
                    var contents = new DB.Content
                    {
                        Cena = TB_cena.Text,
                        Operator = TB_Oper.Text,
                        Tarif = CB_Tarif.SelectedIndex +1

                    };
                    App.Context.Content.Add(contents);
                    App.Context.SaveChanges();
                    MessageBox.Show("Успех", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                { MessageBox.Show("Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else
            {
                if (TB_Oper.Text.Length != 0 && TB_cena.Text.Length != 0 && CB_Tarif.Text.Length != 0)
                {
                    _current.Operator = TB_Oper.Text;
                    _current.Cena = TB_cena.Text;
                    _current.Tarif = CB_Tarif.SelectedIndex + 1;

                    App.Context.SaveChanges();
                    MessageBox.Show("Успех", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }

        }
    }
}
