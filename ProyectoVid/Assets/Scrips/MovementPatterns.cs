using System;
using UnityEngine;

/// <summary>
/// Contenedor de patrones de movimiento para objetos del juego.
/// </summary>
public static class MovementPatterns {
    /// <summary>
    /// `obj` huye de `target`.
    /// </summary>
    public static void Flee(Transform obj, Transform target) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// `obj` persigue a `target`.
    /// </summary>
    public static void Seek(Transform obj, Transform target) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// `obj` acosa a `target` a una distancia `distance`, i.e. se mueve de forma de quedar
    /// a una disntancia `distance`.
    /// </summary>
    public static void Stalk(Transform obj, Transform target, float distance) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// `obj` deambula, i.e. se mueve aleatoriamente.
    /// </summary>
    public static void Wander(Transform obj) {
        throw new NotImplementedException();
    }
}
