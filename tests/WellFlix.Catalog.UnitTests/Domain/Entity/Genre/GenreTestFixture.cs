using System;
using System.Collections.Generic;
using System.Linq;
using WellFlix.Catalog.UnitTests.Common;
using Xunit;
using DomainEntity = WellFlix.Catalog.Domain.Entities;

namespace WellFlix.Catalog.UnitTests.Domain.Entity.Genre;

[CollectionDefinition(nameof(GenreTestFixture))]
public class GenreTestFixtureCollection : ICollectionFixture<GenreTestFixture>
{
    
}

public class GenreTestFixture : BaseFixture
{
    public string GetValidName() => Faker.Commerce.Categories(1)[0];

    public DomainEntity.Genre GetGenre(bool isActive = true,
                                       List<Guid>? categoriesListGuid = null)
    {
        var genre = new DomainEntity.Genre(GetValidName(), isActive);

        if (categoriesListGuid is not null && categoriesListGuid.Any())
        {
            foreach (var categoryId in categoriesListGuid)
            {
                genre.AddCategory(categoryId);
            }
        }

        return genre;
    }
}