using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly LissajousModel _model;

        private double _ax = 100;
        private double _ay = 100;
        private double _fx = 1;
        private double _fy = 1;
        private double _phiXDeg = 0;
        private double _phiYDeg = 90;
        private double _timeStep = 0.005;

        private PointCollection _graphPoints = new PointCollection();
        private double _canvasWidth = 550;
        private double _canvasHeight = 550;

        public double Ax
        {
            get => _ax;
            set { _ax = value; OnPropertyChanged(); Recalculate(); }
        }

        public double Ay
        {
            get => _ay;
            set { _ay = value; OnPropertyChanged(); Recalculate(); }
        }

        public double Fx
        {
            get => _fx;
            set { _fx = value; OnPropertyChanged(); Recalculate(); }
        }

        public double Fy
        {
            get => _fy;
            set { _fy = value; OnPropertyChanged(); Recalculate(); }
        }

        public double PhiXDeg
        {
            get => _phiXDeg;
            set { _phiXDeg = value; OnPropertyChanged(); Recalculate(); }
        }

        public double PhiYDeg
        {
            get => _phiYDeg;
            set { _phiYDeg = value; OnPropertyChanged(); Recalculate(); }
        }

        public double TimeStep
        {
            get => _timeStep;
            set { _timeStep = value; OnPropertyChanged(); Recalculate(); }
        }

        public PointCollection GraphPoints
        {
            get => _graphPoints;
            set { _graphPoints = value; OnPropertyChanged(); }
        }

        public double CanvasWidth
        {
            get => _canvasWidth;
            set { _canvasWidth = value; Recalculate(); }
        }

        public double CanvasHeight
        {
            get => _canvasHeight;
            set { _canvasHeight = value; Recalculate(); }
        }

        public ICommand PlotCommand { get; }
        public ICommand SetRatio11Command { get; }
        public ICommand SetRatio12Command { get; }
        public ICommand SetRatio23Command { get; }
        public ICommand SetRatio34Command { get; }

        public MainViewModel()
        {
            _model = new LissajousModel();
            PlotCommand = new RelayCommand(Recalculate);

            SetRatio11Command = new RelayCommand(() => { Fx = 1; Fy = 1; });
            SetRatio12Command = new RelayCommand(() => { Fx = 1; Fy = 2; });
            SetRatio23Command = new RelayCommand(() => { Fx = 2; Fy = 3; });
            SetRatio34Command = new RelayCommand(() => { Fx = 3; Fy = 4; });

            Recalculate();
        }

        public void Recalculate()
        {
            _model.Ax = Ax;
            _model.Ay = Ay;
            _model.Fx = Fx;
            _model.Fy = Fy;
            _model.PhiXDeg = PhiXDeg;
            _model.PhiYDeg = PhiYDeg;
            _model.TimeStep = TimeStep;

            GraphPoints = _model.CalculatePoints(CanvasWidth, CanvasHeight);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        public RelayCommand(Action execute) => _execute = execute;
        public event EventHandler CanExecuteChanged { add { } remove { } }
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute();
    }
}