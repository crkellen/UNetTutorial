using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

	[SerializeField] Camera FPSCharacterCam;
	[SerializeField] AudioListener FPSCharacterListener;

	void Start () {
		if ( isLocalPlayer ) {
			GameObject.Find ("Scene Camera").SetActive(false);
			GetComponent<CharacterController> ().enabled = true;
			GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = true;
			FPSCharacterCam.enabled = true;
			FPSCharacterListener.enabled = true;
		}
	}
}
