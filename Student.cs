// Student.cs
//
// Neng Moua
// 
// Student class

using System;

public class Student
{
    // private instance variable for storing first name
    private string firstNameValue;

    // private instance variable for storing last name
    private string lastNameValue;

    // private instance variable for storing entrance date
    private DateTime entranceDateValue;

    // parameter-less constructor
    public Student()
    {
    }

    // constructor
    public Student( string first, string last, string gender, bool residency, DateTime entranceDate, decimal credits, Address homeAddress, PhoneNumber homePhone, PhoneNumber cellPhone)
    {
        this.FirstName = first;
        this.LastName = last;
        this.Gender = gender;
        this.Residency = residency;
        this.Credits = credits;
        this.EntranceDate = entranceDate;
        this.HomeAddress = homeAddress;
        this.HomePhone = homePhone;
        this.CellPhone = cellPhone;
    }

    // property to get and set student's first name
    public string FirstName
    {

        get
        {
            return Util.Capitalize(firstNameValue);
        } // end get
        set
        {
            value = value.Trim().ToUpper();
            if(value.Length < 1)
            {
                throw new ApplicationException("First Name is empty!");
            }
            foreach(Char c in value)
            {
                if (c < 'A' || c > 'Z')
                {
                    throw new ApplicationException("First Name must consit of letters only!");
                }
            }
            firstNameValue = value;
        } // end set
    } // end property FirstName

    // property to get and set student's last name
    public string LastName
    {

        get
        {
            return Util.Capitalize(lastNameValue);
        } // end get
        set
        {
            value = value.Trim().ToUpper();
            if (value.Length < 1)
            {
                throw new ApplicationException("Last Name is empty!");
            }
            foreach (Char c in value)
            {
                if (c < 'A' || c > 'Z')
                {
                    throw new ApplicationException("Last Name must consit of letters only!");
                }
            }
            lastNameValue = value;
        } // end set
    } // end property LastName

    // read-only property to get name
    public string Name
    {
        get
        {
            return LastName + ", " + FirstName;
        } // end get
    } // end property Name

    // property to get and set student's gender
    public string Gender 
    {
        get
        {
            if (IsMale)
                return "Male";
            else
                return "Female";
        } // end get
        set
        {
            value = value.ToUpper();
            if (value == "MALE" || value == "M")
                IsMale = true;
            else if (value == "FEMALE" || value == "F")
                IsMale = false;
        } // end set    
    }

    // property to get and set whether gender is male
    public bool IsMale { get; set; }

    // property to get and set whether gender is female
    public bool IsFemale
    {
        get
        {
            return !IsMale;
        } // end get
        set
        {
            IsMale = !value;
        } // end set
    } // end property IsFemale

    public string Title
    {
        get
        {
            if (IsMale)
                return "Mr.";
            else
                return "Ms.";
        } // end get
    } // end property Title

    public string TitleName
    {
        get
        {
            return Title + " " + Name;
        } // end get
    } // end property TitleName

    // property to get and set student's gender
    public bool Residency { get; set; }

    // property to get and set whether student is in-state
    public string State 
    {
        get
        {
            if (Residency)
                return "In-State";
            else
                return "Out-State";
        }
        
    }


    // property to get and set student's entrancedate
    public DateTime EntranceDate
    {
        get
        {
            return entranceDateValue;
        }

        set
        {
            if (value > DateTime.Now)
            {
                throw new ApplicationException("Student entrance date must be pror to today!");
            }
            entranceDateValue = value;
        }
    }

    // property to get and set student's Date of entrance difference
    public byte Years
    {
        get
        {
            // Get the birth date value for the current year.

            DateTime thisYearEntranceDate = new DateTime(DateTime.Now.Year, EntranceDate.Month, EntranceDate.Day);

            // Calculate and return the year based on if the entrance date has occurred this year or not.

            if (thisYearEntranceDate <= DateTime.Now)
            {
                return (byte)(DateTime.Now.Year - thisYearEntranceDate.Year);
            }
            else
            {
                return (byte)(DateTime.Now.Year - thisYearEntranceDate.Year - 1);
            }
        }
    }

    // property to get and set student's Expenses
    public decimal credits { get; set; }

    public decimal Credits
    {
        get { return credits; }
        
        set
        {
            if (value < 0 || value > 22 )
            {
                throw new ApplicationException("Total Expenses must not be negative or zero and must not be higer than 21!");
            }
            credits = value;
        }
    }

    // property to get and set home address
    public Address HomeAddress { get; set; }

    // property to get and set home phone
    public PhoneNumber HomePhone { get; set; }

    // property to get and set work phone
    public PhoneNumber CellPhone { get; set; }

    virtual public string StudentType
    {
        get { return "Student"; }
    }

    // virtual, read-only property to get the registration fee and tuition rate. Will be overriden by derived classes
    virtual public decimal RegistrationFee
    {
        get { return 0; }
    }

    virtual public decimal TuitionRate
    {
        get { return 0; }
    }

    // re-only property  to get the tuition and total.
    public decimal Tuition
    {
        get
        {
            return TuitionRate * Credits;
        }
    }

    public decimal Total
    {
        get
        {
            return RegistrationFee + Tuition;
        }
    }

    // returns string representation of employee object
    public override string ToString()
    {
        return TitleName + " " + StudentType + " " + Credits.ToString() + " credits " 
            + State + " " + Years.ToString() + " years at CU " + "\t"
            + "Registration: " + RegistrationFee.ToString() + " Tuition: " + Tuition.ToString() + " Total: " + Total.ToString() + "\t"
            + HomeAddress + ". "
            + HomePhone.ToString() + "/"
            + CellPhone.ToString();
    } // end method ToString

}
