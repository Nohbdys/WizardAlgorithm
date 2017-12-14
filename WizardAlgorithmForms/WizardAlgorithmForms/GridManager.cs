using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WizardAlgorithmForms.Accesssible;

namespace WizardAlgorithmForms
{

    class GridManager
    {
        bool firstTimeSetup = true;

        private LinkedList<Cell> closedGrid = new LinkedList<Cell>();
        private LinkedList<Cell> openGrid = new LinkedList<Cell>();

        private Cell startCell;
        private Cell endCell;
        private int gScore, keyCount;

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
        public GridManager()
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

        public void aStar()
        {
            int tmpX = 0;
            int tmpY = 0;
            foreach (Cell cell in grid)
            {
                if (firstTimeSetup)
                {
                    if (cell.position.X == 1 && cell.position.Y == 7)
                    {
                        openGrid.AddFirst(cell);
                        firstTimeSetup = false;
                        startCell = cell;
                    }
                    if (cell.position.X == 2 && cell.position.Y == 2)
                    {
                        endCell = cell;
                    }

                }


                    if (openGrid.Contains<Cell>(cell))
                    {

                        #region ForLoop
                        for (int x = -1; x <= 1; x++)
                        {
                            for (int y = -1; y <= 1; y++)
                            {
                                //Skips if it checks itself
                                if (x == 0 & y == 0)
                                {
                                    continue;
                                }

                                // Cornors
                                if ((x != 0 && y != 0))
                                {
                                    if (x == -1 && y == -1)
                                    {

                                        tmpX = cell.position.X - 1;
                                        tmpY = cell.position.Y - 1;

                                        //   MessageBox.Show("Top left cornor");

                                    }
                                    else if (x == 1 && y == -1)
                                    {
                                        tmpX = cell.position.X + 1;
                                        tmpY = cell.position.Y - 1;
                                        //    MessageBox.Show("Top right cornor");

                                    }
                                    else if (x == 1 && y == 1)
                                    {
                                        tmpX = cell.position.X + 1;
                                        tmpY = cell.position.Y + 1;
                                        //    MessageBox.Show("Bottom right cornor");

                                    }
                                    else if (x == -1 && y == 1)
                                    {
                                        tmpX = cell.position.X - 1;
                                        tmpY = cell.position.Y + 1;
                                        //    MessageBox.Show("Bottom left cornor");

                                    }

                                }
                                else
                                {
                                    //Top and bottom
                                    if (x == 0)
                                    {
                                        if (y == -1)
                                        {
                                            tmpX = cell.position.X;
                                            tmpY = cell.position.Y - 1;
                                        }

                                        if (y == 1)
                                        {
                                            tmpX = cell.position.X;
                                            tmpY = cell.position.Y + 1;
                                        }
                                    }

                                    //Sides
                                    if (y == 0)
                                    {
                                        if (x == -1)
                                        {
                                            tmpX = cell.position.X - 1;
                                            tmpY = cell.position.Y;
                                        }

                                        if (x == 1)
                                        {
                                            tmpX = cell.position.X + 1;
                                            tmpY = cell.position.Y;
                                        }
                                    }

                                }

                                //Adds the cell in the position to openGrid list

                                AddToList(tmpX, tmpY);

                            }
                        }
                        #endregion
                        openGrid.Remove(cell);
                        closedGrid.AddLast(cell);
                    }


                //if (openGrid.Count >= 1)
                //{
                //    if (cell.position.X - 1 == openGrid.First.Value.position.X && cell.position.Y == openGrid.First.Value.position.Y)
                //    {
                //        if (cell.walk == WALKABLE)
                //        {

                //            MessageBox.Show("IM TO THE RIGHT OF THE CELL AND I CAN BE WALKED ON");
                //        }
                //        else
                //        {
                //            MessageBox.Show("IM TO THE RIGHT OF THE CELL AND IM UNWALKABLE");
                //        }

                //    }
                //}

            }
        }

        private void AddToList(int x, int y)
        {
            foreach (Cell cell in grid)
            {
                if ((cell.position.X == x && cell.position.Y == y) && !openGrid.Contains<Cell>(cell) && !closedGrid.Contains<Cell>(cell))
                {
                    openGrid.AddLast(cell);


                    //if (diagonal && cell.g != 0)
                    //{


                    //    cell.g = 14;
                    //    diagonal = false;
                    //}
                }
            }
        }

    }
}
