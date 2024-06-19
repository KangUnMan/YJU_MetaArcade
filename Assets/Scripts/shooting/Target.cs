using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    public int redScore = 1;
    public int greenScore = 2;
    public int blueScore = 3;
    public CanvasGroup canvasGroup;
    
    private GameController _gameController;
    public float fadeDuration = 1.0f; // 페이드 인/아웃 지속 시간
    private bool isFadingIn = false;
    private bool isFadingOut = false;

    private void Start()
    {
        // GameController 인스턴스를 가져옵니다.
        _gameController = GameController.Instance;
        canvasGroup.alpha = 0.0f;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌이 감지되었습니다.");
        if (collision.gameObject.CompareTag("Bullet"))
        {
            int score = 0;

            // 충돌한 총알이 빨간, 초록, 파란 과녁 중 어떤 것에 충돌했는지 확인
            if (gameObject.CompareTag("Red"))
            {
                score = redScore;
                
                StartCoroutine(FadeIn());
            }

            else if (gameObject.CompareTag("Green"))
            {
                score = greenScore;
                
                StartCoroutine(FadeIn());
            }
            else if (gameObject.CompareTag("Blue")) 
            {
                Debug.Log("파랑");
                score = blueScore;
                
                StartCoroutine(FadeIn());
            }

            // 점수를 증가시킴
            if (_gameController != null)
            {
                _gameController.IncreaseScore(score);
            }
            else
            {
                Debug.LogError("GameController 인스턴스를 찾을 수 없습니다!");
            }

            // 충돌한 총알 파괴
            Destroy(collision.gameObject);
        }
    }
    public IEnumerator FadeIn()
    {
        isFadingIn = true;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1.0f;
        isFadingIn = false;

        // 페이드 인이 끝난 후 일정 시간 대기
        yield return new WaitForSeconds(0.2f); // 예를 들어 2초 대기

        // 페이드 아웃 시작
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        isFadingOut = true;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Clamp01(1.0f - (elapsedTime / fadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0.0f;
        isFadingOut = false;

        // 페이드 아웃이 끝난 후 게임 오브젝트 비활성화
        gameObject.SetActive(false);
    }
}
