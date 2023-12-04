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
using System.Windows.Shapes;

namespace Shafikov_Glazki_Save
{
    /// <summary>
    /// Логика взаимодействия для AddProdSale.xaml
    /// </summary>
    public partial class AddProdSale : Window
    {
        private Agent currentAgent = new Agent();
        private ProductSale currentProductSale = new ProductSale();
        private Product ProductIndex = new Product();
        public AddProdSale(Agent SelectedAgent)
        {
            InitializeComponent();

            currentAgent = SelectedAgent;

            var currentProduct = Shafikov_GlazkiEntities.GetContext().ProductSale.ToList();
            

            var currentProdIndex = Shafikov_GlazkiEntities.GetContext().Product.ToList();
            ComboProduct.ItemsSource = currentProdIndex;

            /*if (SelectedProductSale != null)
            {
                currentProductSale = SelectedProductSale;
            }

            DataContext = SelectedProductSale;*/
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if(ComboProduct.SelectedItem == null)
            {
                errors.AppendLine("Укажите наименование продукта");
            }
            if(DateSale.Text == "")
            {
                errors.AppendLine("Укажите дату реализации");
            }
            if(string.IsNullOrWhiteSpace(TBoxCountSale.Text))
            {
                errors.AppendLine("Укажите количество реализованной продукции");
            }
            else if(Convert.ToInt32(TBoxCountSale.Text) <= 0)
            {
                errors.AppendLine("Количество реализованной продукции должно быть положительным");
            }

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            currentProductSale.SaleDate = Convert.ToDateTime(DateSale.Text);
            currentProductSale.ProductCount = Convert.ToInt32(TBoxCountSale.Text);
            currentProductSale.ProductID = ComboProduct.SelectedIndex + 1;
            currentProductSale.AgentID = currentAgent.ID;


            //foreach (Product ID in ComboProduct.SelectedItem)
            //{
                
            //}


            if (currentProductSale.ID == 0)
                Shafikov_GlazkiEntities.GetContext().ProductSale.Add(currentProductSale);

            try
            {
                Shafikov_GlazkiEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                //Manager.MainFrame.GoBack();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        /*private void ComboProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;
            if(tb.SelectionStart != 0)
            {
                ComboProduct.SelectedItem = null;
            }
            if(tb.SelectionStart == 0 && ComboProduct.SelectedItem == null)
            {
                ComboProduct.IsDropDownOpen = false;
            }

            ComboProduct.IsDropDownOpen = true;
            if(ComboProduct.SelectedItem == null)
            {
                CollectionView cv = ((CollectionView)CollectionViewSource.GetDefaultView(ComboProduct.ItemsSource));
                cv.Filter = s => ((string)s).IndexOf(ComboProduct.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
        }*/
    }
}
