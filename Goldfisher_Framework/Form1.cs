using System;

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

        private void FormGoldfisher_Load(object sender, EventArgs e)
        {

        }
	}
}
