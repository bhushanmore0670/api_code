//using ContactsApi.Model;
//using System.Collections.Generic;
//using System.Linq;
//namespace ContactsApi
//{
//    public class ContactService
//    {
//        private List<Contact> contacts = new List<Contact>();
//        private int nextId = 1;

//        public List<Contact> GetAll() => contacts;

//        public Contact Get(int id) => contacts.FirstOrDefault(c => c.Id == id);

//        public void Add(Contact contact)
//        {
//            contact.Id = nextId++;
//            contacts.Add(contact);
//        }

//        public void Update(Contact contact)
//        {
//            var existingContact = Get(contact.Id);
//            if (existingContact == null) return;

//            existingContact.FirstName = contact.FirstName;
//            existingContact.LastName = contact.LastName;
//            existingContact.Email = contact.Email;
//        }

//        public void Delete(int id)
//        {
//            var contact = Get(id);
//            if (contact != null)
//            {
//                contacts.Remove(contact);
//            }
//        }
//    }
//}

using System.Collections.Generic;
using System.IO;
using System.Linq;
using ContactsApi.Model;
using Newtonsoft.Json;

public class ContactService
{
    private readonly string _filePath = "Data/contacts.json";
    private List<Contact> contacts;

    public ContactService()
    {
        if (File.Exists(_filePath))
        {
            var jsonData = File.ReadAllText(_filePath);
            contacts = JsonConvert.DeserializeObject<List<Contact>>(jsonData) ?? new List<Contact>();
        }
        else
        {
            contacts = new List<Contact>();
        }
    }

    private void SaveToFile()
    {
        var jsonData = JsonConvert.SerializeObject(contacts, Formatting.Indented);
        File.WriteAllText(_filePath, jsonData);
    }

    public List<Contact> GetAll() => contacts;

    public Contact Get(int id) => contacts.FirstOrDefault(c => c.Id == id);

    public void Add(Contact contact)
    {
        contact.Id = contacts.Count > 0 ? contacts.Max(c => c.Id) + 1 : 1;
        contacts.Add(contact);
        SaveToFile();
    }

    public void Update(Contact contact)
    {
        var existingContact = Get(contact.Id);
        if (existingContact == null) return;

        existingContact.FirstName = contact.FirstName;
        existingContact.LastName = contact.LastName;
        existingContact.Email = contact.Email;
        SaveToFile();
    }

    public void Delete(int id)
    {
        var contact = Get(id);
        if (contact != null)
        {
            contacts.Remove(contact);
            SaveToFile();
        }
    }
}

