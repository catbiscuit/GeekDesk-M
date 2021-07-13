﻿using GeekDesk.Util;
using GeekDesk.ViewModel;
using HandyControl.Controls;
using System;
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

namespace GeekDesk.Control.Windows
{
    /// <summary>
    /// BacklogInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BacklogInfoWindow
    {

        private static int windowType = -1;
        private static readonly int NEW_BACKLOG = 1;
        private static readonly int DETAIL_BACKLOG = 2;

        private AppData appData = MainWindow.appData;

        private BacklogInfo info;

        private BacklogInfoWindow()
        {
            InitializeComponent();
            ExeTime.SelectedDateTime = DateTime.Now.AddMinutes(10);
            this.Topmost = true;
        }
        private BacklogInfoWindow(BacklogInfo info)
        {
            InitializeComponent();
            this.Topmost = true;
            Title.Text = info.Title;
            Msg.Text = info.Msg;
            ExeTime.Text = info.ExeTime;
            DoneTime.Text = info.DoneTime;
            this.info = info;
        }


        /// <summary>
        /// 点击关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                DragMove();
            }
        }

        /// <summary>
        /// 保存待办
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {


            if (Title.Text.Trim() == "" || ExeTime.Text.Trim() == "")
            {
                Growl.Warning("任务标题 和 待办时间不能为空!");
                return;
            } else
            {
                try
                {
                    Convert.ToDateTime(ExeTime.Text);
                } catch (Exception)
                {
                    Growl.Warning("请输入正确的时间!");
                    return;
                }
            }
            if (windowType == NEW_BACKLOG)
            {
                info = new BacklogInfo
                {
                    Title = Title.Text,
                    Msg = Msg.Text,
                    ExeTime = ExeTime.Text
                };
                appData.ExeBacklogList.Add(info);
            } else
            {
                int index =appData.ExeBacklogList.IndexOf(info);
                appData.ExeBacklogList.Remove(info);
                info.Title = Title.Text;
                info.Msg = Msg.Text;
                info.ExeTime = ExeTime.Text;
                info.DoneTime = DoneTime.Text;
                appData.ExeBacklogList.Insert(index, info);
            }
            CommonCode.SaveAppData(MainWindow.appData);
            this.Close();
        }

        private static System.Windows.Window window = null;
        public static void ShowNone()
        {
            if (window == null || !window.Activate())
            {
                window = new BacklogInfoWindow();
                
            }
            windowType = NEW_BACKLOG;
            window.Show();
        }

        private static System.Windows.Window window2 = null;
        public static void ShowDetail(BacklogInfo info)
        {
            if (window2 == null || !window2.Activate())
            {
                window2 = new BacklogInfoWindow(info);
            }
            windowType = DETAIL_BACKLOG;
            window2.Show();
        }
    }
}