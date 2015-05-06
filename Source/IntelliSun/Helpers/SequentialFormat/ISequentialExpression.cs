using System;

namespace IntelliSun.Helpers
{
    internal interface ISequentialExpression
    {
        string Resolve(object[] args, int index);
    }
}