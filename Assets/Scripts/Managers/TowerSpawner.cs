using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TowerSpawner : MonoBehaviour
{
    public GameObject towerToSpawn;
    public int cost;

    private bool canSpawn = false;
    
    public enum Tower
    {
        Lightning,
        Fire
    }

    public Tower tower;

    private ObjectPooler.Key towerKey;

    private void Start()
    {
        if (tower == Tower.Lightning)
        {
            towerKey = ObjectPooler.Key.LightningTower;
        }
        else
        {
            towerKey = ObjectPooler.Key.FireTower;
        }
    }

    void Update()
    {
        if (!canSpawn && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = GameManager.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "TowerSpawner" && hit.transform.gameObject == transform.gameObject)
                {
                    Debug.Log("Test here");
                    if (GameManager.Instance.SpendResource(cost))
                    {
                        SpawnTower();
                        canSpawn = true;
                    }
                }

            }
        }
    }

    void SpawnTower()
    {
        GameObject pooledObj = ObjectPooler.GetPooler(towerKey).GetPooledObject();

        

        pooledObj.transform.position = new Vector3(transform.position.x, (transform.position.y + (transform.localScale.y / 2)), transform.position.z);

        pooledObj.SetActive(true);
        pooledObj.transform.DOScale(transform.localScale, 1);
    }

}
