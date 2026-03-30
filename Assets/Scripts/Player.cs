using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float interactDistance = 2;

    private bool isWalking;
    private Vector3 lastInteractDir;

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = playerInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Hareket ediyorsa yönü güncelle, durunca son yönü hatýrla
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        // E'ye basýldýðýnda önüne raycast at
        if (playerInput.GetInteractPressed())
        {
            // Raycast'in ayaklardan deðil, gövde/göz hizasýndan çýkmasý için ofset (deðeri modeline göre deðiþtirebilirsin)
            Vector3 rayOrigin = transform.position + Vector3.up * 1f;

            if (Physics.Raycast(rayOrigin, lastInteractDir, out RaycastHit hit, interactDistance))
            {
                // TryGetComponent yerine GetComponentInParent kullanýyoruz:
                KitchenStation kitchen = hit.collider.GetComponentInParent<KitchenStation>();

                // Eðer kendisinde veya parent objelerinde KitchenStation varsa:
                if (kitchen != null)
                {
                    kitchen.Interact();
                }
            }
            else
            {
                Debug.Log("Raycast hiçbir þeye çarpmadý");
            }
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = playerInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .5f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove) //cannot move towards moveDir
        {
            //attempt only x movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance); //x yonunde bir seye carpma durumu 

            if (canMove)
            {
                //can move only on the x 
                moveDir = moveDirX;
            }
            else
            {
                //cannot move only on the X

                //attempt only z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance); // z ekseninde bos mu onu kontrol et


                if (canMove)
                {
                    //can move only on the z
                    moveDir = moveDirZ;
                }
                else //cannot move in any direction
                {
                    //hicbir yone hareket etmiyor
                }

            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        if (moveDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
    }
}
