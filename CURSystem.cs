// Payroll.cs
//
// Huimin Zhao
// 
// Payroll System Main Form.

using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace CURSystem
{
    public partial class CURSystem : Form
    {
        public CURSystem()
        {
            InitializeComponent();

            // populate the state combobox
            foreach (string state in Address.StateNames)
                cmbState.Items.Add(state);

            // clear all fields
            clear();
        }

        private void PayrollSystemForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit the system?",
                "Continental Unviersity Registration System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                e.Cancel = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void abroutPayrollSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Continental Unviersity Registration System V1.0, by Neng Moua",
                "About Continental Unviersity Registration System", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The printing function is still under construction!",
                "Continental Unviersity Registration System", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void pageSetupStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The page setup function is still under construction!",
                "Continental Unviersity Registration System", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The print preview function is still under construction!",
                "Continental Unviersity Registration System", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstStudents.Items.Count == 0)
                return;

            // object for serializing Records in binary format
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream output = null; // stream for writing to a file

            DialogResult result;
            string fileName; // name of file to save data

            // create dialog box enabling user to save file
            using (SaveFileDialog fileChooser = new SaveFileDialog())
            {
                fileChooser.CheckFileExists = false; // allow user to create file
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; // get specified file name
            }

            // exit event handler if user clicked "Cancel"
            if (result == DialogResult.Cancel)
                return;

            // show error if user specified invalid file
            if (fileName == "" || fileName == null)
            {
                MessageBox.Show("Invlaid File Name", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // save file via FileStream if user specified valid file
            try
            {
                // open file with write access
                output = new FileStream(fileName,
                   FileMode.OpenOrCreate, FileAccess.Write);
                // save records to file
                foreach (Student item in lstStudents.Items)
                {
                    formatter.Serialize(output, item);
                }
            } // end try
            // handle exception if there is a problem opening the file
            catch (IOException)
            {
                // notify user if file does not exist
                MessageBox.Show("Error opening file", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            } // end catch
            // handle exception if there is a problem in serialization
            catch (SerializationException)
            {
                MessageBox.Show("Error writing to file", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            } // end catch
            finally
            {
                if (output != null)
                    output.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // object for deserializing Record in binary format
            BinaryFormatter reader = new BinaryFormatter();
            FileStream input = null; // stream for reading from a file

            
            DialogResult result;
            string fileName; // name of file containing data

            // create dialog box enabling user to open file
            using (OpenFileDialog fileChooser = new OpenFileDialog())
            {
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; // get specified file name
            }


            // exit event handler if user clicked Cancel
            if (result == DialogResult.Cancel)
                return;

            // show error if user specified invalid file
            if (fileName == "" || fileName == null)
            {
                MessageBox.Show("Invalid File Name", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // create FileStream to obtain read access to file
                input = new FileStream(
                   fileName, FileMode.Open, FileAccess.Read);

                // read records from file
                lstStudents.Items.Clear();
                Student student = (Student)reader.Deserialize(input);
                while (student != null)
                {
                    lstStudents.Items.Add(student);
                    student = (Student)reader.Deserialize(input);
                }
            }
            catch
            {
            }
            finally
            {
                if (input != null)
                    input.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Make sure an item is selected in the list view.
            if (lstStudents.SelectedItems.Count == 0)
            {
                return;
            }

            if (MessageBox.Show("Are you sure you want to update the following student?\n" +
                (Student)lstStudents.SelectedItem,
               "Continental Unviersity Registration System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if(add())
                lstStudents.Items.Remove(lstStudents.SelectedItem);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Make sure an item is selected in the list view.
            if (lstStudents.SelectedItems.Count == 0)
            {
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete the following student?\n" +
                (Student)lstStudents.SelectedItem,
               "Continental Unviersity Registration System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                lstStudents.Items.Remove(lstStudents.SelectedItem);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void lstStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make sure an item is selected in the list view.
            if (lstStudents.SelectedItems.Count == 0)
            {
                return;
            }

            Student student = (Student)lstStudents.SelectedItem;
            clear();
            displayStudent(student);
        }

        // clear all fields
        private void clear()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            radMale.Checked = true;
            chkResident.Checked = false;
            txtCredit.Text = "";
            dtpBirthDate.Text = DateTime.Now.ToShortDateString();
            txtAge.Text = "";
            txtStreet.Text = "";
            txtCity.Text = "Milwaukee";
            cmbState.Text = "WISCONSIN";
            txtZip.Text = "53201";
            txtHomePhone.Text = "(414)";
            txtCellPhone.Text = "(414)"; 
            radBachelor.Checked = true;
            txtAdvisorFirstName.Text = "";
            txtAvisorLastName.Text = "";
            chkScholarProgram.Checked = false;
            txtStudentType.Text = "";
            txtRegistrationFee.Text = "";
            txtTuitionRatePerCredit.Text = "";
            txtTuition.Text = "";
            txtTotal.Text = "";
        }

        // add a new student
        private bool add()
        {
            Student student;
            if (radBachelor.Checked)
                student = new BachelorStudent();
            else
                student = new MasterStudent();
            if (getInputs(student) == true)
            {
                lstStudents.Items.Add(student);
                displayStudent(student);
                return true;
            }
            return false;
        }

        // get input fields of Student
        private bool getInputs(Student student)
        {
            try
            {
                student.FirstName = txtFirstName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,	Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            try
            {
                student.LastName = txtLastName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            try
            {
                student.HomePhone = new PhoneNumber(txtHomePhone.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHomePhone.Focus();
                return false;
            }

            try
            {
                student.CellPhone = new PhoneNumber(txtCellPhone.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCellPhone.Focus();
                return false;
            }

            student.IsMale = radMale.Checked;

            student.Residency = chkResident.Checked;
            
            try
            {
                student.EntranceDate = dtpBirthDate.Value;
                txtAge.Text = student.Years.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpBirthDate.Focus();
                return false;
            }
            
            try
            {
                student.HomeAddress = new Address(txtStreet.Text, txtCity.Text, cmbState.Text, txtZip.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbState.Focus();
                return false;
            }

            if (student is BachelorStudent)
            {
                try
                {
                    if (chkScholarProgram.Checked == true)
                    ((BachelorStudent)student).Scholar = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    chkScholarProgram.Focus();
                    return false;
                }
            }
            else if(student is MasterStudent)
            {
                try
                {
                    if (txtAdvisorFirstName.Text == "")
                        txtAdvisorFirstName.Text = "";
                    ((MasterStudent)student).AdvisorFirstName = txtAdvisorFirstName.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAdvisorFirstName.Focus();
                    return false;
                }
                try
                {
                    if (txtAvisorLastName.Text == "")
                        txtAvisorLastName.Text = "";
                    ((MasterStudent)student).AdvisorLastName = txtAvisorLastName.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAvisorLastName.Focus();
                    return false;
                }                
            }

            return true;
        }

        // display employee
        private void displayStudent(Student student)
        {
            txtFirstName.Text = student.FirstName;
            txtLastName.Text = student.LastName;
            txtCredit.Text = student.credits.ToString();
            txtHomePhone.Text = student.HomePhone.ToString();
            txtCellPhone.Text = student.CellPhone.ToString();
            chkResident.Checked = student.Residency;
            radMale.Checked = student.IsMale;
            radFemale.Checked = student.IsFemale;
            dtpBirthDate.Text = student.EntranceDate.ToShortDateString();
            txtAge.Text = student.Years.ToString();
            txtStreet.Text = student.HomeAddress.Street;
            txtCity.Text = student.HomeAddress.City;
            cmbState.Text = student.HomeAddress.State;
            txtZip.Text = student.HomeAddress.Zip;
            txtStudentType.Text = student.StudentType;
            txtRegistrationFee.Text = student.RegistrationFee.ToString("C");
            txtTuitionRatePerCredit.Text = student.TuitionRate.ToString("C");
            txtTuition.Text = student.Tuition.ToString("C");
            txtTotal.Text = student.Total.ToString("C");
            if (student is BachelorStudent)
            {
                radBachelor.Checked = true;
                chkScholarProgram.Checked = true;
            }
            else
            {
                radMaster.Checked = true;
                txtAdvisorFirstName.Text = ((MasterStudent)student).AdvisorFirstName.ToString();
                txtAvisorLastName.Text = ((MasterStudent)student).AdvisorLastName.ToString();
            }
        }

        private void CURSystemForm_Load(object sender, EventArgs e)
        {

        }
    }
}