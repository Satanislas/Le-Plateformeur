using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Material stone;
    public GameObject brickExplosionPrefab;
    public float brickExplostionTime;
    
    public enum Type{
        Question,
        Brick,
        Immobile,
    }

    public Type type;


    public void DestroyBlock()
    {
        GameObject effect = Instantiate(brickExplosionPrefab, transform.position, Quaternion.identity);
        GameManager.KillInThreeTwoOne(effect,brickExplostionTime);
        Destroy(gameObject);
    }

    public void GetHit(GameObject mario)
    {
        switch (type)
        {
            case Type.Question:
                Debug.Log("+1 coin");
                GameManager.gm.CollectCoin(transform.position + new Vector3(0,0,-0.1f));
                type = Type.Immobile;
                GetComponent<Renderer>().material = stone;
                break;
            case Type.Brick:
                Debug.Log("Breaking brick");
                GameManager.gm.addScore(100);
                DestroyBlock();
                break;
        }
    }
}
