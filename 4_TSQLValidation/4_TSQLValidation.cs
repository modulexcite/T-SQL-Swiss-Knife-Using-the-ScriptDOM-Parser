using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace T_SQLSwissKnifeScriptDom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextReader rdr = new StringReader(textBox1.Text);

            IList<ParseError> errors = null;
            TSql120Parser parser = new TSql120Parser(true);
            TSqlFragment tree = parser.Parse(rdr, out errors);

            foreach(ParseError err in errors)
            {
                Console.WriteLine(err.Message);
            }

            MyVisitor checker = new MyVisitor();

            tree.Accept(checker);

            if(false == checker.containsOnlySelects)
            {
                MessageBox.Show("The code contains something other than SELECT statement");
            }
            else
            {
                MessageBox.Show("Looks ok!");
            }
        }
    }

    class MyVisitor : TSqlFragmentVisitor
    {
        internal bool containsOnlySelects = true;

        public override void Visit(TSqlStatement node)
        {
            if ((node as SelectStatement) == null)
            {
                containsOnlySelects = false;
            }

            base.Visit(node);
        }
    }
}
