﻿using Assets.Source.Game;
using Assets.Source.Model;
using UnityEngine;

namespace Assets.Source.Events
{
    class MoveEntityEvent : GameEvent
    {
        public string id;
        public int x;
        public int y;
        bool immediate = true;

        public override string GetNameText()
        {
            return "Mover entidade";
        }

        public override string GetDescriptionText()
        {
            return id + " - {" + x + ";" + y + "}";
        }

        public override void Execute()
        {
            var obj = GameMasterBehaviour.main.GetEntityObject(id);

            if (obj != null)
            {
                if (immediate)
                {
                    var newY = GameState.main.currentGameMap.height - y - 1;
                    obj.GetComponent<EntityBehaviour>().gameEntity.location = new Vector2(x, newY);
                    obj.GetComponent<Transform>().localPosition = new Vector3(x, newY,
                        GameMasterBehaviour.main.player.transform.localPosition.z);
                }
                else
                {
                    //TODO pathfinding
                }
            }

            finishedExecution = true;
        }

        public override void Update()
        {
        }

        public override PersistenceData GetData()
        {
            var data = new PersistenceData();

            data.Set("event", GetType().FullName);
            data.Set("id", id);
            data.Set("x", x);
            data.Set("y", y);
            data.Set("immediate", immediate);

            return data;
        }

        public override void SetData(PersistenceData data)
        {
            id = data.Get("id", id);
            x = data.Get("id", x);
            y = data.Get("id", y);
            immediate = data.Get("id", immediate);
        }
    }
}
