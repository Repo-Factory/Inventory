using NUnit.Framework;
using Microsoft.Data.SqlClient;

[TestFixture]
public class InventoryIntegrationTests
{
    [Test]
    public void AddItem_CheckIfExists_RemoveItem()
    {
        var itemName = "TestItem";
        var quantity = 10;
        var costBasis = 5.99f;
        AddItemToDatabase(itemName, quantity, costBasis);
        Assert.That(ItemExistsInDatabase(itemName), Is.True);
        RemoveItemFromDatabase(itemName);
        Assert.That(ItemExistsInDatabase(itemName), Is.False);
    }

    private static async void AddItemToDatabase(string name, int quantity, float costBasis)
    {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync($"http://localhost:5000/buyProduct?name={name}");
        response.EnsureSuccessStatusCode();
    }

    private static bool ItemExistsInDatabase(string name)
    {
        using SqlConnection connection = Connect();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Inventory WHERE Name = @Name";
        command.Parameters.AddWithValue("@Name", name);
        var count = (int)command.ExecuteScalar();
        return count > 0;
    }

    private static async void RemoveItemFromDatabase(string name)
    {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync($"http://localhost:5000/sellProduct?name={name}");
        response.EnsureSuccessStatusCode();
    }

    private static SqlConnection Connect()
    {
        SqlConnectionStringBuilder builder = new()
        {
            DataSource = "localhost",
            UserID = "docker_postgres",
            Password = "docker_postgres",
            InitialCatalog = "docker_postgres"
        };
        return new(builder.ConnectionString);
    }
}