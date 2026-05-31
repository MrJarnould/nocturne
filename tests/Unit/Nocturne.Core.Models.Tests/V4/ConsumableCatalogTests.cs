using FluentAssertions;
using Nocturne.Core.Models.V4;
using Xunit;

namespace Nocturne.Core.Models.Tests.V4;

[Trait("Category", "Unit")]
public class ConsumableCatalogTests
{
    [Fact]
    public void GetAll_ShouldReturnNonEmpty()
    {
        ConsumableCatalog.GetAll().Should().NotBeEmpty();
    }

    [Fact]
    public void EveryEntry_ShouldReferenceAKnownDeviceCatalogEntry()
    {
        var deviceIds = DeviceCatalog.GetAll().Select(d => d.Id).ToHashSet();
        var orphans = ConsumableCatalog.GetAll()
            .Where(c => !deviceIds.Contains(c.DeviceCatalogId))
            .Select(c => c.Id)
            .ToList();
        orphans.Should().BeEmpty(
            "every consumable must reference a device that exists in the device catalog");
    }

    [Fact]
    public void EveryEntry_ShouldHaveUniqueId()
    {
        ConsumableCatalog.GetAll().Select(c => c.Id).Should().OnlyHaveUniqueItems();
    }

    [Fact]
    public void EverySensorEntry_ShouldHaveWearDays()
    {
        var sensors = ConsumableCatalog.GetByKind(ConsumableKind.CgmSensor);
        sensors.Should().NotBeEmpty();
        sensors.Should().OnlyContain(s => s.WearDays.HasValue && s.WearDays.Value > 0,
            "sensors run-out projection depends on a positive WearDays");
    }

    [Fact]
    public void EveryPodEntry_ShouldHaveWearDaysAndCannulaSpec()
    {
        var pods = ConsumableCatalog.GetByKind(ConsumableKind.Pod);
        pods.Should().NotBeEmpty();
        pods.Should().OnlyContain(p => p.WearDays.HasValue && p.WearDays.Value > 0);
        pods.Should().OnlyContain(p => p.Cannula != null);
    }

    [Fact]
    public void EveryDisposableTransmitterEntry_ShouldHaveWearDays()
    {
        var transmitters = ConsumableCatalog.GetByKind(ConsumableKind.CgmTransmitter);
        // Only disposable transmitters appear here; rechargeable transmitters
        // are durables (live on PatientDevice) and have no consumable entry.
        transmitters.Should().OnlyContain(t => t.WearDays.HasValue && t.WearDays.Value > 0);
    }

    [Fact]
    public void RechargeableTransmitterDevices_ShouldNotEmitATransmitterConsumable()
    {
        // Guardian 3 has a rechargeable transmitter (durable). Eversense
        // (not yet in the catalog) would behave the same way. The transmitter
        // must not appear as a consumable SKU.
        var guardian3Consumables = ConsumableCatalog.GetForDevice("medtronic-guardian-3");
        guardian3Consumables.Should().OnlyContain(c => c.Kind != ConsumableKind.CgmTransmitter,
            "rechargeable transmitters are durables, not consumables");
    }

    [Fact]
    public void DisposableTransmitterDevices_ShouldEmitASensorAndTransmitterConsumable()
    {
        var g6Consumables = ConsumableCatalog.GetForDevice("dexcom-g6");
        g6Consumables.Should().Contain(c => c.Kind == ConsumableKind.CgmSensor);
        g6Consumables.Should().Contain(c => c.Kind == ConsumableKind.CgmTransmitter);
    }

    [Fact]
    public void TubelessPumps_ShouldEmitExactlyOnePodConsumable()
    {
        foreach (var id in new[] { "omnipod-5", "omnipod-dash" })
        {
            var consumables = ConsumableCatalog.GetForDevice(id);
            consumables.Should().HaveCount(1, $"{id} has a single pod SKU");
            consumables.Single().Kind.Should().Be(ConsumableKind.Pod);
        }
    }

    [Fact]
    public void TandemTubedPumps_ShouldEmitReservoirAndInfusionSetAndSeparateCannula()
    {
        // Tandem sells cannulas separately from set bodies.
        var tslimConsumables = ConsumableCatalog.GetForDevice("tandem-tslim-x2");
        tslimConsumables.Select(c => c.Kind).Should().BeEquivalentTo(new[]
        {
            ConsumableKind.Reservoir, ConsumableKind.InfusionSet, ConsumableKind.Cannula
        });
    }

    [Fact]
    public void MedtronicTubedPumps_ShouldEmitReservoirAndInfusionSetOnly()
    {
        // Medtronic sells the set as one SKU that includes the cannula.
        var medtronicConsumables = ConsumableCatalog.GetForDevice("medtronic-780g");
        medtronicConsumables.Select(c => c.Kind).Should().BeEquivalentTo(new[]
        {
            ConsumableKind.Reservoir, ConsumableKind.InfusionSet
        });
    }

    [Fact]
    public void EveryInfusionSet_ShouldHaveCannulaSpec()
    {
        var sets = ConsumableCatalog.GetByKind(ConsumableKind.InfusionSet);
        sets.Should().NotBeEmpty();
        sets.Should().OnlyContain(s => s.Cannula != null);
    }
}
