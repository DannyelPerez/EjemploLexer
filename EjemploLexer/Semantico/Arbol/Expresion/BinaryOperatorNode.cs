using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public abstract class BinaryOperatorNode: ExpressionNode
    {
        public ExpressionNode LeftOperand { get; set; }
        public ExpressionNode RightOperand { get; set; }
    }
}
