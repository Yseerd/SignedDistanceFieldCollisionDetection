using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDFQuery : MonoBehaviour
{
    public SDFCalculator sdfCalculator;

    void Update()
    {
        // 获取物体的世界坐标
        Vector3 position = transform.position;

        // 将世界坐标转换为SDF网格坐标
        float sdfValue = sdfCalculator.GetSDFValue(position);

        // 输出SDF值用于调试
//        Debug.Log("Position: " + position + " SDF Value: " + sdfValue);

        // 基于SDF值进行相应的处理，比如碰撞检测
        if (sdfValue < 0)
        {
            Debug.Log("Inside geometry!");
        }
        else
        {
         //   Debug.Log("Outside geometry.");
        }
    }
}
