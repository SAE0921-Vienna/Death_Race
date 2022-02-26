using AI;
using UnityEngine;

public class LoadCustomAI : SpaceshipLoad
{

    private AI_VehicleController _aiVehicleController;

    private void Awake()
    {
        if (gameObject.CompareTag("AI"))
        {
            LoadAI();
            SetVehicleStats();
            //SetVehicleStatsAI();
        }
        //if (gameObject.CompareTag("Ghost"))
        //{
        //    LoadGhost();
        //}
        SetVFXPrefab();
    }
    
    private void LoadAI()
    {
        var shipRand = Random.Range(0, allShips.Count);
        var weaponRand = Random.Range(0, allWeapons.Count);
        var materialRand = Random.Range(0, allMaterials.Count);

        currentShip = shipRand;
        currentWeapon = weaponRand;
        currentMaterial = materialRand;

        GetComponentInChildren<MeshFilter>().mesh = allShips[currentShip].vehicleMesh;
        //GetComponentInChildren<MeshCollider>().sharedMesh = allShips[currentShip].vehicleColliderMesh;

        GetComponentInChildren<MeshRenderer>().material = allMaterials[currentMaterial].material;

        var weaponClone = Instantiate(allWeapons[currentWeapon].vehicleWeaponPrefab,
            transform.GetChild(1).GetChild(1).transform, false);
        
        weaponClone.GetComponent<MeshRenderer>().material = 
            allMaterials[currentMaterial].material;
        
        weaponClone.transform.GetChild(0).GetComponent<MeshRenderer>().material = 
            allMaterials[currentMaterial].material;
        
        weaponClone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material =
            allMaterials[currentMaterial].material;

        weaponClone.transform.parent.localPosition = allShips[currentShip].WeaponPosition;
    }
    
    public Mesh LoadGhost(int ghostVehicleIndex)
    {
       return allShips[ghostVehicleIndex].vehicleMesh;       
    }

}
