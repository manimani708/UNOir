using UnityEngine;
using System.Collections;

public class Caution : MonoBehaviour 
{
    [SerializeField]
    private int flashCount = 3;

    [SerializeField]
    private float flashInterval = 0.2f;

    public bool isFlashing { get; set;}

    private float r, g, b;
    private Color transmission, nonTransmission;    //透明、不透明
    private SpriteRenderer spriteRenderer;


    static Caution instance;

    public static Caution Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (Caution)FindObjectOfType(typeof(Caution));
                if (instance == null)
                {
                    Debug.LogError("Caution Instance Error");
                }
            }
            return instance;
        }
    }


    void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }


        spriteRenderer = GetComponent<SpriteRenderer>();
        r = spriteRenderer.color.r;
        g = spriteRenderer.color.g;
        b = spriteRenderer.color.b;
        transmission = new Color(r, g, b, 0);
        nonTransmission = new Color(r, g, b, 1);
        spriteRenderer.color = transmission;
        isFlashing = false;
    }


    //点滅アニメーション
    public void FlashAnimation()
    {
        StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine()
    {
        isFlashing = true;
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_ENEMYATACKALARM);

        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = nonTransmission;
            yield return new WaitForSeconds(flashInterval);

            spriteRenderer.color = transmission;
            yield return new WaitForSeconds(flashInterval);
        }

        yield return null;
    }
}
