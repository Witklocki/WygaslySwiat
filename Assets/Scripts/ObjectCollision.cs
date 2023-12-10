using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    [SerializeField] GameObject objectWriten;
    [SerializeField] ButtonWithLabel upgradeMenuLab;
    [SerializeField] string gameObjectTag;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (objectWriten.CompareTag(gameObjectTag))
            {
                print(upgradeMenuLab.enabled);
                upgradeMenuLab.gameObject.SetActive(true);

            }
        }
    }
}
