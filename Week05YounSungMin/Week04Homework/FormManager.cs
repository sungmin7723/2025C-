using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week04Homework
{
    public partial class FormManager : Form
    {
        // 인스턴스 필드(변수), 멤버변수
        Department[] departments;
        List<Professor> professors;
        Dictionary<string, Student> students;

        List<Grade> testGrade;
        TextBox[] tbxTestScore;

        // 생성자
        // 인스턴스 생성시 초기화가 필요한 코드를 넣어준다.
        public FormManager()
        {
            InitializeComponent();
            departments = new Department[100];
            professors = new List<Professor>();
            students = new Dictionary<string, Student>();
            
            for (int year = 1; year <= 4; year++) {    
                cmbYear.Items.Add(year);
            }

            string[] classes = {"A", "B", "C" };
            for (int Class = 0; Class < classes.Length; Class++) {
                cmbClass.Items.Add(classes[Class]);
            }

            string[] statuses = { "재학", "졸업", "휴학", "퇴학" };
            foreach (string status in statuses) {
                cmbRegStatus.Items.Add(status);
            }

            testGrade = new List<Grade>();
            tbxTestScore = new TextBox[] {
                tbxTestScore1,
                tbxTestScore2,
                tbxTestScore3,
                tbxTestScore4,
                tbxTestScore5,
                tbxTestScore6,
                tbxTestScore7,
                tbxTestScore8,
                tbxTestScore9
            };


        }

        private void btnRegisterDepartment_Click(object sender, EventArgs e)
        {
            // 학과코드랑 이름이 비어있으면 안됨 (구현 완료)
            if (string.IsNullOrEmpty(tbxDepartmentCode.Text)) { 
                tbxDepartmentCode.Focus(); // 포커스 이동
                MessageBox.Show("학과 코드가 비어있습니다.");
                return;
            } else if (string.IsNullOrEmpty(tbxDepartmentName.Text)) { 

                tbxDepartmentName.Focus(); // 포커스 이동
                MessageBox.Show("학과 이름이 비어있습니다.");
                return;
            }

            int index = -1;
            for(int i = 0; i < departments.Length; i++) {
                if (departments[i] == null) {
                    if (index < 0) {
                        index = i;
                    }
                    // break;
                } 
                else {
                    if (departments[i].Code == tbxDepartmentCode.Text) {
                        tbxDepartmentCode.Focus(); // 포커스 이동 (완료
                        MessageBox.Show("학과 코드가 중복되었습니다.");  // 메시지 띄우고 (완료)
                        return;
                    }
                }
                
            }

            if (index < 0) {
                // 메시지 띄우기
                MessageBox.Show("더 이상 신규 학과정보 등록이 불가능합니다.");
                return;
            }

            Department dept = new Department();
            dept.Code = tbxDepartmentCode.Text;
            dept.Name = tbxDepartmentName.Text;

            departments[index] = dept;
            lbxDepartment.Items.Add(dept);

        }

        private void btnRemoveDepartment_Click(object sender, EventArgs e) 
        {
            if (lbxDepartment.SelectedIndex < 0) {
                // 메시지 띄우고(완)
                MessageBox.Show("삭제할 학과를 선택하세요.");
                return;
            }

            // is 연산자, 타입 확인영으로 사용.
            if (lbxDepartment.SelectedItem is Department) {
                var target = (Department)lbxDepartment.SelectedItem;
                for(int i = 0; i < departments.Length; i++) {
                    if (departments[i] != null && departments[i] == target) {
                        departments[i] = null;
                        break;
                    }
                }
                lbxDepartment.Items.RemoveAt(lbxDepartment.SelectedIndex);
                lbxDepartment.SelectedIndex = -1;
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e) 
        {
            switch(tabMain.SelectedIndex) {
                case 0:
                    break;

                case 1:
                    cmbProfessorDepartment.Items.Clear();
                    foreach (var department in  departments) {
                        if (department != null) {
                            cmbProfessorDepartment.Items.Add(department);
                        }
                        cmbProfessorDepartment.SelectedIndex = -1;
                        lbxProfessor.Items.Clear();
                    }
                    break;

                case 2:
                    cmbDepartment.Items.Clear();
                    foreach (var department in departments) {
                        if (department != null) {
                            cmbDepartment.Items.Add(department);
                        }
                        cmbDepartment.SelectedIndex = -1;
                    }

                    cmbAdvisor.Items.Clear();
                    foreach (var professor in professors) {
                        if (professor != null) {
                            cmbAdvisor.Items.Add(professor);
                        }
                        cmbAdvisor.SelectedIndex = -1;
                    }
                    cmbClass.SelectedIndex = -1;


                    break;

                default:
                    break;
            }
        }

        private void cmbProfessorDepartment_SelectedIndexChanged(object sender, EventArgs e) 
        {
            // index값 검사.
            // 구현 조회할 학과를 선택하라는 메세지를 띄우고 종료한다
            if (cmbProfessorDepartment.SelectedIndex < 0)
            {
                // 메시지 띄우고
                return;
            }

            lbxProfessor.Items.Clear();

            // as 형변환 연산자. 
            // 형변환이 정상적이지 않으면 null 변환.
            var department = cmbProfessorDepartment.SelectedItem as Department;

            if (department != null) {
                foreach (var professor in professors) {
                    if (professor != null && professor.DepartmentCode == department.Code) {
                        lbxProfessor.Items.Add(professor);
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            tbxNumber.Text = "";
            tbxName.Text = "";
            tbxBirthYear.Text = "";
            tbxBirthMonth.Text = "";
            tbxBirthDay.Text = "";
            tbxAddress.Text = "";
            tbxContact.Text = "";
            cmbClass.SelectedIndex = -1;
            cmbYear.SelectedIndex = -1;
            cmbRegStatus.SelectedIndex = -1;
            cmbDepartment.SelectedIndex = -1;
            cmbAdvisor.SelectedIndex = -1;
        }

        private void btnRegisterProfessor_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxProfessorName.Text)) {
                tbxProfessorName.Focus();
                MessageBox.Show("교수 이름이 비어있습니다.");
                return;
            }
            else if (string.IsNullOrEmpty(tbxProfessorNumber.Text)) {
                tbxProfessorNumber.Focus();
                MessageBox.Show("교수 번호가 비어있습니다.");
                return;
            }
            else if (cmbProfessorDepartment.SelectedIndex < 0) {
                cmbProfessorDepartment.Focus();
                MessageBox.Show("소속 학과를 선택하세요.");
                return;
            }

            // 교수번호 중복검사
            foreach (var Professor in professors) {
                if (Professor != null && Professor.Number == tbxProfessorNumber.Text) {
                    tbxProfessorNumber.Focus();
                    MessageBox.Show("교수 번호가 중복되었습니다.");
                    return;
                }
            }
            var department = cmbProfessorDepartment.SelectedItem as Department;

            Professor professor = new Professor();
            professor.Number = tbxProfessorNumber.Text;
            professor.Name = tbxProfessorName.Text;
            professor.DepartmentCode = department.Code;

            professors.Add(professor);
            lbxProfessor.Items.Add(professor);

        }

        private void btnRemoveProfessor_Click(object sender, EventArgs e)
        {
            if (lbxProfessor.SelectedIndex < 0) {
                MessageBox.Show("삭제할 교수를 선택하세요.");
                return;
            }
            if (lbxProfessor.SelectedItem is Professor) {
                var target = (Professor)lbxProfessor.SelectedItem;
                for (int i = 0; i < professors.Count; i++) {
                    if (professors[i] != null && professors[i] == target) {
                        professors.Remove(target);
                        break;
                    }
                }
                lbxProfessor.Items.Remove(lbxProfessor.SelectedIndex);
                lbxProfessor.SelectedIndex = -1;
            }
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAdvisor.Items.Clear();

            foreach (var professor in professors) {
                if (professor != null && professor.DepartmentCode == ((Department)cmbDepartment.SelectedItem).Code) {
                    cmbAdvisor.Items.Add(professor);
                }
            }
        }
        Student selectedStudent = null;

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if(selectedStudent == null) {
                RegisterStudent(); // call

            }
            else {
                UpdateStudent(); // call
                
            }

        }

        private void RegisterStudent() // define
        {
            var number = tbxNumber.Text.Trim();

            // 실제 많이 사용하는 방법 1
            if (true == students.ContainsKey(number)) {
                tbxNumber.Focus();
                return;
            }

            Student student = new Student();
            student.Name = tbxName.Text.Trim();
            student.Number = number;

            int birthYear, birthMonth;// , birthDay;

            if(int.TryParse(tbxBirthYear.Text, out birthYear)) {  // 정상일때
                
            }
            else {  // 비정상일때
                return;
            }

            if (int.TryParse(tbxBirthMonth.Text, out birthMonth)) {  // 정상일때

            }
            else {  // 비정상일때
                return;
            }
                                                       // 선언 
            if (int.TryParse(tbxBirthDay.Text, out int birthDay)) {  // 정상일때

            }
            else {  // 비정상일때
                return;
            }

            // 현재시간 : DateTime.Now;
            student.BirthInfo = new DateTime(birthYear, birthMonth, birthDay);

            if (cmbDepartment.SelectedIndex < 0) {
                // cmbDepartment.Focus();
                // return;
                student.DepartmentCode = null;
            }
            else {
                student.DepartmentCode = (cmbDepartment.SelectedItem as Department).Code;
            }

            students[number] = student;        // 딕셔너리에 데이터 추가 방법1
            // students.Add(number, student);  // 딕셔너리에 데이터 추가 방법2
            lbxDictionary.Items.Add(student);
        }   

        private void UpdateStudent() // define
        {
            throw new NotImplementedException();
        }

        private void lbxDictionary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxDictionary.SelectedIndex < 0) {
                return;
            }
            selectedStudent = (lbxDictionary.SelectedItem as Student);

            btnNew_Click(sender, EventArgs.Empty);

            if (selectedStudent != null) {
                DisplaySelectedStudent(selectedStudent);
            }
        }

        private void DisplaySelectedStudent(Student student)
        {
            tbxNumber.ReadOnly = true;
            tbxNumber.Text = student.Number;
            tbxBirthYear.Text = student.BirthInfo.Year.ToString();
        }

        private void btnTestSearchStudent_Click(object sender, EventArgs e)
        {
            selectedStudent = selectedStudentByNumber(tbxTestNumber.Text.Trim());
        }

        private Student selectedStudentByNumber(string number)
        {
            if (students.ContainsKey(number)) {
                return students[number];
            } 
            else {
                return null;
            }
        }
    }
}
