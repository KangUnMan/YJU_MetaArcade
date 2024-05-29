using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomObjectDisplay : MonoBehaviour
{
    public List<GameObject> objects; // ������ ������Ʈ���� ����Ʈ�� �����մϴ�.
    public float interval = 2f; // ������Ʈ ���� ���� (��)
    public float objectDisplayDuration = 0.5f; // ������Ʈ�� Ȱ��ȭ�� ���¸� �����ϴ� �ð� (��)
    private bool isActive = true; // ��ũ��Ʈ Ȱ��ȭ ���θ� ��Ÿ���� ����
    private Coroutine displayCoroutine; // �ڷ�ƾ ���� ����

    void Start()
    {
        // ��ũ��Ʈ ���� �� �ڷ�ƾ�� �����մϴ�.
        displayCoroutine = StartCoroutine(ChangeObjectRepeatedly());
    }

    IEnumerator ChangeObjectRepeatedly()
    {
        // ����Ʈ�� ������Ʈ�� �ִ��� Ȯ���մϴ�.
        if (objects.Count == 0)
        {
            Debug.LogWarning("No objects assigned to display.");
            yield break;
        }

        while (isActive)
        {
            // �������� ������Ʈ�� �����մϴ�.
            int randomIndex = Random.Range(0, objects.Count);

            // ��� ������Ʈ�� ����ϴ�.
            foreach (GameObject obj in objects)
            {
                obj.SetActive(false);
            }

            // ���õ� ������Ʈ�� ���̰� �մϴ�.
            GameObject selectedObject = objects[randomIndex];
            selectedObject.SetActive(true);

            yield return new WaitForSeconds(objectDisplayDuration);
            selectedObject.SetActive(false);

            // �߰����� ��� �ð��� �߰��Ͽ� ������Ʈ�� �����Ǵ� ���ݿ� ���� �Ӵϴ�.
            yield return new WaitForSeconds(interval);

            // ���� �ð��� ���� �Ŀ� ������Ʈ�� �ٽ� ��Ȱ��ȭ�մϴ�.
            
        }
    }

// ��ũ��Ʈ�� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�ϴ� �Լ�
public void SetActive(bool active)
    {
        isActive = active;

        if (!isActive && displayCoroutine != null)
        {
            // ��ũ��Ʈ�� ��Ȱ��ȭ�� �� �ڷ�ƾ�� �����մϴ�.
            StopCoroutine(displayCoroutine);
        }
    }
}
