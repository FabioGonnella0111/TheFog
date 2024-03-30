using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float Rate = 1.0f;
    [SerializeField] private GameObject[] enemyPrefabs;
    public Transform Hero;
    public int numSpawn;

    private List<GameObject> oggettiClonati = new List<GameObject>();
    private int countMorti;
    private bool countMortiTrue;
    private bool condition = true;

    private void Start()
    {
        StartCoroutine(Spawner());
        StartCoroutine(RandomPosition());
    }


    private void Update()
    {
        countMorti = FindObjectOfType<ManagerScript>().CountMessage();
        countMortiTrue = isDeath(countMorti);
    }

    public bool isDeath(int m)
    {
        bool yes = false;

        if((m == numSpawn))
        {
            yes = true;
        }
        else 
            yes = false;

        return yes;
    }

    public IEnumerator Spawner()
    {   
        int i = 0;
        WaitForSeconds wait = new WaitForSeconds(Rate);

        while (condition)
        {
            i++;
            
            yield return wait;
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];
            
            GameObject clone = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            clone.name = "SkeletonClone " + i.ToString();
            oggettiClonati.Add(clone);

            if (i == numSpawn)
            {
                yield return new WaitUntil(() => countMortiTrue);
                numSpawn = 2 * numSpawn + 1;
                countMortiTrue = false;
                yield return new WaitForSeconds(4);
            }

        }
      
    }

    public void DestroyClones()
    {
        

        for (int i = oggettiClonati.Count - 1; i >= 0; i--)
        {
            FindObjectOfType<SkeletonMove>().Damn(oggettiClonati[i]);
        }

    }

    private IEnumerator RandomPosition()
    {
        int i = 0;

        while (true) 
        {   
            i++;
            float Xspawner = transform.position.x;
            float Yspawner = transform.position.y;
            Xspawner += 8;

            if (i % 2 == 1)
                Xspawner = -Xspawner;
            else
                Xspawner = 1 * Xspawner;

            Vector2 Position = new Vector2(Xspawner, Yspawner);
            transform.position = Position;

            yield return new WaitForSeconds(Rate);
        }
        
    }
}
