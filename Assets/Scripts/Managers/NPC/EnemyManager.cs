using System.Collections.Generic;

public class EnemyManager : NetworkEntity
{
    public static EnemyManager Instance;
    public List<Enemy> Enemies = new();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {

    }
}
