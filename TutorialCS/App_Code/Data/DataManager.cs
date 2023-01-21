using System;
using System.Data;
using System.Data.Common;
using DayPilot.Web.Ui;

namespace Data
{
    public class DataManager
    {

        private DbDataAdapter CreateDataAdapter(string select)
        {
            DbDataAdapter da = Factory.CreateDataAdapter();
            da.SelectCommand = CreateCommand(select);
            return da;
        }
        
        public DataTable GetBlocks()
        {
            var da = CreateDataAdapter("select * from [Block] order by [BlockId]");
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void CreateAssignment(DateTime start, DateTime end, string note, string color)
        {
            using (DbConnection con = CreateConnection())
            {
                con.Open();

                var cmd = CreateCommand("insert into [Assignment] ([AssignmentStart], [AssignmentEnd], [AssignmentNote], [AssignmentColor]) values (@start, @end, @note, @color)", con);
                AddParameterWithValue(cmd, "start", start);
                AddParameterWithValue(cmd, "end", end);
                AddParameterWithValue(cmd, "note", note);
                AddParameterWithValue(cmd, "color", color);
                cmd.ExecuteNonQuery();

            }
        }

        public void DeleteAssignment(int id)
        {
            using (var con = CreateConnection())
            {
                con.Open();

                var cmd = CreateCommand("delete from [Assignment] where [AssignmentId] = @id", con);
                AddParameterWithValue(cmd, "id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public object GetAssignments(DayPilotCalendar calendar)
        {
            DataTable dt = new DataTable();
            var da = CreateDataAdapter("select * from [Assignment] where NOT (([AssignmentEnd] <= @start) OR ([AssignmentStart] >= @end))");
            AddParameterWithValue(da.SelectCommand, "start", calendar.StartDate);
            AddParameterWithValue(da.SelectCommand, "end", calendar.EndDate.AddDays(1));
            da.Fill(dt);
            return dt;
        }

        public void MoveAssignment(int id, DateTime start, DateTime end)
        {
            using (var con = CreateConnection())
            {
                con.Open();

                var cmd = CreateCommand("update [Assignment] set [AssignmentStart] = @start, [AssignmentEnd] = @end where [AssignmentId] = @id", con);
                AddParameterWithValue(cmd, "id", id);
                AddParameterWithValue(cmd, "start", start);
                AddParameterWithValue(cmd, "end", end);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateAssignment(int id, DateTime start, DateTime end, string note)
        {
            using (var con = CreateConnection())
            {
                con.Open();

                var cmd = CreateCommand("update [Assignment] set [AssignmentStart] = @start, [AssignmentEnd] = @end, [AssignmentNote] = @note where [AssignmentId] = @id", con);
                AddParameterWithValue(cmd, "id", id);
                AddParameterWithValue(cmd, "start", start);
                AddParameterWithValue(cmd, "end", end);
                AddParameterWithValue(cmd, "note", note);
                cmd.ExecuteNonQuery();
            }

        }

        public DataRow GetAssignment(int id)
        {
            var da = CreateDataAdapter("select * from [Assignment] where [Assignment].[AssignmentId] = @id");
            AddParameterWithValue(da.SelectCommand, "id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                return dt.Rows[0];
            }
            return null;
        }


        #region Helper methods
        private string ConnectionString
        {
            get { return Db.ConnectionString(); }
        }

        private DbProviderFactory Factory
        {
            get { return Db.Factory(); }
        }

        private DbConnection CreateConnection()
        {
            DbConnection connection = Factory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            return connection;
        }

        private DbCommand CreateCommand(string text)
        {
            DbCommand command = Factory.CreateCommand();
            command.CommandText = text;
            command.Connection = CreateConnection();

            return command;
        }

        private DbCommand CreateCommand(string text, DbConnection connection)
        {
            DbCommand command = Factory.CreateCommand();
            command.CommandText = text;
            command.Connection = connection;

            return command;
        }

        private void AddParameterWithValue(DbCommand cmd, string name, object value)
        {
            var parameter = Factory.CreateParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = name;
            parameter.Value = value;
            cmd.Parameters.Add(parameter);
        }

        private int GetIdentity(DbConnection c)
        {
            var cmd = CreateCommand(Db.IdentityCommand(), c);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        #endregion

        public DataRow GetBlock(int id)
        {
            var da = CreateDataAdapter("select * from [Block] where [BlockId] = @id");
            AddParameterWithValue(da.SelectCommand, "id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                return dt.Rows[0];
            }
            return null;

        }



        public void UpdateBlock(int id, TimeSpan start, TimeSpan end)
        {
            using (var con = CreateConnection())
            {
                con.Open();

                var cmd = CreateCommand("update [Block] set [BlockStart] = @start, [BlockEnd] = @end where [BlockId] = @id", con);
                AddParameterWithValue(cmd, "id", id);
                AddParameterWithValue(cmd, "start", DateTime.Today.Add(start));
                AddParameterWithValue(cmd, "end", DateTime.Today.Add(end));
                cmd.ExecuteNonQuery();
            }

        }

        public void InsertBlock(int id, TimeSpan start, TimeSpan end)
        {
            using (var con = CreateConnection())
            {
                con.Open();

                var cmd = CreateCommand("insert into [Block] ([BlockId], [BlockStart], [BlockEnd]) values (@id, @start, @end)", con);
                AddParameterWithValue(cmd, "id", id);
                AddParameterWithValue(cmd, "start", DateTime.Today.Add(start));
                AddParameterWithValue(cmd, "end", DateTime.Today.Add(end));
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateAssignmentNote(int id, string note, string color)
        {
            using (var con = CreateConnection())
            {
                con.Open();

                var cmd = CreateCommand("update [Assignment] set [AssignmentNote] = @note, [AssignmentColor] = @color where [AssignmentId] = @id", con);
                AddParameterWithValue(cmd, "id", id);
                AddParameterWithValue(cmd, "note", note);
                AddParameterWithValue(cmd, "color", color);
                cmd.ExecuteNonQuery();
            }
        }
    }
}