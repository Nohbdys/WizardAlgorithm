using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WizardAlgorithmForms.CellType;

namespace WizardAlgorithmForms
{
    enum CellType { START, GOAL, WALL, EMPTY };
    enum Accesssible { TRUE, FALSE };

    class Cell
    {
        /// <summary>
        /// The grid position of the cell
        /// </summary>
        public Point position;

        /// <summary>
        /// Is the cell walkable or blocked
        /// </summary>
        private bool IsWall;

        /// <summary>
        /// The size of the cell
        /// </summary>
        private int cellSize;

        
        private int G;
        private int H;
        public int F;

        /// <summary>
        /// The cell's sprite
        /// </summary>
        private Image sprite;

        /// <summary>
        /// Sets the celltype to empty as default
        /// </summary>
        CellType myType = EMPTY;

        /// <summary>
        /// The bounding rectangle of the cell
        /// </summary>
        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(position.X * cellSize, position.Y * cellSize, cellSize, cellSize);
            }
        }

        /// <summary>
        /// The cell's constructor
        /// </summary>
        /// <param name="position">The cell's grid position</param>
        /// <param name="size">The cell's size</param>
        public Cell(Point position, int size)
        {
            //Sets the position
            this.position = position;

            //Sets the cell size
            this.cellSize = size;

            F = G + H;
        }

        /// <summary>
        /// Renders the cell
        /// </summary>
        /// <param name="dc">The graphic context</param>
        public void Render(Graphics dc)
        {
            //Draws the rectangles color
            dc.FillRectangle(new SolidBrush(Color.White), BoundingRectangle);

            //Draws the rectangles border
            dc.DrawRectangle(new Pen(Color.Black), BoundingRectangle);

            //If the cell has a sprite, then we need to draw it
            if (sprite != null)
            {
                dc.DrawImage(sprite, BoundingRectangle);
            }
            //Renders all graphics
            //Renders wizard
            if (position.X == 1 && position.Y == 7)
            {
                sprite = Image.FromFile(@"Images\wizardFront.png");
            }
            //Renders Wall
            if (position.X >= 4 && position.X <= 6 && position.Y >= 1 && position.Y <= 6 || position.X == 7 && position.Y >= 5 && position.Y <=6 || position.X == 3 && position.Y == 6)  
            {
                sprite = Image.FromFile(@"Images\wallSingleTile.png");
            }
            //Renders powerTower
            if (position.X == 1 && position.Y == 2)
            {
                sprite = Image.FromFile(@"Images\powerTower.png");
            }
            //Renders iceTower
            if (position.X == 8 && position.Y == 7)
            {
                sprite = Image.FromFile(@"Images\iceTower.png");
            }
            //Renders Portal
            if (position.X == 0 && position.Y == 7)
            {
                sprite = Image.FromFile(@"Images\portalA.png");
            }
            //Renders Trees
            if (position.X >=2 && position.X <=7 && position.Y == 7 || position.X >= 2 && position.X <= 7 && position.Y == 9)
            {
                sprite = Image.FromFile(@"Images\tree.png");
            }
            //Renders path
            if (position.X == 1 && position.Y >= 3 && position.Y <=6 || position.X == 2 && position.Y >= 5 && position.Y <= 6 || position.X == 3 && position.Y >= 0 && position.Y <= 5 || position.X == 7 && position.Y >= 0 && position.Y <= 4 || position.X == 8 && position.Y >= 4 && position.Y <= 6 || position.X == 9 && position.Y >= 6 && position.Y <= 8 || position.X >= 4 && position.X <= 6 && position.Y == 0 || position.X == 1 && position.Y == 8 || position.X == 8 && position.Y == 8)
            {
                sprite = Image.FromFile(@"Images\path.png");
            }
            //Renders forest path
            if (position.X >= 2 && position.X <= 7 && position.Y == 8)
            {
                sprite = Image.FromFile(@"Images\path.png");
            }
            //Renders ground
            if (position.X == 0 && position.Y >= 0 && position.Y <= 6 || position.X == 0 && position.Y >= 8 && position.Y <= 9 || position.X == 1 && position.Y >= 0 && position.Y <= 1 || position.X == 2 && position.Y >= 0 && position.Y <= 4 || position.X == 8 && position.Y >= 0 && position.Y <= 3 || position.X == 9 && position.Y >= 0 && position.Y <= 5 || position.X >= 8 && position.X <= 9 && position.Y == 9 || position.X == 1 && position.Y == 9)
            {
                sprite = Image.FromFile(@"Images\groundSingleTile.png");
            }


            //Write's the cells grid position
            dc.DrawString(string.Format("{0}", position), new Font("Arial", 7, FontStyle.Regular), new SolidBrush(Color.Black), position.X * cellSize, (position.Y * cellSize) + 10);
          }

        /// <summary>
        /// Clicks the cell
        /// </summary>
        /// <param name="clickType">The click type</param>
        public void Click(ref CellType clickType)
        {
            if (clickType == START) //If the click type is START
            {
                sprite = Image.FromFile(@"Images\Start.png");
                myType = clickType;
                clickType = GOAL;
            }
            else if (clickType == GOAL && myType != START) //If the click type is GOAL
            {
                sprite = Image.FromFile(@"Images\Goal.png");
                clickType = WALL;
                myType = GOAL;
            }
            else if (clickType == WALL && myType != START && myType != GOAL && myType != WALL) //If the click type is WALL
            {
                sprite = Image.FromFile(@"Images\Wall.png");
                myType = WALL;
            }
            else if (clickType == WALL && myType == WALL) //If the click type is WALL
            {
                sprite = null;
                myType = EMPTY;
            }


        }
    }
}


/// Til opsætning af wall
//if (x >= 4 && x <= 6 && y >= 1 && y <= 6)  
//{

//}
