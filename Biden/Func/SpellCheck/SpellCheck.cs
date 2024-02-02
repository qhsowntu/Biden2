using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Biden.Func.SpellCheck
{
    class SpellCheck
    {
        private static SpellCheck instance = null;


        private SpellCheck()
        {
            
        }

        public static SpellCheck getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SpellCheck();
                }
                return instance;
            }
        }

        private async static Task SpellCheckStart()
        {
            //var task1 = Task.Run(() => get());
            var engine = IronPython.Hosting.Python.CreateEngine();
            var scope = engine.CreateScope();
            var paths = engine.GetSearchPaths();

            paths.Add(@"C:\Python27");
            paths.Add(@"C:\Python27\DLLs");
            paths.Add(@"C:\Python27\Lib");
            paths.Add(@"C:\Python27\Lib\site-packages");

            paths.Add(Application.StartupPath + @"\py-hanspell-master\setup.py");
            paths.Add(Application.StartupPath + @"\py-hanspell-master\tests.py");
            paths.Add(Application.StartupPath + @"\py-hanspell-master\hanspell\__init__.py");
            paths.Add(Application.StartupPath + @"\py-hanspell-master\hanspell\constants.py");
            paths.Add(Application.StartupPath + @"\py-hanspell-master\hanspell\response.py");
            paths.Add(Application.StartupPath + @"\py-hanspell-master\hanspell\spell_checker.py");

            engine.SetSearchPaths(paths);

            //아래 작업 선행 필요
            //1.파이썬 2.7 설치 (3.x는 호완안됨)
            //2.cmd에서 pip install requests
            //3.cmd에서 pip install pytest
            try
            {
                //var source = engine.CreateScriptSourceFromFile(Application.StartupPath + @"\py-hanspell-master\hanspell\tests.py");


                //var getPythonFuncResult1 = scope.GetVariable<Func<string, string>>("check");
                //var getPythonFuncResult1 = scope.GetVariable<Func<string, string>>("test_long_paragraph");
                //var source = engine.CreateScriptSourceFromFile(Application.StartupPath + @"\test.py");
                //source.Execute(scope);
                //var getPythonFuncResult1 = scope.GetVariable<Func<int,int,int>>("sum");
                //MessageBox.Show(getPythonFuncResult1(1,2)+"");


                string sampleStr = "안녕 하세요.저는 한국인 입니다.이문장은 한글로 작성됬습니다.";


                //var source = engine.CreateScriptSourceFromFile(Application.StartupPath + @"\py-hanspell-master\hanspell\spell_checker.py");
                var source0 = engine.CreateScriptSourceFromFile(Application.StartupPath + @"\py-hanspell-master\test_kys.py");
                source0.Execute(scope);
                var getPythonFuncResult1 = scope.GetVariable<Func<string, string>>("check");
                MessageBox.Show(getPythonFuncResult1(sampleStr) + "@@@@@@@@@@@@@@");


                //var getPythonFuncResult2 = scope.GetVariable<Func<int,int,int>>("sum");ㄴㅇ
                //MessageBox.Show(getPythonFuncResult2(1, 2) + "");



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "!");
            }
            finally
            {

            }



            
        }

        public void go()
        {
            SpellCheckStart().Wait();
        }

    }
}
