using Biden.Func;
using Biden.Model;
using Biden.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Biden.ViewModel
{
    class ViewModelFunc2 : ViewModelCommon
    {

        public Command CmdFuncBtn01 { get; set; }
        public Command CmdFuncBtn02 { get; set; }
        public Command CmdFuncBtn03 { get; set; }
        public Command CmdFuncBtn04 { get; set; }
        public Command CmdFuncBtn01_Add { get; set; }
        public Command CmdEditBtn { get; set; }
        public Command CmdDeleteBtn { get; set; }
        public Command CmdDeleteStrBtn { get; set; }
        public Command CmdOnOffBtn { get; set; }
        public Command TestBtn01 { get; set; }


        private bool spinner;
        private int count;
        private Thread myThread;
        private FuncWindow2_Add tempAddWindow;
        private FuncWindow2_Edit tempEditWindow;

        protected static bool initRuleFlag = false;
        protected static List<Func2RuleClass> ruleList;
        protected static Func2RuleClass rule;
        protected static int ruleCounter;
        private static ObservableCollection<MacroInfo> fileObjectCollection;
        private static ObservableCollection<MacroInfo2> strObjectCollection;
        protected static int editedIndex;
        public ModelFunc2 func2Class;
        private static Macro macro;
        protected static List<string> strList;

        public ViewModelFunc2()
        {
            CmdFuncBtn01 = new Command(Execute_FuncBtn01, CanExecute_Btn01);
            CmdFuncBtn01_Add = new Command(Execute_FuncBtn01_Add, CanExecute_Btn01);
            CmdEditBtn = new Command(Execute_CmdEditBtn01, CanExecute_Btn01);
            CmdDeleteBtn = new Command(Execute_CmdDeleteBtn01, CanExecute_Btn01);
            CmdDeleteStrBtn = new Command(Execute_CmdDeleteStrBtn01, CanExecute_Btn01);
            CmdOnOffBtn = new Command(Execute_CmdOnOffBtn, CanExecute_Btn01);
            TestBtn01 = new Command(Execute_TestBtn01, CanExecute_Btn01);
            spinner = false;
            count = 0;
            initRule();
        }


        private void initRule()
        {
            if (initRuleFlag == false)
            {
                initRuleFlag = true;
                ruleList = ModelFunc2.getInstance.RuleList;
                rule = new Func2RuleClass();
                ruleCounter = 1;
                fileObjectCollection = new ObservableCollection<MacroInfo>();
                strObjectCollection = new ObservableCollection<MacroInfo2>();
                macro = Macro.getInstance;
                func2Class = ModelFunc2.getInstance;
                strList = new List<string>();
                AddSampleRules();
                //ButtonCommand = new RelayCommand(new Action<object>(ChangeBgColor));
            }
        }

        private void AddSampleRules()
        {
            //nameStr = "SampleRules1";
            //fromStr = "1";
            //toStr = "2";
            //prefixStr = "Pre";
            //postfixStr = "Post";
            Func2RuleClass tempRules = new Func2RuleClass();
            tempRules.Name = "SampleFindRules1";
            tempRules.StrList = new List<string>{"a"};
            tempRules.AddStr = "?";
            tempRules.AlertMsg = "Find a";
            Func2RuleClass tempRules2 = new Func2RuleClass();
            tempRules2.Name = "SampleFindRules2";
            tempRules2.StrList = new List<string> { "1", "3", "5" };
            tempRules2.AddStr = "?";
            tempRules2.AlertMsg = "Find 1,3,5";
            ruleList.Add(tempRules);
            ruleList.Add(tempRules2);
            removeStr();
            FileObjectAndSync();
        }

        private void Execute_FuncBtn01(object obj)
        {
            //do Something
            DoSpin();
            DoCount();
            
        }

        private void FileObjectAndSync()
        {

            ObservableCollection<MacroInfo> tempCollection = new ObservableCollection<MacroInfo>();
            for (int i = 0; i < ruleList.Count; i++)
            {
                MacroInfo tempInfo = new MacroInfo();
                {
                    tempInfo.No = "" + (i + 1);
                    tempInfo.Name = "" + ruleList[i].Name;
                }
                tempCollection.Add(tempInfo);
            }
            FileObjectCollection = tempCollection;
        }


        private ObservableCollection<MacroInfo2> StrObjectAndSync(int i)
        {

            ObservableCollection<MacroInfo2> tempCollection = new ObservableCollection<MacroInfo2>();
            for (int j = 0; j < ruleList[i].StrList.Count; j++)
            {
                MacroInfo2 tempInfo = new MacroInfo2();
                {
                    tempInfo.No = "" + (j + 1);
                    tempInfo.Str = "" + ruleList[i].StrList[j];
                }
                tempCollection.Add(tempInfo);
            }
            return tempCollection;
        }

        private void Execute_FuncBtn01_Add(object obj)
        {
            //do Something
            if (tempAddWindow == null)
            {
                tempAddWindow = FuncWindow2_Add.getInstance;
            }
            FuncWindow2.getInstance.Hide();
            tempAddWindow.ShowDialog();
            FuncWindow2.getInstance.Show();

            FileObjectAndSync();
        }

        private void Execute_CmdEditBtn01(object obj)
        {
            if (obj != null)
            {
                //MessageBox.Show(obj.ToString());
                if (tempEditWindow == null)
                {
                    tempEditWindow = FuncWindow2_Edit.getInstance;
                }
                for (int i = 0; i < ruleList.Count; i++)
                {
                    if (ruleList[i].Name == obj + "")
                    {
                        editedIndex = i;
                        nameStr = ruleList[i].Name;
                        StrObjectCollection = StrObjectAndSync(i);
                        addStr = ruleList[i].AddStr;
                        alertStr = ruleList[i].AlertMsg;
                        isCheckedOpt01 = ruleList[i].IsCheckedOpt01;
                        isCheckedOpt02 = ruleList[i].IsCheckedOpt02; 
                        break;
                    }
                }
                FuncWindow2.getInstance.Hide();
                tempEditWindow.show();
                FuncWindow2.getInstance.Show();
                FileObjectAndSync();
            }
        }
        private void Execute_CmdDeleteBtn01(object obj)
        {
            for (int i = 0; i < ruleList.Count; i++)
            {
                if (ruleList[i].Name == obj + "")
                {
                    ruleList.RemoveAt(i);
                }
            }
            removeStr();
            FileObjectAndSync();
        }

        private void Execute_CmdDeleteStrBtn01(object obj)
        {
            for (int i = 0; i < StrObjectCollection.Count; i++)
            {
                if (StrObjectCollection[i].Str == obj)
                {
                    StrObjectCollection.RemoveAt(i);
                }
            }
        }

        public void Execute_CmdOnOffBtn(object obj)
        {
            //MessageBox.Show(obj + "");

            if (obj + "" == "True")
            {
                if (Macro.getInstance.IsInit == false)
                {
                    Macro.getInstance.IsInit = true;
                    Macro.create();
                    macro.start();
                }
                Macro.getInstance.ModeOn2 = true;
                IsChecked02 = true;
            }
            else
            {
                IsChecked02 = false;
                Macro.getInstance.ModeOn2 = false;
            }
            MainWindow.getInstance.SetSyncCheckBox();
            DoSpin();
            DoCount();
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

        

        public ObservableCollection<MacroInfo> FileObjectCollection
        {
            get { return fileObjectCollection; }
            set
            {
                if (value != fileObjectCollection)
                    fileObjectCollection = value;
                OnPropertyChanged("FileObjectCollection");
            }
        }
        public ObservableCollection<MacroInfo> SelectedFileObject
        {
            get { return fileObjectCollection; }
            set
            {
                if (value != fileObjectCollection)
                    fileObjectCollection = value;
                OnPropertyChanged("SelectedFileObject");
            }
        }

        public ObservableCollection<MacroInfo2> StrObjectCollection
        {
            get { return strObjectCollection; }
            set
            {
                if (value != strObjectCollection)
                    strObjectCollection = value;
                OnPropertyChanged("StrObjectCollection");
            }
        }
        public ObservableCollection<MacroInfo2> SelectedStrObject
        {
            get { return strObjectCollection; }
            set
            {
                if (value != strObjectCollection)
                    strObjectCollection = value;
                OnPropertyChanged("SelectedStrObject");
            }
        }

        private void Execute_TestBtn01(object obj)
        {
            //FileObjectCollection.Add("hi2");
        }

        private void DoCounting()
        {
            while(count != 10000)
            {
                count++;
                OnPropertyChanged("Counter1");
                Thread.Sleep(10);
            }
        }
        private void DoSpin()
        {
            if(spinner == true)
            {
                spinner = false;
            }
            else
            {
                spinner = true;
            }
            OnPropertyChanged("CmdFuncSpin01");
        }
        private void DoCount()
        {
            if (myThread == null || myThread.IsAlive == false)
            {
                myThread = new Thread(DoCounting);
                myThread.IsBackground = true;
                myThread.Start();
            }
            else
            {
                myThread.Abort();
            }

        }
        public bool CmdFuncSpin01
        {
            get
            {
                return spinner;
            }
            set
            {
                spinner = value;
                OnPropertyChanged("CmdFuncSpin01");
            }
        }

        public int Counter1
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                OnPropertyChanged("Counter1");
            }
        }


        public class MacroInfo
        {
            public string No { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
            //public string Edit;
            //public string Delete;
        }
        public class MacroInfo2
        {
            public string No { get; set; }
            public string Str { get; set; }
            public override string ToString() => Str;
            //public string Edit;
            //public string Delete;
        }
        public string nameStr
        {
            get
            {
                return rule.Name;
            }
            set
            {
                rule.Name = value;
                OnPropertyChanged("nameStr");
            }
        }
        public string addStr
        {
            get
            {
                return rule.AddStr;
            }
            set
            {
                rule.AddStr = value;
                OnPropertyChanged("addStr");
            }
        }
        public string alertStr
        {
            get
            {
                return rule.AlertMsg;
            }
            set
            {
                rule.AlertMsg = value;
                OnPropertyChanged("alertStr");
            }
        }
        public bool isCheckedOpt01
        {
            get
            {
                return rule.IsCheckedOpt01;
            }
            set
            {
                rule.IsCheckedOpt01 = value;
                OnPropertyChanged("IsCheckedOpt01");
            }
        }
        public bool isCheckedOpt02
        {
            get
            {
                return rule.IsCheckedOpt02;
            }
            set
            {
                rule.IsCheckedOpt02 = value;
                OnPropertyChanged("IsCheckedOpt02");
            }
        }

        internal static ObservableCollection<MacroInfo> FileObjectQueue { get => fileObjectCollection; set => fileObjectCollection = value; }

        public void removeStr()
        {
            nameStr = "";
            StrObjectCollection = new ObservableCollection<MacroInfo2>();
            strList = new List<string>();
            addStr = "";
            alertStr = "";
            isCheckedOpt01 = false;
            isCheckedOpt02 = false;
        }








    }
}
