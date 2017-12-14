﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizardAlgorithmForms
{
    public partial class Form1 : Form
    {

        private GridManager visualManager;
        private TestAstarClass aStarAlgoritm = new TestAstarClass();

        public Form1()
        {
            InitializeComponent();

            //Sets the client size
            ClientSize = new Size(800, 800);

            //Instantiates the visual manager
            visualManager = new GridManager(CreateGraphics(), this.DisplayRectangle);



        }

        private void Loop_Tick(object sender, EventArgs e)
        {
            //Draws all our cells
            visualManager.Render();

                visualManager.aStar();
            
            


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_MouseDown_1(object sender, MouseEventArgs e)
        {
        
        }
    }
}
