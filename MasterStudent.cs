// MasterStudent.cs
//
// Neng Moua
// 
// MasterStudent class that extends Student.
using System;

public class MasterStudent : Student
{
    // private instance variable for storing first & last name 
    private string AdvisorFirstNameValue;

    private string AdvisorLastNameValue;

    // parameter-less constructor
    public MasterStudent()
        : base()
    {
    }

    // Property to get and set student's Advisor Full Name
    public string AdvisorFirstName
    {

        get
        {
            return Util.Capitalize(AdvisorFirstNameValue);
        } // end get
        set
        {
            value = value.Trim().ToUpper();
            if (value.Length < 1)
            {
                throw new ApplicationException("First name is empty!");
            }
            foreach (char c in value)
            {
                if (c < 'A' || c > 'Z')
                {
                    throw new ApplicationException("First name must consit of letters only");
                }
            }
            AdvisorFirstNameValue = value;
        } // end set
    } // end property FirstName

    // property to get and set patient's last name
    public string AdvisorLastName
    {

        get
        {
            return Util.Capitalize(AdvisorLastNameValue);
        } // end get
        set
        {
            value = value.Trim().ToUpper();
            if (value.Length < 1)
            {
                throw new ApplicationException("Last name is empty!");
            }
            foreach (char c in value)
            {
                if (c < 'A' || c > 'Z')
                {
                    throw new ApplicationException("Last name must consit of letters only");
                }
            }
            AdvisorLastNameValue = value;
        } // end set
    } // end property LastName

    public string AdvisorName
    {
        get
        {
            return AdvisorLastName + ", " + AdvisorFirstName;
        }
    }
    public override string ToString()
    {
        return base.ToString() + "Advisor: " + AdvisorName;
    } // end method ToString

    // readonly property for student type
    public override string StudentType
    {
        get
        {
            return "Master";
        }
    } // no implementation here

    // read-only property to get the registration fee.
    override public decimal RegistrationFee
    {
        get
        {
            const decimal REGISTRATION_MASTER_INSTATE = 300;
            const decimal REGISTRATION_MASTER_OUTSTATE = 900;
            if (State.Contains("In-State"))
                return REGISTRATION_MASTER_INSTATE;
            else
                return REGISTRATION_MASTER_OUTSTATE;
        }
    }

    // read-only property to get the tuition rate. 
    override public decimal TuitionRate
    {
        get
        {
            const decimal TUITION_MASTER_INSTATE = 450;
            const decimal TUITION_MASTER_OUTSTATE = 900;

            if (State.Contains("In-State"))
                return TUITION_MASTER_INSTATE;
            else
                return TUITION_MASTER_OUTSTATE;
        }
    }


} // end class OutPatient

