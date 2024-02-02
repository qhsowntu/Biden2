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
using System.Windows.Forms;
using System.Windows.Input;

namespace Biden.ViewModel
{
    class ViewModelFunc3 : ViewModelCommon
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

        private int topPoint;
        private int leftPoint;
        private bool isCheckedOpt03;
        private bool spinner;
        private int count;
        private Thread myThread;

        protected static bool initRuleFlag = false;
        protected static List<Func3RuleClass> ruleList;
        protected static Func3RuleClass rule;
        protected static int ruleCounter;
        public static ObservableCollection<ModelFunc3.MacroInfo3> optionObjectCollection;
        public static ObservableCollection<ModelFunc3.MacroInfo3> optionObjectCollection2;
        protected static int editedIndex;
        private static Macro macro;

        public ViewModelFunc3()
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
                ruleList = ModelFunc3.getInstance.RuleList;
                rule = new Func3RuleClass();
                ruleCounter = 1;
                optionObjectCollection = new ObservableCollection<ModelFunc3.MacroInfo3>();
                macro = Macro.getInstance;
                AddSampleRules();
                //ButtonCommand = new RelayCommand(new Action<object>(ChangeBgColor));
            }
        }

        private void AddSampleRules()
        {
            /*IDataObject obj;
            Clipboard.SetDataObject("전임1");
            obj = Clipboard.GetDataObject();
            ModelFunc3.getInstance.IDataObjList.Add(obj);
            Clipboard.SetDataObject("선임1");
            obj = Clipboard.GetDataObject();
            ModelFunc3.getInstance.IDataObjList.Add(obj);
            Clipboard.SetDataObject("책임1");
            obj = Clipboard.GetDataObject();
            ModelFunc3.getInstance.IDataObjList.Add(obj);
            Clipboard.SetDataObject("수석1");
            obj = Clipboard.GetDataObject();
            ModelFunc3.getInstance.IDataObjList.Add(obj);*/
            StrObjectAndSync();
            ModelFunc3.getInstance.TheSelectedRule = ModelFunc3.getInstance.Source.ElementAt(2);
        }

        private void Execute_FuncBtn01(object obj)
        {
            //do Something
            DoSpin();
            DoCount();
            
        }


        private void Execute_FuncBtn01_Add(object obj)
        {


        }

        private void Execute_CmdEditBtn01(object obj)
        {
            
        }
        private void Execute_CmdDeleteBtn01(object obj)
        {
            //
            /*for (int i = 0; i < ModelFunc3.getInstance.ObjList.Count; i++)
            {
                if (ModelFunc3.getInstance.ObjList[i] == obj + "")
                {
                    ModelFunc3.getInstance.ObjList.RemoveAt(i);
                }
            }*/
            if (Int32.Parse(obj + "") > 1)
            {
                ModelFunc3.getInstance.DataObjList.RemoveAt(Int32.Parse(obj + "") - 1);
            }
            StrObjectAndSync();
        }

        private void Execute_CmdDeleteStrBtn01(object obj)
        {
            
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
                Macro.getInstance.ModeOn3 = true;
                IsChecked03 = true;
            }
            else
            {
                IsChecked03 = false;
                Macro.getInstance.ModeOn3 = false;
            }
            MainWindow.getInstance.SetSyncCheckBox();
            DoSpin();
            DoCount();
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


        public void StrObjectAndSync()
        {

            ObservableCollection<ModelFunc3.MacroInfo3> tempCollection = new ObservableCollection<ModelFunc3.MacroInfo3>();
            for (int i = 0; i < ModelFunc3.getInstance.DataObjList.Count; i++)
            {
                ModelFunc3.MacroInfo3 tempInfo = new ModelFunc3.MacroInfo3();
                {
                    
                    tempInfo.No = "" + (i + 1);
                    if (ModelFunc3.getInstance.DataObjTypeList[i].Contains("Text")) 
                    {
                        tempInfo.Obj = "" + ModelFunc3.getInstance.DataObjList[i].GetData(System.Windows.Forms.DataFormats.Text);
                        //View에 보여지는 문장 수정. 너무 길면 짤리도록.
                        if (((string)tempInfo.Obj).Length > 22)
                        {
                            tempInfo.Obj = ((string)tempInfo.Obj).Substring(0, 21) + "...";
                        }
                        tempInfo.Obj = ((string)tempInfo.Obj).Replace("\n", "").Replace("\n", "").Replace("\r", "").Replace("\a", "");
                    }
                    else if (ModelFunc3.getInstance.DataObjTypeList[i].Contains("Bitmap"))
                    {
                        tempInfo.Obj = "" + ModelFunc3.getInstance.DataObjList[i].GetData(System.Windows.Forms.DataFormats.Bitmap);
                    }

                    //System.Windows.Forms.MessageBox.Show(ModelFunc3.getInstance.ObjList[i].GetType()+"");
                    if (ModelFunc3.getInstance.DataObjList[i].GetType() == typeof(System.Drawing.Image))
                    {
                        //System.Windows.Forms.MessageBox.Show("image");
                    }
                    else if (ModelFunc3.getInstance.DataObjList[i].GetType() == typeof(System.String))
                    {
                    }

                }
                tempCollection.Add(tempInfo);
            }
            OptionObjectCollection = tempCollection;
            OptionObjectCollection2 = tempCollection;
            if (tempCollection.Count > 1)
            {
                SelectedItem = tempCollection[tempCollection.Count - 1];
            }
        }


        public ObservableCollection<ModelFunc3.MacroInfo3> OptionObjectCollection
        {
            get { return optionObjectCollection; }
            set
            {
                if (value != optionObjectCollection)
                    //StrObjectAndSync();
                    optionObjectCollection = value;
                OnPropertyChanged("OptionObjectCollection");
            }
        }
        public ObservableCollection<ModelFunc3.MacroInfo3> SelectedOptionObject
        {
            get { return optionObjectCollection; }
            set
            {
                if (value != optionObjectCollection)
                    optionObjectCollection = value;
                OnPropertyChanged("SelectedOptionObject");
            }
        }
        public ObservableCollection<ModelFunc3.MacroInfo3> OptionObjectCollection2
        {
            get { return optionObjectCollection2; }
            set
            {
                if (value != optionObjectCollection2)
                    optionObjectCollection2 = value;
                OnPropertyChanged("OptionObjectCollection2");
            }
        }

        public ModelFunc3.MacroInfo3 SelectedItem
        {
            get
            {
                return ModelFunc3.getInstance.SelectedItem;
            }
            set
            {
                ModelFunc3.getInstance.SelectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public string SelectedRule
        {
            get
            {
                return ModelFunc3.getInstance.TheSelectedRule;
            }
            set
            {
                ModelFunc3.getInstance.TheSelectedRule = value;
                OnPropertyChanged("SelectedRule");
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

        public bool IsCheckedOpt03
        {
            get
            {
                return ModelFunc3.getInstance.IsCheckedDupOpt;
            }
            set
            {
                ModelFunc3.getInstance.IsCheckedDupOpt = value;
                OnPropertyChanged("IsCheckedOpt03");
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


        //selectedItem
        public string SelectedString
        {
            get
            {
                return ModelFunc3.getInstance.SelectedString;
            }
            set
            {
                ModelFunc3.getInstance.SelectedString = value;
                //WindowPos.SendWpfWindowBack(FuncWindow3_ClipList.getInstance);
                //WindowPos.SendWpfWindowBack(FuncWindow3.getInstance);
                FuncWindow3_ClipList.getInstance.Hide();
                OnPropertyChanged("SelectedString");
            }
        }

        public string SelectedStringIndex
        {
            get
            {
                return ModelFunc3.getInstance.SelectedStringIndex;
            }
            set
            {
                ModelFunc3.getInstance.SelectedStringIndex = value;
                //WindowPos.SendWpfWindowBack(FuncWindow3_ClipList.getInstance);
                //WindowPos.SendWpfWindowBack(FuncWindow3.getInstance);
                FuncWindow3_ClipList.getInstance.Hide();
                OnPropertyChanged("SelectedStringIndex");
            }
        }


        public List<string> Source
        {
            get { return ModelFunc3.getInstance.Source; }
        }



        public void removeStr()
        {
            
        }








    }
}
