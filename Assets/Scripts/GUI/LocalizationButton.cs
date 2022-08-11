using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
public class LocalizationButton : MonoBehaviour
{
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LocalSelectDanish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
    }
    public void LocalSelectEnglish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
    }
}
