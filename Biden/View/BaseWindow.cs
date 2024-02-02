using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Biden.View
{
	public class BaseWindow : Window
	{
		protected Color topColor = Color.FromArgb(0xFF, 0x11, 0x21, 0x39);
		protected Color bottomColor = Color.FromArgb(0xFF, 0x19, 0x2D, 0x59);
		protected Color topColor2 = Color.FromArgb(0xFF, 0x24, 0x41, 0x51);
		protected Color bottomColor2 = Color.FromArgb(0xFF, 0x45, 0x52, 0x85);


		private static List<BaseWindow> baseWindowList;

		public BaseWindow()
		{
			if (baseWindowList == null)
			{
				baseWindowList = new List<BaseWindow>();
			}
			baseWindowList.Add(this);
		}

		/**
		 * @brief 드래그 시 실행
		 * @date 2020/10/15
		 * @author 김양수
		 * @return void
		*/
		virtual public void OnMouseLeftDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		/**
		 * @brief 닫기 버튼 클릭 시 실행
		 * @date 2020/10/15
		 * @author 김양수
		 * @return void
		*/
		virtual public void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			//MessageBox.Show(this+"@");
			string windowName = this+"";
			if (windowName.Contains("MainWindow"))
			{
				deleteAllWindow();
				//this.Close();
				return;
            }
            else
            {
				this.Hide();
			}
		}

		/**
		 * @brief 최대화 버튼 클릭 시 실행. 현재 비활성상태
		 * @date 2020/10/15
		 * @author 김양수
		 * @return void
		*/
		virtual public void MaxButton_Click(object sender, RoutedEventArgs e)
		{
			/*
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }*/
		}

		/** 최소화 버튼 클릭 시 실행
		 * @brief 
		 * @date 2020/10/15
		 * @author 김양수
		 * @return void
		*/
		virtual public void MinButton_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = System.Windows.WindowState.Minimized;
		}

		protected void deleteAllWindow()
		{
			for (int i = 0; i < baseWindowList.Count; i++)
			{
				(baseWindowList[i]).Close();
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(name));
			}
		}

	}
}
