using System.Windows;

namespace WPFContactManager {
    /// <summary>
    /// Interaction logic for EditContactPopup.xaml
    /// </summary>
    public partial class EditContactPopup : Window {

        DatabaseManager dbm = DatabaseManager.Instance;

        public EditContactPopup() {
            InitializeComponent();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact(ID.Text, Name.Text, Email.Text, PhoneNumber.Text, Country.Text, Gender.Text, BirthDate.Text, Language.Text);

            contact.Name = Name.Text.ToString();
            contact.Email = Email.Text.ToString();
            contact.Phone_Number = PhoneNumber.Text.ToString();
            contact.Country = Country.Text.ToString();
            contact.Gender = Gender.Text.ToString();
            contact.Birth_Date = BirthDate.Text.ToString();
            contact.Language = Language.Text.ToString();

            dbm.EditContact(contact);
            Close();
        }
    }
}
