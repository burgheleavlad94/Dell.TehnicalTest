using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace Dell.Data.Entities
{
    public class Customer : PropertyChangedNotification , IIdentifiable
    {
        private int? _age;

        [Range(16,100)]
        public int? Age
        {
            get { return _age; }

            set
            {
                if (value.HasValue && value<= 16)
                    throw new Exception("The age must be grater than 14");
                else
                    _age = value;
            }
        }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Email Address is Invalid")]
        [StringLength(50)]
        public string EmailAddress
        {
            get { return GetValue(() => EmailAddress); }
            set { SetValue(() => EmailAddress, value); }
        }

        public int ID
        {
            get { return GetValue(() => ID); }
            set { SetValue(() => ID, value); }
        }

        [StringLength(50)]
        [Required(ErrorMessage = "Name is required")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Name contains invalid letters")]
        public string Name
        {
            get { return GetValue(() => Name); }
            set { SetValue(() => Name, value); }
        }

        public byte[] RecordVersion
        {
            get { return GetValue(() => RecordVersion); }
            set { SetValue(() => RecordVersion, value); }
        }

        public int Errors { get; set; }

        public void ReadFromReader(DbDataReader reader)
        {
            if (reader == null)
                throw new Exception("Reader was null");

            try
            {
                ID = (int)reader["ID"];
                Name = reader["Name"] == DBNull.Value ? Name = null : (string)reader["Name"];
                EmailAddress = reader["EmailAddress"] == DBNull.Value ? EmailAddress = null : (string)reader["EmailAddress"];
                Age = reader["Age"] == DBNull.Value ? Age = null : (int)reader["Age"];
                RecordVersion = (byte[])reader["RecordVersion"];
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
