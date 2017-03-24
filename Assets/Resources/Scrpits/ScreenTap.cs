
using UnityEngine;
using System.Collections;

public class ScreenTap : MonoBehaviour {

    GameObject dragDetect;
    bool heldDown;
    float holdDownTimer;
	// Use this for initialization
	void Start () 
    {
        UserSingleton.GetInstance();
        UpgradeTabManager.GetInstance();
        heldDown = false;
        holdDownTimer = 1.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //UpgradeTabManager.GetInstance().DragResponse();
        if(heldDown == true)
        {
            holdDownTimer -= Time.deltaTime;
            if(holdDownTimer <= 0)
            {
                holdDownTimer = 1.0f;
                heldDown = false;
                UserSingleton.GetInstance().mainHero.SpecialSkill();
            }
        }
	}
    void OnMouseDown()
    {
        heldDown = true;
    }
    void OnMouseUp()
    {
        if(heldDown == true)
        {
            heldDown = false;
            holdDownTimer = 1.0f;
            Debug.Log("Attack");
            UserSingleton.GetInstance().mainHero.Attack();
        }
    }
    public void NextHero()
    {
        Debug.Log("NEXTHERO");
        UserSingleton.GetInstance().NextHero();
    }
    public void PrevHero()
    {
        UserSingleton.GetInstance().PrevHero();
    }
}
