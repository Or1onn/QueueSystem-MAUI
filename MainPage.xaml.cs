namespace QueueSystem;

public class Monkey
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Details { get; set; }
}
public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();

        List<Monkey> Monkeys = new() { };
        Random ran = new();

        for (int i = 0; i < 30; i++)
        {
            Monkeys.Add(new Monkey());
        }
        for (int i = 0; i < 30; i++)
        {
            Monkeys[i].Name = ran.Next(0, 100000).ToString();
            Monkeys[i].Location = ran.Next().ToString();
            Monkeys[i].Details = ran.Next().ToString();
        }
        list.ItemsSource = Monkeys;

    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        //count++;

        //if (count == 1)
        //	CounterBtn.Text = $"Clicked {count} time";
        //else
        //	CounterBtn.Text = $"Clicked {count} times";

        //SemanticScreenReader.Announce(CounterBtn.Text);
    }
}

