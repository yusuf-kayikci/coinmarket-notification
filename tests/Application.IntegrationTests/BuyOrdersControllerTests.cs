using NUnit.Framework;
using System.Net.Http.Json;
using CoinMarket.Application.Common.Interfaces;
using CoinMarket.Application.IntegrationTests;
using CoinMarket.Application.Order.Models;
using Moq;

[TestFixture]
public class TodoControllerTests
{
    private BuyOrdersController _buyOrderControllers;
    private Mock<IMessagePublisher> _mockMessagePublisher;
    
    public TodoControllerTests()
    {
    }

    [SetUp]
    public void Setup()
    {
        _client = new CustomWebApplicationFactory().CreateClient();
        
    }
    
    [Test]
    public async Task GetAll_ReturnsEmptyList()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/api/buyorders/1");
        response.EnsureSuccessStatusCode();
        var buyOrder = await response.Content.ReadFromJsonAsync<List<BuyOrderDTO>>();

        // Assert
        Assert.IsNotEmpty(buyOrder);
    }

    // [Test]
    // public async Task Create_AddsNewTodoItem()
    // {
    //     // Arrange
    //     var newTodo = new TodoItem { Name = "Test Todo Item" };
    //
    //     // Act
    //     var response = await _client.PostAsJsonAsync("/api/todo", newTodo);
    //     response.EnsureSuccessStatusCode();
    //     var todo = await response.Content.ReadFromJsonAsync<TodoItem>();
    //
    //     // Assert
    //     Assert.IsNotNull(todo);
    //     Assert.AreEqual(newTodo.Name, todo.Name);
    // }
    //
    // [Test]
    // public async Task Update_UpdatesExistingTodoItem()
    // {
    //     // Arrange
    //     var newTodo = new TodoItem { Name = "Test Todo Item" };
    //     var createResponse = await _client.PostAsJsonAsync("/api/todo", newTodo);
    //     createResponse.EnsureSuccessStatusCode();
    //     var createdTodo = await createResponse.Content.ReadFromJsonAsync<TodoItem>();
    //     var updatedTodo = new TodoItem { Id = createdTodo.Id, Name = "Updated Todo Item" };
    //
    //     // Act
    //     var updateResponse = await _client.PutAsJsonAsync($"/api/todo/{createdTodo.Id}", updatedTodo);
    //     updateResponse.EnsureSuccessStatusCode();
    //     var todo = await updateResponse.Content.ReadFromJsonAsync<TodoItem>();
    //
    //     // Assert
    //     Assert.IsNotNull(todo);
    //     Assert.AreEqual(updatedTodo.Name, todo.Name);
    // }
    //
    // [Test]
    // public async Task Delete_RemovesExistingTodoItem()
    // {
    //     // Arrange
    //     var newTodo = new TodoItem { Name = "Test Todo Item" };
    //     var createResponse = await _client.PostAsJsonAsync("/api/todo", newTodo);
    //     createResponse.EnsureSuccessStatusCode();
    //     var createdTodo = await createResponse.Content.ReadFromJsonAsync<TodoItem>();
    //
    //     // Act
    //     var deleteResponse = await _client.DeleteAsync($"/api/todo/{createdTodo.Id}");
    //     deleteResponse.EnsureSuccessStatusCode();
    //     var response = await _client.GetAsync("/api/todo");
    //     response.EnsureSuccessStatusCode();
    //     var todos = await response.Content.ReadFromJsonAsync<List<TodoItem>>();
    //
    //     // Assert
    //     Assert.IsEmpty(todos);
    // }
}