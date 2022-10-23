using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    [SerializeField] GameObject UFOPrefab;

    [SerializeField] int spawnZPos = 7;

    [SerializeField] Player player;
    [SerializeField] float timeOut = 5;

    [SerializeField] float timer = 0;
    int playerLastMaxTravel = 0;
   
    private void SpawnUFO()
    {
        player.enabled = false;
        var position = new Vector3(player.transform.position.x, 1, player.CurrentTravel + spawnZPos);
        var rotation = Quaternion.Euler(0,180,0);
        var ufoObject = Instantiate(UFOPrefab, position, rotation);
        var ufo = ufoObject.GetComponent<UFO>();
        ufo.SetUpTarget(player);
    }

    private void Update(){
        //jika player ada kemajuan
        if(player.MaxTravel != playerLastMaxTravel)
        {
            //maka reset timer
            timer=0;
            playerLastMaxTravel=player.MaxTravel;
            return;
        }

        //jika tidak ada kemajuan
        if(timer < timeOut)
        {
            timer += Time.deltaTime;
            return;
        }

        //jika sudah timeout
        if(player.IsJumping()==false && player.IsDie == false)
        SpawnUFO();
    }
    
}
