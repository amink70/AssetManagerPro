using System.Windows;
using AssetManagerPro.Models;
using AssetManagerPro.Repositories;

namespace AssetManagerPro.Views
{
    public partial class AddCategoryWindow : Window
    {
        private readonly CategoryRepository _repository = new();
        private Category? _editingCategory;
        public AddCategoryWindow()
        {
            InitializeComponent();
        }
        public AddCategoryWindow(Category category)
        {
            InitializeComponent();

            _editingCategory = category;

            txtName.Text = category.Name;
            txtDescription.Text = category.Description;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Category category = new Category
            {
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };

            if (_editingCategory == null)
            {
                _repository.Add(category);

                MessageBox.Show("دسته‌بندی با موفقیت ثبت شد.");
            }
            else
            {
                category.Id = _editingCategory.Id;

                _repository.Update(category);

                MessageBox.Show("دسته‌بندی با موفقیت ویرایش شد.");
            }

            DialogResult = true;
            Close();
        }
       
    }
}