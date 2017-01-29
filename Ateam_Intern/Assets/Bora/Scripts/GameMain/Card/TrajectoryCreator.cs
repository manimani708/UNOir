using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrajectoryCreator : MonoBehaviour {

	static TrajectoryCreator instance;

	public static TrajectoryCreator Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (TrajectoryCreator)FindObjectOfType(typeof(TrajectoryCreator));
				if (instance == null)
				{
					Debug.LogError("TrajectoryCreator Instance Error");
				}
			}
			return instance;
		}
	}

	[SerializeField]
	GameObject trajectory = null;

	List<GameObject> trajectoryList = new List<GameObject>();

	void Awake()
	{
		if (this != Instance)
		{
			Destroy(this.gameObject);
			return;
		}
	}

	public void Reset() {
		for (int i = 0; i < trajectoryList.Count; i++) {
			Destroy (trajectoryList[i].gameObject);
		}
		trajectoryList.Clear ();
	}

	public void Create(Vector3 EndPos) {

		if (FieldCard.Instance.TempList.Count <= 0)
			return;

		Vector3 StartPos = FieldCard.Instance.TempList[FieldCard.Instance.TempList.Count - 1].GetInitPos;

		Vector3 pos = StartPos + ((EndPos - StartPos) / 2.0f);
		float fAngle = Mathf.Atan2 (EndPos.y - StartPos.y, EndPos.x - StartPos.x);

		GameObject temp = (GameObject)Instantiate (trajectory, pos, Quaternion.identity);
		temp.transform.eulerAngles = new Vector3 (0,0, fAngle * Mathf.Rad2Deg + 90.0f);
		//Debug.Log ("StartPos" + StartPos + ",EndPos" + EndPos + ",radian" + fAngle + ",degree" + fAngle * Mathf.Rad2Deg);

		Vector3 scale = temp.transform.localScale;
		temp.transform.localScale = new Vector3 (scale.x, scale.y * Vector3.Distance(StartPos, EndPos), scale.z); 
		//Debug.Log (Vector3.Distance(StartPos, EndPos));

		trajectoryList.Add (temp);
	}
}
