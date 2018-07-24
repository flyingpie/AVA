using ImGuiNET;
using MLaunch.Core.QueryExecutors;
using MUI.DI;
using NCalc2;
using System.Windows.Forms;

namespace MLaunch.Plugins.Calculator
{
    [Service]
    public class CalculatorQueryExecutor : IQueryExecutor
    {
        public string Name => "Calculator";

        public string Description => "1 + 1 = 11";

        public string ExampleUsage => "1 + 1";

        public int Order => 0;

        private string _expression;
        private string _parsedExpression;

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
            ImGui.Text($"{_expression} = {_parsedExpression}");
        }
    }
}