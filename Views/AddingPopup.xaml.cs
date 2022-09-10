using CommunityToolkit.Maui.Views;
using QueueSystem.Models;

namespace QueueSystem.Views;

public partial class AddingPopup : Popup
{
    public bool IsRadioButtonsSelected { get; set; } = false;
    public bool IsPaymentSelected { get; set; }

    public AddingPopup()
    {
        InitializeComponent();
    }

    public AddingPopup(QueueModel model)
    {
        InitializeComponent();
        FullName.Text = model.FullName;
        FIN.Text = model.FIN;

        switch (model.IsPaid)
        {
            case true:
                Paid.IsChecked = true;
                break;
            default:
                NotPaid.IsChecked = true;
                break;
        }
    }

    void OnCancelButtonClicked(object sender, EventArgs e) => Close(null);
    void OnSubmitlButtonClicked(object sender, EventArgs e)
    {
        if (FIN.Text.Length == 7)
        {
            QueueModel model = new() { FIN = FIN.Text.ToUpper(), FullName = FullName.Text, IsPaid = IsPaymentSelected };

            Close(model);
        }
        else
        {
            FIN_Border.StrokeThickness = 1.5;
            FIN_Border.Stroke = Color.FromRgba("#FF0000");
            FIN.Text = "";
            FIN.Placeholder = "FİN uzunluğu 7-ə bərabər olmalıdır";
        }
    }

    public void InputsCheck(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(FIN.Text) || string.IsNullOrEmpty(FullName.Text) || IsRadioButtonsSelected == false)
        {
            Submit.IsEnabled = false;
        }
        else
        {
            Submit.IsEnabled = true;
        }
    }


    void RadioButtonSelected(object sender, EventArgs e)
    {
        IsRadioButtonsSelected = true;

        var value = sender as RadioButton;

        switch (value.Content)
        {
            case "Ödənilib":
                IsPaymentSelected = true;
                break;
            default:
                IsPaymentSelected = false;
                break;
        }

        InputsCheck(sender, e);
    }
}