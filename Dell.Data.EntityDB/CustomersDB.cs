using Dell.Common;
using Dell.Data.Acces.Common;
using Dell.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dell.Data.EntityDB
{
    public class CustomersDB : DataAccesCommon<Customer>
    {
        string _readByEmail;

        public CustomersDB(string connectionString) : base(connectionString)
        {
            _readAll = "CustomersSelectAll";
            _readByID = "CustomersSelectByID";
            _readByEmail = "CustomersSelectByEmail";
            _insert = "CustomersInsert";
            _update = "CustomersUpdate";
        }

        protected override void SetupInsertParams(DbCommand command, Customer entity)
        {
            command.Parameters.Add(new SqlParameter("@id", entity.ID) { Direction = System.Data.ParameterDirection.Output });
            command.Parameters.Add(new SqlParameter("@name", entity.Name) { Direction = System.Data.ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@emailAddress", entity.EmailAddress) { Direction = System.Data.ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@Age", entity.Age) { Direction = System.Data.ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@retVal", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue });
            command.Parameters.Add(new SqlParameter("@recordVersion", System.Data.SqlDbType.Timestamp) { Direction = System.Data.ParameterDirection.Output });
        }

        protected override void SetupSelectParams(DbCommand command, int id)
        {
            command.Parameters.Add(new SqlParameter("@id", id) { Direction = System.Data.ParameterDirection.Input });
        }

        protected void SetupSelectParams(DbCommand command, string email)
        {
            command.Parameters.Add(new SqlParameter("@emailAddress", email) { Direction = System.Data.ParameterDirection.Input });

        }

        protected override void SetupUpdateParams(DbCommand command, Customer entity)
        {
            command.Parameters.Add(new SqlParameter("@id", entity.ID) { Direction = System.Data.ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@emailAddress", entity.EmailAddress) { Direction = System.Data.ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@Age", entity.Age) { Direction = System.Data.ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@name", entity.Name) { Direction = System.Data.ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@retVal", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue });
            command.Parameters.Add(new SqlParameter("@recordVersion", entity.RecordVersion) { SqlDbType= System.Data.SqlDbType.Timestamp, Direction = System.Data.ParameterDirection.InputOutput });
        }


        /// <summary>
        /// Read all data of the specific e-mail address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public List<Customer> ReadByEmail(string email)
        {
            List<Customer> customers = null;
            DbDataReader reader = null;

            using (var connection = new SqlConnection(_constantString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = _readByEmail;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    try
                    {
                        connection.OpenEx();
                        SetupSelectParams(command, email);
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            customers = new List<Customer>();
                            while (reader.Read())
                            {
                                var customer = new Customer();
                                customer.ReadFromReader(reader);
                                customers.Add(customer);
                            }
                        }
                    }
                    finally
                    {
                        reader.CloseEx();
                        connection.CloseEx();
                    }
                }
            }
            return customers;
        }
    }
}
