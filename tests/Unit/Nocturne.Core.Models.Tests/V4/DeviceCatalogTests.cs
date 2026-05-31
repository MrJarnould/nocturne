using FluentAssertions;
using Nocturne.Core.Models.V4;
using Xunit;

namespace Nocturne.Core.Models.Tests.V4;

[Trait("Category", "Unit")]
public class DeviceCatalogTests
{
    [Fact]
    public void GetAll_ShouldReturnAllEntries()
    {
        var entries = DeviceCatalog.GetAll();
        entries.Should().NotBeEmpty();
    }

    [Fact]
    public void GetAll_ShouldContainBothCgmsAndPumps()
    {
        var entries = DeviceCatalog.GetAll();
        entries.Should().Contain(e => e.PrimaryCategory == DeviceCategory.CGM);
        entries.Should().Contain(e => e.PrimaryCategory == DeviceCategory.InsulinPump);
    }

    [Fact]
    public void GetById_ShouldReturnMatchingEntry()
    {
        var entry = DeviceCatalog.GetById("dexcom-g7");
        entry.Should().NotBeNull();
        entry!.Name.Should().Be("Dexcom G7");
        entry.PrimaryCategory.Should().Be(DeviceCategory.CGM);
    }

    [Fact]
    public void GetById_ShouldReturnNullForUnknownId()
    {
        var entry = DeviceCatalog.GetById("nonexistent");
        entry.Should().BeNull();
    }

    [Fact]
    public void GetByCategory_ShouldFilterCorrectly()
    {
        var cgms = DeviceCatalog.GetByCategory(DeviceCategory.CGM);
        cgms.Should().OnlyContain(e => e.PrimaryCategory == DeviceCategory.CGM);
    }

    [Fact]
    public void GetByCategory_ForUnsupportedCategory_ShouldReturnEmpty()
    {
        var meters = DeviceCatalog.GetByCategory(DeviceCategory.GlucoseMeter);
        meters.Should().BeEmpty();
    }

    [Fact]
    public void CgmEntries_ShouldHaveCgmCapability()
    {
        var cgms = DeviceCatalog.GetByCategory(DeviceCategory.CGM);
        cgms.Where(e => e.Id != "custom-cgm")
            .Should().OnlyContain(e => e.Cgm != null);
    }

    [Fact]
    public void PumpEntries_ShouldNotHaveCgmCapability()
    {
        var pumps = DeviceCatalog.GetByCategory(DeviceCategory.InsulinPump);
        pumps.Should().OnlyContain(e => e.Cgm == null);
    }

    [Fact]
    public void PumpEntries_ShouldHavePumpCapability()
    {
        var pumps = DeviceCatalog.GetByCategory(DeviceCategory.InsulinPump);
        pumps.Where(e => e.Id != "custom-pump")
            .Should().OnlyContain(e => e.Pump != null);
    }

    [Fact]
    public void TubelessPumps_ShouldBeMarkedAsTubeless()
    {
        var pods = new[] { "omnipod-5", "omnipod-dash" };
        foreach (var id in pods)
        {
            var entry = DeviceCatalog.GetById(id);
            entry!.Pump!.IsTubeless.Should().BeTrue($"{id} is a pod");
        }
    }

    [Fact]
    public void TubedPumps_ShouldNotBeMarkedAsTubeless()
    {
        var tubed = new[] { "tandem-tslim-x2", "medtronic-780g", "ypsopump", "dana-i" };
        foreach (var id in tubed)
        {
            var entry = DeviceCatalog.GetById(id);
            entry!.Pump!.IsTubeless.Should().BeFalse($"{id} is tubed");
        }
    }

    [Fact]
    public void CommercialAidPumps_ShouldHaveAidCapability()
    {
        // Pumps that ship with a built-in commercial AID algorithm.
        var aidPumps = new[] { "omnipod-5", "tandem-tslim-x2", "tandem-mobi", "medtronic-780g", "medtronic-770g" };
        foreach (var id in aidPumps)
        {
            var entry = DeviceCatalog.GetById(id);
            entry!.Aid.Should().NotBeNull($"{id} ships with a commercial AID");
            entry.Aid!.IsCommercial.Should().BeTrue();
        }
    }

    [Fact]
    public void NonAidPumps_ShouldNotHaveAidCapability()
    {
        // Conventional pumps that don't ship with a closed-loop algorithm. DIY
        // patients run Loop/Trio/AAPS on these and that lives on PatientDevice
        // rather than the catalog.
        var nonAid = new[] { "omnipod-dash", "ypsopump", "dana-i", "dana-rs" };
        foreach (var id in nonAid)
        {
            var entry = DeviceCatalog.GetById(id);
            entry!.Aid.Should().BeNull($"{id} doesn't ship with a built-in AID");
        }
    }

    [Fact]
    public void CgmWithDisposableTransmitter_ShouldFlagItAsDisposable()
    {
        var g6 = DeviceCatalog.GetById("dexcom-g6");
        g6!.Cgm!.HasSeparateTransmitter.Should().BeTrue();
        g6.Cgm.TransmitterIsRechargeable.Should().BeFalse();
        g6.Cgm.TransmitterDurationDays.Should().NotBeNull();
    }

    [Fact]
    public void CgmWithRechargeableTransmitter_ShouldFlagItAsRechargeable()
    {
        var guardian3 = DeviceCatalog.GetById("medtronic-guardian-3");
        guardian3!.Cgm!.HasSeparateTransmitter.Should().BeTrue();
        guardian3.Cgm.TransmitterIsRechargeable.Should().BeTrue();
    }

    [Fact]
    public void CgmWithoutSeparateTransmitter_ShouldNotHaveTransmitterDuration()
    {
        var g7 = DeviceCatalog.GetById("dexcom-g7");
        g7!.Cgm!.HasSeparateTransmitter.Should().BeFalse();
        g7.Cgm.TransmitterDurationDays.Should().BeNull();
    }

    [Fact]
    public void AllEntries_ShouldHaveUniqueIds()
    {
        var entries = DeviceCatalog.GetAll();
        entries.Select(e => e.Id).Should().OnlyHaveUniqueItems();
    }
}
