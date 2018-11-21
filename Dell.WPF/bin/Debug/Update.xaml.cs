using Dell.Data.Entities;
using System;
using System.Windows;
using System.Linq;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Dell.WebAPICall;
using System.Windows.Controls;

namespace Dell.WPF
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        
        public Customer customer = null;
        bool isNew = false;

        public Update()
        {
            InitializeComponent();
            Loaded += CustomersUpdate_Loaded;
        }

        private void CustomersUpdate_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                name.Focus();
                if (customer == null)
                {
                    isNew = true;
                    customer = new Customer();
                }
                this.Title = (isNew ? "Insert" : "Update");
                this.DataContext = customer;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Reading customer for {(isNew ? "insert" : "update") } failed!!!", ex.Message);
            }
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (customer.Errors > 0)
                    MessageBox.Show("Record has errors!");
                else
                {
                    WebAPI webAPI = new WebAPI();
                
                    var customers = await webAPI.ReadByEmail(customer.EmailAddress);
                
                    bool sameEmail = false;

                    if (customers?.Count > 0)
                    {
                        var cust = from x in customers where x.EmailAddress == customer.EmailAddress && (isNew ? true : x.ID != customer.ID) select x;
                        sameEmail = cust?.Count() > 0;
                    }

                    if (sameEmail)
                        MessageBox.Show("Another customer is registered with same email!");
                    else
                    {
                    
                        var result = this.DialogResult = await (isNew ? webAPI.Add(customer) : webAPI.Update(customer));
                        if (result.HasValue && result.Value)
                            MessageBox.Show($"{(isNew? "Insert" : "Update")} succeded!");
                        else
                            MessageBox.Show($"{(isNew ? "Insert" : "Update") } failed!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{(isNew ? "Insert" : "Update")} failed! {Environment.NewLine}{ex.Message}");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void EmailValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added) customer.Errors += 1;
            if (e.Action == ValidationErrorEventAction.Removed) customer.Errors -= 1;
        }
    }
}
