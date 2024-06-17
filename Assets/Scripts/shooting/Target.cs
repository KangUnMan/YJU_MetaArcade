using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    public int redScore = 1;
    public int greenScore = 2;
    public int blueScore = 3;
    public CanvasGroup canvasGroup;
    public GameObject a;
    private GameController _gameController;
    public float fadeDuration = 1.0f; // ���̵� ��/�ƿ� ���� �ð�
    private bool isFadingIn = false;
    private bool isFadingOut = false;

    private void Start()
    {
        // GameController �ν��Ͻ��� �����ɴϴ�.
        _gameController = GameController.Instance;
        canvasGroup.alpha = 0.0f;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�浹�� �����Ǿ����ϴ�.");
        if (collision.gameObject.CompareTag("Bullet"))
        {
            int score = 0;

            // �浹�� �Ѿ��� ����, �ʷ�, �Ķ� ���� �� � �Ϳ� �浹�ߴ��� Ȯ��
            if (gameObject.CompareTag("Red") && !isFadingIn && !isFadingOut)
            {
                score = redScore;
                a.SetActive(true);
                StartCoroutine(FadeIn());
            }

            else if (gameObject.CompareTag("Green") && !isFadingIn && !isFadingOut)
            {
                score = greenScore;
                a.SetActive(true);
                StartCoroutine(FadeIn());
            }
            else if (gameObject.CompareTag("Blue") && !isFadingIn && !isFadingOut) 
            {
                Debug.Log("�Ķ�");
                score = blueScore;
                a.SetActive(true);
                StartCoroutine(FadeIn());
            }

            // ������ ������Ŵ
            if (_gameController != null)
            {
                _gameController.IncreaseScore(score);
            }
            else
            {
                Debug.LogError("GameController �ν��Ͻ��� ã�� �� �����ϴ�!");
            }

            // �浹�� �Ѿ� �ı�
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

        // ���̵� ���� ���� �� ���� �ð� ���
        yield return new WaitForSeconds(0.2f); // ���� ��� 2�� ���

        // ���̵� �ƿ� ����
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

        // ���̵� �ƿ��� ���� �� ���� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
