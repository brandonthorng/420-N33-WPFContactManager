using System.Windows;

namespace WPFContactManager
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddContactPopup : Window
    {
        DatabaseManager dbm = DatabaseManager.Instance;
        

        public AddContactPopup()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        { 
            Contact contact = new Contact(ID.Text, Name.Text, Email.Text, PhoneNumber.Text, Country.Text, Gender.Text, BirthDate.Text, Language.Text);
            dbm.AddContact(contact);
            Close();
        }
    }
}
