using System;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerShoot : NetworkBehaviour {

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private LayerMask mask;
    public PlayerWeapon weapon;

    private const string PLAYER_TAG = "Player";

    private void Start()
    {
        if(this.cam == null)
        {
            Debug.Log("Player shoot: No camera referenced...");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            Debug.Log("Disparando");
            this.Shoot();
        }
    }

    [Client]
    private void Shoot()
    {
        RaycastHit vHit;
        if (Physics.Raycast(this.cam.transform.position, this.cam.transform.forward, out vHit, this.weapon.range, this.mask))
        {
            if(vHit.collider.tag == PlayerShoot.PLAYER_TAG)
            {
                this.CmdPlayerShoot(vHit.collider.name, this.weapon.damage);
            }
        }
    }

    [Command]
    private void CmdPlayerShoot(string pPlayerId, int pDamage)
    {
        Debug.Log("[Debug]: " + pPlayerId + " has been shooted");

        Player vPlayer = GameManager.GetPlayer(pPlayerId);
        vPlayer.RpcTakeDamage(pDamage);

    }
}