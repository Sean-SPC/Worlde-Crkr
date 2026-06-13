namespace GUI;

public partial class Form1 : Form
{
    int width = Screen.PrimaryScreen!.Bounds.Width;
    int height = Screen.PrimaryScreen!.Bounds.Height;
    private int txtHeight = 250;
    private int ctrlWidth = 120;
    public Form1()
    {
        InitializeComponent();

        this.AutoSize = false;
        this.Size = new Size((int)(width*0.5), (int)(height*0.5));

        // PAGE PANEL //

        FlowLayoutPanel pagePanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            Padding = new Padding(20)
        };

        this.Controls.Add(pagePanel);

        // TITLE PANEL //

        FlowLayoutPanel titlePanel = new FlowLayoutPanel
        {
            Padding = new Padding(20)
        };

        pagePanel.Controls.Add(titlePanel);

        Label title = new Label
        {
            Text = "Wordle Crkr",
            Font = new Font("Consolas", 72, FontStyle.Bold),
            AutoSize = true
        };

        // INPUT PANEL //

        FlowLayoutPanel inputPanel = new FlowLayoutPanel
        {
            Padding = new Padding(20)
        };

        pagePanel.Controls.Add(inputPanel);
        
        TextBox CreateTextBox(int tabIndex, int? x = null, int? y = null)
        {
            return new TextBox
            {
                TabIndex = tabIndex,
                AutoSize = false,
                Size = new Size(ctrlWidth, txtHeight),
                Margin = new Padding(20),
                Font = new Font("Consolas", 64),
                TextAlign = HorizontalAlignment.Center,
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        TextBox txtBox1 = CreateTextBox(1);
        TextBox txtBox2 = CreateTextBox(2);
        TextBox txtBox3 = CreateTextBox(3);
        TextBox txtBox4 = CreateTextBox(4);
        TextBox txtBox5 = CreateTextBox(5);

        inputPanel.Controls.Add(txtBox1);
        inputPanel.Controls.Add(txtBox2);
        inputPanel.Controls.Add(txtBox3);
        inputPanel.Controls.Add(txtBox4);
        inputPanel.Controls.Add(txtBox5);

        // BUTTON PANEL //
        
        FlowLayoutPanel btnPanel = new FlowLayoutPanel
        {
            Padding = new Padding(20)
        };

        pagePanel.Controls.Add(btnPanel);

        Button CreateButton(int tabIndex, int? x = null, int? y = null)
        {
            return new Button
            {
                AutoSize = false,
                Size = new Size(ctrlWidth, 80),
                TabIndex = tabIndex,
                Text = "Button"
            };
        }

        Button btn1 = CreateButton(6);
        Button btn2 = CreateButton(7);
        Button btn3 = CreateButton(8);
        Button btn4 = CreateButton(9);
        Button btn5 = CreateButton(10);

        btnPanel.Controls.Add(btn1);
        btnPanel.Controls.Add(btn2);
        btnPanel.Controls.Add(btn3);
        btnPanel.Controls.Add(btn4);
        btnPanel.Controls.Add(btn5);
    }
}
