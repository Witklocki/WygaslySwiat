using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabDataContorller : MonoBehaviour
{
    public int index;
    public Button unlock;
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI p1;
    public TextMeshProUGUI p2;
    public TextMeshProUGUI p3;
    public TextMeshProUGUI p4;
    public TextMeshProUGUI unlocked;

    public WeaponObjectDataController weaponDataController;

    public WeaponUnlockController weaponUnlockController;

    [SerializeField] private bool hasEnoughtMoney;
    // Start is called before the first frame update
    void Start()
    {
        weaponDataController = new WeaponObjectDataController();
        weaponName.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).name;
        p1.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).basicElement.ToString();
        p2.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).commonElement.ToString();
        p3.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).rareElemnt.ToString();
        p4.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).elitElemnts.ToString();

        hasEnoughtMoney = weaponDataController.weaponObject.weapons[index].price <= weaponUnlockController.dropObjectController.dropObj;

        unlock.gameObject.SetActive(!weaponDataController.weaponObject.GetWeaponObjectWithId(index).isUnlocker);

        unlocked.gameObject.SetActive(weaponDataController.weaponObject.GetWeaponObjectWithId(index).isUnlocker);
    }

    private void Update()
    {
        hasEnoughtMoney = weaponDataController.weaponObject.weapons[index].price <= weaponUnlockController.dropObjectController.dropObj;

        unlock.interactable = hasEnoughtMoney;
    }

    public void unlockWeapon()
    {
       
        if (hasEnoughtMoney) {
            weaponDataController.unlockWeapon(index);

            unlock.gameObject.SetActive(!weaponDataController.weaponObject.GetWeaponObjectWithId(index).isUnlocker);

            unlocked.gameObject.SetActive(weaponDataController.weaponObject.GetWeaponObjectWithId(index).isUnlocker);
        }


    }    
}
