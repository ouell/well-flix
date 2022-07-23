using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using DomainEntity = WellFlix.Catalog.Domain.Entities;
using DomainValidator = WellFlix.Catalog.Domain.Validation;

namespace WellFlix.Catalog.UnitTests.Domain.Entity.Genre;

[Collection(nameof(GenreTestFixture))]
public class GenreTest
{
    private readonly GenreTestFixture _fixture;

    public GenreTest(GenreTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(Instantiate))]
    public void Instantiate()
    {
        var genreName = _fixture.GetValidName();

        var datetimeBefore = DateTime.Now;
        var datetimeAfter = DateTime.Now.AddSeconds(1);

        var genre = new DomainEntity.Genre(genreName);

        genre.Should().NotBeNull();
        genre.Id.Should().NotBeEmpty();
        genre.Name.Should().Be(genreName);
        genre.IsActive.Should().BeTrue();
        genre.CreatedAt.Should().NotBeSameDateAs(default);
        genre.CreatedAt.Should().BeOnOrAfter(datetimeBefore);
        genre.CreatedAt.Should().NotBeOnOrAfter(datetimeAfter);
    }

    [Theory(DisplayName = nameof(InstantiateThrowWhenNameEmpty))]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    public void InstantiateThrowWhenNameEmpty(string name)
    {
        var invalidGenre = new DomainEntity.Genre(name);

        invalidGenre.IsValid.Should().BeFalse();
        invalidGenre.Messages.Should().Contain("Name should not be empty or null");
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool isActive)
    {
        var genreName = _fixture.GetValidName();

        var datetimeBefore = DateTime.Now;
        var datetimeAfter = DateTime.Now.AddSeconds(1);

        var genre = new DomainEntity.Genre(genreName, isActive);

        genre.Should().NotBeNull();
        genre.Id.Should().NotBeEmpty();
        genre.Name.Should().Be(genreName);
        genre.IsActive.Should().Be(isActive);
        genre.CreatedAt.Should().NotBeSameDateAs(default);
        genre.CreatedAt.Should().BeOnOrAfter(datetimeBefore);
        genre.CreatedAt.Should().NotBeOnOrAfter(datetimeAfter);
    }

    [Theory(DisplayName = nameof(Activate))]
    [InlineData(true)]
    [InlineData(false)]
    public void Activate(bool isActive)
    {
        var genre = _fixture.GetGenre(isActive);
        
        genre.Activate();
        
        genre.Should().NotBeNull();
        genre.Id.Should().NotBeEmpty();
        genre.IsActive.Should().BeTrue();
        genre.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Theory(DisplayName = nameof(Deactivate))]
    [InlineData(true)]
    [InlineData(false)]
    public void Deactivate(bool isActive)
    {
        var genre = _fixture.GetGenre(isActive);
        
        genre.Deactivate();
        
        genre.Should().NotBeNull();
        genre.Id.Should().NotBeEmpty();
        genre.IsActive.Should().BeFalse();
        genre.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Update))]
    public void Update()
    {
        var genre = _fixture.GetGenre();
        var newName = _fixture.GetValidName();
        
        genre.Update(newName);
        
        genre.Should().NotBeNull();
        genre.Id.Should().NotBeEmpty();
        genre.Name.Should().Be(newName);
        genre.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Theory(DisplayName = nameof(UpdateThrowWhenNameIsEmpty))]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public void UpdateThrowWhenNameIsEmpty(string name)
    {
        var genre = _fixture.GetGenre();
        
        genre.Update(name);
        
        genre.IsValid.Should().BeFalse();
        genre.Messages.Should().Contain("Name should not be empty or null");
    }

    [Fact(DisplayName = nameof(AddCategory))]
    public void AddCategory()
    {
        var genre = _fixture.GetGenre();
        var categoryId = Guid.NewGuid();
        
        genre.AddCategory(categoryId);

        genre.Categories.Should().HaveCount(1);
        genre.Categories.Should().Contain(categoryId);
    }

    [Fact(DisplayName = nameof(AddTwoCategories))]
    public void AddTwoCategories()
    {
        var genre = _fixture.GetGenre();
        var categoryId1 = Guid.NewGuid();
        var categoryId2 = Guid.NewGuid();
        
        genre.AddCategory(categoryId1);
        genre.AddCategory(categoryId2);
        
        genre.Categories.Should().HaveCount(2);
        genre.Categories.Should().Contain(categoryId1);
        genre.Categories.Should().Contain(categoryId2);
    }

    [Fact(DisplayName = nameof(RemoveCategory))]
    public void RemoveCategory()
    {
        var categoryId = Guid.NewGuid();
        var genre = _fixture.GetGenre(categoriesListGuid: new List<Guid>
        {
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            categoryId,
            Guid.NewGuid()
        });
        
        genre.RemoveCategory(categoryId);
        
        genre.Categories.Should().HaveCount(4);
        genre.Categories.Should().NotContain(categoryId);
    }

    [Fact(DisplayName = nameof(RemoveAllCategories))]
    public void RemoveAllCategories()
    {
        var genre = _fixture.GetGenre(categoriesListGuid: new List<Guid>
        {
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid()
        });
        
        genre.RemoveAllCategories();
        
        genre.Categories.Should().HaveCount(0);
    }
}