namespace Nocturne.Core.Models.V4;

/// <summary>
/// Automated Insulin Delivery (AID) capability composed onto a
/// <see cref="DeviceCatalogEntry"/>. Present only for pumps (or pump+CGM
/// systems) that run a closed-loop algorithm. The algorithm is a property of
/// the device model in the catalog; the patient's actual running algorithm
/// lives on <see cref="PatientDevice.AidAlgorithm"/> and may differ when the
/// patient is running a DIY algorithm on commercial hardware.
/// </summary>
/// <seealso cref="DeviceCatalogEntry"/>
/// <seealso cref="AidAlgorithm"/>
/// <seealso cref="PatientDevice"/>
public record AidCapability
{
    /// <summary>
    /// The AID algorithm the device ships with (e.g.
    /// <see cref="AidAlgorithm.SmartGuard"/> for MiniMed 780G,
    /// <see cref="AidAlgorithm.ControlIq"/> for t:slim X2,
    /// <see cref="AidAlgorithm.OmnipodAid"/> for Omnipod 5).
    /// </summary>
    public required AidAlgorithm Algorithm { get; init; }

    /// <summary>
    /// True for commercial closed-loop systems (Omnipod 5, MiniMed 780G,
    /// t:slim X2 Control-IQ). False for DIY algorithms (Loop, Trio, AAPS,
    /// OpenAPS) — those run on otherwise-conventional hardware. Useful for
    /// UI grouping and for distinguishing prescribed vs. DIY therapy in
    /// statistics.
    /// </summary>
    public required bool IsCommercial { get; init; }
}
