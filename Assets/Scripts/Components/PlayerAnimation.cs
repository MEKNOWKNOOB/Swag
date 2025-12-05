using Unity.Netcode;
using UnityEngine;


public class PlayerAnimation : NetworkComponent
{
    [SerializeField] private Animator playerAnimator;

    private Vector2 moveDirection;
    private SpriteRenderer sr;

    void Awake()
    {
        RefName = "Animator";
    }

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        
        moveDirection = InputManager.Instance.MoveVector;
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
        PlayAnimationServerRpc(moveDirection.x, moveDirection.y);
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

    [ServerRpc]
    public void PlayAnimationServerRpc(float dirX, float dirY)
    {
        SyncAnimationClientRpc(dirX, dirY);
    }

    [ClientRpc]
    public void SyncAnimationClientRpc(float dirX, float dirY)
    {
        playerAnimator.SetFloat("MoveDirectionX", dirX);
        playerAnimator.SetFloat("MoveDirectionY", dirY);
        if (dirX != 0)
            sr.flipX = dirX > 0;
    }
}
