using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WizardAlgorithmForms.Accesssible;

namespace WizardAlgorithmForms
{
    class TestAstarClass
    {
        private LinkedList<Object> closedGrid;
        private LinkedList<Cell> openGrid = new LinkedList<Cell>();
        private GridManager cameFrom;
        private Cell startCell;
        private Cell endCell;
        private int gScore;


        public void aStar()
        {
            cameFrom =  new GridManager();
            foreach (Cell cell in cameFrom.grid)
            {

                if (cell.position.X == 1 && cell.position.Y == 7)
                {
                    openGrid.AddFirst(cell);
                }
                if (openGrid.Count >= 1)
                {
                    if (cell.position.X - 1 == openGrid.First.Value.position.X && cell.position.Y == openGrid.First.Value.position.Y)
                    {
                        if (openGrid.First.Value.walk == WALKABLE)
                        {
                            MessageBox.Show("IM TO THE RIGHT OF THE CELL AND I CAN BE WALKED ON");
                        }
                        else
                        {
                            MessageBox.Show("IM TO THE RIGHT OF THE CELL AND IM UNWALKABLE");
                        }
                        
                    }
                }

            }
        }

        //    protected virtual Double Heuristic(PathNode inStart, PathNode inEnd)
        //    {
        //        return Math.Sqrt((inStart.X - inEnd.X) * (inStart.X - inEnd.X) + (inStart.Y - inEnd.Y) * (inStart.Y - inEnd.Y));
        //    }
        //}
    }
}
