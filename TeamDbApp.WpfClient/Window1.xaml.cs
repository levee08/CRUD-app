﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace TeamDbApp.WpfClient
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private MainWindowViewModel TeamviewModel;
        private ManagerWindowViewModel ManagerviewModel;
        private PlayerWindowViewModel PlayerviewModel;
        public Window1()
        {
            InitializeComponent();
            TeamviewModel = new MainWindowViewModel();
            DataContext = TeamviewModel;


            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MainWindow footballTeamWindow = new MainWindow(TeamviewModel);
            footballTeamWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            ManagerviewModel = new ManagerWindowViewModel();
            DataContext = ManagerviewModel;

            ManagerWindow managerWindow = new ManagerWindow(ManagerviewModel);
            managerWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PlayerviewModel = new PlayerWindowViewModel();
            DataContext = PlayerviewModel;

            PlayerWindow playerWindow =new PlayerWindow(PlayerviewModel);
            playerWindow.Show();
        }
    }
}
