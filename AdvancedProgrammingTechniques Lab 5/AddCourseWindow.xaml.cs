using System.Collections.ObjectModel;
using System.Windows;

namespace Lab4
{
    public partial class AddCourseWindow : Window
    {
        public static AddCourseWindow? Instance { get; private set; }
        public ObservableCollection<Student> AvailableStudents { get; }
        public ObservableCollection<Student> SelectedStudents { get; } = new ObservableCollection<Student>();
        public Class? NewClass { get; private set; }

        public AddCourseWindow(ObservableCollection<Student> students, ObservableCollection<Teacher> teachers)
        {
            InitializeComponent();
            Instance = this;
            AvailableStudents = students;
            AvailableStudentsListBox.ItemsSource = AvailableStudents;
            AttendanceListBox.ItemsSource = SelectedStudents;
            TeacherComboBox.ItemsSource = teachers;
            DataContext = this;
        }

        public void AddStudentToAttendance(Student student)
        {
            if (!SelectedStudents.Contains(student))
            {
                SelectedStudents.Add(student);
            }
        }

        private void AddSelected_Click(object sender, RoutedEventArgs e)
        {
            foreach (Student student in AvailableStudentsListBox.SelectedItems)
            {
                if (!SelectedStudents.Contains(student))
                {
                    SelectedStudents.Add(student);
                }
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CourseNameTextBox.Text) && TeacherComboBox.SelectedItem is Teacher teacher)
            {
                NewClass = new Class
                {
                    Name = CourseNameTextBox.Text,
                    Teacher = teacher,
                    Students = new ObservableCollection<Student>(SelectedStudents)
                };
                DialogResult = true;
            }
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}