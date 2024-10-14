using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTrailRenderer : MonoBehaviour
{
    //Changeable sprite properties
    public int ClonesPerSecond = 10;
    public Vector3 scalePerSecond = new Vector3(1f, 1f, 1f);
    public Color colorPerSecond = new Color(255, 255, 255, 1f);

    //Private variables that shouldn't be accessed outside of the script
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Transform tf;
    private List<SpriteRenderer> clones;

    
    void Start()
    {
        //assigns variables
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        clones = new List<SpriteRenderer>();
        StartCoroutine(trail());
    }

    void Update()
    {
        //loops through the clones
        for (int i = 0; i < clones.Count; i++)
        {
            //fades out the clones and makes them smaller
            clones[i].color -= colorPerSecond * Time.deltaTime;
            clones[i].transform.localScale -= scalePerSecond * Time.deltaTime;

            //Removes clones
            if (clones[i].color.a <= 0f || clones[i].transform.localScale == Vector3.zero)
            {
                Destroy(clones[i].gameObject);
                clones.RemoveAt(i);
                i--;
            }
        }
    }

    IEnumerator trail()
    {
        for (; ; ) //while(true)
        {
            if (rb.velocity != Vector2.zero)
            {
                //actively changes clone position scale and sorting order
                var clone = new GameObject("trailClone");
                clone.transform.position = tf.position;
                clone.transform.localScale = tf.localScale;
                var cloneRend = clone.AddComponent<SpriteRenderer>();
                cloneRend.sprite = sr.sprite;
                cloneRend.sortingOrder = sr.sortingOrder - 1;
                clones.Add(cloneRend);
            }
            yield return new WaitForSeconds(1f / ClonesPerSecond);
        }
    }
}