using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace T_SQLSwissKnifeScriptDom
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<ParseError> errors = null;

            TextReader reader = new StreamReader(@"..\..\..\SQL\CreateProc_HumanResources_uspUpdateEmployeeHireInfo.sql");
            TSql120Parser parser = new TSql120Parser(true);
            TSqlFragment tree = parser.Parse(reader, out errors);

            foreach (ParseError err in errors)
            {
                Console.WriteLine(err.Message);
            }

            tree.Accept(new myVisitor());

            reader.Dispose();
        }
    }

    class myVisitor:TSqlConcreteFragmentVisitor
    {
        public override void ExplicitVisit(ExecuteStatement node)
        {
            Console.WriteLine((node.ExecuteSpecification.ExecutableEntity as ExecutableProcedureReference).ProcedureReference.ProcedureReference.Name.BaseIdentifier.Value);

            base.ExplicitVisit(node);
        }
    }


}
