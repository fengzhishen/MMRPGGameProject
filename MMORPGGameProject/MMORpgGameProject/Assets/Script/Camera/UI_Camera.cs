using UnityEngine;
using System.Collections;

public class UI_Camera : MonoBehaviour
{
    [HideInInspector]
    public Camera Camera;
    public static UI_Camera Instance;
	
	void Start ()
    {
        Instance = this;
    }		
}
