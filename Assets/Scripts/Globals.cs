using System;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {    //Aqui ficar�o as configura��es globais do jogo, dispon�veis em qualquer cena, e que poser�o ser salvas se o jogo contar com um sistema de save
    public static int idLanguage = 0;
    public static float volumeOST = 1, volumeSFX = 1;

    //Essas informa��es n�o ser�o salvas e s� servir�o para definir certas coisas no jogo:
    public static bool firstScene = true;
    public enum languages {
        english,
        portuguese,
    }

    public static Dictionary<string, string[]> dictLanguage = new Dictionary<string, string[]> {
        {"txtStart", new string[] {"Start", "Come�ar"} },
        {"txtResume", new string[] {"Resume", "Continuar"} },
        {"txtQuit", new string[] {"Quit", "Sair"} },
        {"txtReset", new string[] {"Reset", "Recome�ar"} },
        {"txtOptions", new string[] {"Options", "Op��es"} },
        {"txtControls", new string[] {"Controls", "Controles" } },
        {"txtLang", new string[] {"Language", "Idioma" } },
        {"txtOST", new string[] {"Music", "M�sica" } },
        {"txtSFX", new string[] {"Sound Effects", "Efeitos Sonoros" } },
        {"txtSensitivity", new string[] {"Cam Sensitivity", "Sensibilidade da C�mera" } },
        {"txtResetSave", new string[] {"Reset Data", "Resetar Dados" } },
        {"langEnglish", new string[] {"English", "Ingl�s" } },
        {"langPortuguese", new string[] {"Portuguese", "Portugu�s" } },
        {"txtControlPause", new string[] {"Pause", "Pausar" } },
    };
}
