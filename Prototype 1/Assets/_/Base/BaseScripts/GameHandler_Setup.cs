/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
using CodeMonkey.MonoBehaviours;

public class GameHandler_Setup : MonoBehaviour {

    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Transform followTransform;

    private void Start() {
        //Sound_Manager.Init();
        cameraFollow.Setup(GetCameraPosition, () => 60f, true, true);

        //FunctionPeriodic.Create(SpawnEnemy, 1.5f);
        //EnemyHandler.Create(new Vector3(20, 0));
    }

    private Vector3 GetCameraPosition() {
        return followTransform.position;
    }

}
