using UnityEngine;

public class AnimatorSwitcher : MonoBehaviour
{
    public RuntimeAnimatorController defaultAnimatorController; // �⺻ �ִϸ����� ��Ʈ�ѷ�
    public RuntimeAnimatorController specialAnimatorController; // Ư�� ��Ȳ �ִϸ����� ��Ʈ�ѷ�

    private Animator animator;
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject Gun;

    void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<PlayershootManager>().enabled = false;

        // ������ �� �⺻ �ִϸ����� ��Ʈ�ѷ��� �����մϴ�.
        animator.runtimeAnimatorController = defaultAnimatorController;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShootingZone"))
        {
            SwitchToSpecialAnimator();
            GetComponent<PlayershootManager>().enabled = true;
            LeftHand.SetActive(true);
            RightHand.SetActive(true);
            Gun.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ShootingZone"))
        {
            SwitchToDefaultAnimator();
            GetComponent<PlayershootManager>().enabled = false;
            LeftHand.SetActive(false);
            RightHand.SetActive(false);
            Gun.SetActive(false);
        }
    }

    void SwitchToSpecialAnimator()
    {
        animator.runtimeAnimatorController = specialAnimatorController;
    }

    void SwitchToDefaultAnimator()
    {
        animator.runtimeAnimatorController = defaultAnimatorController;
    }
}
