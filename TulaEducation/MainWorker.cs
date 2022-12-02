using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;
using SQLite;
using TulaEducation.Entitys;

namespace TulaEducation
{
    public class MainWorker
    {
        public DataBaseWorker DataBase { get; private set; }
        public Navigation MainFrame { get; private set; }
        public string GetDatabasePath
        {
            get
            {
                string databaseName = "data.db";
                var r = AppDomain.CurrentDomain.BaseDirectory;
                var t = Path.Combine(@r, databaseName);
                return t;
            }
        }

        public MainWorker(Frame frame)
        {
            DataBase = new DataBaseWorker(GetDatabasePath);
            MainFrame = new Navigation(frame);
        }

    }
    public class Navigation
    {
        public Action Update;
        private Frame _frame;
        public Navigation(Frame frame)
        {
            _frame = frame;
            _frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }
        public void ViewNewPage(Page newPage)
        {
            _frame.Navigate(newPage);
        }

        public object GetFrameContent() => _frame.Content;
        public void FrameGoTo(Type type, Action action = null)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                if (_frame.Content.GetType().ToString() == type.ToString())
                {
                    action?.Invoke();
                    Update?.Invoke();
                    return;
                }
                else if (_frame.CanGoBack)
                {
                    _frame.GoBack();
                    FrameGoTo(type, action);
                }

            })).Wait();
        }

        public void FrameGoBack()
        {
            Update?.Invoke();
            if(_frame.CanGoBack)
                _frame.GoBack();
        }
    }
    public class DataBaseWorker
    {
        private SQLiteConnection _database;

        public DataBaseWorker(string dataBasePath)
        {
            _database = new SQLiteConnection(dataBasePath);
            _database.CreateTable<EducationInfo>();
        }

        /// <summary>
        /// Добавляет запись в таблицу
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Insert<T>(T data, out string message) where T : new()
        {
            message = "ok";
            try
            {
                _database.Insert(data);
                return true;
            }
            catch (Exception excpt)
            {
                message = excpt.Message;
                return false;
            }
        }
        /// <summary>
        /// Вернуть все записи таблицы
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetEntitys<T>() where T : new()
        {
            return _database.Table<T>();
        }
        /// <summary>
        /// Вернуть запись по ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<T>(int id) where T : DataBaseEntity, new()
        {
            return _database.Table<T>().FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Обновить запись
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public void ChangeData<T>(object data) where T : new()
        {
            _database.Update(data);
        }
        public void Delete<T>(int id) where T : DataBaseEntity, new()
        {
            _database.Delete(new T { Id = id });
        }
    }
}
