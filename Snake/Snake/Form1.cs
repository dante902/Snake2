using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();


        public Form1()
        {
            InitializeComponent();

            new settings();
            

       //     StartGame();
        }

 
        private void StartGame()
        {
            
            if (radioButton1.Checked == true)
            {
                gameTimer.Interval = 1000 / settings.Speed1;
            }
            else if (radioButton2.Checked == true)
            {
                gameTimer.Interval = 1000 / settings.Speed2;
            }
            else if (radioButton3.Checked == true)
            {
                gameTimer.Interval = 1000 / settings.Speed3;
            }
            else
                gameTimer.Interval = 10000 / settings.Speed1;

            lblGameOver.Visible = false;
            new settings();
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            button1.Enabled = false;


        //    gameTimer.Interval = 100 + speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            //nowy obiekt gracza
          //  Snake.Clear();

            Circle head = new Circle();
            head.X = 10;
            head.Y = 1;
            

            Snake.Add(head);

            lblScore.Text = settings.Score.ToString();
            GenerateFood();
        }

        private void GenerateFood()
        {
            
            int max_X_position = bpCanvas.Size.Width / settings.Width;
            int max_Y_position = bpCanvas.Size.Height / settings.Height;

            bool sprawdz = false;
            Random rand = new Random();
            food = new Circle();
            food.X = rand.Next(5, 6);  
            food.Y = rand.Next(5, 8);
            
            for (int i = 0; i <= Snake.Count; i++)
            {
               if ((Snake[i].X == food.X) && (Snake[i].Y == food.Y))
                    {
                        sprawdz = true;
                    }
               if (sprawdz == true)
               {
                   lblGameOver.Visible = true;
               }
               else
                   break;
            }
            
                        
        }

        private void UpdateScreen(object sender, EventArgs e)
        {

           // sprawdzanie konca gry
            if (settings.GameOver == true)
            {
                
                if (input.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                if (input.KeyPressed(Keys.Right) && settings.direction != Direction.Left)
                    settings.direction = Direction.Right;
                else if (input.KeyPressed(Keys.Left) && settings.direction != Direction.Right)
                    settings.direction = Direction.Left;
                else if (input.KeyPressed(Keys.Up) && settings.direction != Direction.Down)
                    settings.direction = Direction.Up;
                else if (input.KeyPressed(Keys.Down) && settings.direction != Direction.Up)
                    settings.direction = Direction.Down;
               
        //        MovePlayer();
            }
         
          
            bpCanvas.Invalidate();
          //  bpCanvas.Refresh();
           

        }

        private void bpCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (settings.GameOver != true) //!= bylo
            {
                Brush snakeColour;
                
                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                        snakeColour = Brushes.Black;
                    else
                        snakeColour = Brushes.Green;
                    //rysowanie snake
                    canvas.FillEllipse(snakeColour, new Rectangle(Snake[i].X * settings.Width,
                                                                  Snake[i].Y * settings.Height,
                                                                  settings.Width,
                                                                  settings.Height));
                    
                    
                    

                    //rysowanie jedzenia

                    canvas.FillEllipse(Brushes.Red, new Rectangle(food.X * settings.Width,
                                                                  food.Y * settings.Height,
                                                                  settings.Width,
                                                                  settings.Height));
                }
                if (input.KeyPressed(Keys.D))
                {
                    gameTimer.Interval = 100;
                }
                if (input.KeyPressed(Keys.S))
                {
                    gameTimer.Interval = 1000 / settings.Speed2;
                }

                if (input.KeyPressed(Keys.F))
                {
                    GenerateFood();
                }
                if (input.KeyPressed(Keys.G))
                {
                    food.X = 10;
                    food.Y = 10;
                }


                  
                MovePlayer();
                
            }
            else
            {
                string GameOver = "Koniec Gry \nTwój wynik to: " + settings.Score + "\nWcisnij Enter \nby zagrać ponownie";
                lblGameOver.Text = GameOver;
                lblGameOver.Visible = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                button1.Enabled = true;
                Snake.Clear();
            }
        }
       /* private void GenerateNewFood()
        {
            int max_X_position = bpCanvas.Size.Width / settings.Width;
            int max_Y_position = bpCanvas.Size.Height / settings.Height;

            Random rand = new Random();
            bool jedzenie = false;
            food = new Circle();
            food.X = rand.Next(5, 6);
            food.Y = rand.Next(5, 8);
            for (int i = 1; i < Snake.Count; i++)
            {
                if ((Snake[i].X == food.X) & (Snake[i].Y == food.Y))
                {
                    jedzenie = true;
                }
            }
            if (jedzenie == true)
            {
                GenerateFood();
            }

        }*/
        private void MovePlayer()
        {
           
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (settings.direction)
                    {
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;
                    }

                    int maxXPos = bpCanvas.Size.Width / settings.Width;
                    int maxYPos = bpCanvas.Size.Height / settings.Height;
                   
                    
                    //sprawdzanie kolizji ze sciana

                    if (Snake[i].X < 0 || Snake[i].Y < 0 
                        || Snake[i].X>=maxXPos || Snake[i].Y >= maxYPos)
                    {
                        break;//Die();
                    }

                    //sprawdzanie kolizji z cialem

                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X &&
                            Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }

                    //sprawdzanie kolizji z jedzeniem
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                         Eat();
                    }
                    

                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }


        }
        private void Die()
        {
            settings.GameOver = true;
        }
        private void Eat()
        {
            Circle food = new Circle();
            food.X = Snake[Snake.Count - 1].X;
            food.Y = Snake[Snake.Count - 1].Y;

            Snake.Add(food);


            settings.Score += settings.Points;
            lblScore.Text = settings.Score.ToString();

            GenerateFood();
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            input.ChangeStates(e.KeyCode, true);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            input.ChangeStates(e.KeyCode, false);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartGame();
            
        }
        
    }
}
