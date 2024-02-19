using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum Type{
        Question,
        Brick,
        Immobile,
    }

    public Type type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DestroyBlock()
    {
        Destroy(gameObject);
    }

    public void GetHit(GameObject mario)
    {
        switch (type)
        {
            case Type.Question:
                Debug.Log("+1 coin");
                GameManager.gm.CollectCoin(transform.position + new Vector3(0,0,-0.1f));
                break;
            case Type.Brick:
                Debug.Log("Breaking brick");
                DestroyBlock();
                break;
        }
    }
}
