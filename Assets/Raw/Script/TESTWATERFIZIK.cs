using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crest;


[AddComponentMenu(Crest.Internal.Constants.MENU_PREFIX_EXAMPLE + "Ocean Sample Height Demo")]


public class TESTWATERFIZIK : MonoBehaviour
{

    [SerializeField, HideInInspector]
#pragma warning disable 414
    int _version = 0;
#pragma warning restore 414

    SampleHeightHelper _sampleHeightHelper = new SampleHeightHelper();

    void Update()
    {
        // Assume a primitive like a sphere or box.
        var r = transform.lossyScale.magnitude;
        _sampleHeightHelper.Init(transform.position, 2f * r);

        if (_sampleHeightHelper.Sample(out var height))
        {
            var pos = transform.position;
            pos.y = height;
            transform.position = pos;
        }
    }
}