using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Lab4
{
    public partial class AddCourseWindow : Window
    {
        public Class? NewClass { get; private set; } // Nullable to avoid CS8618
        private ObservableCollection<Student> availableStudents;
        private ObservableCollection<Student> selectedStudents = new ObservableCollection<Student>();
        private ObservableCollection<Teacher> teachers;

        public AddCourseWindow(ObservableCollection<Student> students, ObservableCollection<Teacher> teachers)
        {
            InitializeComponent();
            this.teachers = teachers;
            TeacherCombo.ItemsSource = teachers;
            availableStudents = new ObservableCollection<Student>(students); // Copy
            AvailableStudentsList.ItemsSource = availableStudents;
            SelectedStudentsList.ItemsSource = selectedStudents;
        }

        private void MoveToSelected_Click(object sender, MouseButtonEventArgs e)
        {
            if (AvailableStudentsList.SelectedItem is Student selected)
            {
                availableStudents.Remove(selected);
                selectedStudents.Add(selected);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CourseNameText.Text) && TeacherCombo.SelectedItem is Teacher teacher)
            {
                NewClass = new Class
                {
                    Name = CourseNameText.Text,
                    Teacher = teacher,
                    Students = selectedStudents
                };
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter a course name and select a teacher.");
            }
        }
    }
}