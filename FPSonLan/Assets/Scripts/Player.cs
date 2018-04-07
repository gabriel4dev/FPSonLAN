using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{
    [SyncVar]
    private bool isDead = false;

    [SyncVar]
    private int currentHealth;

    public bool IsDead
    {
        get { return this.isDead; }
        protected set { this.isDead = value; }
    }

    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;



    internal void Setup()
    {
        int vCount = this.disableOnDeath.Length;
        this.wasEnabled = new bool[vCount];
        for (int i = 0; i < vCount; i++)
        {
            this.wasEnabled[i] = this.disableOnDeath[i].enabled;
        }
        this.SetDefaults();
    }

    internal void SetDefaults()
    {
        this.isDead = false;
        this.currentHealth = this.maxHealth;

        int vCount = this.disableOnDeath.Length;
        for (int i = 0; i < vCount; i++)
        {
            this.disableOnDeath[i].enabled = this.wasEnabled[i];
        }

        Collider vCollider = this.GetComponent<Collider>();
        if(vCollider != null)
        {
            vCollider.enabled = true;
        }
    }

    private void Update()
    {
        if (!this.isLocalPlayer)
        {
            return;
        }
        if (Input.GetKey(KeyCode.K))
        {
            this.RpcTakeDamage(999);
        }
    }

    [ClientRpc]
    internal void RpcTakeDamage(int pDamage)
    {
        if (this.isDead)
        {
            return;
        }

        this.currentHealth -= pDamage;
        Debug.Log(this.transform.name + " now has " + currentHealth + " health");

        if(this.currentHealth <= 0)
        {
            this.Die();
        }
    }

    private void Die()
    {
        this.isDead = true;

        int vCount = this.disableOnDeath.Length;
        for (int i = 0; i < vCount; i++)
        {
            this.disableOnDeath[i].enabled = false;
        }

        Collider vCollider = this.GetComponent<Collider>();
        if (vCollider != null)
        {
            vCollider.enabled = false;
        }

        Debug.Log(this.transform.name + " is dead.");

        this.StartCoroutine(this.Respawn());

    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);
        this.SetDefaults();
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();

        this.transform.position = spawnPoint.position;
        this.transform.rotation = spawnPoint.rotation;

        Debug.Log(this.transform.name + " respawned.");
    }
}
