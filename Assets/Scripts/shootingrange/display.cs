using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class display : MonoBehaviour
{
    public GameObject textMeshProObject1; // TextMesh Pro ������Ʈ
    private bool isTextActive = false;
    public GameObject textMeshProObject2; 
    public GameObject textMeshProObject3; 
    public GameObject textMeshProObject4;
    public GameObject textMeshProObject5;
    public GameObject popup;
    public int b = 0;
    public TextMeshPro text;
    void Start()
    {
        // TextMesh Pro ������Ʈ�� �ʱ⿡�� ��Ȱ��ȭ
        if (textMeshProObject1 != null)
        {
            textMeshProObject1.SetActive(false);
        }
    }

    void Update()
    {
        // ���콺 ��ġ���� Ray�� ��
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Ray�� ������Ʈ 'a'�� �浹�ϴ��� üũ
        if (Physics.Raycast(ray, out hit))
        {
            
            // �浹�� ������Ʈ�� ���� ��ũ��Ʈ�� �پ� �ִ� ������Ʈ���� Ȯ��
            if (hit.collider.gameObject == gameObject)
            {
                popup.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    b++;
                    if (b == 1)
                    {
                        // TextMesh Pro ������Ʈ�� Ȱ��ȭ ���¸� ��ȯ

                        if (textMeshProObject1 != null)
                        {
                            textMeshProObject1.SetActive(true);
                            textMeshProObject5.SetActive(false);
                            stext();
                        }
                    }
                    if (b == 2)
                    {
                        // TextMesh Pro ������Ʈ�� Ȱ��ȭ ���¸� ��ȯ

                        if (textMeshProObject2 != null)
                        {
                            textMeshProObject2.SetActive(true);
                            textMeshProObject1.SetActive(false);
                            stext();
                        }
                    }
                    if (b == 3)
                    {
                        // TextMesh Pro ������Ʈ�� Ȱ��ȭ ���¸� ��ȯ

                        if (textMeshProObject3 != null)
                        {
                            textMeshProObject3.SetActive(true);
                            textMeshProObject2.SetActive(false);
                            stext();
                        }
                    }
                    if (b == 4)
                    {
                        // TextMesh Pro ������Ʈ�� Ȱ��ȭ ���¸� ��ȯ

                        if (textMeshProObject4 != null)
                        {
                            textMeshProObject4.SetActive(true);
                            textMeshProObject3.SetActive(false);
                            stext();
                        }
                    }
                    if (b == 5)
                    {
                        // TextMesh Pro ������Ʈ�� Ȱ��ȭ ���¸� ��ȯ

                        if (textMeshProObject5 != null)
                        {
                            textMeshProObject5.SetActive(true);
                            textMeshProObject4.SetActive(false);
                            stext();
                            b = 0;
                        }
                    }
                    // 'F' Ű�� ���ȴ��� Ȯ��

                }
            

            }
            else
            {
                popup.SetActive(false);
         }

            }
        
    }
    void stext()
    {
        text.text =  b + "/5";
    }
}