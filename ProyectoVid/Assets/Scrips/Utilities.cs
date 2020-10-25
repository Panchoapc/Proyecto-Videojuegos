using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {
    /* Debug log con formato de concatenación de strings. */
    public static void Logf(string format, params object[] args) {
        Debug.Log(string.Format(format, args));
    }
}
