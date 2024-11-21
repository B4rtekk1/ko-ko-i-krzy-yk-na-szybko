using System.Windows.Forms.VisualStyles;

namespace Kółko_i_krzyżyk_na_szybko
{
    public partial class Form1 : Form
    {
        Button[,] buttons;
        readonly int margin = 10;
        int move;

        public Form1()
        {
            InitializeComponent();
            Generate();
            Setup();
        }

        void Generate()
        {
            buttons = new Button[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button button = new Button()
                    {
                        Height = 100,
                        Width = 100,
                        Location = new Point(i * 100 + margin, j * 100 + margin),
                        Font = new Font(FontFamily.GenericSerif, 23),
                        Tag = (i, j)

                    };
                    button.Click += Button_Click;
                    this.Controls.Add(button);
                    buttons[i, j] = button;
                }
            }
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (move % 2 == 0 && string.IsNullOrEmpty(clickedButton.Text))
            {
                clickedButton.Text = "O";
            }
            else if(move % 2 == 1 && string.IsNullOrEmpty(clickedButton.Text))
            {
                clickedButton.Text = "X";
            }
            if(move >= 4)
            {
                if(CheckWin())
                {
                    DialogResult dg = MessageBox.Show($"Gracz {(move % 2 == 0 ? "O" : "X")} wygrywa!\n Zagrać ponownie?", "Koniec gry", MessageBoxButtons.YesNo);
                    if (dg == DialogResult.Yes)
                    {
                        Setup();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                else if(move == 9)
                {
                    DialogResult dg = MessageBox.Show($"Remis! \n Zagrać ponownie?", "Koniec gry", MessageBoxButtons.YesNo);
                    if(dg == DialogResult.Yes)
                    {
                        Setup();
                    }
                    else
                    { 
                        Application.Exit(); 
                    }
                }
            }
            move++;
        }
        bool CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Text == buttons[i, 1].Text && buttons[i, 1].Text == buttons[i, 2].Text && !string.IsNullOrEmpty(buttons[i, 0].Text))
                {
                    return true;
                }
            }
            for(int i = 0; i < 3; i++)
            {
                if (buttons[0, i].Text == buttons[1, i].Text && buttons[1, i].Text == buttons[2, i].Text && !string.IsNullOrEmpty(buttons[0, i].Text))
                {
                    return true;
                }
            }
            if (buttons[0,0].Text == buttons[1, 1].Text && buttons[1, 1].Text == buttons[2, 2].Text && !string.IsNullOrEmpty(buttons[0, 0].Text))
            {
                return true;
            }
            if (buttons[0, 2].Text == buttons[1, 1].Text && buttons[1, 1].Text == buttons[2, 0].Text && !string.IsNullOrEmpty(buttons[0, 2].Text))
            {
                return true;
            }
            return false;
        }

        void Setup()
        {
            move = 0;
            foreach (Button button in this.Controls)
            {
                button.Text = "";
            }

        }
    }
}
