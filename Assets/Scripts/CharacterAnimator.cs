using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private const string IS_WALKING = "isWalking";

    private Animator animator;
    private IMovable movableCharacter; 

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // Ayný objenin (veya ana objenin) üzerindeki IMovable arayüzünü bul
        // (Eðer Animator modeli Player objesinin bir alt(child) objesindeyse GetComponentInParent<IMovable>() kullan)
        movableCharacter = GetComponentInParent<IMovable>();

        if (movableCharacter == null)
        {
            Debug.LogError("CharacterAnimator, IMovable arayüzüne sahip bir script bulamadý!");
        }
    }

    private void Update()
    {
        if (movableCharacter != null)
        {
            // Karakter kim olursa olsun, IsWalking() sonucunu alýp animatörü tetikle
            animator.SetBool(IS_WALKING, movableCharacter.IsWalking());
        }
    }
}