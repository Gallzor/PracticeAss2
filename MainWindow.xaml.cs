﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticeAss2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            throwDiceButton.Content = "Throw Dice!";
            countAmountThrownLabel.Content = $"Thrown: 0";
            thrownDiceOneLabel.Content = _firstThrow;
            thrownDiceTwoLabel.Content = _secondThrow;
            thrownDiceThreeLabel.Content = _thirdThrow;

            enteredFaultNumbersLabel.Content = $"Please press a number.";
            resultCombinationFaultLabel.Content = $"You have not clicked a number.";
            _faultCombinationPressed = "";

            FillStacksWithMatches();

            _changePlayer = false;
            DecidePlayerTurn();
            ShowWhichPlayerTurn();
        }

        private void TicketPriceButton_Click(object sender, RoutedEventArgs e)
        {
            int age;
            int ticketPrice = 12;
            string totalPrice;

            age = Convert.ToInt32(inputYourAgeTextBox.Text);

            totalPrice = CalculateAgeToTicketPrice(age, ticketPrice);

            priceOfTicketLabel.Content = $"Ticket is {totalPrice}";
        }

        private string CalculateAgeToTicketPrice(int age, int ticketPrice)
        {
            if (age >= 5 && age <= 12)
            {
                return (ticketPrice / 2).ToString();
            }
            else if (age > 13 && age <= 54)
            {
                return ticketPrice.ToString();
            }
            else
            {
                return "free";
            }
        }

        private int _countThrows;
        Random _randomNumberGen = new Random();
        private int _firstThrow;
        private int _secondThrow;
        private int _thirdThrow;
        private void ThrowDiceButton_Click(object sender, RoutedEventArgs e)
        {

            int amountPriceWon;

            UpdateThrowState();

            amountPriceWon = CalculateAmountPriceWon();

            countAmountThrownLabel.Content = $"Thrown: {_countThrows}";
            thrownDiceOneLabel.Content = _firstThrow;
            thrownDiceTwoLabel.Content = _secondThrow;
            thrownDiceThreeLabel.Content = _thirdThrow;
            currentAmountWonLabel.Content = $"The amount won = {amountPriceWon} euro";

        }

        private int CalculateAmountPriceWon()
        {

            if (_firstThrow == 6 && _secondThrow == 6 && _thirdThrow == 6)
            {
                return 20;
            }
            else if (_firstThrow != 0 && _secondThrow != 0 && _thirdThrow != 0 && (_firstThrow == _secondThrow && _firstThrow == _thirdThrow))
            {
                return 10;
            }
            else if (_firstThrow != 0 && _secondThrow != 0 && _thirdThrow != 0 &&
            (_firstThrow == _secondThrow || _firstThrow == _thirdThrow || _secondThrow == _thirdThrow))
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }

        private void UpdateThrowState()
        {

            int randomDiceNumber = _randomNumberGen.Next(1, 6);

            if (_countThrows >= 3)
            {
                throwDiceButton.Content = "Restart!";
                _countThrows = 0;
                _firstThrow = 0;
                _secondThrow = 0;
                _thirdThrow = 0;
            }
            else
            {

                throwDiceButton.Content = "Throw Dice!";
                _countThrows++;


                if (_countThrows == 1)
                {
                    _firstThrow = randomDiceNumber;
                }
                else if (_countThrows == 2)
                {
                    _secondThrow = randomDiceNumber;
                }
                else if (_countThrows == 3)
                {
                    _thirdThrow = randomDiceNumber;
                }
            }
        }

        //CRACK THE FAULT ASS

        private string _clickedFaultNumberOne = "1";
        private string _clickedFaultNumberTwo = "2";
        private string _clickedFaultNumberThree = "3";
        private string _faultCombinationPressed = "";
        private int _countPressedFaultNumbers;

        private void FaultNumberOneButton_Click(object sender, RoutedEventArgs e)
        {
            _faultCombinationPressed = _faultCombinationPressed + _clickedFaultNumberOne;
            _countPressedFaultNumbers++;
            CheckFaultNumberThreshold();
        }

        private void FaultNumberTwoButton_Click(object sender, RoutedEventArgs e)
        {
            _faultCombinationPressed = _faultCombinationPressed + _clickedFaultNumberTwo;
            _countPressedFaultNumbers++;
            CheckFaultNumberThreshold();
        }

        private void FaultNumberThreeButton_Click(object sender, RoutedEventArgs e)
        {
            _faultCombinationPressed = _faultCombinationPressed + _clickedFaultNumberThree;
            _countPressedFaultNumbers++;
            CheckFaultNumberThreshold();
        }

        private void CheckFaultNumberThreshold()
        {

            enteredFaultNumbersLabel.Content = $"The numbers you have pressed are {_faultCombinationPressed}";
            if (_faultCombinationPressed == "331121")
            {
                resultCombinationFaultLabel.Content = $"You have cracked the fault!";
            }
            else
            {
                resultCombinationFaultLabel.Content = $"You have not yet cracked the combination";
            }

            ExceedMaximumTries();
        }

        private void ExceedMaximumTries()
        {
            if (_countPressedFaultNumbers == 6)
            {
                RestartFaultGame("Game Over");
                _countPressedFaultNumbers = 0;

            }
        }


        private void RestartFaultButton_Click(object sender, RoutedEventArgs e)
        {
            RestartFaultGame("You have not clicked a number");
        }

        public void RestartFaultGame(string resultFaultLabel)
        {

            enteredFaultNumbersLabel.Content = $"Please press a number.";
            resultCombinationFaultLabel.Content = $"{resultFaultLabel}.";
            _faultCombinationPressed = "";
        }

        //ROCK, PAPER, SCISSORS GAME

        Random _randomGameGen = new Random();
        private const string PAPER_CHOICE = "Paper";
        private const string ROCK_CHOICE = "Rock";
        private const string SCISSOR_CHOICE = "Scissor";
        private string playerChoice;
        private string computerChoice;

        private void PaperButton_Click(object sender, RoutedEventArgs e)
        {
            playerChoice = PAPER_CHOICE;
            GenerateComputerChoice();
            ShowChosenGameOptions();
            CompareChosenGameOptions();
        }

        private void RockButton_Click(object sender, RoutedEventArgs e)
        {
            playerChoice = ROCK_CHOICE;
            GenerateComputerChoice();
            ShowChosenGameOptions();
            CompareChosenGameOptions();
        }

        private void ScissorsButton_Click(object sender, RoutedEventArgs e)
        {
            playerChoice = SCISSOR_CHOICE;
            GenerateComputerChoice();
            ShowChosenGameOptions();
            CompareChosenGameOptions();
        }

        private void CompareChosenGameOptions()
        {
            if ((playerChoice == ROCK_CHOICE && computerChoice == SCISSOR_CHOICE) ||
               (playerChoice == SCISSOR_CHOICE && computerChoice == PAPER_CHOICE) ||
               (playerChoice == PAPER_CHOICE && computerChoice == ROCK_CHOICE))
            {
                endResultGameLabel.Content = "You have won!";
            }

            else if ((playerChoice == ROCK_CHOICE && computerChoice == ROCK_CHOICE) ||
                    (playerChoice == SCISSOR_CHOICE && computerChoice == SCISSOR_CHOICE) ||
                    (playerChoice == PAPER_CHOICE && computerChoice == PAPER_CHOICE))
            {
                endResultGameLabel.Content = "It's a tie!";
            }
            else
            {
                endResultGameLabel.Content = "You have lost!";
            }
        }

        private void ShowChosenGameOptions()
        {
            resultChosenOptionsLabel.Content = $"Player: {playerChoice}. Opponent: {computerChoice}";
        }
        private void GenerateComputerChoice()
        {
            int randomNumber = _randomGameGen.Next(1, 3);

            switch (randomNumber)
            {
                case 1:
                    computerChoice = ROCK_CHOICE;
                    break;
                case 2:
                    computerChoice = PAPER_CHOICE;
                    break;
                case 3:
                    computerChoice = SCISSOR_CHOICE;
                    break;
            }
        }

        //CALCULATOR ASS

        private string calculatorTotal = string.Empty;
        private string calculatorOperator = string.Empty;
        private string calculatorFirstInput = string.Empty;
        private string calculatorSecondInput = string.Empty;
        private void CalculatorNumberButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = (Button)sender;
            calculatorTotal += clickedButton.Content;
            UpdateCalculatorTotal(calculatorTotal);
        }

        private void CalculatorOperatorButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = (Button)sender;
            string clickedOperator = clickedButton.Content.ToString();

            if (clickedOperator == "+" || clickedOperator == "-")
            {
                calculatorOperator = clickedOperator;
                calculatorFirstInput = calculatorTotal;
                ClearCalculatorTotal();
            }
            else if (clickedOperator == "=")
            {
                calculatorSecondInput = calculatorTotal;
                PerformCalculation();
            }
        }

        private void PerformCalculation()
        {
            double firstNumber = Convert.ToDouble(calculatorFirstInput);
            double secondNumber = Convert.ToDouble(calculatorSecondInput);
            double result = 0;

            switch (calculatorOperator)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
            }

            calculatorTotal = result.ToString();
            UpdateCalculatorTotal(calculatorTotal);
            ClearCalculatorInputs();
        }

        private void CalculatorClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearCalculatorTotal();
        }

        private void UpdateCalculatorTotal(string calculatorTotal)
        {
            calculatorTotalTextBlock.Text = calculatorTotal;
        }

        private void ClearCalculatorTotal()
        {
            calculatorTotal = string.Empty;
            UpdateCalculatorTotal(string.Empty);
        }
        private void ClearCalculatorInputs()
        {
            calculatorFirstInput = string.Empty;
            calculatorSecondInput = string.Empty;
            calculatorOperator = string.Empty;
        }

        // NIM GAME (clear matches from 3 stacks) ASS

        private int _stackOne = 0;
        private int _stackTwo = 0;
        private int _stackThree = 0;
        Random _randomGenMatches = new Random();
        private bool _playerTurn;
        private bool _isGameOver;

        private void PlayMatchesGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isGameOver)
            {
                GenerateComputerTurn();

                DecideGameResultByTurn();

                if (!_playerTurn && !_isGameOver)
                {
                    GeneratePlayerTurn();
                    DecideGameResultByTurn();

                }
                if (_isGameOver)
                {
                    _isGameOver = false;
                }
            }

        }

        private void GenerateComputerTurn()
        {

            int computerChoiceStack = SelectComputerChoiceStack();
            int computerChoiceMatches = _randomGenMatches.Next(1, 200);
            _playerTurn = false;

            SubstractMatchesFromStack(computerChoiceStack, computerChoiceMatches);
        }

        private void SubstractMatchesFromStack(int choiceStack, int choiceMatches)
        {
            switch (choiceStack)
            {
                case 0:
                    resultMatchesGameLabel.Content = $"All stacks are empty";
                    break;
                case 1:
                    _stackOne -= choiceMatches;
                    break;
                case 2:
                    _stackTwo -= choiceMatches;
                    break;
                case 3:
                    _stackThree -= choiceMatches;
                    break;
                default:
                    resultMatchesGameLabel.Content = $"there are only 3 stacks!!";
                    break;

            }
        }

        private int SelectComputerChoiceStack()
        {

            if (_stackThree > 0 && _stackTwo > 0 && _stackOne > 0)
            {
                return _randomGenMatches.Next(1, 3);
            }
            else if (_stackTwo <= 0 && _stackOne > 0 && _stackThree > 0)
            {
                return _randomGenMatches.Next(0, 2) == 0 ? 1 : 3;
            }
            else if (_stackThree <= 0 && _stackTwo > 0 && _stackOne > 0)
            {
                return _randomGenMatches.Next(1, 2);
            }
            else if (_stackThree > 0 && _stackTwo <= 0 && _stackOne <= 0)
            {
                return 3;
            }
            else if (_stackTwo > 0 && _stackThree <= 0 && _stackOne <= 0)
            {
                return 2;
            }
            else if (_stackOne > 0 && _stackThree <= 0 && _stackTwo <= 0)
            {
                return 1;
            }
            else if (_stackOne <= 0 && _stackTwo > 0 && _stackThree > 0)
            {
                return _randomGenMatches.Next(2, 3);
            }
            return 0;
        }

        private void GeneratePlayerTurn()
        {
            int playerChoiceMatches;
            int playerChoiceStack;

            playerChoiceStack = Convert.ToInt32(playerChoiceStackTextBox.Text);
            playerChoiceMatches = Convert.ToInt32(playerChoiceMatchesTextBox.Text);
            _playerTurn = true;

            SubstractMatchesFromStack(playerChoiceStack, playerChoiceMatches);
        }

        private void DecideGameResultByTurn()
        {
            if (_stackOne <= 0 && _stackTwo <= 0 && _stackThree <= 0)
            {
                if (!_playerTurn)
                {
                    resultMatchesGameLabel.Content = $"You have won!!";
                    _isGameOver = true;
                    EmptyStacksToDefault();
                    FillStacksWithMatches();
                }
                else
                {
                    resultMatchesGameLabel.Content = $"You have lost!!";
                    _isGameOver = true;
                    EmptyStacksToDefault();
                    FillStacksWithMatches();
                }
            }
            else
            {

                resultMatchesGameLabel.Content = $"No empty stack yet";
            }
        }

        private void FillStacksWithMatches()
        {
            if (_stackOne == 0 && _stackTwo == 0 && _stackThree == 0)
            {
                _stackOne = _randomGenMatches.Next(1, 200);
                _stackTwo = _randomGenMatches.Next(1, 200);
                _stackThree = _randomGenMatches.Next(1, 200);
            }
        }

        private void EmptyStacksToDefault()
        {
            _stackOne = 0;
            _stackTwo = 0;
            _stackThree = 0;
        }

        //Pseudo-random ass
        // My way:
        //Random _randomGenOne = new Random();
        //Random _randomGenTwo = new Random(); 
        //Book solution:
        Random _randomGenOne = new Random(DateTime.Now.Millisecond);
        Random _randomGenTwo = new Random(DateTime.Now.Millisecond + 1);


        private void RandomNumberGeneratorOneButton_Click(object sender, RoutedEventArgs e)
        {
            int generatorNumberOne = _randomGenOne.Next(1, 5000);
            randomNumberGeneratorOneLabel.Content = $"Gen 1 number: {generatorNumberOne}";
        }

        private void RandomNumberGeneratorTwoButton_Click(object sender, RoutedEventArgs e)
        {
            int generatorNumberTwo = _randomGenTwo.Next(1, 5000);
            randomNumberGeneratorTwoLabel.Content = $"Gen 2 number: {generatorNumberTwo}";
        }

        //Form ass
        private void CalculateNettoPriceButton_Click(object sender, RoutedEventArgs e)
        {
            double btwAmount;
            double totalPrice;
            int netPrice = Convert.ToInt32(nettoPriceTextBox.Text);

            if (lowRateCheckBox.IsChecked == false)
            {
                btwAmount = 21;
            }
            else
            {
                btwAmount = 6;
            }

            btwAmount = (netPrice / 100) * btwAmount;
            totalPrice = netPrice + btwAmount;

            btwPriceTextBox.Text = btwAmount.ToString("F2"); ;
            totalPriceTextBox.Text = totalPrice.ToString("F2"); ;

        }

        //Tic Tac Toe Game 
        // _changePlayer = true, is player 1, _changePlayer = false, is player 2

        private string _gameButtonName;
        private string _storedGameOption;
        private bool _changePlayer;
        private string _whichPlayerTurn;
        private bool _isTicTacToeDone = false;
        private void StoreTicTacToeOptionButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedGameButton = (Button)sender;
            _gameButtonName = clickedGameButton.Name;
        }

        private void GameCrossOrCircleButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedGameButton = (Button)sender;
            _storedGameOption += clickedGameButton.Content.ToString();
            Button targetButton = (Button)this.FindName(_gameButtonName);

            ProcessGameMove(targetButton);

        }

        private void ProcessGameMove(Button targetButton)
        {
            if (targetButton != null)
            {
                targetButton.Content = _storedGameOption;
                _storedGameOption = null;

                CalculateGameStateByTurn();

                if (!_isTicTacToeDone)
                {
                    DecidePlayerTurn();
                    ShowWhichPlayerTurn();
                }
            }
            else
            {
                return;
            }
        }

        public void DecidePlayerTurn()
        {
            _changePlayer = !_changePlayer;
            if (_changePlayer)
            {
                _whichPlayerTurn = "Player 1";
            }
            else
            {
                _whichPlayerTurn = "Player 2";
            }
        }

        public void ShowWhichPlayerTurn()
        {
            statusTicTacToeGameLabel.Content = $"It is {_whichPlayerTurn}'s turn!";
        }

        public void CalculateGameStateByTurn()
        {
            if (
         (ticTacToeOneButton.Content != null && ticTacToeOneButton.Content.ToString() != "" &&  ticTacToeOneButton.Content == ticTacToeTwoButton.Content && ticTacToeOneButton.Content == ticTacToeThreeButton.Content) ||
         (ticTacToeOneButton.Content != null && ticTacToeOneButton.Content.ToString() != "" && ticTacToeOneButton.Content == ticTacToeFourButton.Content && ticTacToeOneButton.Content == ticTacToeSevenButton.Content) ||
         (ticTacToeOneButton.Content != null && ticTacToeOneButton.Content.ToString() != "" && ticTacToeOneButton.Content == ticTacToeFiveButton.Content && ticTacToeOneButton.Content == ticTacToeNineButton.Content) ||
         (ticTacToeTwoButton.Content != null && ticTacToeTwoButton.Content.ToString() != "" && ticTacToeTwoButton.Content == ticTacToeFiveButton.Content && ticTacToeTwoButton.Content == ticTacToeEightButton.Content) ||
         (ticTacToeFourButton.Content != null && ticTacToeFourButton.Content.ToString() != "" & ticTacToeFourButton.Content == ticTacToeFiveButton.Content && ticTacToeFourButton.Content == ticTacToeSixButton.Content) ||
         (ticTacToeThreeButton.Content != null && ticTacToeThreeButton.Content.ToString() != "" && ticTacToeThreeButton.Content == ticTacToeSixButton.Content && ticTacToeThreeButton.Content == ticTacToeNineButton.Content) ||
         (ticTacToeSevenButton.Content != null && ticTacToeSevenButton.Content.ToString() != "" && ticTacToeSevenButton.Content == ticTacToeEightButton.Content && ticTacToeSevenButton.Content == ticTacToeNineButton.Content)||
         (ticTacToeThreeButton.Content != null && ticTacToeThreeButton.Content.ToString() != "" && ticTacToeThreeButton.Content == ticTacToeFiveButton.Content && ticTacToeThreeButton.Content == ticTacToeSevenButton.Content)
               )
            {
                statusTicTacToeGameLabel.Content = $"{_whichPlayerTurn} has won!";
                _isTicTacToeDone = !_isTicTacToeDone;
                restartTicTacToeButton.Visibility = Visibility.Visible;
            }
            else
            {
                return;
            }
        }

        private void HandleGameOverState()
        {
            _gameButtonName = null;
            _changePlayer = true;
            _storedGameOption = null;
            _isTicTacToeDone = !_isTicTacToeDone;
            statusTicTacToeGameLabel.Content = null;
            _whichPlayerTurn = null;
            ticTacToeOneButton.Content = null;
            ticTacToeTwoButton.Content = null;
            ticTacToeThreeButton.Content = null;
            ticTacToeFourButton.Content = null;
            ticTacToeFiveButton.Content = null;
            ticTacToeSixButton.Content = null;
            ticTacToeSevenButton.Content = null;
            ticTacToeEightButton.Content = null;
            ticTacToeNineButton.Content = null;
        }

        private void RestartTicTacToeButton_Click(object sender, RoutedEventArgs e)
        {
            HandleGameOverState();
            DecidePlayerTurn();
            ShowWhichPlayerTurn();
            restartTicTacToeButton.Visibility = Visibility.Hidden;
        }
    }
}
