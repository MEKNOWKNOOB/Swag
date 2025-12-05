using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;
public class Health : NetworkComponent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private NetworkVariable<int> HP = new(writePerm: NetworkVariableWritePermission.Server);
    [SerializeField] private NetworkVariable<int> MaxHP = new(writePerm: NetworkVariableWritePermission.Server);

    public delegate void SourceHealthChangeCallback(int health);
    public event SourceHealthChangeCallback SourceHealthChange;

    UnityEvent BeforeDeath;
    UnityEvent AfterDeath;


    private void Start()
    {
        if (BeforeDeath == null)
        {
            BeforeDeath = new UnityEvent();
        }

        if (AfterDeath == null)
        {
            AfterDeath = new UnityEvent();
        }

        BeforeDeath.AddListener(BeforeDeaths);
        AfterDeath.AddListener(AfterDeaths);
    }

    public override void OnNetworkSpawn()
    {
        
        
    }
    void Awake()
    {
        RefName = "Health";
    }

    // Update is called once per frame
    void Update()
    {
        if (HP.Value <= 0)
        {
            Kill();
        }
    }

    public void ChangeHealth(int change)
    {
        HP.Value += change;
        // HealthChange?.Invoke(change);
    }


    public void ChangeHealthOwner(int change)
    {
        HP.Value += change;
        SourceHealthChange?.Invoke(change);
    }
    public void ChangeMaxHealth(int change)
    {
        MaxHP.Value += change;
    }
    void Kill()
    {
        SubmitKillServerRpc();
    }

    void BeforeDeaths()
    {
        Debug.Log("I'm Dying");
    }

    void AfterDeaths()
    {
        Debug.Log("I'm Dead, anything else you want to finish?");
    }

    [ServerRpc]
    public void SubmitKillServerRpc()
    {
        // patch work but it is what it is
        if(gameObject.GetComponent<LocalPlayer>() != null)
        {
            GameManager.Instance.Players.Remove(gameObject.GetComponent<LocalPlayer>());
        }
        BeforeDeath.Invoke();
        if(!Entity.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = GameManager.Instance.RespawnPoint;
            HP.Value = MaxHP.Value;
        }
        AfterDeath.Invoke();
    }
}
