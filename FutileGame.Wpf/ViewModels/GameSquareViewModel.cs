﻿using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using ReactiveUI;
using FutileGame.Models;

namespace FutileGame.ViewModels
{
    public class GameSquareViewModel : ReactiveObject
    {
        private readonly Square _model;

        public GameSquareViewModel(Square m)
        {
            _model = m;

            _text = _model
                .WhenAnyValue(x => x.Value)
                .Select(x => GetValueText(x))
                .ToProperty(this, x => x.Text);

            Check = ReactiveCommand.Create(
                canExecute: _model.CanCheck,
                execute: () => _model.Check()
            );
        }

        private static string GetValueText(int x) => x switch
        {
            0 => "",
            5 => "1",
            _ => x.ToString()
        };

        public int RowIndex => _model.RowIndex;
        public int ColumnIndex => _model.ColumnIndex;

        private readonly ObservableAsPropertyHelper<string> _text;
        public string Text => _text.Value;

        public ReactiveCommand<Unit, Unit> Check { get; }
    }
}