using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    // Start is called before the first frame update
	public int slotNumber;
	public int pointValue;
	private void OnTriggerEnter(Collider other){	
		Debug.Log($"entered {slotNumber} worth {pointValue}");
	}
}
