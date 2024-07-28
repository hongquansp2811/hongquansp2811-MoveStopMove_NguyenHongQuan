using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointText : GameUnit
{
    [SerializeField] private TextMeshProUGUI point;

    private void Update()
    {
        transform.forward = Vector3.forward;
    }

    public void OnInit(int pointChar)
    {
        this.point.text = pointChar.ToString();
    }
}
