
using UnityEngine;
using System.Collections;

public class ScreenTap : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        UserSingleton.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        Debug.Log("Attack");
        UserSingleton.GetInstance().mainHero.Attack();
    }
}
