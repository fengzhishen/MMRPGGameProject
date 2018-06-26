using UnityEngine;
using System.Collections;

public class TestComponent : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Debug.Log("Start 执行。");
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log("Update 执行。");
    }

    public void Init()
    {
        Debug.Log("Init 执行。");
    }
}
