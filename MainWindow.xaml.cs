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

namespace HomeComfort
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Model.HomeComfortEntities DB;
        private List<Model.Product> modelProducts;
        private List<Classes.ProductExstend> products;
        public MainWindow()
        {
            InitializeComponent();
            DB = new Model.HomeComfortEntities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            products = Classes.ProductExstend.Filling(DB);
            CatalogList.ItemsSource = products;
            filterCategory.ItemsSource = DB.Category.ToList();
            filterCategory.DisplayMemberPath = "NameCategory";
            filterCategory.SelectedValuePath = "IDCategory";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Filtration()
        {
            modelProducts = DB.Product.ToList();
            if (filterPrice.SelectedIndex > 0)
            {
                switch (filterPrice.SelectedIndex)
                {
                    case 1:
                        modelProducts = modelProducts.OrderBy(x => x.Price).ToList();
                        break; 
                    case 2:
                        modelProducts = modelProducts.OrderByDescending(x => x.Price).ToList();
                        break;
                }
            }
            if (filterCategory.SelectedIndex >= 0)
            {
                modelProducts = modelProducts.Where(x => x.Category.IDCategory == (int)filterCategory.SelectedValue).ToList();
            }
            if (search.Text != String.Empty)
            {
                modelProducts = modelProducts.Where(x => x.NameProduct.Contains(search.Text)).ToList();
            }
            products = Classes.ProductExstend.Filling(modelProducts);
            CatalogList.ItemsSource = products;
        }

        private void filterPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtration();
        }

        private void filterCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtration();
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtration();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var productWindow = new View.ProductWindow();
            Hide();
            productWindow.Show();
        }
    }
}