using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFContactManager {

    // Singleton Database Manager
    public sealed class DatabaseManager {

        // Database connection string
        private const string CONNECTION_STRING = @"data source=localhost\SQLEXPRESS; database = Contacts; Trusted_Connection = True";

        // Empty Constructor
        private DatabaseManager() { }

        static readonly DatabaseManager instance = new DatabaseManager();

        public static DatabaseManager Instance {
            get {
                return instance;
            }
        }

        // Method to return full database
        public List<Contact> UpdateTable() {
            
            List<Contact> contacts = new List<Contact>();

            using(var connection = new SqlConnection(CONNECTION_STRING)) {
                var query = "select * from Contact";

                using(var command = new SqlCommand(query, connection)) {
                    connection.Open();

                    using(var reader = command.ExecuteReader()) {
                        while(reader.Read()) {
                            // Save all contact values
                            object Id = reader["Id"];
                            object Name = reader["Name"];
                            object Email = reader["Email"];
                            object Phone_Number = reader["Phone_Number"];
                            object Country = reader["Country"];
                            object Gender = reader["Gender"];
                            object Birth_Date = reader["Birth_Date"];
                            object Language = reader["Language"];

                            // Add new contact
                            contacts.Add(new Contact(Id, Name, Email, Phone_Number, Country, Gender, Birth_Date, Language));
                        }
                    }
                }
            }

            return contacts;
        }

        // Method to add a new contact to the database
        public void AddContact() {

        }

        // Method to edit existing contact
        public void EditContact(Contact contact) {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                var query = "UPDATE Contact SET Name=@Name, Email=@Email, Phone_Number=@Phone_Number, Country=@Country, Gender=@Gender, Birth_Date=@Birth_Date, Language=@Language WHERE Id=@Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", contact.Id);
                    command.Parameters.AddWithValue("@Name", contact.Name);
                    command.Parameters.AddWithValue("@Email", contact.Email);
                    command.Parameters.AddWithValue("@Phone_Number", contact.Phone_Number);
                    command.Parameters.AddWithValue("@Country", contact.Country);
                    command.Parameters.AddWithValue("@Gender", contact.Gender);
                    command.Parameters.AddWithValue("@Birth_Date", contact.Birth_Date);
                    command.Parameters.AddWithValue("@Language", contact.Language);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method to delete one or more contacts
        public void DeleteContact(List<Contact> contacts) {
            foreach(Contact contact in contacts) {
                using(var connection = new SqlConnection(CONNECTION_STRING)) {
                    var query = "delete from Contact where " +
                        "Id=@Id and Name=@Name and Email=@Email and Phone_Number=@Phone_Number " +
                        "and Country=@Country and Gender=@Gender and Birth_Date=@Birth_Date and Language=@Language";

                    using(var command = new SqlCommand(query, connection)) {
                        command.Parameters.AddWithValue("@Id", contact.Id);
                        command.Parameters.AddWithValue("@Name", contact.Name);
                        command.Parameters.AddWithValue("@Email", contact.Email);
                        command.Parameters.AddWithValue("@Phone_Number", contact.Phone_Number);
                        command.Parameters.AddWithValue("@Country", contact.Country);
                        command.Parameters.AddWithValue("@Gender", contact.Gender);
                        command.Parameters.AddWithValue("@Birth_Date", contact.Birth_Date);
                        command.Parameters.AddWithValue("@Language", contact.Language);

                        connection.Open();

                        command.ExecuteNonQuery();
                    }
                }
            }
        }


    }
}
