﻿using GeekDesk.Constant;
using GeekDesk.MyThread;
using GeekDesk.Util;
using GeekDesk.ViewModel;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GeekDesk.Control.UserControls.Config
{
    /// <summary>
    /// OtherControl.xaml 的交互逻辑
    /// </summary>
    public partial class OtherControl : UserControl
    {
        public OtherControl()
        {
            InitializeComponent();
            this.Loaded += OtherControl_Loaded;

        }

        private void OtherControl_Loaded(object sender, RoutedEventArgs e)
        {
            Sort_Check();
        }

        private void SelfStartUpBox_Click(object sender, RoutedEventArgs e)
        {
            AppConfig appConfig = MainWindow.appData.AppConfig;
            RegisterUtil.SetSelfStarting(appConfig.SelfStartUp, Constants.MY_NAME);
        }

        /// <summary>
        /// 移动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DragMove(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Window.GetWindow(this).DragMove();
            }
        }

        private void SortType_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            SortType type = (SortType)int.Parse(rb.Tag.ToString());

            SortType resType = type;
            switch (type)
            {
                case SortType.CUSTOM:
                    break;
                case SortType.COUNT_UP:
                    if (rb.IsChecked == true)
                    {
                        CountLowSort.IsChecked = true;
                        CountUpSort.Visibility = Visibility.Collapsed;
                        CountLowSort.Visibility = Visibility.Visible;
                        resType = SortType.COUNT_LOW;
                    }
                    break;
                case SortType.COUNT_LOW:
                    if (rb.IsChecked == true)
                    {
                        CountUpSort.IsChecked = true;
                        CountLowSort.Visibility = Visibility.Collapsed;
                        CountUpSort.Visibility = Visibility.Visible;
                        resType = SortType.COUNT_UP;
                    }
                    break;
                case SortType.NAME_UP:
                    if (rb.IsChecked == true)
                    {
                        NameLowSort.IsChecked = true;
                        NameUpSort.Visibility = Visibility.Collapsed;
                        NameLowSort.Visibility = Visibility.Visible;
                        resType = SortType.NAME_LOW;
                    }
                    break;
                case SortType.NAME_LOW:
                    if (rb.IsChecked == true)
                    {
                        NameUpSort.IsChecked = true;
                        NameLowSort.Visibility = Visibility.Collapsed;
                        NameUpSort.Visibility = Visibility.Visible;
                        resType = SortType.NAME_UP;
                    }
                    break;
            }
            MainWindow.appData.AppConfig.IconSortType = resType;
            CommonCode.SortIconList();
        }

        private void Sort_Check()
        {
            if (NameLowSort.IsChecked == true)
            {
                NameUpSort.Visibility = Visibility.Collapsed;
                NameLowSort.Visibility = Visibility.Visible;
            }
            if (CountLowSort.IsChecked == true)
            {
                CountUpSort.Visibility = Visibility.Collapsed;
                CountLowSort.Visibility = Visibility.Visible;
            }
        }

        private void BakDataFile(object sender, RoutedEventArgs e)
        {
            CommonCode.BakAppData();
        }
    }
}
