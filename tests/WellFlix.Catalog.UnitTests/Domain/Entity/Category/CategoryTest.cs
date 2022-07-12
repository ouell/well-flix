using System;
using FluentAssertions;
using Xunit;

namespace WellFlix.Catalog.UnitTests.Domain.Entity.Category;

[Collection(nameof(CategoryTestFixture))]
public class CategoryTest
{
    private readonly CategoryTestFixture _categoryTestFixture;

    public CategoryTest(CategoryTestFixture categoryTestFixture) => _categoryTestFixture = categoryTestFixture;

    [Fact(DisplayName = nameof(Instantiate))]
    public void Instantiate()
    {
        var datetimeBefore = DateTime.Now;
        var validCategory = _categoryTestFixture.GetValidCategory();

        var category = new Catalog.Domain.Entities.Category(validCategory.Name, validCategory.Description);
        var datetimeAfter = DateTime.Now.AddSeconds(2);
        
        category.Should().NotBeNull();
        category.Name.Should().Be(validCategory.Name);
        category.Description.Should().Be(validCategory.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default);
        (category.CreatedAt >= datetimeBefore).Should().BeTrue();
        (category.CreatedAt <= datetimeAfter).Should().BeTrue();
        category.IsActive.Should().BeTrue();
    }

    [Theory(DisplayName = nameof(IstantiateWithIsActiveParameter))]
    [InlineData(true)]
    [InlineData(false)]
    public void IstantiateWithIsActiveParameter(bool isActive)
    {
        var datetimeBefore = DateTime.Now;
        var validCategory = _categoryTestFixture.GetValidCategory();

        var category = new Catalog.Domain.Entities.Category(validCategory.Name, validCategory.Description, isActive);
        var datetimeAfter = DateTime.Now.AddSeconds(2);
        
        category.Should().NotBeNull();
        category.Name.Should().Be(validCategory.Name);
        category.Description.Should().Be(validCategory.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default);
        (category.CreatedAt >= datetimeBefore).Should().BeTrue();
        (category.CreatedAt <= datetimeAfter).Should().BeTrue();
        category.IsActive.Should().Be(isActive);
    }

    [Theory(DisplayName = nameof(IstantiateErrorWhenNameIsEmpty))]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    public void IstantiateErrorWhenNameIsEmpty(string name)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var invalidCategory = new Catalog.Domain.Entities.Category(name, validCategory.Description);
        
        invalidCategory.IsValid.Should().BeFalse();
        invalidCategory.Messages.Should().Contain("Name should not be empty or null");
    }
    
    [Theory(DisplayName = nameof(IstantiateErrorWhenNameIsEmpty))]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    public void IstantiateErrorWhenDescriptionIsEmpty(string description)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var invalidCategory = new Catalog.Domain.Entities.Category(validCategory.Name, description);
        
        invalidCategory.IsValid.Should().BeFalse();
        invalidCategory.Messages.Should().Contain("Description should not be empty or null");
    }
}