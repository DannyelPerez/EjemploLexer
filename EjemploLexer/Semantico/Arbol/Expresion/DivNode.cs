using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public class DivNode: BinaryOperatorNode
    {
        public override int Evaluate()
        {
            return LeftOperand.Evaluate() / RightOperand.Evaluate();
        }
    }
}
