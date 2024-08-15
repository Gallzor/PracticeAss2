using System;
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
        Random _random = new Random();
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

            int randomDiceNumber = _random.Next(1, 6);

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
            int randomNumber = _random.Next(1, 3);

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
            int computerChoiceMatches = _random.Next(1, 200);
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
                return _random.Next(1, 3);
            }
            else if (_stackTwo <= 0 && _stackOne > 0 && _stackThree > 0)
            {
                return _random.Next(0, 2) == 0 ? 1 : 3;
            }
            else if (_stackThree <= 0 && _stackTwo > 0 && _stackOne > 0)
            {
                return _random.Next(1, 2);
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
                return _random.Next(2, 3);
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
                _stackOne = _random.Next(1, 200);
                _stackTwo = _random.Next(1, 200);
                _stackThree = _random.Next(1, 200);
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
                circleButton.Visibility = Visibility.Hidden;
                crossButton.Visibility = Visibility.Visible;
            }
            else
            {
                _whichPlayerTurn = "Player 2";
                crossButton.Visibility = Visibility.Hidden;
                circleButton.Visibility = Visibility.Visible;
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

        // numbers from 1 to 10 with loop
        private void ShowNumbersWithLoopButton_Click(object sender, RoutedEventArgs e)
        {
            string numbers = "";
            string moreNumbers = "";

            for (int number = 1; number < 20; number++)
            {
                numbers += number.ToString() + " ";
            }

            for (int moreNumber = 255; moreNumber < 275; moreNumber++)
            {
                moreNumbers += moreNumber.ToString() + " ";
            }

            showNumbersWithLoopLabel.Content = numbers;

            showMoreNumbersLabel.Content = moreNumbers;
        }

        // random numbers in loop
        private void ShowRandomNumbersInLoopButton_Click(object sender, RoutedEventArgs e)
        {
            string randomNumbers = "";
            
            for (int i = 1; i < _random.Next(1,100); i++)
            {
                randomNumbers += i + ": " + _random.Next(1,100).ToString() + ", ";
            }

            ShowRandomNumbersInLoopTextBlock.Text = randomNumbers;
        }

        // create staircase with X's
        private void DrawTrapButton_Click(object sender, RoutedEventArgs e)
        {
            //string rows = "";
            int rows = 6;
            string staircase = "";

            for(int i = 1; i < rows; i++)
            {
                for (int c = 1; c <= i; c++) 
                {
                    staircase = staircase + "X ";
                }

                staircase = staircase + "\n";
            }

            DrawTrapTextBlock.Text = staircase;

        }

        //sum of added numbers
        private void CalculateSumOfLoopButton_Click(object sender, RoutedEventArgs e)
        {
            int sum = 0;
            int n = 0;

            for (int i = 0; i < 40; i++)
            {
                sum = n * (n + 1) / 2;
                n++;
            }

            SumOfLoopLabel.Content = sum;
        }

        //Draw ZigZaw ass
        private void DrawZigZawButton_Click(object sender, RoutedEventArgs e)
        {

            int rows = 4;
            string zigZaw = "";

            for (int i = 0; i < 3; i++)
            {
               for (int r = 0; r < rows; r++)
               {
                  for(int c = 0; c <= r; c++)
                    {
                        zigZaw = zigZaw + "S ";
                    }
                  zigZaw = zigZaw + "\n";
               }
            }
            DrawZigZawTextBlock.Text = zigZaw;
        }

        //multiply table ass
        private void DrawMultiplyTableButton_Click(object sender, RoutedEventArgs e)
        {
            int  inputNumber;
            int calculatedNumber;
            string multipliedNumber = "";
            string columns = "";

            inputNumber = Convert.ToInt32(MultiplyTableTextBox.Text);

            for (int i = 1; i <= inputNumber; i++)
            {
                multipliedNumber += "\n" + i + "\t"; 
                for (int c = 1; c <= inputNumber; c++)
                {
                    calculatedNumber = i * c;
                    multipliedNumber += + calculatedNumber + "\t";
                }
               
                multipliedNumber = multipliedNumber + "\n";
            }
            for (int c = 1; c <= inputNumber; c++)
            {
                columns += "\t" + c;
            }

            string finalResult = columns + "\n" + multipliedNumber;
            MultiplyTableTextBlock.Text = finalResult; 
        }


        //Fibo ass
        private void FiboSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            string fibo = "";
            int firstNumber = 0, secondNumber = 1, nextNumber;

            fibo = fibo + firstNumber + " ";
            fibo = fibo + secondNumber + " ";

            for (int i = 2; i < 20; i++) 
            {
                nextNumber = firstNumber + secondNumber;
                fibo = fibo + nextNumber + " ";

                firstNumber = secondNumber;
                secondNumber = nextNumber;
            }

            FiboSeriesLabel.Content = fibo;

        }


        // sum of series
        private void SumOfSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            int n = 20; 
            double sum = 0.0;

            for (int i = 1; i <= n; i++)
            {
                if (i % 2 == 0)
                {
                    sum -= 1.0 / i; 
                }
                else 
                {
                    sum += 1.0 / i; 
                }
            }

            string finalResult = $"The sum of the first {n} of the series is: {sum}";

            SumOfSeriesLabel.Content = finalResult;
        }

        //upside down staircase extra practise
        private void DrawUpsideStaircaseButton_Click(object sender, RoutedEventArgs e)
        {
            string finalResult = "";
            int inputNumber;

            inputNumber = Convert.ToInt32(InputDrawUpsideStaircaseTextBox.Text);

            for (int i = inputNumber; i >= 1; i--)
            {
               
                for (int j = 1; j <= i; j++) 
                {
                    finalResult += "X ";
                }
                finalResult += "\n";
            }


            ResultDrawUpsideStaircaseTextBox.Text = finalResult;
        }
    }
}
