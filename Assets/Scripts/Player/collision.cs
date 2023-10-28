using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    public InventoryObject inventory;
    public PlayerMovement playerMovement;
    private WeaponParent weaponParent;

    private void Start()
    {
        inventory.ClearWeaponInventory();
    }
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
                            Debug.Log("Clear Knife");
                            if (detectedWeapon != null)
                            {
                                Destroy(detectedWeapon);
                                Vector3 spawnPosition = playerMovement.transform.position + new Vector3(5, 0, 0); // Adjust the position as needed
                                if (!knife.gameObject.activeSelf)
                                {
                                    Instantiate(detectedWeapon, spawnPosition, Quaternion.Euler(45, 0, 0));
                                }
                            }
                        }
                        inventory.AddWeapon(weapon);
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
                            Debug.Log("Clear Base");
                            if (detectedWeapon != null)
                            {
                                Destroy(detectedWeapon);
                                Vector3 spawnPosition = playerMovement.transform.position + new Vector3(5, 0, 0); // Adjust the position as needed
                                if (!baseball.gameObject.activeSelf)
                                {
                                    Instantiate(detectedWeapon, spawnPosition, Quaternion.Euler(45, 0, 0));
                                }
                            }
                        }
                        inventory.AddWeapon(weapon);

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
                if (crowbar && crowbar.canPickup)
                {
                    Debug.Log(crowbar.crowbar);
                    ItemObject item = crowbar.crowbar;
                    if (item is WeaponObject)
                    {
                        WeaponObject weapon = (WeaponObject)item;
                        if (inventory.ContainsWeapon())
                        {
                            inventory.SetLastDetectedWeapon(crowbar.gameObject); // Store the last detected weapon GameObject
                            inventory.ClearWeaponInventory();
                            Debug.Log("Clear Crowbar");
                            if (detectedWeapon != null)
                            {
                                Destroy(detectedWeapon);
                                Vector3 spawnPosition = playerMovement.transform.position + new Vector3(5, 0, 0); // Adjust the position as needed
                                if (!crowbar.gameObject.activeSelf)
                                {
                                    Instantiate(detectedWeapon, spawnPosition, Quaternion.Euler(45, 0, 0));
                                }
                            }
                        }
                        inventory.AddWeapon(weapon);

                        if (!crowbar.gameObject.activeSelf)
                        {
                            Instantiate(crowbar.gameObject);
                        }
                        if (playerMovement != null && playerMovement.equippedWeaponSlot != null)
                        {
                            Animator pickedUpAnimator = crowbar.GetComponent<Animator>();

                            crowbar.transform.SetParent(playerMovement.equippedWeaponSlot.transform);
                            crowbar.transform.localPosition = Vector3.zero; // Adjust as needed
                            crowbar.transform.localRotation = Quaternion.identity; // Adjust as needed

                        }

                        crowbar.GetComponent<Collider>().enabled = false;
                        Destroy(crowbar.GetComponent<Rigidbody>());
                    }
                }
                if (glock && glock.canPickup)
                {
                    Debug.Log(glock.glock);
                    ItemObject item = glock.glock;
                    if (item is WeaponObject)
                    {
                        WeaponObject weapon = (WeaponObject)item;
                        if (inventory.ContainsWeapon())
                        {
                            inventory.SetLastDetectedWeapon(glock.gameObject); // Store the last detected weapon GameObject
                            inventory.ClearWeaponInventory();
                            Debug.Log("Clear glock");
                            if (detectedWeapon != null)
                            {
                                Destroy(detectedWeapon);
                                Vector3 spawnPosition = playerMovement.transform.position + new Vector3(5, 0, 0); // Adjust the position as needed
                                if (!glock.gameObject.activeSelf)
                                {
                                    Instantiate(detectedWeapon, spawnPosition, Quaternion.Euler(45, 0, 0));
                                }
                            }
                        }
                        inventory.AddWeapon(weapon);

                        if (!glock.gameObject.activeSelf)
                        {
                            Instantiate(glock.gameObject);
                        }
                        if (playerMovement != null && playerMovement.equippedWeaponSlot != null)
                        {
                            Animator pickedUpAnimator = glock.GetComponent<Animator>();

                            glock.transform.SetParent(playerMovement.equippedWeaponSlot.transform);
                            glock.transform.localPosition = Vector3.zero; // Adjust as needed
                            glock.transform.localRotation = Quaternion.identity; // Adjust as needed

                        }

                        glock.GetComponent<Collider>().enabled = false;
                        Destroy(glock.GetComponent<Rigidbody>());
                    }
                }
                if (rewolwer && rewolwer.canPickup)
                {
                    Debug.Log(rewolwer.rewolwer);
                    ItemObject item = rewolwer.rewolwer;
                    if (item is WeaponObject)
                    {
                        WeaponObject weapon = (WeaponObject)item;
                        if (inventory.ContainsWeapon())
                        {
                            inventory.SetLastDetectedWeapon(rewolwer.gameObject); // Store the last detected weapon GameObject
                            inventory.ClearWeaponInventory();
                            Debug.Log("Clear Rewo");
                            if (detectedWeapon != null)
                            {
                                Destroy(detectedWeapon);

                                Vector3 spawnPosition = playerMovement.transform.position + new Vector3(5, 0, 0); // Adjust the position as needed
                                if (!rewolwer.gameObject.activeSelf)
                                {
                                    Instantiate(detectedWeapon, spawnPosition, Quaternion.Euler(45, 0, 0));
                                }
                            }
                        }
                        inventory.AddWeapon(weapon);

                        if (!rewolwer.gameObject.activeSelf)
                        {
                            Instantiate(rewolwer.gameObject);
                        }
                        if (playerMovement != null && playerMovement.equippedWeaponSlot != null)
                        {
                            Animator pickedUpAnimator = rewolwer.GetComponent<Animator>();

                            rewolwer.transform.SetParent(playerMovement.equippedWeaponSlot.transform);
                            rewolwer.transform.localPosition = Vector3.zero; // Adjust as needed
                            rewolwer.transform.localRotation = Quaternion.identity; // Adjust as needed

                        }

                        rewolwer.GetComponent<Collider>().enabled = false;
                        Destroy(rewolwer.GetComponent<Rigidbody>());
                    }
                }
                if (mp5 && mp5.canPickup)
                {
                    Debug.Log(mp5.mp5);
                    ItemObject item = mp5.mp5;
                    if (item is WeaponObject)
                    {
                        WeaponObject weapon = (WeaponObject)item;
                        if (inventory.ContainsWeapon())
                        {
                            inventory.SetLastDetectedWeapon(mp5.gameObject); // Store the last detected weapon GameObject
                            inventory.ClearWeaponInventory();
                            Debug.Log("Clear mp5");
                            if (detectedWeapon != null)
                            {
                                Destroy(detectedWeapon);
                                Vector3 spawnPosition = playerMovement.transform.position + new Vector3(5, 0, 0); // Adjust the position as needed
                                if (!mp5.gameObject.activeSelf)
                                {
                                    Instantiate(detectedWeapon, spawnPosition, Quaternion.Euler(45, 0, 0));
                                }
                            }
                        }
                        inventory.AddWeapon(weapon);
                        if (!mp5.gameObject.activeSelf)
                        {
                            Instantiate(mp5.gameObject);
                        }
                        if (playerMovement != null && playerMovement.equippedWeaponSlot != null)
                        {
                            Animator pickedUpAnimator = mp5.GetComponent<Animator>();

                            mp5.transform.SetParent(playerMovement.equippedWeaponSlot.transform);
                            mp5.transform.localPosition = Vector3.zero; // Adjust as needed
                            mp5.transform.localRotation = Quaternion.identity; // Adjust as needed

                        }

                        mp5.GetComponent<Collider>().enabled = false;
                        Destroy(mp5.GetComponent<Rigidbody>());
                    }
                }

                if (remington && remington.canPickup)
                {
                    Debug.Log(remington.remington);
                    ItemObject item = remington.remington;
                    if (item is WeaponObject)
                    {
                        WeaponObject weapon = (WeaponObject)item;
                        if (inventory.ContainsWeapon())
                        {
                            inventory.SetLastDetectedWeapon(remington.gameObject); // Store the last detected weapon GameObject
                            inventory.ClearWeaponInventory();

                            Debug.Log("Clear remington");
                            if (detectedWeapon != null)
                            {
                                Destroy(detectedWeapon);
                                Vector3 spawnPosition = playerMovement.transform.position + new Vector3(5, 0, 0); // Adjust the position as needed
                                if (!remington.gameObject.activeSelf)
                                {
                                    Instantiate(detectedWeapon, spawnPosition, Quaternion.Euler(45, 0, 0));
                                }
                            }
                        }
                        inventory.AddWeapon(weapon);

                        if (!remington.gameObject.activeSelf)
                        {
                            Instantiate(remington.gameObject);
                        }
                        if (playerMovement != null && playerMovement.equippedWeaponSlot != null)
                        {
                            Animator pickedUpAnimator = remington.GetComponent<Animator>();

                            remington.transform.SetParent(playerMovement.equippedWeaponSlot.transform);
                            remington.transform.localPosition = Vector3.zero; // Adjust as needed
                            remington.transform.localRotation = Quaternion.identity; // Adjust as needed

                        }

                        remington.GetComponent<Collider>().enabled = false;
                        Destroy(remington.GetComponent<Rigidbody>());
                    }
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
