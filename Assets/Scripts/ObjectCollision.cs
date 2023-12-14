using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    [SerializeField] GameObject objectWriten;
    [SerializeField] ButtonWithLabel upgradeMenuLab;
    [SerializeField] WeaponUnlockController weapon;
    [SerializeField] string gameObjectTag;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (objectWriten.CompareTag("lab"))
            {
                print(upgradeMenuLab.enabled);
                upgradeMenuLab.gameObject.SetActive(true);

            }
            else if(objectWriten.CompareTag("armory"))
            {
                weapon.gameObject.SetActive(true);
            }
        }
    }
}
