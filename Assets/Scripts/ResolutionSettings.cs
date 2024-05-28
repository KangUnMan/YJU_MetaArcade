using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ResolutionSettings : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Button applyButton;

    private Resolution[] availableResolutions;

    void Start()
    {
        availableResolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        // �ػ� �ɼ��� ǥ���ϱ� ���� ���ڿ� ����Ʈ ����
        var options = availableResolutions.Select(resolution => resolution.width + " x " + resolution.height).ToList();

        resolutionDropdown.AddOptions(options);

        // ���� �ػ� ����
        Resolution currentResolution = Screen.currentResolution;
        int currentIndex = availableResolutions.ToList().FindIndex(resolution =>
            resolution.width == Screen.width && resolution.height == Screen.height);

        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();

        applyButton.onClick.AddListener(ApplyResolution);
    }

    void ApplyResolution()
    {
        int selectedIndex = resolutionDropdown.value;
        Resolution selectedResolution = availableResolutions[selectedIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
}