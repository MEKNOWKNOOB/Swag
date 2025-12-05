using UnityEngine;

public class EnemyAnimation : NetworkComponent
{
    [SerializeField] private Animator enemyAnimator;
    private Movement enemyMovement;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveDirection;
    void Awake()
    {
        RefName = "Animator";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyMovement = GetComponent<Movement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = enemyMovement.Direction;

        // Flip visually only
        if (moveDirection.x != 0)
            spriteRenderer.flipX = moveDirection.x > 0;

        if (moveDirection != Vector2.zero)
        {
            UpdateMoveDirection();
        }
        PlayMoveAnimation();
    }

    public void PlayMoveAnimation()
    {
        enemyAnimator.SetFloat("MoveDirectionX", moveDirection.x);
        enemyAnimator.SetFloat("MoveDirectionY", moveDirection.y);
    }
    
    public void UpdateMoveDirection()
    {
        if (moveDirection.x == 0 && moveDirection.y != 0)
        {
            enemyAnimator.SetFloat("MoveDirection", 0);
        }
        else if (moveDirection.y == 0 && moveDirection.x != 0)
        {
            enemyAnimator.SetFloat("MoveDirection", 1);
        }
    }
}
