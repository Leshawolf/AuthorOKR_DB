using ClosedXML.Excel;
using Microsoft.VisualBasic;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
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
using ClosedXML;
using Irony.Parsing.Construction;
using OKR_BD.Models;


namespace OKR_BD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StringBuilder sb = new StringBuilder();
        public MainWindow()
        {
            InitializeComponent();
            dataGrid1.ItemsSource = Writer.CheckList();
            Count_all_lb.Content = $"Количество: {Writer.CountFullDB()}";
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var psevdanim = psevdanim_add_tb.Text;
                var last_name = last_name_add_tb.Text;
                var first_name = first_name_add_tb.Text;
                var birdth_year = birdth_year_add_tb.Text;
                var dead_year = dead_year_add_tb.Text;
                if (psevdanim != null || last_name != null || first_name != null || birdth_year != null || dead_year != null)
                {
                    string query = $"INSERT INTO `Писатель`(`id`, `Псевдоним`, `Имя`, `Отчество`, `Год_рождения`, `Год_смерти`) VALUES (0,'{psevdanim}','{last_name}','{first_name}','{birdth_year}','{dead_year}');";
                    string connStr = "server=************;" + "user=************;" + "database=************;" + "password=************;";

                    MySqlConnection conn = new MySqlConnection(connStr);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                dataGrid1.ItemsSource=Writer.CheckList();
            }
            catch
            {
            }
        }


        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            string id_str = num_delete_tb.Text;
            Writer.DeleteID(id_str);
            dataGrid1.ItemsSource = Writer.CheckList();

        }

        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            var name=name_search_tb.Text;
            dataGrid1.ItemsSource = Writer.SearchName(name);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = Writer.CheckList();
            Count_all_lb.Content = $"Количество: {Writer.CountFullDB()}";

        }

        private void search_psevd_count_btn_Click(object sender, RoutedEventArgs e)
        {
            var text=psevdannim_search_count_tb.Text;
            var count = Writer.SearchCountPsevdanin(text);
            lb_count_psevd.Content = $"Количество: {count}";
        }

        private void old_and_small_btn_Click(object sender, RoutedEventArgs e)
        {
            var writer_young=Writer.CheckYoung();
            small_pisatel_lb.Content = $"Самый молодой писатель: {writer_young[0]} - Возраст:{writer_young[1]}";
            var writer_old = Writer.CheckOld();
            old_pisatel_lb.Content = $"Самый старый писатель: {writer_old[0]} - Возраст:{writer_old[1]}";
        }

        private void Otchet_Create_btn_Click(object sender, RoutedEventArgs e)
        {
            var list = Writer.CheckListSort();
            var workbook = new XLWorkbook();
            workbook.AddWorksheet("sheetName");
            var ws = workbook.Worksheet("sheetName");
            int i = 0;
            ws.Cell("A1").Value = "Имя";
            ws.Cell("B1").Value = "Отчество";
            ws.Cell("C1").Value = "Псевдоним";
            int row = 2;
            foreach (var item in list)
            {
                ws.Cell("A" + row.ToString()).Value = item.First_name;
                ws.Cell("B" + row.ToString()).Value = item.Last_name;
                ws.Cell("C" + row.ToString()).Value = item.Psevdanim;
                row++;
            }
            i++;
            workbook.SaveAs($"author{i}.xlsx");
        }
    }
}
