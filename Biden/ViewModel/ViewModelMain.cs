using Biden.Func;
using Biden.Func.SpellCheck;
using Biden.Model;
using Biden.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace Biden.ViewModel
{
    class ViewModelMain : ViewModelCommon
    {


        public Command CmdBtn01 { get; set; }
        public Command CmdFuncBtn01 { get; set; }
        public Command CmdFuncBtn02 { get; set; }
        public Command CmdFuncBtn03 { get; set; }
        public Command CmdFuncBtn04 { get; set; }
        public Command CmdOnOffBtn01 { get; set; }
        public Command CmdOnOffBtn02 { get; set; }
        public Command CmdOnOffBtn03 { get; set; }
        public Command CmdOnOffBtn04 { get; set; }

        private TempClass tempClass;
        private FuncWindow1 tempWindow1;
        private FuncWindow2 tempWindow2;
        private FuncWindow3 tempWindow3;
        private static Macro macro;

        private ModelFunc1 func1Class;

        public ViewModelMain()
        {
            CmdBtn01 = new Command(Execute_Btn01, CanExecute_Btn01);
            CmdFuncBtn01 = new Command(Execute_FuncBtn01, CanExecute_Btn01);
            CmdFuncBtn02 = new Command(Execute_FuncBtn02, CanExecute_Btn01);
            CmdFuncBtn03 = new Command(Execute_FuncBtn03, CanExecute_Btn01);
            CmdFuncBtn04 = new Command(Execute_FuncBtn04, CanExecute_Btn01);
            CmdOnOffBtn01 = new Command(Execute_CmdOnOffBtn01, CanExecute_Btn01);
            CmdOnOffBtn02 = new Command(Execute_CmdOnOffBtn02, CanExecute_Btn01);
            CmdOnOffBtn03 = new Command(Execute_CmdOnOffBtn03, CanExecute_Btn01);
            CmdOnOffBtn04 = new Command(Execute_CmdOnOffBtn04, CanExecute_Btn01);
            tempClass = new TempClass();
            tempClass.Num = 1;
            tempClass.Num2 = 7;
            macro = Macro.getInstance;
            func1Class = ModelFunc1.getInstance;
            IsChecked01 = ModelFunc1.getInstance.IsChecked01;
            //ButtonCommand = new RelayCommand(new Action<object>(ChangeBgColor));
        }

        private void Execute_Btn01(object obj)
        {
            //do Something
            tempClass.Num = tempClass.Num * 2;
            OnPropertyChanged("Num");

            Text01 = Text01 + "A";

            ColorBtn01 = new SolidColorBrush(Colors.Green);
        }
        
        private void Execute_FuncBtn01(object obj)
        {
            //do Something
            if (tempWindow1 == null)
            {
                tempWindow1 = FuncWindow1.getInstance;
            }
            tempWindow1.Show();
        }


        private void Execute_FuncBtn02(object obj)
        {
            //do Something
            if (tempWindow2 == null)
            {
                tempWindow2 = FuncWindow2.getInstance;
            }
            tempWindow2.Show();
        }
        private void Execute_FuncBtn03(object obj)
        {
            //do Something
            if (tempWindow3 == null)
            {
                tempWindow3 = FuncWindow3.getInstance;
            }
            tempWindow3.Show();
        }
        private void Execute_FuncBtn04(object obj)
        {
            //do Something
            SpellCheck.getInstance.go();
        }

        private void Execute_CmdOnOffBtn01(object obj)
        {
            //do Something
            if (obj + "" == "True")
            {
                if (Macro.getInstance.IsInit == false)
                {
                    Macro.getInstance.IsInit = true;
                    Macro.create();
                    macro.start();
                    macro.start2();
                }
                IsChecked01 = true;
                Macro.getInstance.ModeOn1 = true;
            }
            else
            {
                IsChecked01 = false;
                Macro.getInstance.ModeOn1 = false;
            }
            FuncWindow1.getInstance.SetCheckBox01();
        }
        private void Execute_CmdOnOffBtn02(object obj)
        {
            //do Something
            if (obj + "" == "True")
            {
                if (Macro.getInstance.IsInit == false)
                {
                    Macro.getInstance.IsInit = true;
                    Macro.create();
                    macro.start();
                }
                IsChecked02 = true;
                Macro.getInstance.ModeOn2 = true;
            }
            else
            {
                IsChecked02 = false;
                Macro.getInstance.ModeOn2 = false;
            }
            FuncWindow2.getInstance.SetCheckBox02();
        }
        private void Execute_CmdOnOffBtn03(object obj)
        {
            //do Something
            if (obj + "" == "True")
            {
                Macro.setClipBoardText("");
                if (Macro.getInstance.IsInit == false)
                {
                    Macro.getInstance.IsInit = true;
                    Macro.create();
                    macro.start();
                }
                Macro.doublePasteFlag = true;
                IsChecked03 = true;
                Macro.getInstance.ModeOn3 = true;
            }
            else
            {
                IsChecked03 = false;
                Macro.getInstance.ModeOn3 = false;
            }
            //FuncWindow3.getInstance.SetCheckBox03();
        }

        private void Execute_CmdOnOffBtn04(object obj)
        {
            //do Something
        }

        private bool CanExecute_Btn01(object obj) { return true; }


        public int Num
        {
            get
            {
                return tempClass.Num;
            }
            set
            {
                tempClass.Num = value;
                Num2 = value * 2;
                OnPropertyChanged("Num");
            }
        }

        public int Num2
        {
            get
            {
                return tempClass.Num2;
            }
            set
            {
                tempClass.Num2 = value;
                OnPropertyChanged("Num2");
            }
        }

        public bool IsChecked01
        {
            get
            {
                return ModelFunc1.getInstance.IsChecked01;
            }
            set
            {
                ModelFunc1.getInstance.IsChecked01 = value;
                OnPropertyChanged("IsChecked01");
            }
        }
        public bool IsChecked02
        {
            get
            {
                return ModelFunc2.getInstance.IsChecked02;
            }
            set
            {
                ModelFunc2.getInstance.IsChecked02 = value;
                OnPropertyChanged("IsChecked02");
            }
        }
        public bool IsChecked03
        {
            get
            {
                return ModelFunc3.getInstance.IsChecked03;
            }
            set
            {
                ModelFunc3.getInstance.IsChecked03 = value;
                OnPropertyChanged("IsChecked03");
            }
        }
        /*
        public Brush ColorBtn01
        {
            get
            {
                return _selectedColor;
            }
            set
            {
                if (_selectedColor != null)
                    _selectedColor = value;

                OnPropertyChanged("ColorBtn01");
            }
        }*/



        private SolidColorBrush _selectedColor = new SolidColorBrush(Colors.White);
        public SolidColorBrush ColorBtn01
        {
            get { return _selectedColor; }
            set
            {
                if (value == _selectedColor)
                    return;

                _selectedColor = value;
                OnPropertyChanged("ColorBtn01");
            }
        }



        private string text01 = "*";
        public string Text01
        {
            get { return text01; }
            set
            {
                if (value == text01)
                    return;

                text01 = value;
                OnPropertyChanged("Text01");
            }
        }

        private ICommand m_ButtonCommand;

        public ICommand ButtonCommand
        {
            get
            {
                return m_ButtonCommand;
            }
            set
            {
                m_ButtonCommand = value;
            }
        }


        public void ChangeBgColor(object obj)
        {
            /*HERE I WANT TO CHANGE GRID COLOR*/
            string tempStr = "";

        }



        public void SetStateString(int x, int y, System.Drawing.Color curColor)
        {

            newString = "XY(" + x + "," + y + ")" + "  RGB(" + curColor.R + "," + curColor.G + "," + curColor.B + ")";
            OnPropertyChanged("StateString");

        }

        private string newString;

        public string StateString
        {
            get
            {
                return newString;
            }
            set
            {
                newString = value;
                OnPropertyChanged("StateString");
            }
        }

    }




}
