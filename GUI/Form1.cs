namespace GUI;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        
        TextBox CreateTextBox(int tabIndex, int x, int y)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                TabIndex = tabIndex,
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        TextBox txtBox1 = CreateTextBox(11, 20, 10);
        TextBox txtBox2 = CreateTextBox(12, 100, 10);
        TextBox txtBox3 = CreateTextBox(13, 180, 10);
        TextBox txtBox4 = CreateTextBox(14, 260, 10);
        TextBox txtBox5 = CreateTextBox(15, 340, 10);

        Controls.Add(txtBox1);
        Controls.Add(txtBox2);
        Controls.Add(txtBox3);
        Controls.Add(txtBox4);
        Controls.Add(txtBox5);
    }
}
