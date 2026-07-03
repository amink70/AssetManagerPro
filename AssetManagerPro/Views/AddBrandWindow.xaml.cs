using System.Windows;
using AssetManagerPro.Models;
using AssetManagerPro.Repositories;

namespace AssetManagerPro.Views
{
    public partial class AddBrandWindow : Window
    {
        private readonly BrandRepository _repository = new();

        public AddBrandWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Brand brand = new Brand
            {
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };

            if (_editingBrand == null)
            {
                _repository.Add(brand);
                MessageBox.Show("برند با موفقیت ثبت شد.");
            }
            else
            {
                brand.Id = _editingBrand.Id;

                _repository.Update(brand);

                MessageBox.Show("برند با موفقیت ویرایش شد.");
            }

            DialogResult = true;
            Close();

            
        }
        private Brand? _editingBrand;

        public AddBrandWindow(Brand brand)
        {
            InitializeComponent();

            _editingBrand = brand;

            txtName.Text = brand.Name;
            txtDescription.Text = brand.Description;
        }
    }
}