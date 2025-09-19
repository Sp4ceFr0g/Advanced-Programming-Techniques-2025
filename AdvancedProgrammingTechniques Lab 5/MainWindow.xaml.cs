using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab4
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
        public ObservableCollection<Teacher> Teachers { get; set; } = new ObservableCollection<Teacher>();
        public ObservableCollection<Class> Courses { get; set; } = new ObservableCollection<Class>();

        public MainWindow()
        {
            InitializeComponent();
            StudentsList.ItemsSource = Students;
            TeachersList.ItemsSource = Teachers;
            CoursesList.ItemsSource = Courses;
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FirstNameTextBox.Text) && !string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                var newStudent = new Student
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text
                };
                Students.Add(newStudent);
                FirstNameTextBox.Text = string.Empty;
                LastNameTextBox.Text = string.Empty;
            }
        }

        private void RemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Student selected)
            {
                Students.Remove(selected);
            }
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TeacherFirstNameTextBox.Text) && !string.IsNullOrWhiteSpace(TeacherLastNameTextBox.Text))
            {
                var newTeacher = new Teacher
                {
                    FirstName = TeacherFirstNameTextBox.Text,
                    LastName = TeacherLastNameTextBox.Text
                };
                Teachers.Add(newTeacher);
                TeacherFirstNameTextBox.Text = string.Empty;
                TeacherLastNameTextBox.Text = string.Empty;
            }
        }

        private void RemoveTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Teacher selected)
            {
                Teachers.Remove(selected);
            }
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddCourseWindow(Students, Teachers);
            addWindow.ShowDialog();
            if (addWindow.NewClass != null)
            {
                Courses.Add(addWindow.NewClass);
                foreach (var student in addWindow.SelectedStudents)
                {
                    Students.Remove(student);
                }
            }
        }

        private void RemoveCourse_Click(object sender, RoutedEventArgs e)
        {
            if (CoursesList.SelectedItem is Class selected)
                Courses.Remove(selected);
        }

        private void CoursesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            RemoveCourseButton.IsEnabled = CoursesList.SelectedItem != null;
        }

        private void CopyExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (StudentsList.SelectedItem is Student selectedStudent)
            {
                System.Windows.Clipboard.SetData("Student", selectedStudent);
                e.Handled = true; // Mark as handled to prevent further processing
            }
        }

        private void CanCopyExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = StudentsList.SelectedItem != null;
            e.Handled = true; // Mark as handled
        }

        private void PasteExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (System.Windows.Clipboard.ContainsData("Student") && AddCourseWindow.Instance != null)
            {
                var student = System.Windows.Clipboard.GetData("Student") as Student;
                if (student != null)
                {
                    AddCourseWindow.Instance.AddStudentToAttendance(student);
                    Students.Remove(student);
                    e.Handled = true; // Mark as handled
                }
            }
        }

        private void CanPasteExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = System.Windows.Clipboard.ContainsData("Student") && AddCourseWindow.Instance != null;
            e.Handled = true; // Mark as handled
        }
    }

    [Serializable]
    public class Student
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
    }

    public class Teacher
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
    }

    public class Class
    {
        public string Name { get; set; } = string.Empty;
        public Teacher? Teacher { get; set; }
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
    }
}