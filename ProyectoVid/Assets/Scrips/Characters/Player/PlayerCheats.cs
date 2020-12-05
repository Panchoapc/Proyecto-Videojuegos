using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sistema de trampas del juego.
/// Referencias: https://answers.unity.com/questions/553597/how-to-implement-cheat-codes.html
/// </summary>
public class PlayerCheats : MonoBehaviour {
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
                "Invulnerability",
                new KeyCode[] { KeyCode.S, KeyCode.T, KeyCode.R, KeyCode.O, KeyCode.N, KeyCode.G, KeyCode.M, KeyCode.I, KeyCode.N, KeyCode.D },
                () => { }
            ),
            new GameCheat(
                "Full mental sanity",
                new KeyCode[] { KeyCode.At, KeyCode.M, KeyCode.A, KeyCode.X, KeyCode.I, KeyCode.M, KeyCode.U, KeyCode.M },
                () => { }
            ),
            new GameCheat(
                "Enter nightmare mode",
                new KeyCode[] { KeyCode.B, KeyCode.O, KeyCode.R, KeyCode.I, KeyCode.N, KeyCode.G },
                () => { }
            ),
            new GameCheat(
                "Get RayGun",
                new KeyCode[] { KeyCode.C, KeyCode.A, KeyCode.N, KeyCode.D, KeyCode.Y },
                () => { this.player.PickUpWeapon(this.rayGunPrefab, false); }
            ),
            new GameCheat(
                "Get ShockSword",
                new KeyCode[] { KeyCode.S, KeyCode.T, KeyCode.I, KeyCode.C, KeyCode.K },
                () => { this.player.PickUpWeapon(this.swordPrefab, false); }
            )
        };
        this.cheatCodesIndex = new int[this.cheats.Length];
        this.ClearCheatIndex();
    }

    private void Update() {
        if (Input.anyKeyDown) {
            for (int k = 0; k < this.cheats.Length; k++) {
                if ( Input.GetKey(this.cheats[k].code[this.cheatCodesIndex[k]]) ) { // revisando condición de cumplir con el código de algún truco
                    if (this.cheatCodesIndex[k] == this.cheats[k].code.Length-1) { // viendo si llegó al final de un código
                        this.cheats[k].Activate();
                        this.ClearCheatIndex();
                        return;
                    }
                    this.cheatCodesIndex[k]++;
                } else {
                    this.cheatCodesIndex[k] = 0;
                }
            }
        }
    }

    private void ClearCheatIndex() {
        for (int n = 0; n < this.cheatCodesIndex.Length; n++) {
            this.cheatCodesIndex[n] = 0;
        }
    }
}