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

        // 해상도 옵션을 표시하기 위해 문자열 리스트 생성
        var options = availableResolutions.Select(resolution => resolution.width + " x " + resolution.height).ToList();

        resolutionDropdown.AddOptions(options);

        // 현재 해상도 설정
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