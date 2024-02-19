using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [Header("Question Blocs")]
    public Material questionMark;
    public float questionMarkOffset;

    [Header("Coin")]
    public Sprite coinSprite;
    public List<Texture> coinTextures;
    private int coinIndex;
    
    public float cycleTime;
    // Start is called before the first frame update
    void Start()
    {
        questionMark.mainTextureOffset = Vector2.zero;
        StartCoroutine(AnimationControl());
    }
    

    IEnumerator AnimationControl()
    {
        while (true)
        {
            questionMark.mainTextureOffset += new Vector2(0,questionMarkOffset);
            yield return new WaitForSecondsRealtime(cycleTime);
        }
    }
}
