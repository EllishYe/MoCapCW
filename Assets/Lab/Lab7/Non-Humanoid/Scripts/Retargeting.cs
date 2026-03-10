using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retargeting : MonoBehaviour
{
    [System.Serializable] public struct ObjectsToRetargetPos
    {
        public Transform from;
        public Transform to;
        public float scaleMultiplier;
        public Vector3 offset;
    }

    [System.Serializable] public struct ObjectsToRetarget
    {
        public Transform from;
        public Transform to;
        public Quaternion offset;
    }

    public List<ObjectsToRetargetPos> retargetPosition;
    public List<ObjectsToRetarget> retargetRotation;


    void Update()
    {
        if (retargetPosition.Count > 0)
        {
            for (int i = 0; i < retargetPosition.Count; i++)
            {
                retargetPosition[i].to.localPosition = retargetPosition[i].from.localPosition * retargetPosition[i].scaleMultiplier +retargetPosition[i].offset;
            }
        }

        if (retargetRotation.Count > 0)
        {
            for (int i = 0; i < retargetRotation.Count; i++)
            {
                retargetRotation[i].to.rotation = retargetRotation[i].from.rotation * retargetRotation[i].offset;
            }
        }
    
    }
    
}
