
using UnityEngine;
using System.Collections;

public class ScreenTap : MonoBehaviour {

    GameObject dragDetect;
	// Use this for initialization
	void Start () 
    {
        UserSingleton.GetInstance();
        UpgradeTabManager.GetInstance();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //UpgradeTabManager.GetInstance().DragResponse();
	}
    void OnMouseDown()
    {
        Debug.Log("Attack");
        UserSingleton.GetInstance().mainHero.Attack();
    }
    public void NextHero()
    {
        UserSingleton.GetInstance().NextHero();
    }
    public void PrevHero()
    {
        UserSingleton.GetInstance().PrevHero();
    }
}
