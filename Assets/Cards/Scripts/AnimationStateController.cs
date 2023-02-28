using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isMoveHash;
    void Start()
    {
        animator = GetComponent<Animator>();
        isMoveHash = Animator.StringToHash("IsMove");
    }

    void Update()
    {
        bool IsMove = animator.GetBool(isMoveHash);
        bool MovePressed = Input.GetKeyDown(KeyCode.Space);
        if (!IsMove && MovePressed)
        {
            animator.SetBool(isMoveHash, true);
        }
        if (IsMove && !MovePressed)
        {
            animator.SetBool(isMoveHash, false);
        }
    }
}
