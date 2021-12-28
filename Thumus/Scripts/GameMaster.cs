// CLASE ESTÁTICA PARA PASAR DATOS ENTRE ESCENAS
// NO SE ACCEDE DIRECTAMENTE A LAS VARIABLES, SE USAN PROPIEDADES (PROGRA II :)

/* EJEMPLO:

Desde cualquier clase, podemos acceder al progress (o nivel) de Abigail con: GameMaster.Progress


GameMaster.Progress = 4;

int progressAbigail = GameMaster.Progress;

Debug.Log(progressAbigail); //--> 4

*/

using System.Collections.Generic;

public static class GameMaster {


	public static Dictionary<string, int> fakeProgressDic = new Dictionary<string, int>();

	private static string previousSceneName;
	private static int progress;
	private static int nAction;
	private static bool tableIsFixed;
	public static bool End =  false;
	// El inventario se busca.

	public static string PreviousSceneName {get {return previousSceneName;} set {previousSceneName = value;}}
	public static int Progress {get {return progress;} set {progress = value;}}
	public static int NAction {get {return nAction;} set {nAction = value;}}
	public static bool TableIsFixed {get {return tableIsFixed;} set {tableIsFixed = value;}}

}
