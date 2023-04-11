using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
using System.Windows.Threading;
using System.Xml;

namespace GruntTestRosNeft
{
    /// <summary>
    /// Класс который хранит в себе поля названия, значения и времени проведения расчета
    /// </summary>
    public class Item
    {
        public string Name { get; set; }
        public float Value { get; set; }
        public string Data { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Решил сделать так чтобы каждый раз при открытий программы создавался лист Item
        /// После создания нового расчета добавляется в лист новый класс с указанными значениями
        /// Используется для дальнейшего сохранения в json
        /// </summary>
        public List<Item> items = new List<Item>();

        
        private DispatcherTimer Timer;
        public MainWindow()
        {
            InitializeComponent();
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0,0,1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        /// <summary>
        /// Метод который каждую секунду обновляет текущее время и дату 
        /// Тип данных DateTime преобразовывается в строку чтобы текст поля в окне мог копировать
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            TBCount.Text = DateTime.Now.ToString();
        }
        /// <summary>
        /// Создаем диалоговое окно для сохранения списка расчетов в json файл
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog
            {
                Filter = "Only json (*.json)|*.json"
            };
            if (ofd.ShowDialog() == true)
            {
                SaveToListViewToJson(ofd.FileName);
            }
        }


        /// <summary>
        /// При нажатий на кнопку расчитать идет проверка полей
        /// Если все поля заполнены правильно то расчет записывается в лист 
        /// В случае если найдена ошибка то выводится диалоговое окно уведомляющее пользователя 
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StaticData staticData = new StaticData();
            float A = 0;
            float B = 0;
            float Result;
            switch (Grunt1.SelectedIndex)
            {
                
                    case 0:
                    A = staticData.Peski1;
                    break;
                    case 1:
                    A = staticData.Suspeci1;
                    break;
                    case 2:
                    A = staticData.SuGlenok;
                    break;
                    case 3:
                    A = staticData.GLina1;
                    break;
                    case 4:
                    break;
            }
            switch(Grunt2.SelectedIndex)
            {
                case 0:
                    B = 0;
                    break;
                case 1:
                    B = 1;
                    break;
                case 2:
                    B = 0.85f;
                    break;

            }
            try
            {
                Result = staticData.Formula_TBF(A,
                B,
                Convert.ToSingle(Box1.Text.ToString()),
                Convert.ToSingle(Box2.Text.ToString()),
                Convert.ToSingle(Box3.Text.ToString()),
                Convert.ToSingle(Box4.Text.ToString()));
                List.Items.Add(new Item
                {
                    Name = BoxName.Text.ToString(),
                    Value = Result,
                    Data = TBCount.Text
                });
                items.Add(new Item
                {
                    Name = BoxName.Text.ToString(),
                    Value = Result,
                    Data = TBCount.Text
                });
            }
            catch 
            {
                MessageBox.Show("Пожалуйста, введите число корректно!");
            }
            finally
            {
                
            }
        }
        /// <summary>
        /// Метод сохранения json 
        /// List Item который был создан в начале программы в итоге обрабатывается в этом методе
        /// </summary>
        private void SaveToListViewToJson(string fileName)
        {
            string json = JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        /// <summary>
        ///  Появление таблиц для торфа при выборе
        ///  Данные взяты из Таблицы Б.2 Приложения Б
        /// </summary>
        private void Grunt1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Grunt1.SelectedIndex)
            {
                case 4:
                    Window1 win1 = new Window1();
                    win1.Show();
                    break;
                case 5:
                    Window2 win2 = new Window2();
                    win2.Show();
                    break;
            }
        }
    }

}
