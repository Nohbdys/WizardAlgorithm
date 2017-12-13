using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardAlgorithmForms
{
    class TestAstarClass
    {
        private LinkedList<Object> closedGrid;
        private LinkedList<Object> openGrid = inStart;
        private GridManager cameFrom;
        private int gScore;


        private void aStar(inStart, inEnd)
        {
            cameFrom.
        }
        protected virtual Double Heuristic(PathNode inStart, PathNode inEnd)
        {
            return Math.Sqrt((inStart.X - inEnd.X) * (inStart.X - inEnd.X) + (inStart.Y - inEnd.Y) * (inStart.Y - inEnd.Y));
        }
    }
}
