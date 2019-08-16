using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RandChecker.Annotations;

namespace RandChecker
{
    public class ViewModel : INotifyPropertyChanged
    {
        private long _ctStart = 1;
        private long _ctStop = 100000;
        private long _ctCurrent = 0;
        private int _split = 0;
        private long _ctSplit = 5000;
        private bool _isStart = false;

        public string BtnTitle => _isStart ? "停止" : "开始";

        public long CtStart
        {
            get => _ctStart;
            set
            {
                _ctStart = value;
                OnPropertyChanged(nameof(CtStart));
            }
        }

        public long CtStop
        {
            get => _ctStop;
            set
            {
                _ctStop = value;
                OnPropertyChanged(nameof(CtStop));
            }
        }

        public long CtCurrent
        {
            get => _ctCurrent;
            set
            {
                _ctCurrent = value;
                OnPropertyChanged(nameof(CtCurrent));
            }
        }

        public int Split
        {
            get => _split;
            set
            {
                _split = value;
                OnPropertyChanged(nameof(Split));
            }
        }

        public long CtSplit
        {
            get => _ctSplit;
            set
            {
                _ctSplit = value;
                OnPropertyChanged(nameof(CtSplit));
            }
        }

        public bool IsStart
        {
            get => _isStart;
            set
            {
                _isStart = value;
                OnPropertyChanged(nameof(IsStart));
                OnPropertyChanged(nameof(IsStop));
                OnPropertyChanged(nameof(BtnTitle));
            }
        }

        public bool IsStop
        {
            get => !_isStart;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}