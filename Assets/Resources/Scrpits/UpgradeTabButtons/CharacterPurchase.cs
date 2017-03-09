using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CharacterPurchase : MonoBehaviour {

    Hero hero;
    int tabNumber;//number to get from the list
    public void SetHero(Hero chara , int tabNumber)
    {
        hero = chara;
        this.tabNumber = tabNumber;
    }
    void OnMouseUp()
    {
        Debug.Log("UpgradeDone");
        UpgradeTabManager.GetInstance().UpdageTab(hero,tabNumber);
        hero.UpgradeStats();
    }
}
