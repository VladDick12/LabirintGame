using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Labyrinth
{
    public partial class MainWindow : Window
    {
        private Rectangle finish;
        private Rectangle player;
        private double playerSpeed = 5.0;

        public MainWindow()
        {
            InitializeComponent();

            
            player = new Rectangle
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(player, 70); 
            Canvas.SetTop(player, 70);  
            MainCanvas.Children.Add(player); 

            
            this.KeyDown += MainWindow_KeyDown;
            finish = new Rectangle
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Green
            };
            Canvas.SetLeft(finish, 550); 
            Canvas.SetTop(finish, 450);  
            MainCanvas.Children.Add(finish); 


        }





        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            
            switch (e.Key)
            {
                case Key.Up:
                    MovePlayer(0, -playerSpeed);
                    break;
                case Key.Down:
                    MovePlayer(0, playerSpeed);
                    break;
                case Key.Left:
                    MovePlayer(-playerSpeed, 0);
                    break;
                case Key.Right:
                    MovePlayer(playerSpeed, 0);
                    break;
            }
        }

            private void MovePlayer(double x, double y)
            {
            
            double newLeft = Canvas.GetLeft(player) + x;
            double newTop = Canvas.GetTop(player) + y;

                if (newLeft < 0 || newTop < 0 || newLeft + player.Width > MainCanvas.ActualWidth || newTop + player.Height > MainCanvas.ActualHeight)
                {
                    MessageBox.Show("Ошибка: Выход за пределы игрового поля!");
                    return; 
                }    


                if (CheckCollision(newLeft, newTop))
            {
                
                Canvas.SetLeft(player, 70);
                Canvas.SetTop(player, 70);
            }
            else
            {
                
                Canvas.SetLeft(player, newLeft);
                Canvas.SetTop(player, newTop);
            }
                if (IsPlayerAtFinish(newLeft, newTop))
                {
                    MessageBox.Show("Поздравляем! Вы достигли финиша!");
                    this.Close(); 
                }
  
            }


        private bool IsPlayerAtFinish(double left, double top)
        {
            Rectangle finish = (Rectangle)this.FindName("HiddenFinish");
            return left < Canvas.GetLeft(finish) + finish.Width &&
           left + player.Width > Canvas.GetLeft(finish) &&
           top < Canvas.GetTop(finish) + finish.Height &&
           top + player.Height > Canvas.GetTop(finish);
        }

        private bool CheckCollision(double left, double top)
        {
            
            foreach (UIElement element in MainCanvas.Children)
            {
                if (element is Rectangle wall && wall.Fill == Brushes.Black)
                {
                    if (left < Canvas.GetLeft(wall) + wall.Width &&
                    left + player.Width > Canvas.GetLeft(wall) &&
                    top < Canvas.GetTop(wall) + wall.Height &&
                    top + player.Height > Canvas.GetTop(wall))
                    {
                        
                        return true;
                    }
                }
            }
            
            return false;
        }
    }

}
