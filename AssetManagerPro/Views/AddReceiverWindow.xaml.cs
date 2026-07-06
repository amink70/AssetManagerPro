using AssetManagerPro.Models;
using AssetManagerPro.Repositories;
using System.Windows;

namespace AssetManagerPro.Views
{
    public partial class AddReceiverWindow : Window
    {
        private readonly ReceiverRepository _receiverRepository = new();
        private readonly DepartmentRepository _departmentRepository = new();

        private Receiver? _editingReceiver;

        public AddReceiverWindow()
        {
            InitializeComponent();

            cmbDepartment.ItemsSource = _departmentRepository.GetAll();
        }
        public AddReceiverWindow(Receiver receiver)
        {
            InitializeComponent();

            cmbDepartment.ItemsSource = _departmentRepository.GetAll();

            _editingReceiver = receiver;

            txtFullName.Text = receiver.FullName;
            txtPersonnelCode.Text = receiver.PersonnelCode;
            txtPhone.Text = receiver.Phone;
            txtEmail.Text = receiver.Email;

            cmbDepartment.SelectedValue = receiver.DepartmentId;

            chkIsActive.IsChecked = receiver.IsActive;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbDepartment.SelectedItem is not Department department)
            {
                MessageBox.Show("لطفاً دپارتمان را انتخاب کنید.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("نام و نام خانوادگی را وارد کنید.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPersonnelCode.Text))
            {
                MessageBox.Show("کد پرسنلی را وارد کنید.");
                return;
            }

            Receiver receiver = new()
            {
                FullName = txtFullName.Text.Trim(),
                PersonnelCode = txtPersonnelCode.Text.Trim(),
                DepartmentId = department.Id,
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                IsActive = chkIsActive.IsChecked == true
            };

            if (_editingReceiver == null)
            {
                _receiverRepository.Add(receiver);

                MessageBox.Show("گیرنده با موفقیت ثبت شد.");
            }
            else
            {
                receiver.Id = _editingReceiver.Id;

                _receiverRepository.Update(receiver);

                MessageBox.Show("گیرنده با موفقیت ویرایش شد.");
            }

            DialogResult = true;
            Close();
        }
    }
}