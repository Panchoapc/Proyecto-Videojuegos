using UnityEngine;

public class CharacterAux : MonoBehaviour
{
    /// <summary>
    /// Voltea el sprite de `obj` según movimiento en eje X `xMovement`.
    /// </summary>
    /// <param name="spriteSize">Tamaño en Unity del sprite CUADRADO asociado a `obj`.</param>
    public static void FlipMovementX(MonoBehaviour obj, float xMovement, float spriteSize) {
        if (xMovement > 0) {
            obj.transform.localScale = new Vector3(spriteSize, spriteSize, spriteSize);
        }
        else if (xMovement < 0) {
            obj.transform.localScale = new Vector3(-spriteSize, spriteSize, spriteSize);
        }
    }
}
