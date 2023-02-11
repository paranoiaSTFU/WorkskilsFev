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
using Урок4.BD;

namespace Урок4.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageTwo.xaml
    /// </summary>
    public partial class PageTwo : Page
    {
        public PageTwo()
        {
            InitializeComponent();
            DGridHotels.ItemsSource = Test02Entities.GetContext().Hotels.ToList();
        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.PageAddEdit((sender as Button).DataContext as Hotel));
        }

        private void DGridHotels_SelectionChanged()
        {

        }

        private void BTNadd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.PageAddEdit(null));
        }

        private void BTNdelete_Click(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = DGridHotels.SelectedItems.Cast<Hotel>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {hotelsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Test02Entities.GetContext().Hotels.RemoveRange(hotelsForRemoving);
                    Test02Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGridHotels.ItemsSource = Test02Entities.GetContext().Hotels.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Test02Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridHotels.ItemsSource = Test02Entities.GetContext().Hotels.ToList();
            }
        }
    }
}
