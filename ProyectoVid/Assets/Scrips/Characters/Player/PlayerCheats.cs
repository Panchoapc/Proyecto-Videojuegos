using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sistema de trampas del juego. Se pueden llamar en cualquier momento ingresando el código en el teclado.
/// Referencias: https://answers.unity.com/questions/553597/how-to-implement-cheat-codes.html
/// </summary>
public class PlayerCheats : MonoBehaviour {
    public static bool GOD_MODE { get; private set; } = false;
    public static bool FORCED_NIGHTMARE_MODE { get; private set; } = false;
    private const float SUPER_MOVE_SPEED = 18;

    [SerializeField] private GameObject rayGunPrefab = null;
    [SerializeField] private GameObject swordPrefab = null;
    private Player player;
    private GameCheat[] cheats;
    private int[] cheatCodesIndex; // variable auxiliar para identificar códigos de cheats

    /// <summary>
    /// Contenedor del código de una trampa y lo que hace al activarse.
    /// </summary>
    private class GameCheat {
        private string name; // nombre de la trampa (no importante)
        public KeyCode[] code { get; private set; } // código que activa la trampa
        private System.Action action; // acción a ejecutar cuando se activa la trampa. Esta variable es una función void lambda.

        public GameCheat(string name, KeyCode[] code, System.Action action) {
            this.name = name;
            this.code = code;
            this.action = action;
        }

        public void Activate() { // se activó el cheat, ejecutando acción
            Debug.LogFormat("[PlayerCheats] Cheat activated: {0}", this.name);
            this.action.Invoke();
        }
    }

    private void Start() {
        this.player = FindObjectOfType<Player>();

        /**
         * Ver README para tabla de cheats.
         */
        this.cheats = new GameCheat[] {
            new GameCheat(
                "Toggle invulnerability",
                new KeyCode[] { KeyCode.S, KeyCode.T, KeyCode.R, KeyCode.O, KeyCode.N, KeyCode.G, KeyCode.M, KeyCode.I, KeyCode.N, KeyCode.D },
                () => {
                    GOD_MODE = !GOD_MODE;
                }
            ),
            new GameCheat(
                "Full mental sanity",
                new KeyCode[] { KeyCode.At, KeyCode.M, KeyCode.A, KeyCode.X },
                () => {
                    this.player.Heal(Player.MAX_SANITY);
                }
            ),
            new GameCheat(
                "Toggle force nightmare mode",
                new KeyCode[] { KeyCode.F, KeyCode.R, KeyCode.E, KeyCode.N, KeyCode.Z, KeyCode.Y },
                () => {
                    FORCED_NIGHTMARE_MODE = !FORCED_NIGHTMARE_MODE;
                }
            ),
            new GameCheat(
                "Get RayGun",
                new KeyCode[] { KeyCode.P, KeyCode.I, KeyCode.U, KeyCode.M, KeyCode.P, KeyCode.I, KeyCode.U, KeyCode.M },
                () => {
                    player.PickUpWeapon(rayGunPrefab, false);
                }
            ),
            new GameCheat(
                "Get ShockSword",
                new KeyCode[] { KeyCode.S, KeyCode.T, KeyCode.I, KeyCode.C, KeyCode.K },
                () => {
                    player.PickUpWeapon(swordPrefab, false);
                }
            ),
            new GameCheat(
                "Super movement speed",
                new KeyCode[] { KeyCode.A, KeyCode.T, KeyCode.O, KeyCode.D, KeyCode.O, KeyCode.G, KeyCode.A, KeyCode.S },
                () => {
                    player.ChangeMoveSpeed(SUPER_MOVE_SPEED);
                }
            ),
            new GameCheat(
                "Jump to next level",
                new KeyCode[] { KeyCode.A, KeyCode.E, KeyCode.G, KeyCode.I, KeyCode.S },
                () => {
                    player.LoadNextLevel();
                }
            )
        };
        this.cheatCodesIndex = new int[cheats.Length];
        this.ClearCheatIndex();
    }

    private void Update() {
        if (Input.anyKeyDown) {
            for (int k = 0; k < cheats.Length; k++) {
                if ( Input.GetKey(cheats[k].code[cheatCodesIndex[k]]) ) { // revisando condición de cumplir con el código de algún truco
                    if (cheatCodesIndex[k] == cheats[k].code.Length-1) { // viendo si llegó al final de un código
                        cheats[k].Activate();
                        ClearCheatIndex();
                        return;
                    }
                    cheatCodesIndex[k]++;
                } else {
                    cheatCodesIndex[k] = 0;
                }
            }
        }
    }

    /// <summary>
    /// Rellena `cheatCodesIndex` con ceros, reiniciando así el "progreso" de los códigos de todos los cheats.
    /// </summary>
    private void ClearCheatIndex() {
        for (int n = 0; n < this.cheatCodesIndex.Length; n++) {
            this.cheatCodesIndex[n] = 0;
        }
    }
}