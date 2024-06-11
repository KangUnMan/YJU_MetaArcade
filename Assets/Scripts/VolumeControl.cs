using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer audioMixer;

    void Start()
    {
        // �����̴� �� �ʱ� ����
        float currentVolume;
        audioMixer.GetFloat("MasterVolume", out currentVolume);
        volumeSlider.value = currentVolume;

        // �����̴� ���� ����� �� ȣ��� �޼��� ���
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }
}