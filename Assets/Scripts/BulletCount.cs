using UnityEngine;
using UnityEngine.UI; // UI�� ����ϱ� ���� �߰�

public class BulletCount : MonoBehaviour
{
    public Text counterText; // UI Text�� ������ ����
    private int count = 0; // ī���� ����
    private int maxCount = 30; // �ִ� ī��Ʈ
   
    

    


    void Update()
    {
        // ���콺 ��Ŭ�� ����

        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
           
            // �ִ� ī��Ʈ�� ���� �ʵ��� ����
            if (count < maxCount)
            {
                count++;
                UpdateCounterText();
            }
            
           
        }
        
    }

    // UI �ؽ�Ʈ�� ������Ʈ�ϴ� �޼���
    void UpdateCounterText()
    {
        counterText.text = count + "/30";
    }
    private void Reset()
    {
        count = 0;
    }




}