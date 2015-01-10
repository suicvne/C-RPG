using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGameCliCore.Core
{
    ///The class that contains and takes care of all the chests in the game
    public class ChestHandler
    {
        private List<Chest> _chestList = new List<Chest>();
        public ChestHandler()
        {
            _chestList.Clear();
        }

        public void TryLoadingAllChests()
        {
            bool err = false;
            for (int i = 0; err == false; i++)
            {
                Chest ch = new Chest();
                    ch.ID = i;
                    ch.LoadChestContents(CliProgram.GamesSaveDirectory +
                        System.IO.Path.DirectorySeparatorChar +
                        CliProgram.MainPlayer.Name +
                        System.IO.Path.DirectorySeparatorChar +
                        "player.inv");
                    if (!ch.IsNullChest)
                        _chestList.Add(ch);
                    else
                        err = true;
            }
        }

        public List<Chest> ChestsList()
        {
            return _chestList;
        }
        //writing of the chests is handled in the inventory class

        public Chest AccessChest(int ID)
        {
            foreach(var i in _chestList)
            {
                if (i.ID == ID)
                {
                    i.LoadChestContents(CliProgram.GamesSaveDirectory + 
                        System.IO.Path.DirectorySeparatorChar + 
                        CliProgram.MainPlayer.Name + 
                        System.IO.Path.DirectorySeparatorChar + 
                        "player.inv");
                    return i;
                }
            }
            return null;
        }


    }
}
