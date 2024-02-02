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
    class ViewModelFunc1 : ViewModelCommon
    {

        public Command CmdFuncBtn01 { get; set; }
        public Command CmdFuncBtn02 { get; set; }
        public Command CmdFuncBtn03 { get; set; }
        public Command CmdFuncBtn04 { get; set; }
        public Command CmdFuncBtn01_Add { get; set; }
        public Command CmdEditBtn { get; set; }
        public Command CmdDeleteBtn { get; set; }
        public Command CmdOnOffBtn { get; set; }
        public Command TestBtn01 { get; set; }


        private bool spinner;
        private int count;
        private Thread myThread;
        private FuncWindow1_Add tempAddWindow;
        private FuncWindow1_Edit tempEditWindow;
        protected static bool initRuleFlag = false;
        protected static List<Func1RuleClass> ruleList;
        protected static Func1RuleClass rule;
        protected static int ruleCounter;
        private static ObservableCollection<MacroInfo> fileObjectCollection;
        protected static int editedIndex;
        public ModelFunc1 func1Class;//
        private static Macro macro;

        public ViewModelFunc1()
        {
            CmdFuncBtn01 = new Command(Execute_FuncBtn01, CanExecute_Btn01);
            CmdFuncBtn01_Add = new Command(Execute_FuncBtn01_Add, CanExecute_Btn01);
            CmdEditBtn = new Command(Execute_CmdEditBtn01, CanExecute_Btn01);
            CmdDeleteBtn = new Command(Execute_CmdDeleteBtn01, CanExecute_Btn01);
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
                ruleList = ModelFunc1.getInstance.RuleList;
                rule = new Func1RuleClass();
                ruleCounter = 1;
                fileObjectCollection = new ObservableCollection<MacroInfo>();
                macro = Macro.getInstance;
                func1Class = ModelFunc1.getInstance;
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
            Func1RuleClass tempRules = new Func1RuleClass();
            tempRules.NameStr = "SampleRules1";
            tempRules.FromStr = "1";
            tempRules.ToStr = "2";
            tempRules.PrefixStr = "Pre";
            tempRules.PostfixStr = "Post";
            ruleList.Add(tempRules);
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
                    tempInfo.Name = "" + ruleList[i].NameStr;
                }
                tempCollection.Add(tempInfo);
            }

            FileObjectCollection = tempCollection;
        }


        private void Execute_FuncBtn01_Add(object obj)
        {
            //do Something
            if (tempAddWindow == null)
            {
                tempAddWindow = FuncWindow1_Add.getInstance;
            }
            FuncWindow1.getInstance.Hide();
            tempAddWindow.ShowDialog();
            FuncWindow1.getInstance.Show();

            FileObjectAndSync();
        }

        private void Execute_CmdEditBtn01(object obj)
        {
            if (obj != null)
            {
                //MessageBox.Show(obj.ToString());
                if (tempEditWindow == null)
                {
                    tempEditWindow = FuncWindow1_Edit.getInstance;
                }
                for (int i = 0; i < ruleList.Count; i++)
                {
                    if (ruleList[i].NameStr == obj + "")
                    {
                        editedIndex = i;
                        nameStr = ruleList[i].NameStr;
                        fromStr = ruleList[i].FromStr;
                        toStr = ruleList[i].ToStr;
                        postfixStr = ruleList[i].PostfixStr;
                        prefixStr = ruleList[i].PrefixStr;
                    }
                }
                FuncWindow1.getInstance.Hide();
                tempEditWindow.show();
                FuncWindow1.getInstance.Show();
                FileObjectAndSync();
            }
        }
        private void Execute_CmdDeleteBtn01(object obj)
        {
            for (int i = 0; i < ruleList.Count; i++)
            {
                if (ruleList[i].NameStr == obj + "")
                {
                    ruleList.RemoveAt(i);
                }
            }
            removeStr();
            FileObjectAndSync();
        }

        public void Execute_CmdOnOffBtn(object obj)
        {
            //MessageBox.Show(obj + "");

            if (obj + "" == "True")
            {
                if (Macro.getInstance.IsInit == false)
                {
                    macro.IsInit = true;
                    macro.create();
                    macro.start();
                    macro.start2();
                }
                macro.ModeOn1 = true;
                IsChecked01 = true;
            }
            else
            {
                IsChecked01 = false;
                macro.ModeOn1 = false;
            }
            MainWindow.getInstance.SetSyncCheckBox();
            DoSpin();
            DoCount();
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

        public string nameStr
        {
            get
            {
                return rule.NameStr;
            }
            set
            {
                rule.NameStr = value;
                OnPropertyChanged("nameStr");
            }
        }
        public string fromStr
        {
            get
            {
                return rule.FromStr;
            }
            set
            {
                rule.FromStr = value;
                OnPropertyChanged("fromStr");
            }
        }
        public string toStr
        {
            get
            {
                return rule.ToStr;
            }
            set
            {
                rule.ToStr = value;
                OnPropertyChanged("toStr");
            }
        }
        public string prefixStr
        {
            get
            {
                return rule.PrefixStr;
            }
            set
            {
                rule.PrefixStr = value;
                OnPropertyChanged("prefixStr");
            }
        }
        public string postfixStr
        {
            get
            {
                return rule.PostfixStr;
            }
            set
            {
                rule.PostfixStr = value;
                OnPropertyChanged("postfixStr");
            }
        }

        internal static ObservableCollection<MacroInfo> FileObjectQueue { get => fileObjectCollection; set => fileObjectCollection = value; }

        public void removeStr()
        {
            nameStr = "";
            fromStr = "";
            toStr = "";
            prefixStr = "";
            postfixStr = "";
        }








    }
}
