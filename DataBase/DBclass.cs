using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Questions;
namespace DataBase
{
    public class DBclass
    {
        private SqlConnection connection;

        private SqlCommand command;

        private DataTable[] Tables = new DataTable[4];//to cache data from all tables in data base 

        public DBclass()
        {
            connection = new SqlConnection(); //define object of sqlconnection class
            command = new SqlCommand();//define object of sqlcommand class
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;//save connection string that saved in configuration file
            command.Connection = connection;
     
        }
        /// <summary>
        ///  open SQL connection if it is closed otherwise leave it open 
        /// </summary>
        public void Open_connection()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }
        /// <summary>
        ///  insert data to database 
        /// </summary>
        /// <param name="question_type_index">Index of the question type.</param>
        /// <param name="question">Question.</param>
        public void Insert(Question question)//this method to insert data to database 
        {
            int Id=-1;
            Open_connection();//open connection to server via Open_connection() method
            command.CommandText = string.Format("insert into questions values ('{0}',{1},'{2}')", question.Question_text, question.Question_order, question.Question_type);
            command.ExecuteNonQuery();//execute command

            command.CommandText = "select max(Id) from questions";
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Id = reader.GetInt32(0);
                }
            }
            switch (question.Question_type)//index of groupbox that determine type of question : 0 => slider , 1 => smiley, 2 => stars
            {
                case "Slider":
                    Slider slider = (Slider)question;
                    command.CommandText = string.Format("insert into Slider values ({0},{1},{2},'{3}','{4}',{5})", question.Question_order, slider.Start, slider.End, slider.Start_Caption, slider.End_Caption, Id);
                    command.ExecuteNonQuery();//execute command
                    break;
                case "Smiley":
                    Smiley smiley = (Smiley)question;
                    command.CommandText = string.Format("insert into Smiley values ({0},{1},{2})", question.Question_order, smiley.Smiles,Id);
                    command.ExecuteNonQuery();//execute command
                    break;
                case "Stars":
                    Stars stars = (Stars)question;
                    command.CommandText = string.Format("insert into Stars values ({0},{1},{2})", question.Question_order, stars.Star, Id);
                    command.ExecuteNonQuery();//execute command
                    break;
            }
        }
        /// <summary>
        /// Updates the specified question.
        /// </summary>
        /// <param name="question">question.</param>
        public void Update(Question question)
        {
            Open_connection();
            command.CommandText = string.Format("update questions set question_text ='{0}' ,question_order ={1} where id={2}", question.Question_text, question.Question_order, question.ID);
            command.ExecuteNonQuery();//execute command
            switch (question.Question_type)
            {
                case "Slider":
                    Slider slider = (Slider)question;
                    command.CommandText = string.Format("update Slider set Start_Value ={0},End_Value ={1},Start_Value_Caption ='{2}',End_Value_Caption ='{3}',question_order ={4} where Question_Id={5}", slider.Start, slider.End, slider.Start_Caption, slider.End_Caption, question.Question_order, question.ID);
                    command.ExecuteNonQuery();
                    break;
                case "Smiley":
                    Smiley smiley = (Smiley)question;
                    command.CommandText = string.Format("update Smiley set Num_Faces ={0},question_order={1} where Question_Id={2}", smiley.Smiles, question.Question_order, question.ID);
                    command.ExecuteNonQuery();
                    break;
                case "Stars":
                    Stars stars = (Stars)question;
                    command.CommandText = string.Format("update Stars set Num_Stars ={0},question_order={1} where Question_Id={2}", stars.Star, question.Question_order, question.ID);
                    command.ExecuteNonQuery();
                    break;
            }
        }
        /// <summary>
        /// Deletes the specified question.
        /// </summary>
        /// <param name="type">type of question.</param>
        /// <param name="Id">id of question.</param>
        public void Delete(int Id)//method to delete a row that contain a specific question order 
        {
            Open_connection();
            command.CommandText = string.Format("delete from questions where id= {0}", Id);//change sql command text 
            command.ExecuteNonQuery();//execute command 
        }
       
        /// <summary>
        /// extract orderes of saved questions
        /// </summary>
        /// <returns> 
        /// table of orderes
        /// </returns>
        public List <int> Orders()
        {
            List<int> Orders = new List<int>();
            DataTable Temp_Table  = Tables[0].DefaultView.ToTable(false, "Question_order");
            foreach (DataRow row in Temp_Table.Rows)
            {
                Orders.Add((int)row.ItemArray[0]);
            }
            return Orders;
        }
        /// <summary>
        /// Loads the database and save it in tables.
        /// </summary>
        /// <returns>
        /// table contain questions only
        /// </returns>
        public DataTable Load()//this method used to load all data from database and cache them
        {
            Tables[0] = new DataTable();//define object of class datatable to save all rows from  questions table
            Tables[1] = new DataTable();//define object of class datatable to save all rows from  Slider table
            Tables[2] = new DataTable(); ;//define object of class datatable to save all rows from  Smiley table
            Tables[3] = new DataTable(); ;//define object of class datatable to save all rows from  Stars table

            Open_connection();
            SqlDataAdapter dataAdapter;//store blocks of data from data base 
            command.CommandText = "select * from questions";//return rows from questions table 
            dataAdapter = new SqlDataAdapter(command);//execute command and save it in adapter
            dataAdapter.Fill(Tables[0]);//fill data table with data retrived from database 

            command.CommandText = "select * from Slider";//return rows from Slider table 
            dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(Tables[1]);

            command.CommandText = "select * from Smiley";//return rows from Smiley table 
            dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(Tables[2]);

            command.CommandText = "select * from Stars";//return rows from Stars table 
            dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(Tables[3]);

            return Tables[0].DefaultView.ToTable(false, "question_text");//extract one column from data table ;

        }
        /// <summary>
        /// Extracts the row of specified quesion using id.
        /// </summary>
        /// <param name="Id">The id of selected question.</param>
        /// <param name="Index">The index of question types.</param>
        /// <returns>.
        /// first row : return row from question table
        /// second row : return row from Slider or Smiley or Stars table depends on the index of question table
        /// </returns>
        public DataRow[] Extract_row(int Id, int Index)//return two rows one from question table and another from specific table determined by index 
        {
            DataRow[] temp = new DataRow[2];
            temp[0] = Tables[0].Select(string.Format("Convert(Id,'System.String') LIKE '%{0}%'", Id)).First();//Like method compare two elements with same type so question order is int type and order converted to string implcitly , we must convert question_order to string first 
            temp[1] = Tables[Index].Select(string.Format("Convert(Question_Id,'System.String') LIKE '%{0}%'", Id)).First();//Like method compare two elements with same type so question order is int type and order converted to string implcitly , w
            return temp;
        }
        /// <summary>
        /// Extracts the row of specified quesion using id.
        /// </summary>
        /// <param name="Id">The id of selected question.</param>
        /// <returns>.
        /// first row : return row from question table
        /// second row : return row from Slider or Smiley or Stars table depends on the index of question table
        /// </returns>
        public DataRow[] Extract_row(int Id)
        {
            DataRow temp;
            temp = Tables[0].Select(string.Format("Convert(Id,'System.String') LIKE '%{0}%'", Id)).First();
            int _index = Table_Index(temp.ItemArray[2].ToString());
            return Extract_row(Id, _index);
        }
        /// <summary>
        /// return question table with all properties.
        /// </summary>
        /// <returns></returns>
        public DataTable Question_table()//return  question table
        {
            return Tables[0];
        }
        /// <summary>
        /// Find value pair of question type
        /// </summary>
        /// <param name="Type"></param>
        /// <returns>
        /// Table index 
        /// </returns>
        private int Table_Index(string Type)
        {
            switch (Type)
            {
                case "Slider":
                    return 1;
                case "Smiley":
                    return 2;
                case "Stars":
                    return 3;
            }
            return -1;
        }
        /// <summary>
        /// Reset Ids of questions for all tables in database
        /// 
        /// </summary>
        public void Reset_IDs()
        {
            command.CommandText= "DBCC CHECKIDENT ('questions', RESEED,-1);DBCC CHECKIDENT ('Stars', RESEED,-1);DBCC CHECKIDENT ('Smiley', RESEED,-1);DBCC CHECKIDENT ('Slider', RESEED,-1);";
            command.ExecuteNonQuery();
        }
    }
}
