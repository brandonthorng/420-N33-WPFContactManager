using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFContactManager {

    // Singleton Database Manager

    public sealed class DatabaseManager {

        private DatabaseManager() { }

        static readonly DatabaseManager instance = new DatabaseManager();

        public static DatabaseManager Instance {
            get {
                return instance;
            }
        }

        // This method will be called on startup. 
        // Will only return basic information like Id and Name.
        public void ViewContact() {

        }

        // This method will be called to add a new contact.
        public void AddContact() {

        }

        public void EditContact() {

        }

        public void DeleteContact() {

        }
    }
}
