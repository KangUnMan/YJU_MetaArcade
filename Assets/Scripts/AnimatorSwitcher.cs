using UnityEngine;

public class AnimatorSwitcher : MonoBehaviour
{
    public RuntimeAnimatorController defaultAnimatorController; // 기본 애니메이터 컨트롤러
    public RuntimeAnimatorController specialAnimatorController; // 특수 상황 애니메이터 컨트롤러

    private Animator animator;
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject Gun;

    void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<PlayershootManager>().enabled = false;

        // 시작할 때 기본 애니메이터 컨트롤러를 설정합니다.
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
