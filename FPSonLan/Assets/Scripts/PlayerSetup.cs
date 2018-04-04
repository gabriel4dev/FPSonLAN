using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentsToDisable;

    private Camera sceneCamera;

    private void Start()
    {
        int vCount = this.componentsToDisable.Length;

        if (!this.isLocalPlayer)
        {
            for(int i = 0; i < vCount; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            this.sceneCamera = Camera.main;
            Camera.main.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if(this.sceneCamera != null)
        {
            this.sceneCamera.gameObject.SetActive(true);
        }
    }
}
