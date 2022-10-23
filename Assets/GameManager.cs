using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
   [SerializeField] GameObject grass;

   [SerializeField] GameObject gameOverPanel;

   [SerializeField] Player player;

   [SerializeField] GameObject road;

   [SerializeField] int extent = 7;

   [SerializeField] int frontDistance = 10;

   [SerializeField] int minZPos = -5;

   [SerializeField] int maxSameTerrainRepeat = 3;

    //int maxZPos;

    Dictionary<int, TerrainBlock> map = new Dictionary<int, TerrainBlock>(50);
    TMP_Text gameOverText;

   private void Start() 
   {
        //setup gameOverPanel
        gameOverPanel.SetActive(false);
        gameOverText = gameOverPanel.GetComponentInChildren<TMP_Text>();
        //belakang
        for  (int z = minZPos; z <= 0; z++)
        {
           CreateTerrain(grass, z);
        }

        //depan
        for (int z = 1; z <= frontDistance; z++)
        {
            
            var prefab = GetNextRandomTerrainPrefab(z);

            //instantiate block
            CreateTerrain(prefab, z);
        }

        player.SetUp(minZPos, extent);
    }

    private int playerLastMaxTravel;

    private void Update(){
            //cek player
            if(player.IsDie && gameOverPanel.activeInHierarchy==false)
               StartCoroutine(ShowGameOverPanel());

            //infinite terrain
            if(player.MaxTravel==playerLastMaxTravel)
                return;

            playerLastMaxTravel = player.MaxTravel;

            //buat ke depan
            var randTbPrefab = GetNextRandomTerrainPrefab(player.MaxTravel+frontDistance);
            CreateTerrain(randTbPrefab,player.MaxTravel+frontDistance);

            //hapus belakang
            var lastTB = map[player.MaxTravel-1+minZPos];
            map.Remove(player.MaxTravel-1+minZPos);
            Destroy(lastTB.gameObject);

            player.SetUp(player.MaxTravel+minZPos,extent);
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(3);

        gameOverText.text = "YOUR SCORE: " + player.MaxTravel;
        gameOverPanel.SetActive(true);      
    }

    private void CreateTerrain(GameObject prefab, int zPos)
    {
        var go = Instantiate(prefab,new Vector3(0, 0, zPos), Quaternion.identity);
        var tb = go.GetComponent<TerrainBlock>();
        tb.Build(extent);
        
        map.Add(zPos, tb);
    }

    private GameObject GetNextRandomTerrainPrefab(int nextPos)
    {
        bool isUniform = true;
        var tbRef = map[nextPos - 1];
        for (int distance = 2; distance <= maxSameTerrainRepeat;  distance++)
        {
            if (map[nextPos - distance].GetType() != tbRef.GetType())
            {
                isUniform = false;
                break; 
            }        
        }

        if (isUniform)
        {
            if (tbRef is Grass)
                return road;
            else
                return grass;

        }
        //mendapatkan terrain block dengan probabilitas50%
        return Random.value > 0.5f ? road : grass;
    }


}
