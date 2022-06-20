using System;
using Bogus;

namespace WellFlix.Catalog.UnitTests.Common;

public abstract class BaseFixture
{
    public Faker Faker { get; set; }
    
    protected BaseFixture() => Faker = new Faker("pt_BR");
    
    public bool GetRandomBoolena() => new Random().NextDouble()  < 0.5;
}