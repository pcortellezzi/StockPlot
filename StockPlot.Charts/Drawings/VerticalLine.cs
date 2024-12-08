using ReactiveUI;
using ScottPlot.Avalonia;
using System.Drawing;

namespace StockPlot.Charts.Drawings
{
    public class VerticalLine : ReactiveObject
    {
        internal ScottPlot.Plottables.VerticalLine _line = new();
        private bool _inCreationMode = false;

        public VerticalLine()
        {
            _line.IsDraggable = true;
            //_line.Dragged += _line_Dragged;
        }

        private void _line_Dragged(object? sender, EventArgs e)
        {
            this.RaisePropertyChanged(nameof(X));
        }

        public double X
        {
            get => _line.X;
            set
            {
                _line.X = value;
                this.RaisePropertyChanged(nameof(X));
            }
        }

        public Color Color
        {
            get => _line.Color.ToSDColor();
            set
            {
                _line.Color = ScottPlot.Color.FromSDColor(value);
                this.RaisePropertyChanged(nameof(Color));
            }
        }

        internal void Create(AvaPlot plot)
        {
            _inCreationMode = true;
            plot.PointerPressed += Plot_PointerPressed;
        }

        private void Plot_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            if (_inCreationMode)
            {
                //(sender as AvaPlot).Plot.Add(_line);
                (sender as AvaPlot).Plot.Add.Plottable(_line);

                (double coordinateX, double coordinateY) = (sender as AvaPlot).GetMouseCoordinates();

                X = coordinateX;

                _inCreationMode = false;

                (sender as AvaPlot).PointerPressed -= Plot_PointerPressed;
            }
        }
    }
}
