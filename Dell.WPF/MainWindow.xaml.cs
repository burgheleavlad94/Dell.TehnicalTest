using Dell.Data.Entities;
using Dell.WebAPICall;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace Dell.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Customer customer = new Customer();

        public IList<Customer> Customers { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg_Customers = sender as DataGrid;

            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                customer = e.AddedItems[0] as Customer;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Customers_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Customers =await new WebAPI().Read();
                dg_Customers.ItemsSource = Customers ;
            }
            catch (Exception)
            {
                MessageBox.Show("No customers found");
            }
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            if(dg_Customers.SelectedItem != null)
            {
                try
                {
                    Update updateWindow = new Update();
                    customer = dg_Customers.SelectedItem as Customer;
                    updateWindow.customer = customer;
                    var result = updateWindow.ShowDialog();
                    if(result.HasValue && result.Value)
                    {
                        Customers = await new WebAPI().Read();
                        dg_Customers.ItemsSource = Customers;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Refreshing page with new customer failed!!", ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a person to update");
            }
                
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Update insertWindow = new Update();
                var result = insertWindow.ShowDialog();
                if(result.HasValue && result.Value)
                {
                    Customers = await new WebAPI().Read();
                    dg_Customers.ItemsSource = Customers;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Refreshing page with new customer failed", ex.Message);
            }
        }
    }
}
