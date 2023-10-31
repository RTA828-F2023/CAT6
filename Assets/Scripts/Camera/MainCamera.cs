using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Animator _animator;
    private static readonly int IntroAnimationTrigger = Animator.StringToHash("intro");
    private static readonly int OutroAnimationTrigger = Animator.StringToHash("outro");

    #region Unity Events

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    #endregion

    public void Intro()
    {
        _animator.SetTrigger(IntroAnimationTrigger);
    }

    public void Outro()
    {
        _animator.SetTrigger(OutroAnimationTrigger);
    }
}
