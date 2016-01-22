using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncRotation : NetworkBehaviour {

	[SyncVar] private Quaternion syncPlayerRotation;
	[SyncVar] private Quaternion syncCamRotation;

	[SerializeField] private Transform playerTransform;
	[SerializeField] private Transform camTransform;
	[SerializeField] private float lerpRate = 15.0f;

	void Update () {
		TransmitRotation ();
		LerpRotation ();
	}

	void LerpRotation() {
		if ( !isLocalPlayer ) {
			playerTransform.rotation = Quaternion.Lerp (playerTransform.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
			camTransform.rotation = Quaternion.Lerp (camTransform.rotation, syncCamRotation, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void CmdProvideRotationToServer(Quaternion playerRot, Quaternion camRot) {
		syncPlayerRotation = playerRot;
		syncCamRotation = camRot;
	}

	[ClientCallback]
	void TransmitRotation() {
		if ( isLocalPlayer ) {
			CmdProvideRotationToServer (playerTransform.rotation, camTransform.rotation);
		}
	}
}
