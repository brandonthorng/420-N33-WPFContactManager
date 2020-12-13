using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace WPFContactManager {
    public partial class MainWindow : Window {

        // Get Singleton Database Manager Instance
        DatabaseManager dbm = DatabaseManager.Instance;

        // Current view bool (Either Default or Summary view)
        bool summaryView = false;

        // Main method
        public MainWindow() {
            InitializeComponent();
            LoadDefaultView();
        }

        private void LoadDefaultView() {
            // Gets back all the rows from the database and sets it as the DataTable's item source
            DataTable.ItemsSource = dbm.UpdateTable();

            SetColumnVisibility(1, 2);
        }

        private void SetColumnVisibility(params int[] cols) {
            // Hides all headers
            foreach(var col in DataTable.Columns) {
                col.Visibility = Visibility.Hidden;
            }

            // Sets parameter columns to visible
            foreach(int col in cols) {
                // Sets the column to visibile
                DataTable.Columns[col-1].Visibility = Visibility.Visible;
            }
        }

        // If a button from the Controls category was clicked, handle it here.
        private void ControlsButton_Clicked(object sender, RoutedEventArgs e) {
            if(sender.Equals(ADD_CONTACT)) {
                if(ADD_CONTACT.Content.ToString() == "Add Contact") {
                    //TODO POPUP WINDOW
                    AddContactPopup addContactPopup = new AddContactPopup();
                    addContactPopup.Show();
                } else {
                    // If user clicks confirm button

                    List<Contact> toDelete = new List<Contact>();

                    for(int i = 0; i < DataTable.Items.Count; i++) {
                        var item = DataTable.Items[i];
                        var checkbox = DataTable.Columns[8].GetCellContent(item) as System.Windows.Controls.CheckBox;
                        if((bool)checkbox.IsChecked) {
                            toDelete.Add(DataTable.Items[i] as Contact);
                        }
                    }

                    // If no items were selected, do nothing
                    if(toDelete.Count == 0) {
                        return;
                    }

                    // Sets checkbox column as hidden
                    DeleteCheckboxes.Visibility = Visibility.Hidden;
                    // Reset button text to default
                    DELETE_CONTACT.Content = "Delete Contact";
                    ADD_CONTACT.Content = "Add Contact";

                    dbm.DeleteContact(toDelete);

                    DataTable.ItemsSource = dbm.UpdateTable();

                    if(summaryView) {
                        SetColumnVisibility(1, 2, 3, 4, 5, 6, 7, 8);
                    } else {
                        LoadDefaultView();
                    }
                }
            }
            if(sender.Equals(DELETE_CONTACT)) {
                if(DELETE_CONTACT.Content.ToString() == "Delete Contact") {
                    // Sets checkbox column as visible
                    DeleteCheckboxes.Visibility = Visibility.Visible;

                    // Reset button text to default
                    DELETE_CONTACT.Content = "Cancel";
                    ADD_CONTACT.Content = "Confirm";
                } else {
                    // If user clicks cancel button

                    // Sets checkbox column as hidden
                    DeleteCheckboxes.Visibility = Visibility.Hidden;

                    // Reset button text to default
                    DELETE_CONTACT.Content = "Delete Contact";
                    ADD_CONTACT.Content = "Add Contact";
                }
            }
        }

        // If a button from the Other category was clicked, handle it here.
        private void OtherButton_Clicked(object sender, RoutedEventArgs e) {
            if(sender.Equals(SUMMARIZE_CONTACT)) {
                DataTable.ItemsSource = dbm.UpdateTable(); 
                if(SUMMARIZE_CONTACT.Content.ToString() == "Summarize Contacts") {
                    if(ADD_CONTACT.Content.ToString() == "Confirm") {
                        // If user clicks summary view while deleting
                        SetColumnVisibility(1, 2, 3, 4, 5, 6, 7, 8, 9);

                        // Sets summary view to true
                        summaryView = true;

                        // Change button name
                        SUMMARIZE_CONTACT.Content = "Return to default view";
                    } else {
                        // Sets summary view to true
                        summaryView = true;

                        // Show all columns
                        SetColumnVisibility(1, 2, 3, 4, 5, 6, 7, 8);

                        // Change button name
                        SUMMARIZE_CONTACT.Content = "Return to default view";
                    }
                } else {
                    if(ADD_CONTACT.Content.ToString() == "Confirm") {
                        // If user clicks return to default view while deleting
                        SetColumnVisibility(1, 2, 9);

                        // Sets summary view to false
                        summaryView = false;

                        // Change button text
                        SUMMARIZE_CONTACT.Content = "Summarize Contacts";
                    } else {
                        // If user clicks return to default view

                        // Sets summary view to true
                        summaryView = false;

                        // Show all columns
                        SetColumnVisibility(1, 2);

                        // Change button text
                        SUMMARIZE_CONTACT.Content = "Summarize Contacts";
                    }
                }
            }
            if(sender.Equals(IMPORT_CONTACTS)) {
                var ofd = new OpenFileDialog();
                ofd.Filter = "CSV File (.csv)|*.csv";

                if(ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    string[] fileContent = File.ReadAllLines(ofd.FileName);

                    foreach(string fileLine in fileContent) {
                        var line = fileLine.Split(',');

                        Contact newContact = new Contact(int.Parse(line[0]), line[1], line[2], line[3], line[4], line[5], line[6], line[7]);

                        dbm.AddContact(newContact);
                    }

                    DataTable.ItemsSource = dbm.UpdateTable();
                }
            }
            if(sender.Equals(EXPORT_CONTACTS)) {
                var sfd = new SaveFileDialog {
                    FileName = "Contacts",
                    DefaultExt = ".text",
                    Filter = "CSV File (.csv)|*.csv"
                };

                if(sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    List<Contact> contacts = dbm.UpdateTable();

                    string fileContents = "";

                    foreach(Contact c in contacts) {
                        string formattedString = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}\n",
                            c.Id, c.Name, c.Email, c.Phone_Number, c.Country, c.Gender, c.Birth_Date, c.Language);
                        
                        fileContents += formattedString;
                    }

                    File.WriteAllText(sfd.FileName, fileContents);
                }
            }
        }

        // Detects a double click on a cell on the table. Popup window to edit person here.
        private void DataTable_DoubleClick(object sender, MouseButtonEventArgs e) {
            if(DataTable.SelectedItem == null) return;

            if(DELETE_CONTACT.Content.ToString() == "Delete Contact") {
                // Returns the Contact object that was double clicked if table is not in delete mode
                var selectedContact = DataTable.SelectedItem as Contact;

                EditContactPopup editContactPopup = new EditContactPopup();

                editContactPopup.Show();

                editContactPopup.ID.Text = selectedContact.Id.ToString();
                editContactPopup.Name.Text = selectedContact.Name.ToString();
                editContactPopup.Email.Text = selectedContact.Email.ToString();
                editContactPopup.PhoneNumber.Text = selectedContact.Phone_Number.ToString();
                editContactPopup.Country.Text = selectedContact.Country.ToString();
                editContactPopup.Gender.Text = selectedContact.Gender.ToString();
                editContactPopup.BirthDate.Text = selectedContact.Birth_Date.ToString();
                editContactPopup.Language.Text = selectedContact.Language.ToString();
            }
        }


    }
}
