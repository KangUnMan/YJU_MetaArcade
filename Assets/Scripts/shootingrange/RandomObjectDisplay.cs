using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomObjectDisplay : MonoBehaviour
{
    public List<GameObject> objects; // 보여줄 오브젝트들을 리스트로 선언합니다.
    public float interval = 2f; // 오브젝트 변경 간격 (초)
    public float objectDisplayDuration = 0.5f; // 오브젝트가 활성화된 상태를 유지하는 시간 (초)
    private bool isActive = false; // 스크립트 활성화 여부를 나타내는 변수
    private Coroutine displayCoroutine; // 코루틴 참조 변수

    void Start()
    {
        if (isActive)
        {
            displayCoroutine = StartCoroutine(ChangeObjectRepeatedly());
        }
    }

    IEnumerator ChangeObjectRepeatedly()
    {
        // 리스트에 오브젝트가 있는지 확인합니다.
        if (objects.Count == 0)
        {
            Debug.LogWarning("No objects assigned to display.");
            yield break;
        }

        while (isActive)
        {
            // 랜덤으로 오브젝트를 선택합니다.
            int randomIndex = Random.Range(0, objects.Count);

            // 모든 오브젝트를 숨깁니다.
            foreach (GameObject obj in objects)
            {
                obj.SetActive(false);
            }

            // 선택된 오브젝트를 보이게 합니다.
            GameObject selectedObject = objects[randomIndex];
            selectedObject.SetActive(true);

            yield return new WaitForSeconds(objectDisplayDuration);
            selectedObject.SetActive(false);

            // 추가적인 대기 시간을 추가하여 오브젝트가 생성되는 간격에 텀을 둡니다.
            yield return new WaitForSeconds(interval);
        }
    }

    // 스크립트를 활성화 또는 비활성화하는 함수
    public void SetActive(bool active)
    {
        isActive = active;

        if (isActive)
        {
            if (displayCoroutine == null)
            {
                displayCoroutine = StartCoroutine(ChangeObjectRepeatedly());
            }
        }
        else
        {
            if (displayCoroutine != null)
            {
                StopCoroutine(displayCoroutine);
                displayCoroutine = null;
            }
        }
    }
}
