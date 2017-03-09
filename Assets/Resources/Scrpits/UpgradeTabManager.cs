using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class UpgradeTabManager : Singleton<UpgradeTabManager> 
{
    public List<GameObject> tabs = new List<GameObject>();
    GameObject UpgradeTabPrefab;
    GameObject Options = new GameObject();

    public UpgradeTabManager()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        Options.transform.SetParent(canvas.transform);
        Options.name = "OptionsBackDrop";
        Options.AddComponent<Image>();
        Options.GetComponent<RectTransform>().sizeDelta = new Vector2(canvas.GetComponent<RectTransform>().sizeDelta.x, canvas.GetComponent<RectTransform>().sizeDelta.y/2);
        Options.transform.localPosition = new Vector3(0, -canvas.GetComponent<RectTransform>().sizeDelta.y / 4 * 1.2f, 0);
        Options.transform.localScale = new Vector3(1, 1, 1);

        UpgradeTabPrefab = Resources.Load("Prefabs/UpgradeCharacterTab") as GameObject;
    }
    public void CreateTab(Hero hero)
    {
        /******************Basic transform*********************/
        GameObject newTab = new GameObject();
        newTab = GameObject.Instantiate(UpgradeTabPrefab);
        newTab.transform.SetParent(Options.transform);
        newTab.transform.localPosition = new Vector3(0,  (Options.GetComponent<RectTransform>().rect.height/4 * (1 -tabs.Count)), 0);
        newTab.transform.localScale = new Vector3(1, 1, 1);

        /*********************Button Scripts for the UpgradeTab*****************************/
        //a popup will appear when pressed
        newTab.AddComponent<CharacterStats>();
        newTab.GetComponent<CharacterStats>().SetHero(hero);
        //upgrade the character
        newTab.transform.FindChild("Price").gameObject.AddComponent<CharacterPurchase>();
        newTab.transform.FindChild("Price").gameObject.GetComponent<CharacterPurchase>().SetHero(hero,tabs.Count);
        /******************Create the upgrade tab********************/

        newTab.transform.FindChild("Price/PriceTag").GetComponent<Text>().text = hero.upgradeLevel.ToString() + " Gold";
        newTab.transform.FindChild("UnitFace").GetComponent<Image>().sprite = hero.spriteRenderer.sprite;
        newTab.transform.FindChild("Name").GetComponent<Text>().text = hero.name;
        newTab.transform.FindChild("Attack").GetComponent<Text>().text = "Attack :" + hero.attack.ToString();
        tabs.Add(newTab);
    }
    public void UpdageTab(Hero hero,int tabNumber)
    {
        tabs[tabNumber].transform.FindChild("Price/PriceTag").GetComponent<Text>().text = hero.price.ToString() + " Gold";
        tabs[tabNumber].transform.FindChild("Attack").GetComponent<Text>().text = "Attack :" + hero.attack.ToString();
    }
}
