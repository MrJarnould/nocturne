using FluentAssertions;
using Nocturne.Core.Models.V4;
using Xunit;

namespace Nocturne.Core.Models.Tests.V4;

[Trait("Category", "Unit")]
public class InsulinSkuCatalogTests
{
    [Fact]
    public void GetAll_ShouldReturnNonEmpty()
    {
        InsulinSkuCatalog.GetAll().Should().NotBeEmpty();
    }

    [Fact]
    public void EverySku_ShouldReferenceAKnownFormulation()
    {
        var formulationIds = InsulinCatalog.GetAll().Select(f => f.Id).ToHashSet();
        var orphans = InsulinSkuCatalog.GetAll()
            .Where(s => !formulationIds.Contains(s.FormulationId))
            .Select(s => s.Id)
            .ToList();
        orphans.Should().BeEmpty(
            "every SKU must reference a formulation that exists in the insulin catalog");
    }

    [Fact]
    public void EverySku_ShouldHaveUniqueId()
    {
        InsulinSkuCatalog.GetAll().Select(s => s.Id).Should().OnlyHaveUniqueItems();
    }

    [Fact]
    public void EverySku_ShouldHavePositiveUnitsPerPack()
    {
        InsulinSkuCatalog.GetAll().Should().OnlyContain(s => s.UnitsPerPack > 0);
    }

    [Fact]
    public void StandardU100Formulation_ShouldHaveAtLeastOneVialSku()
    {
        // U100 rapid-acting insulin should be available in a vial form (the
        // most common pump-fill form).
        var humalogSkus = InsulinSkuCatalog.GetForFormulation("humalog");
        humalogSkus.Should().Contain(s => s.Form == InsulinPackForm.Vial);
    }

    [Fact]
    public void U200Formulation_ShouldNotHaveAVialSku()
    {
        // U200 insulin is not sold in vial form commercially — only in pens.
        var u200Skus = InsulinSkuCatalog.GetForFormulation("humalog-u200");
        u200Skus.Should().NotBeEmpty();
        u200Skus.Should().NotContain(s => s.Form == InsulinPackForm.Vial,
            "U200 insulin is not commercially available as a vial");
    }

    [Fact]
    public void U100Vial_ShouldBe1000UnitsPerPack()
    {
        // 10 mL × 100 u/mL = 1000 u.
        var humalogVial = InsulinSkuCatalog.GetById("humalog-vial-10ml");
        humalogVial.Should().NotBeNull();
        humalogVial!.UnitsPerPack.Should().Be(1000);
    }

    [Fact]
    public void U200Cartridge_ShouldBe600UnitsPerPack()
    {
        // 3 mL × 200 u/mL = 600 u.
        var humalogU200 = InsulinSkuCatalog.GetById("humalog-u200-kwikpen-3ml");
        humalogU200.Should().NotBeNull();
        humalogU200!.UnitsPerPack.Should().Be(600);
    }
}
