using TMPro;
using UnityEngine;

namespace View
{
    public class HudView : UiView
    {
        [field: SerializeField] public TextMeshProUGUI PositionLabel { get; set; }
        [field: SerializeField] public TextMeshProUGUI RotationLabel { get; set; }
        [field: SerializeField] public TextMeshProUGUI VelocityLabel { get; set; }
        [field: SerializeField] public TextMeshProUGUI LaserChargesLabel { get; set; }
        [field: SerializeField] public TextMeshProUGUI LaserCooldownLabel { get; set; }
        [field: SerializeField] public TextMeshProUGUI PointsLabel { get; set; }
    }
}