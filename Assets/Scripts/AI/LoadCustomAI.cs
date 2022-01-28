using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class LoadCustomAI : SpaceshipLoad
{
    

    private void Awake()
    {
        int shiprand = Random.Range(0, allShips.Count);
        int weaponrand = Random.Range(0, allWeapons.Count);
        int materialrand = Random.Range(0, allMaterials.Count);

        currentShip = shiprand;
        currentWeapon = weaponrand;
        currentMaterial = materialrand;

        GetComponentInChildren<MeshFilter>().mesh = allShips[currentShip].vehicleMesh;
        GetComponentInChildren<MeshCollider>().sharedMesh = allShips[currentShip].vehicleColliderMesh;

        GetComponentInChildren<MeshRenderer>().material = allMaterials[currentMaterial].material;

        GameObject weaponClone = Instantiate(allWeapons[currentWeapon].vehicleWeaponPrefab, transform.GetChild(1).GetChild(1).transform, false);
        if (weaponClone.GetComponent<IWeapon>() == null) return;
        vehicleWeaponScript = weaponClone.GetComponent<IWeapon>();
        weaponClone.GetComponent<MeshRenderer>().material = allMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = allMaterials[currentMaterial].material;
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = allMaterials[currentMaterial].material;

        weaponClone.transform.parent.localPosition = allShips[currentShip].WeaponPosition;
    }


}