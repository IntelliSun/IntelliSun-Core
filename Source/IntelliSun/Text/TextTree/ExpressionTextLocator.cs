using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IntelliSun.Collections;

namespace IntelliSun.Text
{
    public abstract class ExpressionTextLocator : ITextLocator
    {
        private const string CountingRegex = @"[^$]\$(\d)";
        private static readonly Regex _countingRegex = new Regex(CountingRegex);

        private readonly string expression;
        private ExpressinPosition position;

        private readonly StringBuilder parameterBuilder;

        private readonly string[] parameters;

        protected ExpressionTextLocator(string expression)
        {
            this.expression = expression;

            var paramsCount = _countingRegex
                .Matches(expression)
                .OfType<Match>()
                .Select(match => (int)Char.GetNumericValue(match.Groups[1].Value[0]))
                .Concat(-1).Max();

            this.parameters = new string[paramsCount + 1];
            this.parameterBuilder = new StringBuilder();

            this.Reset();
        }

        public TextLocatorResult Check(char current)
        {
            var selection = this.expression.Substring(this.position.Index, this.position.Length);
            return this.position.Part == ExpressionPart.Text
                       ? this.MatchText(selection, current)
                       : this.MatchParameter(current);
        }

        private TextLocatorResult MatchParameter(char current)
        {
            var exitChar = this.expression[this.position.Index + 2];
            if (current == exitChar)
                return this.CloseParameter();

            this.parameterBuilder.Append(current);
            return TextLocatorResult.Valid;
        }

        private TextLocatorResult MatchText(string selection, char current)
        {
            if (selection.Length != 1)
                return this.Reset();

            if (selection[0] == current)
                return this.AdvanceSelection();

            if(this.position.Index == 0 && Char.IsWhiteSpace(current))
                return TextLocatorResult.Valid;

            return this.Reset();
        }

        private TextLocatorResult CloseParameter()
        {
            var param = this.position.Parameter;
            if (param != -1)
                this.parameters[param] = this.parameterBuilder.ToString();

            this.position.Index++;
            this.parameterBuilder.Clear();
            return this.AdvanceSelection();
        }

        private TextLocatorResult AdvanceSelection()
        {
            var index = this.position.Index + this.position.Length;
            if (index >= this.expression.Length)
                return this.Reset(true);

            var isParameter = this.IsParameter(index);    
            this.position.Index = index;
            this.position.Length = isParameter ? 2 : 1;
            this.position.Parameter = isParameter ? this.GetParameterIndex() : -1;
            this.position.Part = isParameter ? ExpressionPart.Parameter : ExpressionPart.Text;

            return TextLocatorResult.Valid;
        }

        private bool IsParameter(int startIndex)
        {
            if (startIndex < 0)
                return false;

            if (this.expression.Length <= startIndex + 2)
                return false;

            if (this.expression[startIndex] != '$' || !this.IsNumber(startIndex + 1))
                return false;

            if (startIndex != 0 && this.expression[startIndex - 1] == '$')
                return false;

            return !this.IsParameter(startIndex + 2) && !this.IsParameter(startIndex - 2);
        }

        private int GetParameterIndex()
        {
            var index = this.position.Index + 1;
            if (this.expression.Length <= index || !this.IsNumber(index))
                return -1;

            return (int)Char.GetNumericValue(this.expression, index);
        }

        private bool IsNumber(int index)
        {
            var @char = this.ExpressionAt(index);
            return Char.IsNumber(@char);
        }

        private char ExpressionAt(int index)
        {
            return this.expression.Length <= index ? '\uFFFF' : this.expression[index];
        }

        private TextLocatorResult Reset(bool finished = false)
        {
            this.position = new ExpressinPosition(0, 1);

            return finished ? TextLocatorResult.Match : TextLocatorResult.Invalid;
        }

        protected string Expression
        {
            get { return this.expression; }
        }

        protected string[] Parameters
        {
            get { return this.parameters; }
        }
    }
}