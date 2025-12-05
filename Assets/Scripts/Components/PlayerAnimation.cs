using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    private Movement playerMovement;
    private Vector2 moveDirection;
    private SpriteRenderer sr;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = playerMovement.Direction;

        // Flip visually only
        if (moveDirection.x != 0)
            sr.flipX = moveDirection.x > 0;

        if (moveDirection != Vector2.zero)
        {
            UpdateMoveDirection();
        }
        PlayMoveAnimation();
    }

    public void PlayMoveAnimation()
    {
        playerAnimator.SetFloat("MoveDirectionX", moveDirection.x);
        playerAnimator.SetFloat("MoveDirectionY", moveDirection.y);
    }
    
    public void UpdateMoveDirection()
    {
        if (moveDirection.x == 0 && moveDirection.y != 0)
        {
            playerAnimator.SetFloat("MoveDirection", 0);
        }
        else if (moveDirection.y == 0 && moveDirection.x != 0)
        {
            playerAnimator.SetFloat("MoveDirection", 1);
        }
    }
}
