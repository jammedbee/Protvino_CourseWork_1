using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using PeopleManagementApp.Entities;

namespace CourseWork_1
{
    public partial class MainWindow : Window
    {
        private readonly EmployeeContext context =
            new EmployeeContext();

        private CollectionViewSource employeeViewSource;

        public MainWindow()
        {
            InitializeComponent();
            employeeViewSource = (CollectionViewSource)FindResource(nameof(employeeViewSource));
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await GetTableFromDatabase();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            context.Dispose();
            base.OnClosing(e);
        }

        private async void SaveChangesAsyncButton_Click(object sender, RoutedEventArgs e)
        {
            await context.SaveChangesAsync();
            MessageBox.Show("Все изменения сохранены", "Сообщение");
        }

        private async void AddRowAsyncButton_Click(object sender, RoutedEventArgs e)
        {
            await context.AddAsync(new Employee());
            MessageBox.Show("Запись добавлена", "Сообщение");
        }

        private async void DeleteRowAsyncBottom_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(
                "Вы уверены, что хотите удалить выбранную запись? Отменить действие будет невозможно!",
                "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                context.Employees.Remove(employeeDataGrid.SelectedItem as Employee);
                await context.SaveChangesAsync();
                MessageBox.Show("Запись удалена", "Сообщение");
            }
        }

        private async Task GetTableFromDatabase()
        {
            await context.Database.EnsureCreatedAsync();
            await context.Employees.LoadAsync();
            employeeViewSource.Source = context.Employees.Local.ToObservableCollection();
        }

        private async void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = searchTextBox.Text;

            var result = context.Employees.Local.Where(employee =>
            employee.FirstName.Contains(query) ||
            employee.LastName.Contains(query) ||
            employee.MiddleName.Contains(query) ||
            employee.HomeAddress.Contains(query) ||
            employee.PhoneNumber.Contains(query));

            if ((result == null) || (result.Count() == 0))
            {
                MessageBox.Show("По введённым данным ничего не было найдено");
                await GetTableFromDatabase();
                return;
            }
            employeeViewSource.Source = result.ToList();
        }

        private async void resetSearchAsyncButton_Click(object sender, RoutedEventArgs e)
        {
            searchTextBox.Clear();
            await GetTableFromDatabase();
        }
    }
}
