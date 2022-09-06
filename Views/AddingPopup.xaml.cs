using CommunityToolkit.Maui.Views;
using QueueSystem.Models;

namespace QueueSystem.Views;

public partial class AddingPopup : Popup
{
	public AddingPopup()
	{
		InitializeComponent();
	}

    void OnCancelButtonClicked(object sender, EventArgs e) => Close(null);
	void OnSubmitlButtonClicked(object sender, EventArgs e)
	{
		QueueModel model = new() { FIN = FIN.Text, FullName = FullName.Text, Id = 1, IsPaid = true, Queue = 2 };

		Close(model);
	}
}