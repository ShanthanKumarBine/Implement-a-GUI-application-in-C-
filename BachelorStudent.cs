// BachelorStudent.cs
//
// Neng Moua
// 
// BachelorStudent class that extends Student.
using System;

public class BachelorStudent : Student
{

    // private String ScholarYN;

    public BachelorStudent()
        : base()
    {
    }

    public bool Scholar { get; set; }

    public string ScholarStatus
    {
        get
        {
            if (Scholar)
                return "Scholar";
            else
                return "Not Scholar";
        }
    }

    public override string ToString()
    {
        return base.ToString()+ " " + ScholarStatus;
    } // end method ToString

    // readonly property for student type
    public override string StudentType
    {
        get
        {
            return " Bachelor";
        }
    } // no implementation here

    // read-only property to get the registration fee.
    override public decimal RegistrationFee
    {
        get
        {
            const decimal REGISTRATION_BACHELOR_INSTATE = 200;
            const decimal REGISTRATION_BACHELOR_OUTSTATE = 600;
            if (State.Contains("In-State"))
                return REGISTRATION_BACHELOR_INSTATE;
            else
                return REGISTRATION_BACHELOR_OUTSTATE;
        }
    }

    // read-only property to get the tuition rate. 
    override public decimal TuitionRate
    {
        get
        {
            const decimal TUITION_BACHELOR_INSTATE = 350;
            const decimal TUITION_BACHELOR_OUTSTATE = 700;

            if (State.Contains("In-State"))
                return TUITION_BACHELOR_INSTATE;
            else
                return TUITION_BACHELOR_OUTSTATE;
        }
    }


} // end class BachelorStudent