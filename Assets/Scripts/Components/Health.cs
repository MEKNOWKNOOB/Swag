using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;
public class Health : NetworkComponent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private NetworkVariable<int> HP = new(writePerm: NetworkVariableWritePermission.Server);
    [SerializeField] private NetworkVariable<int> MaxHP = new(writePerm: NetworkVariableWritePermission.Server);
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
        HP.OnValueChanged += SyncHP;
        MaxHP.OnValueChanged += SyncMaxHP;
    }
    void Awake()
    {
        RefName = "Health";
    }

    // Update is called once per frame
    void Update()
    {
        if(HP.Value == 0)
        {
            Kill();
        }
    }

    void ChangeHealth(int change)
    {
        HP.Value += change; 
    }
    void ChangeMaxHealth(int change)
    {
        MaxHP.Value += change;
    }
    void Kill()
    {
        

        if (IsClient && !IsServer)
        {
            GameObject.Destroy(gameObject);
        }
        SubmitKillServerRpc();
    }
    
    void BeforeDeaths()
    {
        Debug.Log("I'm Dying");
    }

    void AfterDeaths() {
        Debug.Log("I'm Dead, anything else you want to finish?");
    }

    [ServerRpc]
    public void SubmitKillServerRpc()
    {
        GameObject.Destroy(gameObject);
    }

    private void SyncHP(int prev, int curr)
    {
        HP.Value = curr;
    }
    private void SyncMaxHP(int prev, int curr)
    {
        MaxHP.Value = curr;
    }
}
