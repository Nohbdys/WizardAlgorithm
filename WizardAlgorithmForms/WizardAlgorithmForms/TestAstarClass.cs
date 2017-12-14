/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizardAlgorithmForms
{
    class TestAstarClass
    {
        private LinkedList<Object> closedGrid;
        private LinkedList<Cell> openGrid;
        private GridManager cameFrom;
        private int gScore;


        private void aStar()
        {

            foreach (Cell cell in cameFrom.grid)
            {
                if (cell.position.X == 1 && cell.position.Y == 7)
                {
                    openGrid.AddFirst(cell);
                }
                if (cell.position.X - 1 == openGrid.First.Value.position.X)
                {
                    MessageBox.Show("Some text Some title");
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
*/