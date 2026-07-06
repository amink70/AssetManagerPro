using System;
using System.Windows;
using AssetManagerPro.Models;
using AssetManagerPro.Repositories;

namespace AssetManagerPro.Views
{
    public partial class AddSupplierWindow : Window
    {
        private readonly SupplierRepository _repository = new();
        private Supplier? _editingSupplier;
        public AddSupplierWindow()
        {
            InitializeComponent();
        }
        public AddSupplierWindow(Supplier supplier)
        {
            InitializeComponent();

            _editingSupplier = supplier;

            txtName.Text = supplier.Name;
            txtManagerName.Text = supplier.ManagerName;
            txtPhone.Text = supplier.Phone;
            txtEmail.Text = supplier.Email;
            txtAddress.Text = supplier.Address;
            txtDescription.Text = supplier.Description;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Supplier supplier = new Supplier
            {
                Name = txtName.Text.Trim(),
                ManagerName = txtManagerName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };

            if (_editingSupplier == null)
            {
                _repository.Add(supplier);

                MessageBox.Show("تأمین‌کننده با موفقیت ثبت شد.");
            }
            else
            {
                supplier.Id = _editingSupplier.Id;

                _repository.Update(supplier);

                MessageBox.Show("تأمین‌کننده با موفقیت ویرایش شد.");
            }

            DialogResult = true;
            Close();
        }
    }
}