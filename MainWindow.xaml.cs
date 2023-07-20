using System;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToeGame
{
    public partial class MainWindow : Window
    {
        private char currentPlayer = 'X';
        private char[,] board = new char[3, 3];

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();

            btn00.Click += OnButtonClicked;
            btn01.Click += OnButtonClicked;
            btn02.Click += OnButtonClicked;
            btn10.Click += OnButtonClicked;
            btn11.Click += OnButtonClicked;
            btn12.Click += OnButtonClicked;
            btn20.Click += OnButtonClicked;
            btn21.Click += OnButtonClicked;
            btn22.Click += OnButtonClicked;
        }

        private void InitializeGame()
        {
            currentPlayer = 'X';
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = '\0';
                    ((Button)gameGrid.FindName($"btn{i}{j}")).Content = string.Empty;
                }
            }

            lblStatus.Content = "Randul jucatorului X";
        }

        private void OnButtonClicked(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int row = Grid.GetRow(clickedButton);
            int col = Grid.GetColumn(clickedButton);

            // Verifica daca casuta este goala
            if (board[row, col] == '\0')
            {
                board[row, col] = currentPlayer;
                clickedButton.Content = currentPlayer.ToString();

                // Verifica daca exista un castigator sau este egalitate
                if (CheckForWin(currentPlayer))
                {
                    lblStatus.Content = $"Jucatorul {currentPlayer} castiga!";
                    return;
                }
                else if (CheckForDraw())
                {
                    lblStatus.Content = "Egalitate!";
                    return;
                }

                
                // Muta pe urmatorul jucator
                currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
                lblStatus.Content = $"Randul jucatorului {currentPlayer}";

                
                // Daca jucatorul curent este calculatorul, atunci el va face o miscare
                if (currentPlayer == 'O')
                {
                    MakeComputerMove();
                }
            }
        }

        private bool CheckForWin(char player)
        {
            // Check rows, columns, and diagonals for a win
            // Verifica randurile, coloanele si diagonalele pentru a vedea castigatorul
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                    return true;

                if (board[0, i] == player && board[1, i] == player && board[2, i] == player)
                    return true;
            }

            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
                return true;

            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
                return true;

            return false;
        }

        private bool CheckForDraw()
        {
           
            // Verifica daca toate casutele sunt pline
            foreach (var cell in board)
            {
                if (cell == '\0')
                    return false;
            }
            return true;
        }

        private void MakeComputerMove()
        {
            
            // Miscarile calculatorului sunt random
            Random random = new Random();
            int row;
            int col;
            do
            {
                row = random.Next(0, 3);
                col = random.Next(0, 3);
            } while (board[row, col] != '\0');

            board[row, col] = currentPlayer;
            ((Button)gameGrid.FindName($"btn{row}{col}")).Content = currentPlayer.ToString();

         
            // Verifica daca exista un castigator sau verifica egalitatea dupa ce a mutat calculatorul
            if (CheckForWin(currentPlayer))
            {
                lblStatus.Content = $"Jucatorul {currentPlayer} castiga!";
                return;
            }
            else if (CheckForDraw())
            {
                lblStatus.Content = "Este egalitate!";
                return;
            }

            currentPlayer = 'X';
            lblStatus.Content = "Randul jucatorului X";
        }
    }
}