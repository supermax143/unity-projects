using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{

    class BattleModel
    {
        private PlayerModel player;
        private PlayerModel enemy;
        private static BattleModel instance;
        private bool inited = false;
        internal PlayerModel Player
        {
            get { return player; }
        }
        internal PlayerModel Enemy
        {
            get { return enemy; }
            set { enemy = value; }
        }
        public static BattleModel Instance
        {
            get {
                if (instance == null)
                    instance = new BattleModel();
                if (instance.inited)
                    instance.init();
                return instance; 
            }
        }

        public BattleModel() { }

        public void init() {
            player = new PlayerModel();
            enemy = new PlayerModel();
            inited = true;
        }

    }
}
