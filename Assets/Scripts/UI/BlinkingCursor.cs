using UnityEngine;

public class BlinkingCursor : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("BlinkingCursor");
    }
}
