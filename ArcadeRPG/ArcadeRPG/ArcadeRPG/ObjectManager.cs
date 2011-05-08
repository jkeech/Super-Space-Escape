using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcadeRPG
{
    enum objectType { SCENERY, ITEM, SPAWN };

    class ObjectManager
    {

        TileLayer objects_layer;
        GameState game_state;

        objectType[] obj_hash;
        enemyType[] enem_hash;
        itemType[] item_hash;

        static int NUM_OBJECTS = 104;
        static int PLAYER_SPAWN = 80;

        public ObjectManager(GameState _game_state)
        {
            game_state = _game_state;
            obj_hash = new objectType[NUM_OBJECTS];
            item_hash = new itemType[NUM_OBJECTS];

            for (int i = 0; i < NUM_OBJECTS; ++i)
            {
                if ((i >= 63 && i <= 67) || (i >= 81 && i <= 84)) //Object ids on the sheet
                {
                    obj_hash[i] = objectType.ITEM;
                }
                else if (i >= 70 && i <= 80)
                {
                    obj_hash[i] = objectType.SPAWN;
                }
                else
                {
                    obj_hash[i] = objectType.SCENERY;
                }

                item_hash[i] = itemType.NONE;
            }



            item_hash[65] = itemType.SWORD;

            item_hash[63] = itemType.LASER;

            item_hash[66] = itemType.ATT_BOOST;

            item_hash[67] = itemType.DEF_BOOST;

            item_hash[83] = itemType.KEY;



        }

        public void load(TileLayer _objects_layer)
        {
            objects_layer = _objects_layer;
            for (int w = 0; w < objects_layer.getWidth(); ++w)
            {
                for (int h = 0; h < objects_layer.getHeight(); ++h)
                {
                    Tile t = objects_layer.getTile(w, h);
                    if (t.getTexture() == -1)
                    {
                        continue;
                    }
                    if (obj_hash[t.getTexture()] == objectType.SPAWN)
                    {
                        if (t.getTexture() == PLAYER_SPAWN)
                        {
                            game_state.local_player.setX((w * TileEngine.TILE_SIZE)+(TileEngine.TILE_SIZE/2) - game_state.local_player.getWidth()/2);///2+game_state.local_player.getWidth());
                            game_state.local_player.setY((h * TileEngine.TILE_SIZE) + (TileEngine.TILE_SIZE / 2) - game_state.local_player.getHeight());///2+game_state.local_player.getHeight());
                        }
                        else
                        {

                            int width = 0;
                            int height = 0;
                            enemyType type = enemyType.BEETLE;

                            switch (t.getTexture())
                            {
                                case 78: //Beetle
                                    width = 30;
                                    height = 30;
                                    type = enemyType.BEETLE;
                                    break;
                                case 72: //grunt
                                    width = 32;
                                    height = 36;
                                    type = enemyType.GRUNT;
                                    break;
                                case 73: //Berserker
                                    width = 32;
                                    height = 36;
                                    type = enemyType.BERSERKER;
                                    break;
                                default:
                                    continue;

                            }
                    
                            Enemy monster = new Enemy((w * TileEngine.TILE_SIZE)+16-(width/2), (h * TileEngine.TILE_SIZE)+16-(height/2), width, height, type);
                            game_state.monster_engine.AddMonster(monster);
                        }

                        t.setTexture(-1);
                    }
                }
            }
        }
        public void Clear() //needs to be implemented to prevent leak only clears monsters atm, not collision
        {
            game_state.monster_engine.Clear();
        }

        //Item lookup - coords
        public Item getItemAt(int x, int y, int width, int height)
        {


            int tx = (x) / game_state.tile_engine.getTileSize();
            int ty = (y + height-10) / game_state.tile_engine.getTileSize();

            Tile t = objects_layer.getTile(tx, ty);
            int type = t.getTexture();

            if (type == -1)
            {
                //Bottom Right
                tx = (x + width) / game_state.tile_engine.getTileSize();
                ty = (y + height-10) / game_state.tile_engine.getTileSize();


                t = objects_layer.getTile(tx, ty);
                type = t.getTexture();
                if (type == -1)
                {
                    return null;
                }
            }
            if (obj_hash[type] == objectType.ITEM)
            {
                itemType item_type = item_hash[type];
                Item new_item;
                switch (item_type)
                {
                    case itemType.LASER:
                        new_item = new Item(itemType.LASER, 3, 0, 0, 0, type);
                        break;
                    case itemType.SWORD:
                        new_item = new Item(itemType.SWORD, 3, 0, 0, 0, type);
                        break;
                    case itemType.ATT_BOOST:
                        new_item = new Item(itemType.ATT_BOOST, 5, 0, 0, 15, type);
                        break;
                    case itemType.DEF_BOOST:
                        new_item = new Item(itemType.DEF_BOOST, 0, 0, 5, 15, type);
                        break;
                    case itemType.KEY:
                        new_item = new Item(itemType.KEY, 0, 0, 0, 0, type);
                        break;
                    default:
                        new_item = null;
                        break;
                }
                t.setTexture(-1);
                return new_item;

            }

            return null;
        }
        public bool checkForGateAt(int x, int y, int width, int height)
        {

            int tx = (x) / game_state.tile_engine.getTileSize();
            int ty = (y + height - 10) / game_state.tile_engine.getTileSize();

            Tile t = objects_layer.getTile(tx, ty);
            int type = t.getTexture();

            if (type == -1)
            {
                //Bottom Right
                tx = (x + width) / game_state.tile_engine.getTileSize();
                ty = (y + height - 10) / game_state.tile_engine.getTileSize();


                t = objects_layer.getTile(tx, ty);
                type = t.getTexture();
                if (type == -1)
                {
                    return false;
                }
            }
            if ((type == /*gateID*/) || (type == /*gateID*/))
            {
                return true;
            }
            return false;
        }//checkForGateAt
    }
}
