using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    // 과녁이 맞을 때 부여되는 점수
    public int redScore = 1;
    public int greenScore = 2;
    public int blueScore = 3;

    // 총알이 충돌했을 때 호출되는 함수
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌이 감지되었습니다.");
        if (collision.gameObject.CompareTag("Bullet"))
        {
            int score = 0;

            // 충돌한 총알이 빨간, 초록, 파란 과녁 중 어떤 것에 충돌했는지 확인
            if (gameObject.CompareTag("Red"))
                score = redScore;
            else if (gameObject.CompareTag("Green"))
                score = greenScore;
            else if (gameObject.CompareTag("Blue"))
            {
                Debug.Log("파랑");
                score = blueScore;
            }

            // 점수를 증가시킴
            ScoreManager.Instance.IncreaseScore(score);

            // 충돌한 총알 파괴
            Destroy(collision.gameObject);
        }
    }

}
