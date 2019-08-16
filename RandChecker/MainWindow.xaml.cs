using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RandChecker
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _vm;


        public MainWindow()
        {
            InitializeComponent();
            _vm = this.DataContext as ViewModel;
        }

        private string FILE_PREFIX = "account";

        private List<Runner> runners = new List<Runner>();

        private void Start()
        {
            //计算需要分多少个文件
            runners.Clear();

            var counter = _vm.CtStop - _vm.CtStart;
            var total = counter + 0;
            var sp = (double) counter / (double) _vm.CtSplit;
            var count = Math.Ceiling(Convert.ToDecimal(sp));
            Console.WriteLine($"一共需要创建：{counter}-{sp}  -  {count} 个文件");
            //创建这么多个现成
            FinishCounter = Convert.ToInt32(count);
            for (int i = 0; i < count; i++)
            {
                var run = new Runner {filename = $"{FILE_PREFIX}_{i}.txt"};
                run.Fs = File.Open(run.filename, FileMode.Create);
                run.FWriter = new StreamWriter(run.Fs);
                run.cStart = _vm.CtStart + _vm.CtSplit * i;
                run.cStop = run.cStart + _vm.CtSplit;
                run.Finish += Run_Finish;
                run.cCurrent = 0;
                run.RunThread = new Thread(() =>
                {
                    for (long u = run.cStart; u < run.cStop; u++)
                    {
                        run.FWriter.WriteLine($"{u}_nickname_0");
                        run.cCurrent++;
                    }

                    run.Finished();
                });
                run.Start();
                runners.Add(run);
            }

            _vm.IsStart = true;
        }

        private int FinishCounter = 0;

        private void Run_Finish(Runner runner)
        {
            lock (runners)
            {
                FinishCounter--;
                _vm.CtCurrent += runner.cCurrent;
                runner.Close();
                Console.WriteLine($"{runner.filename} 执行完成,还剩余：{FinishCounter}");
                if (FinishCounter <= 0)
                {
                    _vm.IsStart = false;
                    runners.Clear();
                    Task.Run(() => { MessageBox.Show($"任务执行完成"); });
                }
            }
        }


        private void Stop()
        {
            lock (runners)
            {
                foreach (var runner in runners)
                {
                    runner.RunThread?.Abort();
                    runner.Close();
                }

                _vm.IsStart = false;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (_vm.IsStart)
            {
                this.Stop();
            }
            else
            {
                this.Start();
            }
        }
    }
}