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

    public WeaponObjectDataController weaponDataController;

    // Start is called before the first frame update
    void Start()
    {
        weaponDataController = new WeaponObjectDataController();
        weaponName.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).name;
        p1.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).basicElement.ToString();
        p2.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).commonElement.ToString();
        p3.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).rareElemnt.ToString();
        p4.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).elitElemnts.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        weaponName.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).name;
        p1.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).basicElement.ToString();
        p2.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).commonElement.ToString();
        p3.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).rareElemnt.ToString();
        p4.text = weaponDataController.weaponObject.GetWeaponObjectWithId(index).elitElemnts.ToString();
    }
}
