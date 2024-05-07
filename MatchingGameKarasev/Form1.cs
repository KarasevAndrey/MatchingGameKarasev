using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGameKarasev
{
    public partial class Form1 : Form
    {
        //обьявляем 
        Random random = new Random();
        //обьявлеям список элементов 
        List<string> icons = new List<string>()
        {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
        };
        //метки для первого и второго щелчка
        Label firstClicked = null;
        Label secondClicked = null;



        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();

        }

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {//назначение элементов символа и закрашевание
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // If the clicked label is black, the player clicked
                // an icon that's already been revealed --
                // ignore the click
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // If firstClicked is null, this is the first icon
                // in the pair that the player clicked, 
                // so set firstClicked to the label that the player 
                // clicked, change its color to black, and return
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // If the player gets this far, the timer isn't
                // running and firstClicked isn't null,
                // so this must be the second icon the player clicked
                // Set its color to black
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;
                CheckForWinner();
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // If the player gets this far, the player 
                // clicked two different icons, so start the 
                // timer (which will wait three quarters of 
                // a second, and then hide the icons)
                timer1.Start();
            }
        }

            

        private void timer1_Tick(object sender, EventArgs e)
        {
            //если таймер работает останавливаем 
            timer1.Stop();

            //открыты первые метки
            firstClicked.ForeColor = firstClicked.BackColor;
            //открытую вторую метку скрываем
            secondClicked.ForeColor = secondClicked.BackColor;
            //значение метки сбрасываем 
           
            firstClicked = null;
            secondClicked = null;

            
        }
        //метод для проверки открытыли все метки
        private void CheckForWinner()
        {
            //перебираем все метки
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                //назначаем метки направление
                Label iconLabel = control as Label;
                //метка не пуста
                if (iconLabel != null)
                {//если метка совпадает цвет фона и цвет шрифта
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        //показываем карту
                        return;

                    MessageBox.Show("Вы открыли все карты");

                    Close();
                }


            }
        }
    }
}


