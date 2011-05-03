using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcadeRPG
{
    class GameState
    {
       public Player local_player;
       public MonsterEngine monster_engine;
       public TileEngine tile_engine;
       public BulletEngine bullet_engine;
       public CollisionEngine coll_engine;
       public EffectsEngine fx_engine;
       public ObjectManager obj_mang;
    }
}
