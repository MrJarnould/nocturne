using System.Text.Json.Serialization;

namespace Nocturne.Core.Models.V4;

/// <summary>
/// The physical pack form a quantity of insulin ships in. One
/// <see cref="InsulinFormulation"/> can be sold in multiple pack forms (e.g.
/// Humalog ships as 10 mL vials, 3 mL cartridges, and disposable KwikPens);
/// inventory tracks each pack form as its own <see cref="InsulinSku"/>.
/// </summary>
/// <seealso cref="InsulinSku"/>
[JsonConverter(typeof(JsonStringEnumConverter<InsulinPackForm>))]
public enum InsulinPackForm
{
    /// <summary>Glass vial drawn into a syringe or transferred into a pump cartridge.</summary>
    Vial,

    /// <summary>Insulin cartridge designed for a reusable pen or for a pump that accepts cartridges.</summary>
    Cartridge,

    /// <summary>Pre-filled disposable pen (e.g. KwikPen, FlexPen, SoloStar).</summary>
    DisposablePen
}
