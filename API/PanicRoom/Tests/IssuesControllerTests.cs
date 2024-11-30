using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanicRoom.Controllers;
using PanicRoom.DAL;
using PanicRoom.Entities;
using Xunit;

namespace PanicRoom.Tests
{
    public class IssuesControllerTests
    {
        private readonly PanicRoomDbContext _context;
        private readonly IssuesController _controller;

        public IssuesControllerTests()
        {
            var options = new DbContextOptionsBuilder<PanicRoomDbContext>()
                .UseInMemoryDatabase(databaseName: "PanicRoomTestDb")
                .Options;

            _context = new PanicRoomDbContext(options);
            _controller = new IssuesController(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Issues.AddRange(new[]
            {
                new Issue { Id = 3, Title = "Issue 3", Description = "Description 3", Created = DateTime.UtcNow },
                new Issue { Id = 4, Title = "Issue 4", Description = "Description 4", Created = DateTime.UtcNow }
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetIssues_ReturnsAllIssues()
        {
            var result = await _controller.GetIssues();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var issues = Assert.IsAssignableFrom<IEnumerable<Issue>>(okResult.Value);

            Assert.Equal(2, issues.Count());
        }

        [Fact]
        public async Task GetIssue_ReturnsIssueById()
        {
            var result = await _controller.GetIssue(1);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var issue = Assert.IsType<Issue>(okResult.Value);

            Assert.Equal(1, issue.Id);
        }

        [Fact]
        public async Task CreateIssue_AddsNewIssue()
        {
            var newIssue = new Issue { Title = "Issue 5", Description = "Description 5" };
            var result = await _controller.CreateIssue(newIssue);

            Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(3, _context.Issues.Count());
        }

        [Fact]
        public async Task DeleteIssue_RemovesIssue()
        {
            var result = await _controller.DeleteIssue(1);

            Assert.IsType<NoContentResult>(result);
            Assert.Equal(1, _context.Issues.Count());
        }
    }
}
