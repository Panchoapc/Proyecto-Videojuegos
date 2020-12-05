using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sistema de trampas del juego.
/// Referencias: https://answers.unity.com/questions/553597/how-to-implement-cheat-codes.html
/// </summary>
public class PlayerCheats : MonoBehaviour {
    public static bool GOD_MODE { get; private set; } = false;
    [SerializeField] private GameObject rayGunPrefab = null;
    [SerializeField] private GameObject swordPrefab = null;
    private Player player;
    private GameCheat[] cheats;
    private int[] cheatCodesIndex; // variable auxiliar para identificar códigos de cheats

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
         * STRONGMIND | Ningún ataque hace daño al jugador.
         * @MAXIMUM   | Rellenar sanidad mental.
         * BORING     | Bajar la sanidad lo suficiente como para entrar en modo pesadilla.
         * CANDY      | Obtener pistola de rayos (RayGun).
         * STICK      | Obtener espada (ShockSword).
         */
        this.cheats = new GameCheat[] {
            new GameCheat(
                "Toggle invulnerability",
                new KeyCode[] { KeyCode.S, KeyCode.T, KeyCode.R, KeyCode.O, KeyCode.N, KeyCode.G, KeyCode.M, KeyCode.I, KeyCode.N, KeyCode.D },
                () => {
                    PlayerCheats.GOD_MODE = !PlayerCheats.GOD_MODE;
                }
            ),
            new GameCheat(
                "Full mental sanity",
                new KeyCode[] { KeyCode.At, KeyCode.M, KeyCode.A, KeyCode.X, KeyCode.I, KeyCode.M, KeyCode.U, KeyCode.M },
                () => {
                    this.player.Heal(Player.MAX_SANITY);
                }
            ),
            new GameCheat(
                "Enter nightmare mode",
                new KeyCode[] { KeyCode.B, KeyCode.O, KeyCode.R, KeyCode.I, KeyCode.N, KeyCode.G },
                () => {
                    if (player.mentalSanity > Player.NIGHTMARE_SANITY) { // sólo entra en modo pesadilla si no estaba antes en ese estado
                        player.TakeDamage(player.mentalSanity - Player.NIGHTMARE_SANITY);
                    }
                }
            ),
            new GameCheat(
                "Get RayGun",
                new KeyCode[] { KeyCode.C, KeyCode.A, KeyCode.N, KeyCode.D, KeyCode.Y },
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
    /// Rellena `cheatCodesIndex` de ceros, reiniciando así el "progreso" de los códigos de todos los cheats.
    /// </summary>
    private void ClearCheatIndex() {
        for (int n = 0; n < this.cheatCodesIndex.Length; n++) {
            this.cheatCodesIndex[n] = 0;
        }
    }
}