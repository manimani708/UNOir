using UnityEngine;
using System.Collections;

public class CharaIconFade : MonoBehaviour {


	[SerializeField]
	float fTime = 0.5f;

    [SerializeField]
    float fMaxAlpha = 0.5f;
	
	bool bAdd = true;

	SpriteRenderer render = null;
	
	void Start () {
		render = GetComponent<SpriteRenderer>();
		render.color = new Color(1,1,1,0);
	}
	
	void Update () {
	
		if(bAdd) {
            render.color += new Color(0, 0, 0, fMaxAlpha * (Time.deltaTime / fTime));

            if (render.color.a >= fMaxAlpha)
            {
				bAdd = false;
			}
		} else {
            render.color -= new Color(0, 0, 0, fMaxAlpha * (Time.deltaTime / fTime));
			
			if(render.color.a <= 0.0f) {
				bAdd = true;
			}
		}
	
	}
}
