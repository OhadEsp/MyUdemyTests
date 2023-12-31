﻿using System.Windows;
using System.Windows.Input;

//This library is used for Regular Expression
using System.Text.RegularExpressions;

//This library is used for DataTable
using System.Data;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace CurrencyConverter_Static
{

    //Root class is the main class. API return rates in the rates. It returns all currency name with value.
    public class Root
    {
        //Get all record in rates and set in rate class as currency name wise
        public Rate rates { get; set; }
        public long timestamp;
        public string license;
    }

    //Make sure API return value that name and where you want to store that name are the same. Like in API, get response INR, then set it with INR name.
    public class Rate
    {
        public double INR { get; set; }
        public double JPY { get; set; }
        public double USD { get; set; }
        public double NZD { get; set; }
        public double EUR { get; set; }
        public double CAD { get; set; }
        public double ISK { get; set; }
        public double PHP { get; set; }
        public double DKK { get; set; }
        public double CZK { get; set; }
    }

    public partial class MainWindow : Window
    {
        Root val = new Root();

        public MainWindow()
        {
            InitializeComponent();

            //ClearControls method is used to clear all control values
            ClearControls();

            ////BindCurrency is used to bind currency name with the value in the Combobox
            //BindCurrency();
            GetValue();
        }

        public static async Task<Root> GetDataGetMethod<T>(string url)
        {
            var ss = new Root();
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(1);
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponceString = await response.Content.ReadAsStringAsync();
                        var ResponceObject = JsonConvert.DeserializeObject<Root>(ResponceString);

                        //MessageBox.Show("Licencse: " + ResponceObject.license, "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                        return ResponceObject;
                    }
                    return ss;
                }
            }
            catch
            {
                return ss;
            }
        }

        private async void GetValue()
        {
            val = await GetDataGetMethod<Root>("https://openexchangerates.org/api/latest.json?app_id=6b75a15010164525adffc83fa4fff20b");
            BindCurrency();
        }

        #region Bind Currency From and To Combobox
        private void BindCurrency()

        {
            //Create a Datatable Object
            DataTable dt = new DataTable();


            //Add display column in DataTable
            dt.Columns.Add("Text");

            //Add Value Column in DataTable
            dt.Columns.Add("Rate");

            //Add rows in Datatable with text and value. set a value which are fetched from API
            dt.Rows.Add("--SELECT--", 0);
            dt.Rows.Add("INR", val.rates.INR);
            dt.Rows.Add("USD", val.rates.USD);
            dt.Rows.Add("NZD", val.rates.NZD);
            dt.Rows.Add("JPY", val.rates.JPY);
            dt.Rows.Add("EUR", val.rates.EUR);
            dt.Rows.Add("CAD", val.rates.CAD);
            dt.Rows.Add("ISK", val.rates.ISK);
            dt.Rows.Add("PHP", val.rates.PHP);
            dt.Rows.Add("DKK", val.rates.DKK);
            dt.Rows.Add("CZK", val.rates.CZK);

            //Datatable data assigned from the currency combobox
            cmbFromCurrency.ItemsSource = dt.DefaultView;

            //DisplayMemberPath property is used to display data in the combobox
            cmbFromCurrency.DisplayMemberPath = "Text";

            //SelectedValuePath property is used to set the value in the combobox
            cmbFromCurrency.SelectedValuePath = "Rate";

            //SelectedIndex property is used to bind the combobox to its default selected item 
            cmbFromCurrency.SelectedIndex = 0;

            //All properties are set to To Currency combobox as it is in the From Currency combobox
            cmbToCurrency.ItemsSource = dt.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text";
            cmbToCurrency.SelectedValuePath = "Rate";
            cmbToCurrency.SelectedIndex = 0;
        }
        #endregion

        #region Button Click Event

        //Convert the button click event
        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            //Create the variable as ConvertedValue with double datatype to store currency converted value
            double ConvertedValue;

            //Check if the amount textbox is Null or Blank
            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                //If amount textbox is Null or Blank it will show this message box
                MessageBox.Show("Please Enter Currency", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                //After clicking on messagebox OK set focus on amount textbox
                txtCurrency.Focus();
                return;
            }
            //Else if currency From is not selected or select default text --SELECT--
            else if (cmbFromCurrency.SelectedValue == null || cmbFromCurrency.SelectedIndex == 0)
            {
                //Show the message
                MessageBox.Show("Please Select Currency From", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                //Set focus on the From Combobox
                cmbFromCurrency.Focus();
                return;
            }
            //Else if currency To is not selected or select default text --SELECT--
            else if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                //Show the message
                MessageBox.Show("Please Select Currency To", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                //Set focus on the To Combobox
                cmbToCurrency.Focus();
                return;
            }

            //Check if From and To Combobox selected values are same
            if (cmbFromCurrency.Text == cmbToCurrency.Text)
            {
                //Amount textbox value set in ConvertedValue.
                //double.parse is used for converting the datatype String To Double.
                //Textbox text have string and ConvertedValue is double Datatype
                ConvertedValue = double.Parse(txtCurrency.Text);

                //Show the label converted currency and converted currency name and ToString("N3") is used to place 000 after the dot(.)
                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
            }
            else
            {
                //Calculation for currency converter is From Currency value multiply(*) 
                //With the amount textbox value and then that total divided(/) with To Currency value
                ConvertedValue = (double.Parse(cmbToCurrency.SelectedValue.ToString()) * double.Parse(txtCurrency.Text)) / double.Parse(cmbFromCurrency.SelectedValue.ToString());

                //Show the label converted currency and converted currency name.
                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
            }
        }

        //Clear Button click event
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            //ClearControls method is used to clear all controls value
            ClearControls();
        }
        #endregion

        #region Extra Events

        //ClearControls method is used to clear all controls value
        private void ClearControls()
        {
            txtCurrency.Text = string.Empty;
            if (cmbFromCurrency.Items.Count > 0)
                cmbFromCurrency.SelectedIndex = 0;
            if (cmbToCurrency.Items.Count > 0)
                cmbToCurrency.SelectedIndex = 0;
            lblCurrency.Content = "";
            txtCurrency.Focus();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) //Allow Only Integer in Text Box
        {
            //Regular Expression is used to add regex.
            // Add Library using System.Text.RegularExpressions;
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion
    }
}