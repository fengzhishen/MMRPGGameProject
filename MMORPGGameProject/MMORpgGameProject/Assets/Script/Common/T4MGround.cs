using UnityEngine;
using System.Collections;

public class T4MGround : MonoBehaviour
{
	private void Start()
    {
        Renderer[] renderers = GetComponents<MeshRenderer>();

        if(renderers != null && renderers.Length > 0)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].sharedMaterial.shader = GlobalInit.Instance.T4MGroundShader;
            }
        }

        Destroy(this);
    }
}
