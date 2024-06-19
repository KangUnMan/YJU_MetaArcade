using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class display : MonoBehaviour
{
    public GameObject textMeshProObject1; // TextMesh Pro 오브젝트
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
        // TextMesh Pro 오브젝트를 초기에는 비활성화
        if (textMeshProObject1 != null)
        {
            textMeshProObject1.SetActive(false);
        }
    }

    void Update()
    {
        // 마우스 위치에서 Ray를 쏨
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Ray가 오브젝트 'a'와 충돌하는지 체크
        if (Physics.Raycast(ray, out hit))
        {
            
            // 충돌한 오브젝트가 현재 스크립트가 붙어 있는 오브젝트인지 확인
            if (hit.collider.gameObject == gameObject)
            {
                popup.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    b++;
                    if (b == 1)
                    {
                        // TextMesh Pro 오브젝트의 활성화 상태를 전환

                        if (textMeshProObject1 != null)
                        {
                            textMeshProObject1.SetActive(true);
                            textMeshProObject5.SetActive(false);
                            stext();
                        }
                    }
                    if (b == 2)
                    {
                        // TextMesh Pro 오브젝트의 활성화 상태를 전환

                        if (textMeshProObject2 != null)
                        {
                            textMeshProObject2.SetActive(true);
                            textMeshProObject1.SetActive(false);
                            stext();
                        }
                    }
                    if (b == 3)
                    {
                        // TextMesh Pro 오브젝트의 활성화 상태를 전환

                        if (textMeshProObject3 != null)
                        {
                            textMeshProObject3.SetActive(true);
                            textMeshProObject2.SetActive(false);
                            stext();
                        }
                    }
                    if (b == 4)
                    {
                        // TextMesh Pro 오브젝트의 활성화 상태를 전환

                        if (textMeshProObject4 != null)
                        {
                            textMeshProObject4.SetActive(true);
                            textMeshProObject3.SetActive(false);
                            stext();
                        }
                    }
                    if (b == 5)
                    {
                        // TextMesh Pro 오브젝트의 활성화 상태를 전환

                        if (textMeshProObject5 != null)
                        {
                            textMeshProObject5.SetActive(true);
                            textMeshProObject4.SetActive(false);
                            stext();
                            b = 0;
                        }
                    }
                    // 'F' 키가 눌렸는지 확인

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