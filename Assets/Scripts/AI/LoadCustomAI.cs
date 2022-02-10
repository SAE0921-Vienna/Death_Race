using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class LoadCustomAI : SpaceshipLoad
{

    private void Awake()
    {
        if (gameObject.CompareTag("AI"))
        {

            int shiprand = Random.Range(0, allShips.Count);
            int weaponrand = Random.Range(0, allWeapons.Count);
            int materialrand = Random.Range(0, allMaterials.Count);

            currentShip = shiprand;
            currentWeapon = weaponrand;
            currentMaterial = materialrand;

            GetComponentInChildren<MeshFilter>().mesh = allShips[currentShip].vehicleMesh;
            //GetComponentInChildren<MeshCollider>().sharedMesh = allShips[currentShip].vehicleColliderMesh;

            GetComponentInChildren<MeshRenderer>().material = allMaterials[currentMaterial].material;

            GameObject weaponClone = Instantiate(allWeapons[currentWeapon].vehicleWeaponPrefab, transform.GetChild(1).GetChild(1).transform, false);
            weaponClone.GetComponent<MeshRenderer>().material = allMaterials[currentMaterial].material;
            weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = allMaterials[currentMaterial].material;
            weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = allMaterials[currentMaterial].material;

            weaponClone.transform.parent.localPosition = allShips[currentShip].WeaponPosition;
        }
        if (gameObject.CompareTag("Ghost"))
        {
            int shiprand = saveLoadScript.lastGhostMaterialIndex;
            int materialrand = saveLoadScript.lastGhostMaterialIndex;

            currentShip = shiprand;
            currentMaterial = materialrand;

            GetComponentInChildren<MeshFilter>().mesh = allShips[currentShip].vehicleMesh;
            GetComponentInChildren<MeshCollider>().sharedMesh = allShips[currentShip].vehicleColliderMesh;

            GetComponentInChildren<MeshRenderer>().material = allMaterials[currentMaterial].material;
        }
    }


}
