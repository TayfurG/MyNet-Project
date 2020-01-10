using AbilityFactoryStatic;
using UnityEngine;
using System.Collections;
public class AbilityPanel : MonoBehaviour
{
    [SerializeField]
    private AbilityButton buttonPrefab = null;

    [System.NonSerialized]
    public PlayerScript playerAccess = null;

    private void Start()
    {
        playerAccess = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    private void OnEnable()
    {

        foreach (string name in AbilityFactory.GetAbilityNames())
        {
            var button = Instantiate(buttonPrefab);
            button.gameObject.name = name + " Button";
            button.transform.SetParent(transform);
            button.SetButtonName(name);
            button.transform.localScale = new Vector3(1, 1, 1);
        }

    }
  
   


}
