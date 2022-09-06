using QueueSystem.Models;
using System.Collections.Specialized;
using System.Globalization;
using QueueSystem.Views;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;

namespace QueueSystem.Views;


public partial class MainPage : ContentPage
{
    public ObservableCollection<QueueModel> Queues { get; set; } = new();
    public MainPage()
    {
        InitializeComponent();
    }

    async void OnAddButtonTapped(object sender, EventArgs args)
    {
        await AddButton.ScaleTo(0.75, 100);
        await AddButton.ScaleTo(1, 100);
        var popup = new AddingPopup();
        var result = await this.ShowPopupAsync(popup);


        if (result != null)
        {
            Queues.Add(result as QueueModel);
            list.ItemsSource = Queues;
        }
        else
        {
            list.ItemsSource = Queues;
        }
    }
    async void OnEditButtonTapped(object sender, EventArgs e)
    {
        Border item = sender as Border;
        await item.ScaleTo(0.75, 100);
        await item.ScaleTo(1, 100);
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            list.ItemsSource = (from items in Queues
                                where items.FullName.ToLower().Contains(e.NewTextValue.ToLower()) ||
                                items.FIN.ToLower().Contains(e.NewTextValue.ToLower())
                                select items);
        }
        catch (Exception)
        {
        }
    }
}

