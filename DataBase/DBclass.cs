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

        private DataTable[] dataTables = new DataTable[4];//to cache data from all tables in data base 
        
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
        /// <param name="q">Question.</param>
        public void Insert(Question q)//this method to insert data to database 
        {
            Open_connection();//open connection to server via Open_connection() method
           
            switch (q.Question_type)//index of groupbox that determine type of question : 0 => slider , 1 => smiley, 2 => stars
            {
                case "Slider":
                    Slider slider = (Slider)q;
                    command.CommandText = string.Format("insert into questions values ('{0}',{1},'{2}',{3})", q.Question_text, q.Question_order,"Slider", q.ID);
                    command.ExecuteNonQuery();//execute command

                    command.CommandText = string.Format("insert into Slider values ({0},{1},{2},'{3}','{4}',{5})", q.Question_order, slider.Start,slider.End,slider.Start_Caption,slider.End_Caption, q.ID);
                    command.ExecuteNonQuery();//execute command
                    break;
                case "Smiley":
                    Smiley smiley = (Smiley)q;
                    command.CommandText = string.Format("insert into questions values ('{0}',{1},'{2}',{3})", q.Question_text, q.Question_order, "Smiley", q.ID);
                    command.ExecuteNonQuery();//execute command
                    command.CommandText = string.Format("insert into Smiley values ({0},{1},{2})", q.Question_order,smiley.Faces, q.ID);
                    command.ExecuteNonQuery();//execute command
                    break;
                case "Stars":
                    Stars stars = (Stars)q;
                    command.CommandText = string.Format("insert into questions values ('{0}',{1},'{2}',{3})", q.Question_text, q.Question_order, "Stars", q.ID);
                    command.ExecuteNonQuery();//execute command
                    command.CommandText = string.Format("insert into Stars values ({0},{1},{2})", q.Question_order,stars.Star, q.ID);
                    command.ExecuteNonQuery();//execute command
                    break;
            }
        }
        /// <summary>
        /// Updates the specified question.
        /// </summary>
        /// <param name="q">question.</param>
        public void Update(Question q)
        {
            command.CommandText = string.Format("update questions set question_text ='{0}' ,question_order ={1} where id={2}", q.Question_text, q.Question_order, q.ID);
            Open_connection();
            command.ExecuteNonQuery();//execute command
            switch (q.Question_type)
            {
                case "Slider":
                    Slider slider = (Slider)q;
                    command.CommandText = string.Format("update Slider set Start_Value ={0},End_Value ={1},Start_Value_Caption ='{2}',End_Value_Caption ='{3}',question_order ={4} where id={5}",slider.Start, slider.End, slider.Start_Caption, slider.End_Caption, q.Question_order, q.ID);
                    command.ExecuteNonQuery();
                    break;
                case "Smiley":
                    Smiley smiley = (Smiley)q;
                    command.CommandText = string.Format("update Smiley set Num_Faces ={0},question_order={1} where id={2}",smiley.Faces, q.Question_order, q.ID);
                    command.ExecuteNonQuery();
                    break;
                case "Stars":
                    Stars stars = (Stars)q;
                    command.CommandText = string.Format("update Stars set Num_Stars ={0},question_order={1} where id={2}",stars.Star, q.Question_order, q.ID);
                    command.ExecuteNonQuery();
                    break;
            }
        }
        /// <summary>
        /// Deletes the specified question.
        /// </summary>
        /// <param name="type">type of question.</param>
        /// <param name="id">id of question.</param>
        public void Delete(string type, int id)//method to delete a row that contain a specific question order 
        {
            Open_connection();

            command.CommandText = string.Format("delete from {0} where id={1}", type, id);//sql command
            command.ExecuteNonQuery();//execute command 

            command.CommandText = string.Format("delete from questions where id= {0}", id);//change sql command text 
            command.ExecuteNonQuery();//execute command 
        }
        public void Delete(int id)
        {
            Open_connection();
            command.CommandText = string.Format("select * from questions where id={0}", id);//sql command
            SqlDataAdapter _Adapter = new SqlDataAdapter(command);
            DataTable _Table = new DataTable();
            _Adapter.Fill(_Table);
            string type = _Table.Rows[0].ItemArray[2].ToString();
            Delete(type, id);
        }
        /// <summary>
        /// extract orderes of saved questions
        /// </summary>
        /// <returns> 
        /// table of orderes
        /// </returns>
        public DataTable Orders()
        {
            DataTable Temp_table = new DataTable();
            Open_connection();
            command.CommandText = "select question_order from questions";
            SqlDataAdapter temp = new SqlDataAdapter(command);
            temp.Fill(Temp_table);
            return Temp_table;
        }
        /// <summary>
        /// Loads the database and save it in tables.
        /// </summary>
        /// <returns>
        /// table contain questions only
        /// </returns>
        public DataTable load()//this method used to load all data from database and cache them
        {
            Open_connection();

            SqlDataAdapter dataAdapter;//store blocks of data from data base 
            command.CommandText = "select * from questions";//return rows from questions table 
            dataAdapter = new SqlDataAdapter(command);//execute command and save it in adapter
            dataTables[0] = new DataTable();//define object of class datatable to save all rows from  questions table
            dataAdapter.Fill(dataTables[0]);//fill data table with data retrived from database 

            command.CommandText = "select * from Slider";//return rows from Slider table 
            dataAdapter = new SqlDataAdapter(command);
            dataTables[1] = new DataTable();//define object of class datatable to save all rows from  Slider table
            dataAdapter.Fill(dataTables[1]);

            command.CommandText = "select * from Smiley";//return rows from Smiley table 
            dataAdapter = new SqlDataAdapter(command);
            dataTables[2] = new DataTable(); ;//define object of class datatable to save all rows from  Smiley table
            dataAdapter.Fill(dataTables[2]);

            command.CommandText = "select * from Stars";//return rows from Stars table 
            dataAdapter = new SqlDataAdapter(command);
            dataTables[3] = new DataTable(); ;//define object of class datatable to save all rows from  Stars table
            dataAdapter.Fill(dataTables[3]);

            return dataTables[0].DefaultView.ToTable(false, "question_text");//extract one column from data table ;

        }
        /// <summary>
        /// Extracts the row of specified quesion using id.
        /// </summary>
        /// <param name="id">The id of selected question.</param>
        /// <param name="index">The index of question types.</param>
        /// <returns>.
        /// first row : return row from question table
        /// second row : return row from Slider or Smiley or Stars table depends on the index of question table
        /// </returns>
        public DataRow[] extract_row(int id, int index)//return two rows one from question table and another from specific table determined by index 
        {
            DataRow[] temp = new DataRow[2];
            temp[0] = dataTables[0].Select(string.Format("Convert(id,'System.String') LIKE '%{0}%'", id)).First();//Like method compare two elements with same type so question order is int type and order converted to string implcitly , we must convert question_order to string first 
            temp[1] = dataTables[index].Select(string.Format("Convert(id,'System.String') LIKE '%{0}%'", id)).First();//Like method compare two elements with same type so question order is int type and order converted to string implcitly , w
            return temp;
        }
        /// <summary>
        /// Extracts the row of specified quesion using id.
        /// </summary>
        /// <param name="id">The id of selected question.</param>
        /// <returns>.
        /// first row : return row from question table
        /// second row : return row from Slider or Smiley or Stars table depends on the index of question table
        /// </returns>
        public DataRow[] extract_row(int id)
        {
            DataRow temp;
            temp = dataTables[0].Select(string.Format("Convert(id,'System.String') LIKE '%{0}%'",id)).First();
            int _index = Table_Index(temp.ItemArray[2].ToString());
            return extract_row(id,_index);
        }
        /// <summary>
        /// return question table with all properties.
        /// </summary>
        /// <returns></returns>
        public DataTable question_table()//return  question table
        {
            return dataTables[0];
        }
        private int Table_Index (string type)
        {
            switch (type)
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
    }
}
