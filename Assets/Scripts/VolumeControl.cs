using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer audioMixer;

    void Start()
    {
        // 슬라이더 값 초기 설정
        float currentVolume;
        audioMixer.GetFloat("MasterVolume", out currentVolume);
        volumeSlider.value = currentVolume;

        // 슬라이더 값이 변경될 때 호출될 메서드 등록
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }
}