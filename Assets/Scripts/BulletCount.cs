using UnityEngine;
using UnityEngine.UI; // UI를 사용하기 위해 추가

public class BulletCount : MonoBehaviour
{
    public Text counterText; // UI Text를 연결할 변수
    private int count = 0; // 카운터 변수
    private int maxCount = 30; // 최대 카운트
   
    

    


    void Update()
    {
        // 마우스 좌클릭 감지

        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
           
            // 최대 카운트를 넘지 않도록 설정
            if (count < maxCount)
            {
                count++;
                UpdateCounterText();
            }
            
           
        }
        
    }

    // UI 텍스트를 업데이트하는 메서드
    void UpdateCounterText()
    {
        counterText.text = count + "/30";
    }
    private void Reset()
    {
        count = 0;
    }




}