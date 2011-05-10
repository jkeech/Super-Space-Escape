using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcadeRPG
{
    enum WhichList { NONE, OPEN, CLOSED };

    class Node : IComparable {
        public int parent_x;
        public int parent_y;

        public int loc_x;
        public int loc_y;

        public int G;
        public int H;

        public Node(int x, int y, int _parent_x, int _parent_y) //establish a node
        {
            loc_x = x;
            loc_y = y;

            parent_x = _parent_x;
            parent_y = _parent_y;
            G = 0;
            H = 0;
        }

        public Node(int x, int y, int _parent_x, int _parent_y, int _G, int _H) // different parameters
        {
            loc_x = x;
            loc_y = y;

            parent_x = _parent_x;
            parent_y = _parent_y;
            G = _G;
            H = _H;
        }

        public int CompareTo(object a) // are nodes the same?
        {
            Node other = (Node)a;
            int loc_rating = G + H;
            int other_rating = other.G + other.H;
            return loc_rating.CompareTo(other_rating);
        }
    };

    class PathFind // class to be able to tell enemy AI where to go
    {
        WhichList[,] which_list;

        List<Node> open_list = new List<Node>();
        Node[,] closed_list;

        GameState game_state;
        TileLayer back_layer;

        public PathFind(GameState _game_state) {
            game_state = _game_state;

            back_layer = game_state.tile_engine.getCurrentMap().getLayer(LayerType.BACKGROUND);

            int tile_dim_x = game_state.tile_engine.getCurrentMap().getWidth();
            int tile_dim_y = game_state.tile_engine.getCurrentMap().getHeight();

            which_list = new WhichList[tile_dim_x, tile_dim_y];
            closed_list = new Node[tile_dim_x, tile_dim_y];

            for (int i = 0; i < tile_dim_x; ++i)
            {
                for (int j = 0; j < tile_dim_y; ++j)
                {
                    WhichList item = which_list[i, j];
                    item = WhichList.NONE;
                }
            }

        }


        public List<Node> FindPath(int cur_x, int cur_y, int target_x, int target_y) // this is where we literally tell the enemies where to go
        {
            //A* Pathfinding algorithm
            Node start = new Node(cur_x, cur_y, -1, -1);
            open_list.Add(start);

            Node last = null;
            List<Node> ret_val = new List<Node>();

            while (open_list.Count() > 0)
            {
                Node x = open_list.Min(nodey => nodey);
                open_list.Remove(x);
                which_list[x.loc_x, x.loc_y] = WhichList.CLOSED;
                closed_list[x.loc_x, x.loc_y] = x;

                List<Node> neighbors = GetNeighbors(x);
                for (int i = 0; i < neighbors.Count(); ++i)
                {
                    Node temp = neighbors.ElementAt(i);
                    temp.G = x.G + 10;

                    int t_H_x = Math.Abs(target_x - temp.loc_x) * 10;
                    int t_H_y = Math.Abs(target_y - temp.loc_y) * 10;
                    temp.H += t_H_x + t_H_y;

                    if (temp.loc_x == target_x && temp.loc_y == target_y)
                    {
                        last = temp;
                        break;
                    }

                    WhichList TheList = which_list[temp.loc_x, temp.loc_y];

                    if (TheList == WhichList.NONE)
                    {
                        which_list[temp.loc_x, temp.loc_y] = WhichList.OPEN;
                        open_list.Add(temp);
                    }
                    else if (TheList == WhichList.OPEN)
                    {
                        Node old_node = SearchOpenList(temp);
                        if (old_node.G > temp.G)
                        {
                            old_node.G = temp.G;
                            old_node.parent_x = temp.parent_x;
                            old_node.parent_y = temp.parent_y;
                        }
                    }
                }
            }

            if (last == null)
            {
                return ret_val;
            }
            else
            {
                int next_x = last.parent_x;
                int next_y = last.parent_y;
                ret_val.Add(last);
                while (next_x != -1 && next_y != -1)
                {
                    Node t = closed_list[next_x, next_y];
                    ret_val.Add(t);
                    next_x = t.parent_x;
                    next_y = t.parent_y;
                }

            }
            ret_val.Reverse();
            return ret_val;
        }

        List<Node> GetNeighbors(Node node) // what is around the enemy?
        {
            Node top = new Node(node.loc_x, node.loc_y - 1, node.loc_x, node.loc_y);
            Node bottom = new Node(node.loc_x, node.loc_y + 1, node.loc_x, node.loc_y);
            Node left = new Node(node.loc_x - 1, node.loc_y, node.loc_x, node.loc_y);
            Node right = new Node(node.loc_x + 1, node.loc_y, node.loc_x, node.loc_y);

            List<Node> ret_val = new List<Node>();

            Tile t_top = back_layer.getTile(top.loc_x, top.loc_y);
            if (!t_top.hasCollision())
            {
                ret_val.Add(top);
            }

            Tile t_bottom = back_layer.getTile(bottom.loc_x, bottom.loc_y);
            if (!t_bottom.hasCollision())
            {
                ret_val.Add(bottom);
            }

            Tile t_left = back_layer.getTile(left.loc_x, left.loc_y);
            if (!t_left.hasCollision())
            {
                ret_val.Add(left);
            }

            Tile t_right = back_layer.getTile(right.loc_x, right.loc_y);
            if (!t_right.hasCollision())
            {
                ret_val.Add(right);
            }

            return ret_val;
        }

        Node SearchOpenList(Node to_find)
        {
            for (int i = 0; i < open_list.Count(); ++i)
            {
                Node temp = open_list.ElementAt(i);
                if ((temp.loc_x == to_find.loc_x) && (temp.loc_y == to_find.loc_y))
                {
                    return temp;
                }
            }
            return null;
        }
    }
}
