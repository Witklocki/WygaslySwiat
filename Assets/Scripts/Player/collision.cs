using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    public InventoryObject inventory;
    public PlayerMovement playerMovement;
    private WeaponParent weaponParent;
    private void OnTriggerEnter(Collider other)
    {
        GameObject detectedWeapon = inventory.TempDetectedWeapon;
        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "Exit":
                print("Emtry Exit");
                break;
            case "lab":
                print("Emtry lab");
                break;
            case "storage":
                print("Emtry storage");
                break;
            case "armory":
                print("Emtry armory");
                break;
            case "savedPeople":
                print("Emtry savedPeople");
                break;
            case "Fence":
                break;
            case "WeaponPickup":
                var knife = other.gameObject.GetComponent<KnifeScript>();
                var baseball = other.gameObject.GetComponent<BaseballScript>();
                var crowbar = other.gameObject.GetComponent<CrowbarScript>();
                var glock = other.gameObject.GetComponent<GlockScript>();
                var rewolwer = other.gameObject.GetComponent<RewolwerScript>();
                var mp5 = other.gameObject.GetComponent<MP5Script>();
                var remington = other.gameObject.GetComponent<RemingtonScript>();
                
                if (knife && knife.canPickup)
                {
                    Debug.Log(knife.knife);
                    ItemObject item = knife.knife;
                    if (item is WeaponObject)
                    {
                        WeaponObject weapon = (WeaponObject)item;
                        if (inventory.ContainsWeapon())
                        {
                            inventory.SetLastDetectedWeapon(knife.gameObject); // Store the last detected weapon GameObject
                            inventory.ClearWeaponInventory();
                            Debug.Log("Clear");
                            if (detectedWeapon != null)
                            {
                                Destroy(detectedWeapon);
                                Vector3 spawnPosition = playerMovement.transform.position + new Vector3(5, 0, 0); // Adjust the position as needed
                                Instantiate(detectedWeapon, spawnPosition, Quaternion.Euler(45, 0, 0));

                            }
                        }
                        inventory.AddWeapon(weapon, 1);
                        if (!knife.gameObject.activeSelf)
                        {
                            Instantiate(knife.gameObject);
                        }
                        if (playerMovement != null && playerMovement.equippedWeaponSlot != null)
                        {
                            Animator pickedUpAnimator = knife.GetComponent<Animator>();

                            knife.transform.SetParent(playerMovement.equippedWeaponSlot.transform);
                            knife.transform.localPosition = Vector3.zero; // Adjust as needed
                            knife.transform.localRotation = Quaternion.identity; // Adjust as needed
                        }

                        knife.GetComponent<Collider>().enabled = false;
                        Destroy(knife.GetComponent<Rigidbody>());
                    }
                }
                if (baseball && baseball.canPickup)
                {
                    Debug.Log(baseball.baseball);
                    ItemObject item = baseball.baseball;
                    if (item is WeaponObject)
                    {
                        WeaponObject weapon = (WeaponObject)item;
                        if (inventory.ContainsWeapon())
                        {
                            inventory.SetLastDetectedWeapon(baseball.gameObject); // Store the last detected weapon GameObject
                            inventory.ClearWeaponInventory();
                            Debug.Log("Clear");
                            if (detectedWeapon != null)
                            {
                                Destroy(detectedWeapon);
                                Vector3 spawnPosition = playerMovement.transform.position + new Vector3(5, 0, 0); // Adjust the position as needed
                                Instantiate(detectedWeapon, spawnPosition, Quaternion.Euler(45, 0, 0));
                            }
                        }
                        inventory.AddWeapon(weapon, 1);

                        if (!baseball.gameObject.activeSelf)
                        {
                            Instantiate(baseball.gameObject);
                        }
                        if (playerMovement != null && playerMovement.equippedWeaponSlot != null)
                        {
                            Animator pickedUpAnimator = baseball.GetComponent<Animator>();

                            baseball.transform.SetParent(playerMovement.equippedWeaponSlot.transform);
                            baseball.transform.localPosition = Vector3.zero; // Adjust as needed
                            baseball.transform.localRotation = Quaternion.identity; // Adjust as needed

                        }

                        baseball.GetComponent<Collider>().enabled = false;
                        Destroy(baseball.GetComponent<Rigidbody>());
                    }
                }
                if (crowbar)
                {
                    Debug.Log(crowbar.crowbar);
                    inventory.AddItem(crowbar.crowbar, 1);
                    Destroy(other.gameObject);
                }
                if (glock)
                {
                    Debug.Log(glock.glock);
                    inventory.AddItem(glock.glock, 1);
                    Destroy(other.gameObject);
                }
                if (rewolwer)
                {
                    Debug.Log(rewolwer.rewolwer);
                    inventory.AddItem(rewolwer.rewolwer, 1);
                    Destroy(other.gameObject);
                }
                if (mp5)
                {
                    Debug.Log(mp5.mp5);
                    inventory.AddItem(mp5.mp5, 1);
                    Destroy(other.gameObject);
                }
                if (remington)
                {
                    Debug.Log(remington.remington);
                    inventory.AddItem(remington.remington, 1);
                    Destroy(other.gameObject);
                }
                break;

        }

    }


    private void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Fence":
                   break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Fence":
                break;
        }
    }

    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }

}
