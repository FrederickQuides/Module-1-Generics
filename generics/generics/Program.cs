using System;
using System.Collections.Generic;

namespace DictionaryRepositoryDemo
{
    // Generic class for managing objects with CRUD operations
    public class DictionaryRepository<TKey, TValue> where TKey : IComparable
    {
        private readonly Dictionary<TKey, TValue> items = new Dictionary<TKey, TValue>();

        // Add: Adds a new item with a unique identifier
        public void Add(TKey id, TValue item)
        {
            if (items.ContainsKey(id))
            {
                throw new ArgumentException($"An item with the key '{id}' already exists.");
            }
            items[id] = item;
            Console.WriteLine($"Item added successfully with key: {id}");
        }

        // Get: Retrieves an item by its key
        public TValue Get(TKey id)
        {
            if (!items.ContainsKey(id))
            {
                throw new KeyNotFoundException($"No item found with the key '{id}'.");
            }
            return items[id];
        }

        // Update: Updates an item with the specified key
        public void Update(TKey id, TValue newItem)
        {
            if (!items.ContainsKey(id))
            {
                throw new KeyNotFoundException($"Cannot update. No item found with the key '{id}'.");
            }
            items[id] = newItem;
            Console.WriteLine($"Item with key '{id}' updated successfully.");
        }

        // Delete: Removes an item by its key
        public void Delete(TKey id)
        {
            if (!items.ContainsKey(id))
            {
                throw new KeyNotFoundException($"Cannot delete. No item found with the key '{id}'.");
            }
            items.Remove(id);
            Console.WriteLine($"Item with key '{id}' deleted successfully.");
        }

        // List all items (for testing purposes)
        public void ListAll()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
                return;
            }

            foreach (var item in items)
            {
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }
        }
    }

    // Main program to test DictionaryRepository
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Testing DictionaryRepository<int, Product>");
            var productRepo = new DictionaryRepository<int, Product>();

            // Add products
            productRepo.Add(1, new Product { ProductId = 1, ProductName = "Laptop" });
            productRepo.Add(2, new Product { ProductId = 2, ProductName = "Phone" });

            // List all products
            Console.WriteLine("\nAll Products:");
            productRepo.ListAll();

            // Retrieve a product
            Console.WriteLine("\nRetrieve Product with key 1:");
            var product = productRepo.Get(1);
            Console.WriteLine(product);

            // Update a product
            Console.WriteLine("\nUpdate Product with key 1:");
            productRepo.Update(1, new Product { ProductId = 1, ProductName = "Gaming Laptop" });

            // List all products after update
            Console.WriteLine("\nAll Products after update:");
            productRepo.ListAll();

            // Delete a product
            Console.WriteLine("\nDelete Product with key 2:");
            productRepo.Delete(2);

            // List all products after deletion
            Console.WriteLine("\nAll Products after deletion:");
            productRepo.ListAll();

            // Test exception handling
            try
            {
                Console.WriteLine("\nAttempt to retrieve non-existent product:");
                productRepo.Get(99);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}