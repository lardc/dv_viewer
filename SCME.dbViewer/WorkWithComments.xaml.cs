using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using SCME.Types;

namespace SCME.dbViewer
{
    /// <summary>
    /// Interaction logic for WorkWithComments.xaml
    /// </summary>
    public partial class WorkWithComments : Window, INotifyPropertyChanged
    {
        private int[] FDevIDArray;
        private bool FWasSaved = false;

        public WorkWithComments(int[] devIDArray)
        {
            InitializeComponent();

            this.Owner = Application.Current.MainWindow;

            this.FDevIDArray = devIDArray;
            this.Title = string.Concat(Properties.Resources.DeviceCommentsOneString, ": ", this.DeviceCodeBydevIDArray(devIDArray));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public DataView DeviceComments
        {
            get
            {
                DataTable dt = new DataTable();
                DbRoutines.FillDataTableByDeviceComments(dt, this.FDevIDArray);

                return dt.DefaultView;
            }
        }

        public bool ShowModal()
        {
            MainWindow main = (MainWindow)this.Owner;

            if (Common.Routines.IsUserCanReadCreateComments(main.PermissionsLo))
            {
                lbComment.Visibility = Visibility.Visible;
                tbComment.Visibility = Visibility.Visible;
                BtOk.Visibility = Visibility.Visible;
            }
            else
            {
                lbComment.Visibility = Visibility.Collapsed;
                tbComment.Visibility = Visibility.Collapsed;
                BtOk.Visibility = Visibility.Collapsed;
            }

            bool? result = this.ShowDialog();

            return this.FWasSaved;
        }

        private string DeviceCodeBydevIDArray(int[] devIDArray)
        {
            string result = string.Empty;

            foreach (int devID in devIDArray)
            {
                if (result != string.Empty)
                    result = string.Concat(result, ", ");

                result = string.Concat(result, DbRoutines.DeviceCodeByDevID(devID));
            }

            return result;
        }

        private void BtOk_Click(object sender, RoutedEventArgs e)
        {
            //сохраняем в базу данных введённый пользователем комментарий для всех элементов группы
            foreach (int devID in this.FDevIDArray)
            {
                DbRoutines.SaveToDeviceComment(devID, ((MainWindow)this.Owner).FUserID, this.tbComment.Text);
            }

            this.tbComment.Clear();

            this.FWasSaved = true;

            OnPropertyChanged();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.DialogResult = false;
        }
    }
}
