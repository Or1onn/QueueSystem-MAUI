using QueueSystem.Models;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using SQLite;

namespace QueueSystem.Views;


public partial class MainPage : ContentPage
{
    public ObservableCollection<QueueModel> Queues { get; set; } = new();
    private SQLiteAsyncConnection connection;

    public MainPage()
    {
        InitializeComponent();
        Application.Current.UserAppTheme = AppTheme.Dark;
        DBConnection();
    }

    public async void DBConnection()
    {
        if (connection != null)
            return;

        connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.CacheDirectory, "QueueDB.db3"));
        await connection.CreateTableAsync<QueueModel>();

        var users = await connection.Table<QueueModel>().ToListAsync();

        foreach (var item in users)
        {
            Queues.Add(item);
        }

        //for (int i = 0; i < 1500; i++)
        //{
        //    Queues.Add(new QueueModel { FIN = "6X1FS7X", FullName = "Aliev Raul Kerim", IsPaid = i % 2 == 0, Queue = i + 1 });
        //}
        //await connection.InsertAllAsync(Queues);

        list.ItemsSource = Queues;
    }

    async void OnAddButtonTapped(object sender, EventArgs args)
    {
        await AddButton.ScaleTo(0.75, 100);
        await AddButton.ScaleTo(1, 100);
        var popup = new AddingPopup();

        if (await this.ShowPopupAsync(popup) is QueueModel result)
        {
            if (Queues.Any(obj => obj.FIN == result.FIN))
            {
                await DisplayAlert("Diqqət!", "Belə bir şəxs artıq mövcuddur", "Beli");
            }
            else
            {
                if (Queues.Any())
                    result.Queue = Queues.Last().Queue + 1;
                else
                    result.Queue = 1;

                Queues.Add(result);
                await connection.InsertAsync(result);
            }
        }
    }
    async void OnEditButtonTapped(object sender, EventArgs e)
    {
        Border item = sender as Border;
        await item.ScaleTo(0.75, 100);
        await item.ScaleTo(1, 100);

        var obj = list.SelectedItem as QueueModel;
        var popup = new AddingPopup(obj);


        if (await this.ShowPopupAsync(popup) is QueueModel result)
        {
            if (Queues.Any(obj => obj.FIN == result.FIN && obj != list.SelectedItem))
            {
                await DisplayAlert("Diqqət!", "Belə bir şəxs artıq mövcuddur", "Beli");
            }
            else
            {
                obj.FIN = result.FIN;
                obj.FullName = result.FullName;
                obj.IsPaid = result.IsPaid;

                list.ItemsSource = null;
                list.ItemsSource = Queues;
                await connection.InsertOrReplaceAsync(obj);
            }
        }
    }

    async void OnDeleteButtonTapped(object sender, EventArgs e)
    {
        Border item = sender as Border;
        await item.ScaleTo(0.75, 100);
        await item.ScaleTo(1, 100);
        QueueModel obj = list.SelectedItem as QueueModel;
        await connection.DeleteAsync<QueueModel>(obj.Id);
        int position = Queues.IndexOf(obj);
        for (int i = position + 1; i < Queues.Count; i++)
        {
            Queues[i].Queue--;
        }


        Queues.RemoveAt(position);
    }

    async void OnAllDeleteButtonTapped(object sender, EventArgs e)
    {
        Border item = sender as Border;
        await item.ScaleTo(0.75, 100);
        await item.ScaleTo(1, 100);

        bool answer = await DisplayAlert("Diqqət!", "Siz dəqiq siyahısı bütün öğeleri aradan qaldırılması istəyirsiniz?", "Bəli", "Xeyr");
        if (answer)
        {
            string result = await DisplayPromptAsync("Diqqət!", "Siyahısı bütün öğeleri aradan qaldırılması üçün 'Queues/Delete_All' daxil edin");
            if (result == "Queues/Delete_All")
            {
                await connection.DeleteAllAsync<QueueModel>();
                Queues.Clear();
                list.ItemsSource = Queues;
            }
            else
            {
                await DisplayAlert("Diqqət!", "Doğrulama sözünü səhv yazmısınız", "Bəli");
            }
        }
    }
    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        list.ItemsSource = (from items in Queues
                            where items.FullName.ToLower().Contains(e.NewTextValue.ToLower()) ||
                            items.FIN.ToLower().Contains(e.NewTextValue.ToLower())
                            select items);
    }
}

