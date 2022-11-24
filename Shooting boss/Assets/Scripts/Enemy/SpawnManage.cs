using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManage : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemy_death;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 location = new Vector3(Random.Range(0, 16), 2, Random.Range(0, 16));
        Instantiate(enemy, location, enemy.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void active_Spawn(Transform pos)
    {
        GameObject enemy_delete = Instantiate(enemy_death, pos.position, pos.rotation);
        StartCoroutine(spawn());
        StartCoroutine(delete(enemy_delete));
    }    

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(7f);
        Vector3 location = new Vector3(Random.Range(-130, -23), 2, Random.Range(-32, 33));
        Instantiate(enemy, location, enemy.transform.rotation);

        location = new Vector3(Random.Range(-130, -23), 2, Random.Range(-32, 33));
        Instantiate(enemy, location, enemy.transform.rotation);
    }

    IEnumerator delete(GameObject enemy_delete)
    {
        yield return new WaitForSeconds(5f);
        Destroy(enemy_delete);
    }


}
