﻿using System.Collections.Generic;
using UnityEngine;

namespace WankulCrazyPlugin
{
    public class WankulCardsData : MonoBehaviour
    {
        // Instance unique du singleton
        private static WankulCardsData _instance;
        private static readonly object _lock = new object();

        // Propriété pour accéder à l'instance unique
        public static WankulCardsData Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        // Chercher une instance existante dans la scène
                        _instance = FindObjectOfType<WankulCardsData>();

                        // Si aucune instance n'est trouvée, en créer une nouvelle
                        if (_instance == null)
                        {
                            GameObject singletonObject = new GameObject();
                            _instance = singletonObject.AddComponent<WankulCardsData>();
                            singletonObject.name = typeof(WankulCardsData).ToString() + " (Singleton)";
                            DontDestroyOnLoad(singletonObject);
                            Debug.Log("CardsData singleton instance created");
                        }
                        else
                        {
                            Debug.Log("CardsData singleton instance found in scene");
                        }
                    }
                    else
                    {
                        Debug.Log("CardsData singleton instance already exists");
                    }
                    return _instance;
                }
            }
        }

        // Constructeur privé pour empêcher la création d'instances supplémentaires
        private WankulCardsData() { }

        // Liste des cartes
        public List<WankulCardData> cards = new List<WankulCardData>();

        public Dictionary<EMonsterType, WankulCardData> association = [];

        public WankulCardData GetFromMonster(EMonsterType type)
        {
            return association[type];
        }

        public void SetFromMonster(EMonsterType type, WankulCardData card)
        {
            association[type] = card;
        }

        // Méthode appelée à la destruction de l'objet
        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
                Debug.Log("CardsData singleton instance destroyed");
            }
        }
    }
}
