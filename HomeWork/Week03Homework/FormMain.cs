using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week03Homework
{
    public partial class FormMain : Form
    {
        public char lastChar;
        public void SetLastChar()
        {
            if (lblNumbers.Text.Length > 0)
            {
                lastChar = lblNumbers.Text[lblNumbers.Text.Length - 1];

            }
        }
        public FormMain()
        {
            InitializeComponent();

        }
        private void lblNumbers_Click(object sender, EventArgs e)
        {
            // 계산식이 표시되는 레이블
        }

        private void lblExpression_Click(object sender, EventArgs e)
        {
            // '=' 버튼을 눌렀을 때 계산식과 결과 값을 표시하는 레이블
        }


        // 숫자 버튼 클릭 시 lblNumbers에 해당 숫자를 추가
        private void btnNum0_Click(object sender, EventArgs e)
        {
            lblNumbers.Text += 0;
        }

        private void btnNum1_Click(object sender, EventArgs e)
        {
            lblNumbers.Text += 1;
        }

        private void btnNum2_Click(object sender, EventArgs e)
        {
            lblNumbers.Text += 2;
        }

        private void btnNum3_Click(object sender, EventArgs e)
        {
            lblNumbers.Text += 3;
        }

        private void btnNum4_Click(object sender, EventArgs e)
        {
            lblNumbers.Text += 4;
        }

        private void btnNum5_Click(object sender, EventArgs e)
        {
            lblNumbers.Text += 5;
        }

        private void btnNum6_Click(object sender, EventArgs e)
        {
            lblNumbers.Text += 6;
        }

        private void btnNum7_Click(object sender, EventArgs e)
        {
            lblNumbers.Text += 7;
        }

        private void btnNum8_Click(object sender, EventArgs e)
        {
            lblNumbers.Text += 8;
        }

        private void btnNum9_Click(object sender, EventArgs e)
        {
            lblNumbers.Text += 9;
        }

        // 연산자 버튼 클릭 시 호출되는 함수
        private void btnDiv_Click(object sender, EventArgs e)
        {
            AddOperator('/');
        }

        private void btnMul_Click(object sender, EventArgs e)
        {
            AddOperator('*');
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            AddOperator('-');
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddOperator('+');
        }

        // '=' 버튼 클릭 시 계산 수행
        private void btnCal_Click(object sender, EventArgs e)
        {
            if (lblNumbers.Text == "")  // 입력된 값이 없을 경우 오류 메시지 출력
            {
                MessageBox.Show("숫자를 먼저 입력하세요.");
                return;
            }
            SetLastChar();  // 마지막 입력된 문자 업데이트
            if (lastChar == '+' || lastChar == '-' || lastChar == '*' || lastChar == '/')
            {
                MessageBox.Show("마지막 문자열이 연산자입니다.");
                return;
            }
            float result = 0; // 계산 실행
            result = calculate();
            lblExpression.Text = lblNumbers.Text + " = " + result.ToString(); // lblNumbers의 결과 lblExpress로 이동
            lblNumbers.Text = result.ToString(); // 결과를 입력창에 반영


        }

        // 연산자 추가 로직(연산자 중복 입력 방지)
        private void AddOperator(char op)
        {
            if (lblNumbers.Text == "")
            {
                MessageBox.Show("숫자를 먼저 입력하세요.");
                return;
            }

            SetLastChar(); // 마지막 문자 업데이트

            if (lastChar == '+' || lastChar == '-' || lastChar == '*' || lastChar == '/')
            {
                // 마지막 문자가 연산자라면 교체
                lblNumbers.Text = lblNumbers.Text.Substring(0, lblNumbers.Text.Length - 1) + op;
            }
            else
            {
                lblNumbers.Text += op;
            }
        }

        // 입력된 계산식을 해석하여 연산 수행
        public float calculate()
        {   

            string expTerms = lblNumbers.Text;
            if (expTerms.Length > 0 && expTerms[0] == '-')
            {
                expTerms = "0" + expTerms; // 음수일 경우 앞에 0 추가
            }
            string operators = "";
            string numbers = "";
            string num = "";

            for (int i = 0; i < expTerms.Length; i++) 
            {
                char term = expTerms[i];

                // ASCII 코드 값으로 숫자인지 확인 // 숫자와 소수점 처리
                if ((term >= '0' && term <= '9') || term == '.')

                {
                    num += term;
                }
                // 연산자인 경우
                else
                {
                    numbers += num + ","; // 숫자를 저장하고
                    num = "";
                    operators += term; // 연산자를 저장
                }
            }
            numbers += num; // 마지막 숫자 추가

            string[] numArr = numbers.Split(','); // 숫자 배열로 변환
            float result = float.Parse(numArr[0]);

            // 연산 수행
            for (int i = 0; i < operators.Length; i++)
            {
                float nextNum = float.Parse(numArr[i + 1]);

                switch (operators[i])
                {
                    case '+':
                        result += (nextNum * (float)1.0);
                        break;
                    case '-':
                        result -= nextNum;
                        break;
                    case '*':
                        result *= nextNum;
                        break;
                    case '/':
                        if (nextNum == 0)
                        {
                            MessageBox.Show("0으로 나누기 불가능");
                        }
                        else
                        {
                            result /= nextNum;
                        }
                        break;
                }
            }
            return result; // 최종 결과 반환


        }
    }
}
