using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFContactManager {

    // CLass used to store information from the database and presented to the table to view.
    public class Contact {

        // Table headers
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone_Number { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public string Birth_Date { get; set; }
        public string Language { get; set; }

        // Constructor for Contact object
        public Contact(object Id, object Name, object Email, object Phone_Number, object Country, object Gender, object Birth_Date, object Language) {
            this.Id = int.Parse(Id.ToString());
            this.Name = Name.ToString();
            this.Email = Email.ToString();
            this.Phone_Number = Phone_Number.ToString();
            this.Country = Country.ToString();
            this.Gender = Gender.ToString();
            this.Birth_Date = Birth_Date.ToString();
            this.Language = Language.ToString();
        }

        public bool Equals(Contact c) {
            if(this.Id == c.Id && this.Name == c.Name && this.Email == c.Email &&
               this.Phone_Number == c.Phone_Number && this.Country == c.Country &&
               this.Gender == c.Gender && this.Birth_Date == Birth_Date && this.Language == c.Language) {
                return true;
            } else {
                return false;
            }
        }
    }
}
