﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private Button[,] fieldButtons;
        private GameModel model;

        private Dictionary<GameModel.Side, string> symbols = new Dictionary<GameModel.Side, string>();

        public Form1()
        {
            InitializeComponent();

            Button start = new Button();
            start.Text = "Новая игра";
            start.Size = new Size(80, 50);
            start.Top = 20;
            start.Left = 200;
            start.Click += start_Click;
            this.Controls.Add(start);

            model = new GameModel();

            symbols[GameModel.Side.x] = "x";
            symbols[GameModel.Side.o] = "o";
            symbols[GameModel.Side.none] = "";

            model.UpdateView += model_UpdateView;

            fieldButtons = new Button[3,3];
            for (int i = 0; i < fieldButtons.GetLength(0); i++)
            {
                for (int j = 0; j < fieldButtons.GetLength(1); j++)
                {
                    Button b = new Button();
                    b.Size = new Size(50,50);
                    b.Tag = new Point(i,j);
                    b.Top = j * 55 + 20;
                    b.Left = i * 55 + 20;
                    b.Click += b_Click;
                    this.Controls.Add(b);
                    fieldButtons[i, j] = b;
                }
            }

        }

        void start_Click(object sender, EventArgs e)
        {
            GameModel NewGame = new GameModel();
            model = NewGame;
            model_UpdateView(model);
            model.UpdateView += model_UpdateView;
        }


        void model_UpdateView(GameModel model)
        {
            for (int i = 0; i < fieldButtons.GetLength(0); i++)
            {
                for (int j = 0; j < fieldButtons.GetLength(1); j++)
                {
                    fieldButtons[i, j].Text = symbols[model.Field[i, j]];
                }
            }
            toolStripStatusLabel1.Text = "Ходят " + symbols[model.CurrentTurn];
        }

        void b_Click(object sender, EventArgs e)
        {
            Button b = (Button) sender;
            Point p = (Point) b.Tag;
            model.MakeMove(p.X,p.Y,model.CurrentTurn);
        }
    }
}
