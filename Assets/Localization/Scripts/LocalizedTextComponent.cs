using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizedTextComponent : MonoBehaviour
{

    [SerializeField] private string tableReference;
    [SerializeField] private string localizationKey;

    private LocalizedString localizedString;
    private Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<Text>();
        localizedString = new LocalizedString { TableReference = tableReference, TableEntryReference = localizationKey };

        LocalizationSettings.SelectedLocaleChanged += UpdateText;

        //Sets to french by default
        //var frenchLocale = LocalizationSettings.AvailableLocales.GetLocale("fr");
        //LocalizationSettings.SelectedLocale = frenchLocale;

        //UpdateText(frenchLocale);
    }

    private void OnDestroy()
    {
        LocalizationSettings.SelectedLocaleChanged -= UpdateText;
    }

    // Update is called once per frame
    void UpdateText(Locale locale)
    {
        textComponent.GetComponent<Text>();
        textComponent.text = localizedString.GetLocalizedString();
    }

    public void ChangeLanguage(string lang)
    {
        var locale = LocalizationSettings.AvailableLocales.GetLocale(lang);
        LocalizationSettings.SelectedLocale = locale;

        UpdateText(locale);
    }
}
