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
            if(_countPressedFaultNumbers == 6 )
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
    }
}
