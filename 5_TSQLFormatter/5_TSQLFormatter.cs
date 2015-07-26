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

            Sql120ScriptGenerator scrGen = new Sql120ScriptGenerator();
            string formattedSQL = null;
            scrGen.GenerateScript(tree, out formattedSQL);

            textBox2.Text = formattedSQL;

            rdr.Dispose();
        }
    }
}
