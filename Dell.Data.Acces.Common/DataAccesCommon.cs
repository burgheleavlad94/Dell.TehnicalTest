using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Dell.Common;
using Dell.Data.Entities;

namespace Dell.Data.Acces.Common
{
    public abstract partial class DataAccesCommon<T> where T: IIdentifiable,new()
    {
        protected string _constantString = null;
  
        protected string _readAll;
        protected string _readByID;
        protected string _insert;
        protected string _update;

        public DataAccesCommon(string connectionString)
        {
            _constantString = connectionString;
        }

        /// <summary>
        /// Read all data from database
        /// </summary>
        /// <returns></returns>
        public List<T> ReadAll()
        {
            List<T> list = new List<T>();
            DbDataReader reader = null;

            using (var connection = new SqlConnection(_constantString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = _readAll;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    try
                    {
                        connection.OpenEx();
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var t = new T();
                            t.ReadFromReader(reader);
                            list.Add(t);
                        }
                    }
                    finally
                    {
                        reader.CloseEx();
                        connection.CloseEx();
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Read all data from the specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T ReadByID(int id)
        {
            var t = default(T);
            DbDataReader reader = null;

            using(var connection=new SqlConnection(_constantString))
            {
                using(var command = connection.CreateCommand())
                {
                    command.CommandText = _readByID;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    try
                    {
                        connection.OpenEx();
                        SetupSelectParams(command, id);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            t = new T();
                            t.ReadFromReader(reader);
                        }
                    }
                    finally
                    {
                        reader.CloseEx();
                        connection.CloseEx();
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// Insert new data in database
        /// When one customer is added/updated, the application should return a response with Customer fully populated and a generated customer id.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(T entity)
        {
            int retVal = -1;

            using(var connection=new SqlConnection(_constantString))
            {
                using(var command = connection.CreateCommand())
                {
                    command.CommandText = _insert;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    try
                    {
                        connection.OpenEx();
                        SetupInsertParams(command, entity);

                        command.ExecuteNonQuery();
                        retVal = command.GetParamReturnValueEx();

                        var paramID = command.Parameters["@id"].Value;
                        if (paramID != null) entity.ID = (int)paramID;

                        var recVer = command.Parameters["@recordVersion"].Value;
                        if (recVer != null) entity.RecordVersion = (byte[])recVer;
                    }
                    finally
                    {
                        connection.CloseEx();
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        /// Update data from the database at specific id 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(T entity)
        {
            int retVal = -1;

            using(var connection=new SqlConnection(_constantString))
            {
                using(var command = connection.CreateCommand())
                {
                    command.CommandText = _update;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    try
                    {
                        connection.OpenEx();
                        SetupUpdateParams(command, entity);

                        command.ExecuteNonQuery();

                        retVal = command.GetParamReturnValueEx();

                        var recVer = command.Parameters["@recordVersion"].Value;
                        if (recVer != null) entity.RecordVersion = (byte[])recVer;
                    }
                    finally
                    {
                        connection.CloseEx();
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        /// Set parameters for select command
        /// </summary>
        /// <param name="command"></param>
        /// <param name="id"></param>
        protected abstract void SetupSelectParams(DbCommand command, int id);

        /// <summary>
        /// Set parameters for insert command
        /// </summary>
        /// <param name="command"></param>
        /// <param name="entity"></param>
        protected abstract void SetupInsertParams(DbCommand command, T entity);

        /// <summary>
        /// Set parameters for update command
        /// </summary>
        /// <param name="command"></param>
        /// <param name="entity"></param>
        protected abstract void SetupUpdateParams(DbCommand command, T entity);
    }
}
