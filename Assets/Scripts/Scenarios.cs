using System.Collections.Generic;
using UnityEngine;

public class Scenarios : MonoBehaviour {
    public static ScenarioDictionary PopulateScenarios(GameObject[] scenariosListInOrder) {
        ScenarioDictionary scenarios = new ScenarioDictionary();
        scenarios.ScenariosDictionary = new List<ScenarioDictionaryItem>();
        foreach (GameObject scenario in scenariosListInOrder) {
            string objName = scenario.name;
            if (objName.Contains("_")) {   //Se o nome do cen�rio tiver o s�mbolo '_', quer dizer que � um cen�rio derivado de outro
                int lastFoundUnderscore = objName.IndexOf('_');
                int nextUnderscore = objName.Substring(lastFoundUnderscore + 1).IndexOf('_');
                int idsLimitation = objName.Substring(lastFoundUnderscore + 1).IndexOf('-');   //O s�mbolo '-' indica onde acaba os ids e come�a o nome do cen�rio

                int mainId = int.Parse(objName.Substring(0, lastFoundUnderscore));
                ScenarioDictionaryItem itemDict = scenarios.ScenariosDictionary[mainId];

                while (true) {   //Esse while vai ser quebrado quando chegarmos ao �ltimo �ndice de cen�rio
                    if (nextUnderscore == -1) {
                        int idChild = int.Parse(objName.Substring(lastFoundUnderscore + 1, idsLimitation));
                        ScenarioDictionaryItem newItemDict = new ScenarioDictionaryItem();
                        newItemDict.ScenarioObject = scenario;
                        newItemDict.PartsOfScenario = new List<ScenarioDictionaryItem>();
                        newItemDict.SceneId = idChild;
                        newItemDict.ParentScenario = itemDict;
                        itemDict.PartsOfScenario.Add(newItemDict);
                        break;
                    }
                    else {
                        int idChild = int.Parse(objName.Substring(lastFoundUnderscore + 1, nextUnderscore));
                        itemDict = itemDict.PartsOfScenario[idChild];
                        lastFoundUnderscore = nextUnderscore + lastFoundUnderscore + 1;
                        nextUnderscore = objName.Substring(lastFoundUnderscore + 1).IndexOf('_');
                        idsLimitation = objName.Substring(lastFoundUnderscore + 1).IndexOf('-');
                    }
                }
            }
            else {
                int mainId = int.Parse(objName.Substring(0));
                ScenarioDictionaryItem newItemDict = new ScenarioDictionaryItem();
                newItemDict.ScenarioObject = scenario;
                newItemDict.PartsOfScenario = new List<ScenarioDictionaryItem>();
                newItemDict.SceneId = mainId;
                newItemDict.ParentScenario = null;
                scenarios.ScenariosDictionary.Add(newItemDict);
            }
        }

        return scenarios;
    }
}

public class ScenarioDictionary {
    public List<ScenarioDictionaryItem> ScenariosDictionary;
}

public class ScenarioDictionaryItem {
    public int SceneId;
    public GameObject ScenarioObject;
    public GameObject[] Sections;
    public ScenarioDictionaryItem ParentScenario;
    public List<ScenarioDictionaryItem> PartsOfScenario;
}
