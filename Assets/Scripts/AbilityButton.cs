using AbilityFactoryStatic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityButton : MonoBehaviour
{
    private string AbilityName = "";

    private PlayerScript PS;

    void Start()
    {
        PS = transform.parent.GetComponent<AbilityPanel>().playerAccess;
        transform.GetComponent<Button>().onClick.AddListener(GiveAbilityToAll);
    }

    void GiveAbilityToAll()
    {
        
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
            {
                AbilityFactory.GetAbility(AbilityName).Process(g.GetComponent<PlayerScript>());
            }

        /*
         If you got some skills before you spawn a new player and you do not want to transfer previously gained skills,
         basically use the code below and remove foreach statement above.

         AbilityFactory.GetAbility(AbilityName).Process(GameObject.FindWithTag("Player").GetComponent<PlayerScript>());

         */

        Button selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        selectedButton.enabled = false;
        selectedButton.gameObject.transform.GetChild(1).GetComponent<Image>().enabled = true;

        PS.abilityCount++;

        if (PS.abilityCount >= 3)
        {
            foreach (Button b in transform.parent.GetComponentsInChildren<Button>())
            {
                b.enabled = false;
                b.gameObject.transform.GetChild(1).GetComponent<Image>().enabled = true;
            }
        }

        

    }

    public void SetButtonName(string name)
    {
        transform.GetChild(0).GetComponent<Text>().text = name;
        AbilityName = name;
    }


}
