using System;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentsToDisable;

    [SerializeField]
    private string remoteLayerName = "RemotePlayer";

    [SerializeField]
    private string dontDrawLayerName = "DontDraw";

    [SerializeField]
    private GameObject playerGraphics;

    [SerializeField]
    private GameObject playerUIPrefab;

    private GameObject playerUIInstance;
    private Camera sceneCamera;

    private void Start()
    {
        if (!this.isLocalPlayer)
        {
            this.DisableComponents();
            this.AssingRemoteLayer();
        }
        else
        {
            this.sceneCamera = Camera.main;
            if (this.sceneCamera != null)
            {
                Camera.main.gameObject.SetActive(false);
            }

            this.SetLayerRecursively(this.playerGraphics, LayerMask.NameToLayer(this.dontDrawLayerName));

            this.playerUIInstance = Instantiate(this.playerUIPrefab);
            this.playerUIInstance.name = this.playerUIPrefab.name;
        }
        this.GetComponent<Player>().Setup();
    }

    private void SetLayerRecursively(GameObject pObj, int pNewLayer)
    {
        pObj.layer = pNewLayer;

        foreach(Transform t in pObj.transform)
        {
            this.SetLayerRecursively(t.gameObject, pNewLayer);
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string vNetId = this.GetComponent<NetworkIdentity>().netId.ToString();
        Player vPlayer = this.GetComponent<Player>();
        GameManager.RegisterPlayer(vNetId, vPlayer);
    }

    private void AssingRemoteLayer()
    {
        this.gameObject.layer = LayerMask.NameToLayer(this.remoteLayerName);
    }

    private void DisableComponents()
    {
        int vCount = this.componentsToDisable.Length;
        for (int i = 0; i < vCount; i++)
        {
            this.componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        Destroy(this.playerUIInstance);

        if (this.sceneCamera != null)
        {
            this.sceneCamera.gameObject.SetActive(true);
        }

        GameManager.UnRegisterPlayer(this.transform.name);
    }
}
