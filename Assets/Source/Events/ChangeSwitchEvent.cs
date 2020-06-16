﻿using System;
using Assets.Source.Game;
using Assets.Source.Model;

namespace Assets.Source.Events
{
    public class ChangeSwitchEvent : GameEvent
    {
        public string id;
        public bool value;

        public override string GetNameText()
        {
            return "Ativar/desativar interruptor";
        }

        public override string GetDescriptionText()
        {
            return id + " - {" + value + "}";
        }

        public override void Execute()
        {
            GameState.main.switches[id] = value;

            // revalidates entities
            GameMasterBehaviour.main.InstantiateEntities(GameState.main.currentGameMap);

            finishedExecution = true;
        }

        public override void Update()
        {
        }

        public override PersistenceData GetData()
        {
            var data = new PersistenceData();

            data.Set("id", id);
            data.Set("value", value);

            return data;
        }

        public override void SetData(PersistenceData data)
        {
            throw new NotImplementedException();
        }
    }
}
