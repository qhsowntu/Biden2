using Biden.Model;
using Biden.View;
using Biden.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.Func
{
    class MultiClipboard
    {

        private static FuncWindow3_ClipList tempWindow;
        public MultiClipboard()
        {

        }


        //클립보드 ListView의 Item을 추가 
        public void SetItem(string str)
        {
            if (str == "" || str == null)
            {
                return;
            }
            List<object> list = ModelFunc3.getInstance.ObjList;

            if (ModelFunc3.getInstance.IsCheckedDupOpt == true && list.Contains(str) == true)
            {
            }
            else
            {
                list.Add(str);
            }
            ModelFunc3.getInstance.ObjList = list;
            DispatcherService.Invoke((System.Action)(() =>
            {
                FuncWindow3.getInstance.SetListView();
            }));
        }
        public void SetItem(System.Drawing.Image image)
        {
            if (image == null)
            {
                return;
            }
            List<object> list = ModelFunc3.getInstance.ObjList;

            if (ModelFunc3.getInstance.IsCheckedDupOpt == true && list.Contains(image) == true)
            {
            }
            else
            {
                list.Add(image);
            }
            ModelFunc3.getInstance.ObjList = list;
            DispatcherService.Invoke((System.Action)(() =>
            {
                FuncWindow3.getInstance.SetListView();
            }));
        }
        public void SetItem(DataObjectClass obj)
        {
            if (obj == null)
            {
                return;
            }
            List<DataObject> list = ModelFunc3.getInstance.DataObjList;
            List<string> list2 = ModelFunc3.getInstance.DataObjTypeList;

            //중복 확인
            if (ModelFunc3.getInstance.IsCheckedDupOpt == true && list.Contains(obj.dataObject) == true)
            {
            }
            //중복이 아닌 경우 삽입
            else
            {
                list.Add(obj.dataObject);
                list2.Add(obj.type);
            }
            ModelFunc3.getInstance.DataObjList = list;
            DispatcherService.Invoke((System.Action)(() =>
            {
                FuncWindow3.getInstance.SetListView();
            }));
        }

   
        


        //클립보드 ListView의 Item을 추출
        public static object GetItem()
        {
            object res = "";
            if (ModelFunc3.getInstance.TheSelectedRule == ModelFunc3.getInstance.Source.ElementAt(0)) //큐
            {
                res = ModelFunc3.getInstance.ObjList.ElementAt(0);
                ModelFunc3.getInstance.ObjList.RemoveAt(0);
            }
            else if (ModelFunc3.getInstance.TheSelectedRule == ModelFunc3.getInstance.Source.ElementAt(1)) //스택
            {
                res = ModelFunc3.getInstance.ObjList.ElementAt(ModelFunc3.getInstance.ObjList.Count - 1);
                ModelFunc3.getInstance.ObjList.RemoveAt(ModelFunc3.getInstance.ObjList.Count - 1);
            }
            else if (ModelFunc3.getInstance.TheSelectedRule == ModelFunc3.getInstance.Source.ElementAt(2))  //선택창
            {
                if (tempWindow == null)
                {
                    DispatcherService.Invoke((System.Action)(() =>
                    {
                        tempWindow = FuncWindow3_ClipList.getInstance;
                    }));
                }
                //바인딩 문제로 창 위치 임시방편 처리
                FuncWindow3.getInstance.SetListView();
                FuncWindow3_ClipList.getInstance.setPos();
                //tempWindow.Activate();
                tempWindow.ShowDialog();
                res = ModelFunc3.getInstance.SelectedString;
                //MessageBox.Show("2", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                return res;
                //MessageBox.Show("선택창");
            }

            
            //ModelFunc3.;

            return res;
        }
        public DataObjectClass GetMapItem()
        {
            DataObjectClass res = new DataObjectClass();
            if (ModelFunc3.getInstance.TheSelectedRule == ModelFunc3.getInstance.Source.ElementAt(2))  //선택창
            {
                if (tempWindow == null)
                {
                    DispatcherService.Invoke((System.Action)(() =>
                    {
                        tempWindow = FuncWindow3_ClipList.getInstance;
                    }));
                }
                //바인딩 문제로 창 위치 임시방편 처리
                DispatcherService.Invoke((System.Action)(() =>
                {
                    FuncWindow3_ClipList.getInstance.SetListView();
                    FuncWindow3_ClipList.getInstance.setPos();
                    tempWindow.ShowDialog();
                }));
                res.dataObject = ModelFunc3.getInstance.DataObjList[Int32.Parse(ModelFunc3.getInstance.SelectedStringIndex)];
                res.type = ModelFunc3.getInstance.DataObjTypeList[Int32.Parse(ModelFunc3.getInstance.SelectedStringIndex)];
                //MessageBox.Show("2", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                return res;
            }

            return res;
        }
    }
}
