using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace WellFlix.Catalog.UnitTests.Domain.Entity.Category;

[Collection(nameof(CategoryTestFixture))]
public class CategoryTest
{
    private readonly CategoryTestFixture _categoryTestFixture;

    public CategoryTest(CategoryTestFixture categoryTestFixture)
    {
        _categoryTestFixture = categoryTestFixture;
    }

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

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
    [MemberData(nameof(GetNamesWithLessThan3Characters), 10)]
    public void InstantiateErrorWhenNameIsLessThan3Characters(string invalidName)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var invalidCategory = new Catalog.Domain.Entities.Category(invalidName, validCategory.Description);

        invalidCategory.IsValid.Should().BeFalse();
        invalidCategory.Messages.Should().Contain("Name should be at least 3 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
    public void InstantiateErrorWhenNameIsGreaterThan255Characters()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var invalidName = string.Join(null, Enumerable.Range(0, 256).Select(_ => "a").ToArray());

        var invalidCategory = new Catalog.Domain.Entities.Category(invalidName, validCategory.Description);

        invalidCategory.IsValid.Should().BeFalse();
        invalidCategory.Messages.Should().Contain("Name should be at most 255 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters))]
    public void InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var invalidDescription = string.Join(null, Enumerable.Range(0, 10_001).Select(_ => "a").ToArray());

        var invalidCategory = new Catalog.Domain.Entities.Category(validCategory.Name, invalidDescription);

        invalidCategory.IsValid.Should().BeFalse();
        invalidCategory.Messages.Should().Contain("Description should be at most 10,000 characters long");
    }

    [Fact(DisplayName = nameof(Activate))]
    public void Activate()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();

        var category = new Catalog.Domain.Entities.Category(validCategory.Name, validCategory.Description, false);
        category.Activate();

        category.IsActive.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(Deactivate))]
    public void Deactivate()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();

        var category = new Catalog.Domain.Entities.Category(validCategory.Name, validCategory.Description);
        category.Deactivate();

        category.IsActive.Should().BeFalse();
    }

    [Fact(DisplayName = nameof(Update))]
    public void Update()
    {
        var category = _categoryTestFixture.GetValidCategory();
        var categoryWithNewValues = _categoryTestFixture.GetValidCategory();

        category.Update(categoryWithNewValues.Name, categoryWithNewValues.Description);

        category.Name.Should().Be(categoryWithNewValues.Name);
        category.Description.Should().Be(categoryWithNewValues.Description);
    }

    [Fact(DisplayName = nameof(UpdateOnlyName))]
    public void UpdateOnlyName()
    {
        var category = _categoryTestFixture.GetValidCategory();
        var oldDescription = category.Description;
        var newName = _categoryTestFixture.GetValidCategoryName();
        
        category.Update(newName);

        category.Name.Should().Be(newName);
        category.Description.Should().Be(oldDescription);
    }

    private static IEnumerable<object[]> GetNamesWithLessThan3Characters(int numberOfTests = 6)
    {
        var fixture = new CategoryTestFixture();
        for (var i = 0; i < numberOfTests; i++)
        {
            var isOdd = i % 2 == 1;
            yield return new object[]
            {
                fixture.GetValidCategoryName()[..(isOdd ? 1 : 2)]
            };
        }
    }
}