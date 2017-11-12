using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MyCamara : MonoBehaviour
{
    public Camera Camara;

    public Vector3 Offset = new Vector3(0,7.5f,7f);

    protected Figure myFigure;
    public Transform targetTransform;

    protected Transform myTransform;

    public Figure Figure {
        set {
            myFigure = value;
            targetTransform = myFigure.Behavior.Transform;
            myTransform = transform;
        }
    }

    public enum State {
        None,
        LookAtMain,
        LookAtTartget
    }

    [SerializeField]
    protected State myState = State.None;

    public State CamaraState {
        set {
            myState = value;
        }
    }

    public void Update() {
        if (myFigure == null)
            return;

        myTransform.position = targetTransform.position + Offset;
        myTransform.LookAt(targetTransform);
        
    }
    /// <summary>
    /// 找到或創立一個主攝影機
    /// 擁有MyCamera類別
    /// </summary>
    /// <returns></returns>
    static public MyCamara FindCamera() {
        Camera cam = Camera.main;
        if (cam == null) {
            cam = Instantiate(LoadFactory.Instance.LoadGameObject("Camera")).GetComponent<Camera>();
        }
        MyCamara result = cam.GetComponent<MyCamara>();
        if (result == null) {
            result = cam.gameObject.AddComponent<MyCamara>();
        }
        return result;
    }
}
