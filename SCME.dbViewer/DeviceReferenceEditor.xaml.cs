using SCME.Types;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SCME.dbViewer
{
    /// <summary>
    /// Interaction logic for DeviceReferenceEditor.xaml
    /// </summary>
    public partial class DeviceReferenceEditor : Window
    {
        public DeviceReferenceEditor()
        {
            InitializeComponent();

            this.Owner = Application.Current.MainWindow;
        }

        public bool? ShowModal(int? deviceReferenceID, out int? createdDeviceReferenceID, int itav, int deviceTypeID, string constructive, string modification, int? idrmMax, decimal? utmMax, int? qrrMax, int? igtMax, decimal? ugtMax, int? tjMax, int? prsmMin, string caseType, decimal? utmCorrection)
        {
            //данная реализация принимает на вход идентификатор записи deviceReferenceID и её, отображает их значения в данной форме
            //в out параметре createdDeviceReferenceID возвращается идентификатор созданной записи. если запись редактировалась или удалялась - возвращает null
            //возвращает True - реквизиты принятой записи были обновлены пользователем - пользователь нажал кнопку OK. на момент получения такого результата в базу данных уже было выполнено сохранение этих изменений;
            //возвращает False - пользователь закрыл форму, т.е. отказался от редактирования

            createdDeviceReferenceID = null;
            this.TbItav.Text = itav.ToString();
            this.TbConstructive.Text = constructive;

            string[] deviceTypeItem = this.CmbDeviceType.Items.OfType<string[]>().FirstOrDefault(x => int.Parse(x[0]) == deviceTypeID);
            if (deviceTypeItem == null)
            {
                this.CmbDeviceType.SelectedIndex = -1;
            }
            else
                this.CmbDeviceType.SelectedItem = deviceTypeItem;

            this.TbModification.Text = modification;
            this.TbIdrmMax.Text = idrmMax?.ToString();
            this.TbUtmMax.Text = (utmMax == null) ? null : string.Format("{0:N2}", utmMax);
            this.TbQrrMax.Text = qrrMax?.ToString();
            this.TbIgtMax.Text = igtMax?.ToString();
            this.TbUgtMax.Text = (ugtMax == null) ? null : string.Format("{0:N2}", ugtMax);
            this.TbTjMax.Text = tjMax?.ToString();
            this.TbPrsmMin.Text = prsmMin?.ToString();
            this.TbCaseType.Text = caseType;
            this.TbUtmCorrection.Text = (utmCorrection == null) ? null : string.Format("{0:N2}", utmCorrection);

            bool? result = this.ShowDialog();

            if (result ?? false)
            {
                //пользователь хочет сохранить сделанные изменения
                string tabNum = ((MainWindow)this.Owner).TabNum;

                //проверяем, что он аутентифицирован в системе
                if (string.IsNullOrEmpty(tabNum))
                {
                    MessageBox.Show(Properties.Resources.PleaseAuthenticate, Application.ResourceAssembly.GetName().Name, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null;
                }

                if (this.CheckData())
                {
                    int editedItav = int.Parse(TbItav.Text);

                    string[] selectedItem = this.CmbDeviceType.SelectedItem as string[];
                    int editedDeviceTypeID = int.Parse(selectedItem[0]);
                    string editedConstructive = this.TbConstructive.Text.Trim();

                    string editedModification = string.IsNullOrEmpty(this.TbModification.Text.Trim()) ? null : this.TbModification.Text.Trim();
                    int? editedIdrmMax = string.IsNullOrEmpty(this.TbIdrmMax.Text.Trim()) ? null : (int?)int.Parse(this.TbIdrmMax.Text.Trim());
                    decimal? editedUtmMax = string.IsNullOrEmpty(this.TbUtmMax.Text.Trim()) ? null : (decimal?)decimal.Parse(this.TbUtmMax.Text.Trim());
                    int? editedQrrMax = string.IsNullOrEmpty(this.TbQrrMax.Text.Trim()) ? null : (int?)int.Parse(this.TbQrrMax.Text.Trim());
                    int? editedIgtMax = string.IsNullOrEmpty(this.TbIgtMax.Text.Trim()) ? null : (int?)int.Parse(this.TbIgtMax.Text.Trim());
                    decimal? editedUgtMax = string.IsNullOrEmpty(this.TbUgtMax.Text.Trim()) ? null : (decimal?)decimal.Parse(this.TbUgtMax.Text.Trim());
                    int? editedTjMax = string.IsNullOrEmpty(this.TbTjMax.Text.Trim()) ? null : (int?)int.Parse(this.TbTjMax.Text.Trim());
                    int? editedPrsmMin = string.IsNullOrEmpty(this.TbPrsmMin.Text.Trim()) ? null : (int?)int.Parse(this.TbPrsmMin.Text.Trim());
                    string editedCaseType = this.TbCaseType.Text.Trim();
                    decimal? editedUtmCorrection = string.IsNullOrEmpty(this.TbUtmCorrection.Text.Trim()) ? null : (decimal?)decimal.Parse(this.TbUtmCorrection.Text.Trim());

                    createdDeviceReferenceID = DbRoutines.SaveToDeviceReferences(deviceReferenceID, editedItav, editedDeviceTypeID, editedConstructive, editedModification, editedIdrmMax, editedUtmMax, editedQrrMax, editedIgtMax, editedUgtMax, editedTjMax, editedPrsmMin, editedCaseType, editedUtmCorrection, tabNum);
                }
            }

            return result;
        }

        private bool CheckData()
        {
            //выполняет проверку введённых пользователем данных с точки зрения возможности выполнения сохранения в базу данных
            if (string.IsNullOrEmpty(TbItav.Text.Trim()) || (!int.TryParse(TbItav.Text, out int itav)))
            {
                MessageBox.Show(string.Concat(Properties.Resources.AverageCurrent, ". ", Properties.Resources.DataWillNotBeSaved), Application.ResourceAssembly.GetName().Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            //проверяем значение типа изделия
            if (this.CmbDeviceType.SelectedItem == null)
            {
                MessageBox.Show(string.Format(Properties.Resources.SubjectNotSetted, Properties.Resources.DeviceType, Properties.Resources.NotSetted), Application.ResourceAssembly.GetName().Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            string[] selectedItem = CmbDeviceType.SelectedItem as string[];
            if (!int.TryParse(selectedItem[0], out int deviceTypeID))
                throw new Exception(string.Format("The '{0}' is not an integer value", selectedItem[0]));

            if (string.IsNullOrEmpty(TbConstructive.Text))
            {
                MessageBox.Show(string.Concat(Properties.Resources.Constructive, ". ", Properties.Resources.DataWillNotBeSaved), Application.ResourceAssembly.GetName().Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            //значение TbModification.Text может быть любым - не делаем его проверку

            //если значение введено - оно должно без ошибки преобразовываться в тип int
            if (!string.IsNullOrEmpty(TbIdrmMax.Text) && !int.TryParse(TbIdrmMax.Text, out int idrmMax))
            {
                MessageBox.Show(string.Concat(Properties.Resources.IdrmMax, ". ", Properties.Resources.DataWillNotBeSaved), Application.ResourceAssembly.GetName().Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!string.IsNullOrEmpty(TbUtmMax.Text) && !decimal.TryParse(TbUtmMax.Text, out decimal utmMax))
            {
                MessageBox.Show(string.Concat(Properties.Resources.UtmMax, ". ", Properties.Resources.DataWillNotBeSaved), Application.ResourceAssembly.GetName().Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!string.IsNullOrEmpty(TbQrrMax.Text) && !int.TryParse(TbQrrMax.Text, out int qrrMax))
            {
                MessageBox.Show(string.Concat(Properties.Resources.QrrMax, ". ", Properties.Resources.DataWillNotBeSaved), Application.ResourceAssembly.GetName().Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!string.IsNullOrEmpty(TbIgtMax.Text) && !int.TryParse(TbIgtMax.Text, out int igtMax))
            {
                MessageBox.Show(string.Concat(Properties.Resources.IgtMax, ". ", Properties.Resources.DataWillNotBeSaved), Application.ResourceAssembly.GetName().Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!string.IsNullOrEmpty(TbUgtMax.Text) && !decimal.TryParse(TbUgtMax.Text, out decimal ugtMax))
            {
                MessageBox.Show(string.Concat(Properties.Resources.UgtMax, ". ", Properties.Resources.DataWillNotBeSaved), Application.ResourceAssembly.GetName().Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!string.IsNullOrEmpty(TbTjMax.Text) && !int.TryParse(TbTjMax.Text, out int tjMax))
            {
                MessageBox.Show(string.Concat(Properties.Resources.TjMax, ". ", Properties.Resources.DataWillNotBeSaved), Application.ResourceAssembly.GetName().Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!string.IsNullOrEmpty(TbPrsmMin.Text) && !int.TryParse(TbPrsmMin.Text, out int prsmMin))
            {
                MessageBox.Show(string.Concat(Properties.Resources.PrsmMin, ". ", Properties.Resources.DataWillNotBeSaved), Application.ResourceAssembly.GetName().Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            //значение TbCaseType.Text может быть любым - не делаем его проверку

            if (!string.IsNullOrEmpty(TbUtmCorrection.Text) && !decimal.TryParse(TbUtmCorrection.Text, out decimal utmCorrection))
            {
                MessageBox.Show(string.Concat(Properties.Resources.UtmCorrection, ". ", Properties.Resources.DataWillNotBeSaved), Application.ResourceAssembly.GetName().Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            return true;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.DialogResult = false;
        }

        private void BtOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.CheckData())
                this.DialogResult = true;
        }
    }
}
