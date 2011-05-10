using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeRPG
{
    enum soundType { HURT, SHOOT, SWORD, NUM_S_TYPES }; // types of sounds
    enum explosionType { SMALL, BIG, NUM_E_TYPES };
    enum effectType { SOUND, EXPLOSION };
    
    class Effect
    {
        public effectType type;
        public soundType s_type;
        public explosionType e_type;
        public int x, y;

        public Effect(effectType _type, soundType _s_type, explosionType _e_type, int _x, int _y)
        {
            type = _type;
            s_type = _s_type;
            e_type = _e_type;
            x = _x;
            y = _y;
        }
    }

    class EffectsEngine
    {
        GameState game_state;
        List<Effect> all_effects; // all possible effects
        List<Effect> to_draw; // which effect
        SoundEffect[] sounds;
        Sprite[] expls;

        public EffectsEngine(GameState _game_state)
        {
            game_state = _game_state;
            all_effects = new List<Effect>();
            to_draw = new List<Effect>();
            sounds = new SoundEffect[(int)soundType.NUM_S_TYPES];
            expls = new Sprite[(int)explosionType.NUM_E_TYPES];
        }

        public void LoadSound(ContentManager cont_man, string _path, soundType s_type) 
        {
            sounds[(int)s_type] = cont_man.Load<SoundEffect>(_path); // play a sound
        }

        public void LoadExplosion(ContentManager cont_man, string _path, explosionType e_type)
        {
            expls[(int)e_type] = new Sprite();
            expls[(int)e_type].Load(cont_man, _path, 16, 16, 200); // draw an explosion

        }

        public void RequestSound(soundType s_type)
        {
            all_effects.Add(new Effect(effectType.SOUND, s_type, explosionType.BIG, 0, 0));
        }

        public void RequestExplosion(explosionType e_type, int _x, int _y)
        {
            all_effects.Add(new Effect(effectType.EXPLOSION, soundType.SHOOT, explosionType.SMALL, _x, _y));
        }

        public void Update()
        {
            for (int i = 0; i < all_effects.Count(); ++i) // does a sound need to be played?
            {
                Effect fct = all_effects.ElementAt(i);
                if (fct.type == effectType.SOUND)
                {
                    sounds[(int)fct.s_type].Play();
                }
                else if (fct.type == effectType.EXPLOSION)
                {
                    expls[(int)fct.e_type].AnimateOnce();
                    to_draw.Add(fct);
                }
            }
            all_effects.Clear(); // all effects have been played
        }

        public void Draw(SpriteBatch batch, int offset_x, int offset_y) // draw explosions
        {

            for (int i = 0; i < to_draw.Count(); ++i)
            {
                Effect fct = to_draw.ElementAt(i);
                if (fct.type == effectType.EXPLOSION)
                {
                    expls[(int)fct.e_type].Draw(batch, fct.x-offset_x, fct.y-offset_y);
                    if (expls[(int)fct.e_type].IsAnimating() == false)
                    {
                        to_draw.RemoveAt(i);
                    }
                }
            }

        }
    }
}
