using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WizardAlgorithmForms.Accesssible;

namespace WizardAlgorithmForms
{

    class GridManager
    {
        //Handeling of graphics
        private BufferedGraphics backBuffer;
        private Graphics dc;
        private Rectangle displayRectangle;

        /// <summary>
        /// Amount of rows in the grid
        /// </summary>
        private int cellRowCount;

        /// <summary>
        /// This list contains all cells
        /// </summary>
        public List<Cell> grid;
        
        public int keyCount = 0;

        public GridManager(Graphics dc, Rectangle displayRectangle)
        {
            //Create's (Allocates) a buffer in memory with the size of the display
            this.backBuffer = BufferedGraphicsManager.Current.Allocate(dc, displayRectangle);

            //Sets the graphics context to the graphics in the buffer
            this.dc = backBuffer.Graphics;

            //Sets the displayRectangle
            this.displayRectangle = displayRectangle;

            //Sets the row count to then, this will create a 10 by 10 grid.
            cellRowCount = 10;

            CreateGrid();
        }

        /// <summary>
        /// Renders all the cells
        /// </summary>
        public void Render()
        {
            dc.Clear(Color.White);

            foreach (Cell cell in grid)
            {
                cell.Render(dc);
            }

            //Renders the content of the buffered graphics context to the real context(Swap buffers)
            backBuffer.Render();
            KeySpawn();
        }

        /// <summary>
        /// Creates the grid
        /// </summary>
        public void CreateGrid()
        {
            //Instantiates the list of cells
            grid = new List<Cell>();

            //Sets the cell size
            int cellSize = displayRectangle.Width / cellRowCount;

            //Creates all the cells
            for (int x = 0; x < cellRowCount; x++)
            {
                for (int y = 0; y < cellRowCount; y++)
                {
                    grid.Add(new Cell(new Point(x, y), cellSize));
                }
            }

        }

        //Spawns key
        public void KeySpawn()
        {
            if (keyCount <= 1)  //Maximum of two keys can spawn - 0,1
            {
                foreach (Cell cell in grid)
                {
                    if (cell.isGround == true)
                    {
                        Random rnd = new Random();      //Creates new random

                        int x = rnd.Next(0, 9);         //Randomises x
                        int y = rnd.Next(0, 9);         //Randomises y

                        if (cell.position.X == x && cell.position.Y == y)       //Sets cell position to randomised x and y
                        {
                            if (cell.walk == WALKABLE)
                            {
                                cell.sprite = Image.FromFile(@"Images\key.png");
                                cell.hasKey = true;
                                keyCount++;

                            }
                            else
                            {
                                //Run method again, if randomised cell is unwalkable - x & y
                                KeySpawn();
                            }
                        }
                    }
                }
            }
        }

    }
}
