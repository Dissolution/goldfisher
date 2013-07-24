using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Goldfisher
{
	public partial class FormGoldfisher : Form
	{
		public FormGoldfisher()
		{
			InitializeComponent();
		}

		private void btnBelcher_Click(object sender, EventArgs e)
		{
			var fisher = new DefaultFisher();

		}
	}
}
