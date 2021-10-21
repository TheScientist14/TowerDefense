using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyBehaviour : MonoBehaviour
{
	// Start is called before the first frame update
	void OnEnable()
	{
		StartCoroutine("CheckIfAlive");
	}

	IEnumerator CheckIfAlive()
	{
		while (true)
		{
			yield return new WaitForSeconds(1f);
			GameObject.Destroy(this.gameObject);
			break;
		}
	}
}
