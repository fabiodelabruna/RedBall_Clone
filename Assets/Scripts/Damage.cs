using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyTag"))
        {
            foreach (ContactPoint2D hitPos in other.contacts)
            {
                if (hitPos.normal.y > 0 && hitPos.normal.y >= Mathf.Abs(hitPos.normal.x))
                {
                    rb.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
                    Destroy(other.gameObject);
                }
                else
                {
                    rb.AddForce(new Vector2(hitPos.normal.x * 2, hitPos.normal.y * 2), ForceMode2D.Impulse);
                    if (GameManager.instance.life > 0)
                    {
                        GameManager.instance.life--;
                        GameManager.instance.imgHearts[GameManager.instance.life].enabled = false;
                    }

                    if (GameManager.instance.life <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

}
