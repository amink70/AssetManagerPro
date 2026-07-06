using AssetManagerPro.Models;
using AssetManagerPro.Repositories;
using System.Windows;

namespace AssetManagerPro.Views
{
    public partial class AddLocationWindow : Window
    {
        private readonly LocationRepository _locationRepository = new();
        private readonly DepartmentRepository _departmentRepository = new();
        private Location? _editingLocation;

        public AddLocationWindow()
        {
            InitializeComponent();

            cmbDepartment.ItemsSource = _departmentRepository.GetAll();
        }
        public AddLocationWindow(Location location)
        {
            InitializeComponent();

            cmbDepartment.ItemsSource = _departmentRepository.GetAll();

            _editingLocation = location;

            cmbDepartment.SelectedValue = location.DepartmentId;
            txtName.Text = location.Name;
            txtDescription.Text = location.Description;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbDepartment.SelectedItem is not Department department)
            {
                MessageBox.Show("لطفاً دپارتمان را انتخاب کنید.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("نام مکان را وارد کنید.");
                return;
            }

            Location location = new()
            {
                DepartmentId = department.Id,
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };

            if (_editingLocation == null)
            {
                _locationRepository.Add(location);

                MessageBox.Show("مکان با موفقیت ثبت شد.");
            }
            else
            {
                location.Id = _editingLocation.Id;

                _locationRepository.Update(location);

                MessageBox.Show("مکان با موفقیت ویرایش شد.");
            }


            DialogResult = true;
            Close();
        }
    }
}