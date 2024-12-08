﻿using ReactiveUI;
using ScottPlot;
using ScottPlot.Avalonia;

namespace StockPlot.Charts.Drawings
{
    public class HorizontalLine : ReactiveObject
    {
        internal ScottPlot.Plottables.HorizontalLine _line = new();
        private bool _inCreationMode = false;

        public HorizontalLine()
        {
            _line.IsDraggable = true;
            //_line.Dragged += _line_Dragged;
        }

        private void _line_Dragged(object? sender, EventArgs e)
        {
            this.RaisePropertyChanged(nameof(Y));
        }

        public double Y
        {
            get => _line.Y;
            set
            {
                _line.Y = value;
                this.RaisePropertyChanged(nameof(Y));
            }
        }

        public Color Color
        {
            get => _line.Color;
            set
            {
                _line.Color = value;
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
                (sender as AvaPlot).Plot.Add.Plottable(_line);

                (double coordinateX, double coordinateY) = (sender as AvaPlot).GetMouseCoordinates();

                Y = coordinateY;

                _inCreationMode = false;

                (sender as AvaPlot).PointerPressed -= Plot_PointerPressed;
            }
        }
    }
}
