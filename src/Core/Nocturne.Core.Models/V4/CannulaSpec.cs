namespace Nocturne.Core.Models.V4;

/// <summary>
/// Physical specification of a cannula. Present on pods (where the cannula
/// is integrated into the pod body) and on infusion-set / standalone-cannula
/// consumables. Used for rotation analytics and for showing the patient what
/// they're actually wearing.
/// </summary>
/// <seealso cref="ConsumableCatalogEntry"/>
public record CannulaSpec
{
    /// <summary>
    /// Cannula gauge — higher number = thinner (e.g. 25, 27, 29). Driven by
    /// manufacturer / model and tied to insertion comfort and absorption.
    /// </summary>
    public required int Gauge { get; init; }

    /// <summary>
    /// Cannula length in millimeters (e.g. 6 mm, 9 mm). Patient body type
    /// and rotation pattern affect which length is appropriate.
    /// </summary>
    public required decimal LengthMm { get; init; }

    /// <summary>
    /// Cannula material (e.g. "Teflon", "Steel"). Drives wear-time tolerance
    /// and allergy considerations.
    /// </summary>
    public required string Material { get; init; }
}
