using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ProjectSMEHelper.API.Contracts.Users.Requests;
using ProjectSMEHelper.API.DBContext.PostgreDBContext;
using ProjectSMEHelper.API.Models.Users;
using ProjectSMEHelper.API.Services.UserServices.Implementations;
using System.Linq.Expressions;
using Xunit;

namespace UnitTests;

public class ManageUserServiceTests
{
    [Fact]
    public async Task RegisterUser_EmailExists_ReturnsEmailExistsResponse()
    {
        // Arrange
        var mockDbContext = new Mock<PostgreDbContext>();
        var mockUtility = new Mock<ProjectSMEHelper.API.Helpers.Utility>();
        var userService = new ManageUserService(mockDbContext.Object, mockUtility.Object);

        var existingUser = new User { Email = "existing@example.com" };
        //mockDbContext.Setup(db => db.User.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()))
        //             .ReturnsAsync(existingUser);
        mockDbContext.Setup(db => db.User.FirstOrDefaultAsync(It.IsAny<User>()))
                     .ReturnsAsync(existingUser);

        var registerRequest = new RegisterUserRequest { Email = "existing@example.com" };

        // Act
        var result = await userService.RegisterUser(registerRequest);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual("Email exists", result.FirstName);
    }

    // Similar test cases for other scenarios...

    [Fact]
    public async Task VerifyToken_ValidToken_ReturnsTrue()
    {
        // Arrange
        var mockDbContext = new Mock<PostgreDbContext>();
        var mockUtility = new Mock<ProjectSMEHelper.API.Helpers.Utility>();
        var userService = new ManageUserService(mockDbContext.Object, mockUtility.Object);

        var token = "validToken";
        var user = new User { VerificationToken = token };
        mockDbContext.Setup(db => db.User.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>()))
                     .Returns(user);

        // Act
        var result = await userService.VerifyToken(token);

        // Assert
        Assert.True(result);
        Assert.Null(user.VerificationToken);
        Assert.AreEqual(2, user.Status);
        // Verify that SaveChangesAsync was called
        mockDbContext.Verify(db => db.SaveChangesAsync(), Times.Once);
    }
}