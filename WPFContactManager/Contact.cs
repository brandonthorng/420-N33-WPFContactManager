using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFContactManager {

    // CLass used to store information from the database and presented to the table to view.
    class Contact {

        public int Id { get; set; }
        public string Name { get; set; }

        public Contact(int Id, string Name) {
            this.Id = Id;
            this.Name = Name;
        }
    }
}
