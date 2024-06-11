using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    // ������ ���� �� �ο��Ǵ� ����
    public int redScore = 1;
    public int greenScore = 2;
    public int blueScore = 3;

    // �Ѿ��� �浹���� �� ȣ��Ǵ� �Լ�
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�浹�� �����Ǿ����ϴ�.");
        if (collision.gameObject.CompareTag("Bullet"))
        {
            int score = 0;

            // �浹�� �Ѿ��� ����, �ʷ�, �Ķ� ���� �� � �Ϳ� �浹�ߴ��� Ȯ��
            if (gameObject.CompareTag("Red"))
                score = redScore;
            else if (gameObject.CompareTag("Green"))
                score = greenScore;
            else if (gameObject.CompareTag("Blue"))
            {
                Debug.Log("�Ķ�");
                score = blueScore;
            }

            // ������ ������Ŵ
            ScoreManager.Instance.IncreaseScore(score);

            // �浹�� �Ѿ� �ı�
            Destroy(collision.gameObject);
        }
    }

}
