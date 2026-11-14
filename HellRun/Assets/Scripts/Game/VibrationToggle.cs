using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class VibrationToggle : MonoBehaviour
{
    private Toggle toggle;

    private void OnEnable()
    {
        toggle = GetComponent<Toggle>();
        toggle.SetIsOnWithoutNotify(Services.Settings.VibrationOn);
        toggle.onValueChanged.AddListener(SetVibration);
    }

    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(SetVibration);
    }

    private void SetVibration(bool enabled)
    {
        Services.Settings.VibrationOn = enabled;
    }
}
