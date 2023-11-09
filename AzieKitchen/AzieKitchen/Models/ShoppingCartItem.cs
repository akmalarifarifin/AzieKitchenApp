using System.Collections.Generic;
using SQLite;

namespace AzieKitchen.Models
{
    public class ShoppingCartItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }

    public class ShoppingCartDatabase
    {
        readonly SQLiteConnection database;

        public ShoppingCartDatabase(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            database.CreateTable<ShoppingCartItem>();
        }

        public List<ShoppingCartItem> GetItems()
        {
            return database.Table<ShoppingCartItem>().ToList();
        }

        public int AddItem(ShoppingCartItem item)
        {
            return database.Insert(item);
        }

        public int UpdateItem(ShoppingCartItem item)
        {
            return database.Update(item);
        }

        public int DeleteItem(ShoppingCartItem item)
        {
            return database.Delete(item);
        }
        public void DeleteAllItems()
        {
            database.DeleteAll<ShoppingCartItem>();
        }

    }
}
