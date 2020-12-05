using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace WPFContactManager {
    public partial class MainWindow : Window {

        // Get Singleton Database Manager Instance
        DatabaseManager dbm = DatabaseManager.Instance;

        // Main method
        public MainWindow() {
            InitializeComponent();
            LoadDefaultView();
        }

        // Hides all table headers, call this when modifying views
        private void HideAllHeaders() {
            foreach(var column in DataTable.Columns) {
                column.Visibility = Visibility.Hidden;
            }
        }

        private void LoadDefaultView() {
            // EXAMPLE
            // The table takes in a item source in the form of a list
            List<Contact> contacts = new List<Contact> {
                new Contact(1, "Joe"),
                new Contact(2, "Donald"),
                new Contact(3, "Putin"),
                new Contact(4, "Freddy"),
                new Contact(5, "God"),
                new Contact(6, "Banana"),
                new Contact(7, "Me")
            };

            // Set the ItemsSource as the List and display
            DataTable.ItemsSource = contacts;

            // Hides all headers
            HideAllHeaders();

            // Only shows the Id and Name headers
            DataTable.Columns[0].Visibility = Visibility.Visible;
            DataTable.Columns[1].Visibility = Visibility.Visible;
        }

        // If a button from the Controls category was clicked, handle it here.
        private void ControlsButton_Clicked(object sender, RoutedEventArgs e) {
            if(sender.Equals(ADD_CONTACT)) {
                //TODO
                //Window popup with relevant details
            }
            if(sender.Equals(DELETE_CONTACT)) {
                //TODO
                //Checklist to delete contacts
            }
        }

        // If a button from the Other category was clicked, handle it here.
        private void OtherButton_Clicked(object sender, RoutedEventArgs e) {
            if(sender.Equals(SUMMARIZE_CONTACT)) {
                //TODO
                //Display a summarized view on the DataTable
            }
            if(sender.Equals(IMPORT_CONTACTS)) {
                //Popup window to accept a .csv file
            }
            if(sender.Equals(EXPORT_CONTACTS)) {
                //Popup window to save a .csv file
            }
        }

        // Detects a double click on a cell on the table. Popup window to edit person here.
        private void DataTable_DoubleClick(object sender, MouseButtonEventArgs e) {
            if(DataTable.SelectedItem == null) return;

            // Returns the Contact object
            var selectedContact = DataTable.SelectedItem as Contact;
            //TODO popup window
        }
    }
}
