using System.Collections.ObjectModel;
using System.Windows;
using Vehicles.Core;

namespace Vehicles.WpfApp;

public partial class MainWindow : Window
{
    private readonly ObservableCollection<Vehicle> vehicles = new();

    public MainWindow()
    {
        InitializeComponent();
        TypeComboBox.ItemsSource = new[] { "Car", "Boat", "AmphibiousCar" };
        VehiclesDataGrid.ItemsSource = vehicles;
    }

    private void AddVehicleButton_Click(object sender, RoutedEventArgs e)
    {
        StatusTextBlock.Text = string.Empty;
        string type = TypeComboBox.SelectedItem?.ToString() ?? string.Empty;

        bool success = VehicleFactory.TryCreate(
            type,
            NameTextBox.Text,
            MileageTextBox.Text,
            FuelTextBox.Text,
            out Vehicle? vehicle,
            out string errorMessage);

        if (!success || vehicle is null)
        {
            StatusTextBlock.Text = errorMessage;
            return;
        }

        vehicles.Add(vehicle);
        LogListBox.Items.Insert(0, $"Lisatud {vehicle.TypeName}: {vehicle.Name}.");
        ClearInputs();
    }

    private void MoveButton_Click(object sender, RoutedEventArgs e)
    {
        StatusTextBlock.Text = string.Empty;

        if (VehiclesDataGrid.SelectedItem is not Vehicle selectedVehicle)
        {
            StatusTextBlock.Text = "Vali tabelist sõiduk.";
            return;
        }

        try
        {
            string logMessage = selectedVehicle.Move(10);
            LogListBox.Items.Insert(0, logMessage);
            VehiclesDataGrid.Items.Refresh();
        }
        catch (InvalidOperationException exception)
        {
            StatusTextBlock.Text = exception.Message;
            LogListBox.Items.Insert(0, $"Liikumine ebaõnnestus: {exception.Message}");
        }
        catch (ArgumentOutOfRangeException exception)
        {
            StatusTextBlock.Text = exception.Message;
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        StatusTextBlock.Text = string.Empty;

        if (VehiclesDataGrid.SelectedItem is not Vehicle selectedVehicle)
        {
            StatusTextBlock.Text = "Vali tabelist sõiduk.";
            return;
        }

        vehicles.Remove(selectedVehicle);
        LogListBox.Items.Insert(0, $"Kustutatud {selectedVehicle.TypeName}: {selectedVehicle.Name}.");
    }

    private void ClearInputs()
    {
        TypeComboBox.SelectedIndex = -1;
        NameTextBox.Clear();
        MileageTextBox.Text = "0";
        FuelTextBox.Text = "20";
        NameTextBox.Focus();
    }
}
