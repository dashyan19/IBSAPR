using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace lab1
{
    public partial class Form1 : Form
    {

        string russianAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ0123456789 .,:;!?-"; // Русский алфавит, цифры, пробел, знаки препинания
        int countrussianAlphabet = 51; // Количесвто символов в алфавите

        string text1 = "";
        string text2 = "";

        int characterCount1 = 0; // Переменная для хранения количества символов ОТКРЫТОГО ТЕКСТА

        string resultat1 = "";
        string resultat2 = "";

        StringBuilder result = new StringBuilder();

        public Form1()
        {
            InitializeComponent();
            textBox3.Enabled = false; // Заблокировать редактирование textBox3 (шифрование)
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            textBox3.Text = "";
            textBox3.Clear();

            text1 = "";

            int maxLength = 50;
            if (textBox1.Text.Length > maxLength)
            {
                textBox1.Text = textBox1.Text.Substring(0, maxLength);
                textBox1.SelectionStart = maxLength;
            }

            string inputText = textBox1.Text.ToUpper(); // Преобразуем к верхнему регистру

            // Подсчитываем количество символов
            characterCount1 = textBox1.Text.Length;

            StringBuilder resultText = new StringBuilder();

            foreach (char character in inputText)
            {
                int position = russianAlphabet.IndexOf(character);

                if (position != -1) // если символ найден в алфавите, то...
                {
                    // Номер символа по счёту (начиная с 1)
                    resultText.Append(position.ToString() + " ");
                }
                else // если символ не найден в алфавите, то...
                {
                    inputText = inputText.Substring(0, inputText.Length - 1);
                    textBox1.Text = inputText;
                    textBox1.SelectionStart = textBox1.Text.Length; // Переместить курсор в конец текста
                }
            }

            text1 = resultText.ToString().Trim();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            textBox3.Text = "";
            textBox3.Clear();

            text2 = "";

            int maxLength = 50;
            if (textBox2.Text.Length > maxLength)
            {
                textBox2.Text = textBox2.Text.Substring(0, maxLength);
                textBox2.SelectionStart = maxLength;
            }

            string inputText = textBox2.Text.ToUpper(); // Преобразуем к верхнему регистру
            string inputText0 = "";

            inputText0 = inputText;

            while (inputText0.Length < characterCount1)
            {
                inputText0 += inputText0; // Удваиваем строку
            }

            // Если строка превысила желаемую длину, обрезаем ее
            if (inputText0.Length > characterCount1)
            {
                inputText0 = inputText0.Substring(0, characterCount1);
            }

            inputText = inputText0;

            StringBuilder resultText = new StringBuilder();

            foreach (char character in inputText)
            {
                int position = russianAlphabet.IndexOf(character);

                if (position != -1) // если символ найден в алфавите, то...
                {
                    // Номер символа по счёту (начиная с 1)
                    resultText.Append(position.ToString() + " ");
                }
                else // если символ не найден в алфавите, то...
                {
                    inputText = inputText.Substring(0, inputText.Length - 1);
                    textBox2.Text = inputText;
                    textBox2.SelectionStart = textBox2.Text.Length; // Переместить курсор в конец текста
                }
            }

            text2 = resultText.ToString().Trim();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Результат: " + text1);
            //MessageBox.Show("Результат: " + text2);
            //MessageBox.Show("Количество символов: " + characterCount1.ToString());

            string[] posl1 = text1.Split(' ');
            string[] posl2 = text2.Split(' ');

            if ((!string.IsNullOrWhiteSpace(textBox1.Text)) && (!string.IsNullOrWhiteSpace(textBox2.Text)))
            {

                for (int i = 0; i < posl1.Length; i++)
                {
                    int posled1 = int.Parse(posl1[i]);
                    int posled2 = int.Parse(posl2[i]);
                    int сумма = posled1 ^ posled2;
                    int остаток = сумма % countrussianAlphabet;
                    if (остаток == сумма)
                    {
                        resultat1 += сумма.ToString() + " ";
                    }
                    else if (остаток != сумма)
                    {
                        resultat1 += остаток.ToString() + " ";
                    }
                }

                // Удаление лишнего пробела в конце строки
                resultat1 = resultat1.Trim();

                string[] numbers1 = resultat1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string numberStr in numbers1)
                {
                    if (int.TryParse(numberStr, out int number))
                    {
                        char russianChar = russianAlphabet[number];
                        resultat2 += russianChar;
                    }
                    else
                    {
                        resultat2 += "*"; // Здесь можно установить другой символ по вашему выбору для некорректных чисел
                    }
                }
                textBox3.Text = resultat2;
            }

            else if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, введите открытый текст");
            }

            else if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Пожалуйста, введите гамма-ключ");
            }

            characterCount1 = 0;
            resultat1 = "";
            resultat2 = "";
            //MessageBox.Show("Результат: " + text1);
            //MessageBox.Show("Результат: " + text2);
            //MessageBox.Show("Количество символов: " + characterCount.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int maxCharacters = 50;

            string filePath = "C:/Users/NDA/Desktop/5sem/Информационная безопасность САПР - з/lab/lab1/lab1/input.txt"; // указать путь к файлу
            string[] lines = File.ReadAllLines(filePath); // считываем все строки из файла

            if (lines.Length >= 2)
            {
                bool hasInvalidChars = false; // флаг для определения наличия невалидных символов

                // Проверяем каждую строку на наличие невалидных символов
                foreach (var line in lines)
                {
                    foreach (char c in line)
                    {
                        if (!russianAlphabet.Contains(c))
                        {
                            hasInvalidChars = true;
                            break;
                        }
                    }

                    if (hasInvalidChars)
                    {
                        break;
                    }
                }

                if (hasInvalidChars)
                {
                    MessageBox.Show("Обнаружены невалидные символы в файле.", "Ошибка");
                    textBox1.Text = string.Empty; // очищаем textBox1
                    textBox2.Text = string.Empty; // очищаем textBox2
                }
                else
                {
                    if (lines[0].Length > maxCharacters && lines[1].Length > maxCharacters)
                    {
                        MessageBox.Show("Ошибка: Исходный текст и гамма в файле превышают максимальное количество символов!");
                    }
                    else if (lines[0].Length > maxCharacters)
                    {
                        MessageBox.Show("Ошибка: Исходный текст в файле превышает максимальное количество символов!");
                    }
                    else if (lines[1].Length > maxCharacters)
                    {
                        MessageBox.Show("Ошибка: Гамма в файле превышает максимальное количество символов!");
                    }
                    else
                    {
                        // Отображаем данные в textBox1 и textBox2
                        textBox1.Text = lines[0];
                        textBox2.Text = lines[1];
                    }
                }
            }
            else
            {
                MessageBox.Show("В файле не хватает двух строк.", "Ошибка");
                textBox1.Text = string.Empty; // очищаем textBox1
                textBox2.Text = string.Empty; // очищаем textBox2
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Clear();
            textBox2.Text = "";
            textBox2.Clear();
            textBox3.Text = "";
            textBox3.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text01 = textBox1.Text;
            string text02 = textBox2.Text;

            if (string.IsNullOrEmpty(text01) || string.IsNullOrEmpty(text02))
            {
                MessageBox.Show("Пожалуйста, заполните оба текстовых поля перед сохранением.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    // Получаем текст из TextBox и добавляем пояснения
                    string textToSave = $"Открытый текст: {textBox1.Text}\r\nГамма: {textBox2.Text}\r\nРезультат шифрования: {textBox3.Text}";

                    // Задаем путь к файлу
                    string filePath = "C:/Users/NDA/Desktop/5sem/Информационная безопасность САПР - з/lab/lab1/lab1/output.txt";

                    // Записываем текст в файл
                    File.WriteAllText(filePath, textToSave);

                    MessageBox.Show("Данные успешно сохранены в файл.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string filePath = "C:/Users/NDA/Desktop/5sem/Информационная безопасность САПР - з/lab/lab1/lab1/info.txt";

            try
            {
                // Проверяем, существует ли файл по указанному пути
                if (File.Exists(filePath))
                {
                    // Открываем файл в блокноте (или другом текстовом редакторе)
                    System.Diagnostics.Process.Start("notepad.exe", filePath);
                }
                else
                {
                    MessageBox.Show("Файл не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string filePath = "C:/Users/NDA/Desktop/5sem/Информационная безопасность САПР - з/lab/lab1/lab1/output.txt";

            try
            {
                // Проверяем, существует ли файл по указанному пути
                if (File.Exists(filePath))
                {
                    // Открываем файл в блокноте (или другом текстовом редакторе)
                    System.Diagnostics.Process.Start("notepad.exe", filePath);
                }
                else
                {
                    MessageBox.Show("Файл не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string filePath = "C:/Users/NDA/Desktop/5sem/Информационная безопасность САПР - з/lab/lab1/lab1/input.txt";

            try
            {
                // Проверяем, существует ли файл по указанному пути
                if (File.Exists(filePath))
                {
                    // Открываем файл в блокноте (или другом текстовом редакторе)
                    System.Diagnostics.Process.Start("notepad.exe", filePath);
                }
                else
                {
                    MessageBox.Show("Файл не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
