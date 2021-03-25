using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DisplayChecker : MonoBehaviour {

    private void Awake() {

        var txt = new StringBuilder();
        txt.AppendLine($"Num of displays : {Display.displays.Length}");
        for (var i = 0; i < Display.displays.Length; i++) {
            var d = Display.displays[i];
            if (!d.active)
                d.Activate();
            txt.AppendLine($"{i}:\tsize={d.systemWidth}x{d.systemHeight}");
        }

        Debug.Log(txt.ToString());
    }
}
