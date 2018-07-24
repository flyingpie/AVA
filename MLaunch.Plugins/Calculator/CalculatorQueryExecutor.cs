﻿using ImGuiNET;
using MLaunch.Core.QueryExecutors;
using MUI;
using MUI.DI;
using NCalc2;
using System.Windows.Forms;

namespace MLaunch.Plugins.Calculator
{
    [Service]
    public class CalculatorQueryExecutor : IQueryExecutor
    {
        [Dependency] public UIContext UI { get; set; }

        public string Name => "Calculator";

        public string Description => "1 + 1 = 11";

        public string ExampleUsage => "1 + 1";

        public int Order => 0;

        private string _expression;
        private string _parsedExpression;

        public CalculatorQueryExecutor()
        {
            // Some initialization is done under the covers, do that now to prevent lag on the first query
            _parsedExpression = new Expression("1 + 1").Evaluate().ToString();
        }

        public bool TryHandle(string term)
        {
            try
            {
                _expression = term;
                _parsedExpression = new Expression(term).Evaluate().ToString();

                return true;
            }
            catch { }

            return false;
        }

        public bool TryExecute(string term)
        {
            if (!string.IsNullOrWhiteSpace(_parsedExpression))
            {
                Clipboard.SetText(_parsedExpression);
                return true;
            }

            return false;
        }

        public void Draw()
        {
            ImGui.PushFont(UI.Font32);
            ImGui.Text($"{_expression} = {_parsedExpression}");
            ImGui.PopFont();

            ImGui.PushFont(UI.Font16);
            ImGui.Text("Press <Enter> to copy the result to the clipboard");
            ImGui.PopFont();
        }
    }
}