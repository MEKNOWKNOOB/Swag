using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    private Movement playerMovement;
    private Vector2 moveDirection;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = playerMovement.Direction;
        if(moveDirection != Vector2.zero)
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
