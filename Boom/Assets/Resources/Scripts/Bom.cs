using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour
{
    // types
    public static string bom1 = "Bom";
    public int size;
    public static int MAX_SIZE = 6;

    float timer = 3;
    private void Awake()
    {
        Boom();
    }

    // Phat no
    void Boom()
    {
        StartCoroutine(IBoom(timer));
    }

    // ANI
    IEnumerator IBoom(float time)
    {
        yield return new WaitForSeconds(time - 1);
        // damage
        GameObject damage;
        Vector3 pos = transform.position;
        int i;
        // center
        damage = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
        damage.transform.position = transform.position;
        // left
        for (i = 1; i <= size; i++)
        {
            GameObject dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
            dtemp.transform.position = new Vector3(pos.x - i * transform.localScale.x, pos.y, pos.z);
        }
        // right
        for (i = 1; i <= size; i++)
        {
            GameObject dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
            dtemp.transform.position = new Vector3(pos.x + i * transform.localScale.x, pos.y, pos.z);
        }
        // top
        for (i = 1; i <= size; i++)
        {
            GameObject dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
            dtemp.transform.position = new Vector3(pos.x, pos.y + i * transform.localScale.y, pos.z);
        }
        // bottom
        for (i = 1; i <= size; i++)
        {
            GameObject dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
            dtemp.transform.position = new Vector3(pos.x, pos.y - i * transform.localScale.y, pos.z);
        }

        // damage
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
