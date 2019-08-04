//***************************************************************************
//File: MainWindow.xaml.cs
//
//Purpose: To add logic to the xaml graphical user interface. Helps open a
//  JSON file using a file dialog object. Searches for country data by storing 
//  JSON data into a list of country objects and comparing the target while
//  looping through the list.
//
//Written By: Timothy Negron
//
//Compiler: Visual Studio C# 2017
//
//Update Information
//------------------
//
//***************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
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
using CSprogramming_CS_DLL_for_Country_Data; // My Imported DLL
using Microsoft.Win32; // For File Dialog
using System.Runtime.Serialization.Json; // For reading JSON file

namespace CSprogramming_CS_Project4_FrontEnd_with_CountryDLL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Member Variables
        // The purpose of this variable is to help easily set the initial directory when the FileDialog opens
        string initialDirectory = @"C:\Users\Tim\source\repos\CSprogramming_CS_Project4_FrontEnd_with_CountryDLL\CSprogramming_CS_Project4_FrontEnd_with_CountryDLL\bin\Debug";
        
        // Cronstruct a list of countries using generics, list, data type
        List<Country> ListOfCountries = new List<Country>();

        // Contruct an OpenFileDialog Object
        OpenFileDialog openFileDialog = new OpenFileDialog();

        #endregion

        #region Main Method
        //***************************************************************************
        //Method: Main
        //
        //Purpose: To run the main program
        //
        //***************************************************************************
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods for: Open Countries JSON File Button
        //***************************************************************************
        //Method: ButtonOpenCountriesJSONFile_Click(object sender, RoutedEvenArgs e)
        //
        //Purpose: To display a file dialog and allows the user to open a JSON file.
        //  If a file is selected, it will then store the data inside the file to the 
        //  ListOfCountries member variable.
        //
        //***************************************************************************
        private void ButtonOpenCountriesJSONFile_Click(object sender, RoutedEventArgs e)
        {
            InitializeOpenFileDialog();

            ShowOpenFileDialog();
        }

        //***************************************************************************
        //Method: InitializeOpenFileDialog()
        //
        //Purpose: To initialize the open file dialog member variable.
        //
        //***************************************************************************
        private void InitializeOpenFileDialog()
        {
            // Initialize the initial directory to the current directory
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;

            // Set the initial director to the current working directory
            openFileDialog.InitialDirectory = initialDirectory; // 12/4/2018 Don't know what I did here,, Doesn't seem to be needed.

            // Set the title of the File Dialog
            openFileDialog.Title = "Get Countries Data from JSON file";

            // Filter visible files in the File Dialog (only show json files)
            openFileDialog.Filter = "Json files(*.json)| *.json";
        }

        //***************************************************************************
        //Method: ShowOpenFileDialog()
        //
        //Purpose: To show the open file dialog and check if a file was selected. If
        // a file was selected, clear the data shown and read the file.
        //
        //***************************************************************************
        private void ShowOpenFileDialog()
        {
            // Show dialog and Check if a file was selected
            if (openFileDialog.ShowDialog() == true)
            {
                ClearData();

                ReadFile();
            }
        }

        //***************************************************************************
        //Method: ReadFile()
        //
        //Purpose: To read the JSON file. Desrializes the JSON file and stores the
        // data in object ListOfCountries
        //
        //***************************************************************************
        private void ReadFile()
        {

            // DeserializeJSON

            // Put the name of the file that was open into a string type variable
            string filename = openFileDialog.FileName;

            // Construct a filestream obj with the filename, open the file, and give obj read access
            FileStream readFILE = new FileStream(filename, FileMode.Open, FileAccess.Read);

            // Construct a streamreader obj with the filestream obj and encode with UTF-8
            StreamReader streamReader = new StreamReader(readFILE, Encoding.UTF8);
            
            // Initialize a string variable with data in streamReader
            string jsonString = streamReader.ReadToEnd();

            // Initialize byte variable with encoding abstract data type method, use string with json data as parameter
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonString);
            
            // Construct a memorystream obj with byteArray variable
            MemoryStream stream = new MemoryStream(byteArray);

            // Construct a DCJS Object with List<Country> obj
            DataContractJsonSerializer serListOfCountryData = new DataContractJsonSerializer(typeof(List<Country>));

            // Initialize ListOfCoutries with DataConstractJsonSerialize read object method
            ListOfCountries = (List<Country>)serListOfCountryData.ReadObject(stream);

            readFILE.Close();
        }
        #endregion

        #region Methods for: Find Country by Name Button
        //***************************************************************************
        //Method: ButtonOpenCountriesJSONFile_Click(object sender, RoutedEvenArgs e)
        //
        //Purpose: To search through the list for a target country and, if found,
        //  display the data on the GUI.
        //
        //***************************************************************************
        private void ButtonFindCountryByName_Click(object sender, RoutedEventArgs e)
        {
            // Clear the list view
            ClearListViews();

            // Store target in string variable to make it easier to read the code
            string Target = TextBoxTargetCountryName.Text;

            // If target is not found textboxes will be set to empty
            Boolean found = false;

            CountryTargetSearch(Target, ref found);

            CheckFound(found);
        }

        //***************************************************************************
        //Method: CountryTargetSearch(string Target, ref Boolean found)
        //
        //Purpose: To search for the target country.
        //
        //***************************************************************************
        private void CountryTargetSearch(string Target, ref Boolean found)
        {
            // Start searching through country list by first name
            foreach (Country c in ListOfCountries)
            {
                // Check if the name of each country object matches with the target
                if (c.Name == Target)
                {
                    // Set found to true to that the text boxes do not get cleared
                    found = true;

                    // Initialize strings on text boxes and list view
                    ShowCountryData(c);
                    
                    break;
                }
            }
        }

        //***************************************************************************
        //Method: CountryTargetSearch(string Target, ref Boolean found)
        //
        //Purpose: To show the country data on the GUI. Initializes string on text
        // boxes and list views.
        //
        //***************************************************************************
        private void ShowCountryData(Country c)
        {
            // Display the data in the textboxes
            TextBoxName.Text = c.Name;
            TextBoxCapital.Text = c.Capital;
            TextBoxRegion.Text = c.Region;
            TextBoxSubregion.Text = c.Subregion;
            TextBoxPopulation.Text = Convert.ToString(c.Population);

            // Loop through each currency object in the country object
            foreach (Currency curr in c.Currencies)
            {
                // Add the names of the currencies to the list view
                ListViewCurrencies.Items.Add(curr.Name);
            }

            // Loop through each language object in the country object
            foreach (Language l in c.Languages)
            {
                // Add the names of the languages to the list view
                ListViewLanguages.Items.Add(l.Name);
            }
        }

        //***************************************************************************
        //Method: CheckFound()
        //
        //Purpose: To check if a country target was note found. If a country target 
        // was not found, clear the data on GUI.
        //
        //***************************************************************************
        private void CheckFound(Boolean found)
        {
            // Check if target was found
            if (found == false)
            {
                // Clear data on GUI if country target wasn't found
                ClearData();
            }
        }
        #endregion

        #region Methods
        //***************************************************************************
        //Method: ClearData()
        //
        //Purpose: To clear the data shown on the user interfaces text boxes and list
        // view. Used by both buttons.
        //
        //***************************************************************************
        private void ClearData()
        {
            TextBoxCountryFilename.Text = openFileDialog.FileName;
            TextBoxTargetCountryName.Text = string.Empty;
            TextBoxCapital.Text = string.Empty;
            TextBoxName.Text = string.Empty;
            TextBoxRegion.Text = string.Empty;
            TextBoxSubregion.Text = string.Empty;
            TextBoxPopulation.Text = string.Empty;

            ClearListViews();
            
        }

        //***************************************************************************
        //Method: ClearData()
        //
        //Purpose: To clear the data shown on the list view.
        //
        //***************************************************************************
        private void ClearListViews()
        {
            ListViewCurrencies.Items.Clear();
            ListViewLanguages.Items.Clear();
        }
        #endregion

    }
}
