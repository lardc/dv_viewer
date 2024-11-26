using SCME.CustomControls;
using SCME.Types;
using SCME.Types.BaseTestParams;
using SCME.Types.Profiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SCME.dbViewer
{
    /// <summary>
    /// Interaction logic for ManuallyAddDevices.xaml
    /// </summary>
    public partial class ManuallyAddDevices : Window
    {
        //тип изделия, который система вычисляет по коду профиля
        //всякий раз когда вводится код профиля система пересчитывает тип изделия см. реализацию btSelectProfile_KeyUp (по окончанию ввода кода профиля - пользователь жмёт Enter)
        string FDeviceTypeRu = null;

        int? FProfileID = null;
        string FProfileGUID = null;

        int? FDevID = null;

        //идентификатор ПЗ, который выбрал пользователь
        int? FGroupID = null;

        //здесь будем хранить список DataGridSqlResult, которые были созданы для ввода пользователем описания измеренных параметров, которые требуется сохранить в базу данных
        List<DataGridSqlResult> FListOfDataGridWithInformationForSave = new List<DataGridSqlResult>();

        public ManuallyAddDevices(string groupName, int groupID, string profileName, int? profileID, string profileGUID, string deviceCode, int? devID, bool? sapID)
        {
            InitializeComponent();

            //если входные данные не null - запоминаем их
            if ((groupName != null) && (groupName != string.Empty))
            {
                tbGroupName.Text = groupName;
                this.FGroupID = groupID;
            }

            this.FProfileID = profileID;
            if ((profileName != null) && (profileName != string.Empty))
            {
                tbProfileName.Text = profileName;
                this.FProfileID = profileID;
                this.FProfileGUID = profileGUID;
            }

            this.FDevID = devID;
            if ((deviceCode != null) && (deviceCode != string.Empty))
            {
                //проверяем, что обозначение изделия содержит код ПЗ
                int delimeterIndex = deviceCode.LastIndexOf(tbDeviceCodeDelimeter.Text);

                if (delimeterIndex == -1)
                {
                    //в обозначении кода изделия нет ПЗ
                    tbDeviceCode.Text = deviceCode;
                }
                else
                {
                    //в обозначении кода изделия есть ПЗ.  вырезаем из него обозначение кода изделия и пишем его в tbDeviceCode
                    tbDeviceCode.Text = deviceCode.Substring(0, delimeterIndex);

                    //вырезаем из него ПЗ и пишем вырезанное ПЗ в tbDeviceCodePartOfGroupName
                    string codePartOfGroupName = deviceCode.Substring(delimeterIndex + 1);

                    if (codePartOfGroupName != string.Empty)
                        tbDeviceCodePartOfGroupName.Text = codePartOfGroupName;

                    tbDeviceCodePartOfGroupName.Foreground = (tbDeviceCodePartOfGroupName.Text == Properties.Resources.NotSetted) ? Brushes.Red : Brushes.Black;
                }
            }

            //выставляем в cmb_StatusByAssemblyProtocol принятое значение sapID
            int itemIndex = -1;

            if (sapID != null)
            {
                DbRoutines.StatusByAssemblyProtocol item = CommonResources.DataSourceOfStatusByAssemblyProtocol.Where(x => x.SapID == (bool)sapID).FirstOrDefault();
                itemIndex = CommonResources.DataSourceOfStatusByAssemblyProtocol.IndexOf(item);
            }

            this.cmb_StatusByAssemblyProtocol.SelectedIndex = itemIndex;
        }

        private int NewColumnInDataTable(DataTable dt, string columnName)
        {
            //создание нового столбца в dt
            //возвращает индекс созданного столбца
            if (dt == null)
                return -1;

            DataColumn column = new DataColumn(columnName, typeof(string));
            column.Unique = false;
            column.AllowDBNull = true;
            column.AutoIncrement = false;
            column.DefaultValue = null;

            dt.Columns.Add(column);

            return dt.Columns.IndexOf(column);
        }

        private void StoreToRecord(DataRow dataRow, int columnIndex, string columnValue)
        {
            //сохраняет в принятой записи dataRow в столбце с индексом columnIndex значение columnValue
            dataRow[columnIndex] = columnValue;
        }

        private void PutCondition(DataGridSqlResult dg, DataRow dataRowConditionUnitMeasure, DataRow dataRowConditionValues, string conditionName, string conditionValue, string conditionunitMeasure)
        {
            //помещает описание принятого condition в принятые dg и dataRow
            DataTable dt = dg.Tag as DataTable;

            if (dt != null)
            {
                //создаём новый столбец
                int columnIndex = this.NewColumnInDataTable(dt, conditionName);

                this.StoreToRecord(dataRowConditionUnitMeasure, columnIndex, conditionunitMeasure);
                this.StoreToRecord(dataRowConditionValues, columnIndex, conditionValue);
            }
        }

        private string TranslateConditionName(string conditionNameInDataBase, TestParametersType testType)
        {
            //преобразует имя conditionNameInDataBase (имя условия в базе данных) в имя условия понятное пользователю
            //возвращает:
            //            в случае наличия условия conditionNameInDataBase в словаре - имя условия понятное пользователю;
            //            conditionNameInDataBase - в случае отсутствия conditionNameInDataBase в словаре;
            //получаем температурный режим из наименования профиля
            string result = null;

            string sTC = ProfileRoutines.StringTemperatureConditionByProfileName(tbProfileName.Text).ToUpper();
            TemperatureCondition tc;

            //мы считали обозначение температурного режима прямо из обозначения профиля, вполне возможно, что технологи ошиблись в обозначении профиля. нам надо проверить это и скорректировать считанное tc
            //если температурный режим не определёный - будем такие столбцы относить к температурному режиму RT
            if ((!Enum.TryParse(sTC, out tc)) || (!Enum.IsDefined(typeof(TemperatureCondition), sTC)))
                tc = TemperatureCondition.RT;

            //получаем список имён условий, которые пользователь хочет видеть при визуализации теста testType для типа изделия this.FDeviceTypeRu при температурном режиме tc
            //в this.FDeviceTypeRu хранится всегда актуальный тип изделия
            List<string> conditions = Routines.ConditionNamesByDeviceTypeRu(testType, this.FDeviceTypeRu, tc);

            if ((conditions != null) && (conditions.IndexOf(conditionNameInDataBase) != -1))
            {
                //переводим имя условия в понятное пользователю
                result = Dictionaries.ConditionName(tc, conditionNameInDataBase);
            }

            return result;
        }

        private void LoadConditionsAndParametersByProfileID()
        {
            //грузит список условий профиля this.FProfileID
            if (this.FProfileID == null)
                return;

            //удаляем ранее построенную визуализацию
            this.MainStackPanel.Children.Clear();
            this.FListOfDataGridWithInformationForSave.Clear();

            //загружаем все conditions профиля profileID
            string sqlText = string.Format(
                                             "SELECT PTT.PTT_ID, TT.TEST_TYPE_NAME, C.COND_NAME, PC.VALUE" +
                                             " FROM PROF_COND PC WITH(NOLOCK)" +
                                             "  INNER JOIN PROFILES P WITH(NOLOCK) ON (" +
                                             "                                         (P.PROF_ID={0}) AND" +
                                             "                                         (PC.PROF_ID=P.PROF_ID)" +
                                             "                                        )" +
                                             "  INNER JOIN PROF_TEST_TYPE PTT WITH(NOLOCK) ON (PC.PROF_TESTTYPE_ID=PTT.PTT_ID)" +
                                             "  INNER JOIN TEST_TYPE TT WITH(NOLOCK) ON (" +
                                             "                                           (PTT.TEST_TYPE_ID=TT.TEST_TYPE_ID) AND" +
                                             "                                           NOT(TT.TEST_TYPE_NAME='Clamping') AND" +
                                             "                                           NOT(TT.TEST_TYPE_NAME='Commutation')" +
                                             "                                          )" +
                                             "  INNER JOIN CONDITIONS C WITH(NOLOCK) ON (PC.COND_ID=C.COND_ID)" +
                                             " ORDER BY PTT.PTT_ID, PTT.ORD", this.FProfileID
                                          );

            SqlConnection connection = DBConnections.Connection;

            bool connectionOpened = false;

            if (!DbRoutines.IsDBConnectionAlive(connection))
            {
                connection.Open();
                connectionOpened = true;
            }

            try
            {
                SqlCommand command = new SqlCommand(sqlText, connection)
                {
                    CommandTimeout = 1000
                };

                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    bool loop = true;
                    string conditionName = null;
                    string translatedConditionName = null;
                    string conditionValue = null;

                    List<Condition> listOfConditions = new List<Condition>();

                    TestParametersType? currentTestType = null;
                    TestParametersType? previousTestType = null;

                    int? currentProfTestTypeID = null;
                    int? previousProfTestTypeID = null;

                    bool currentTestEnable = false;
                    bool previousTestEnable = false;

                    while (loop)
                    {
                        loop = reader.Read();

                        if (loop)
                        {
                            currentProfTestTypeID = Convert.ToInt32(reader["PTT_ID"]);
                            currentTestType = Routines.StrToTestParametersType(Convert.ToString(reader["TEST_TYPE_NAME"]));
                            conditionName = Convert.ToString(reader["COND_NAME"]).TrimEnd();
                            conditionValue = Convert.ToString(reader["VALUE"]).TrimEnd();

                            //смотрим включен текущий тест или отключен
                            if (conditionName.EndsWith("_En"))
                                currentTestEnable = Convert.ToBoolean(conditionValue);
                        }

                        //если происходит смена теста или это последняя запись теста - определяем строить средства отображения загруженного теста или игнорировать уже полностью просмотренный тест
                        if (((previousProfTestTypeID != null) && (currentProfTestTypeID != previousProfTestTypeID)) || (!loop))
                        {
                            //тест включен - строим его визуальное представление
                            if (loop)
                            {
                                //текущая запись не последняя
                                if ((previousTestEnable) && (previousTestType != null))
                                    this.BuildTest((TestParametersType)previousTestType, (int)previousProfTestTypeID, listOfConditions);
                            }
                            else
                            {
                                //мы стоим на последней записи обрабатываемого набора данных
                                if ((currentTestEnable) && (currentTestType != null))
                                    this.BuildTest((TestParametersType)currentTestType, (int)currentProfTestTypeID, listOfConditions);
                            }

                            //мы построили визуальное представление
                            listOfConditions.Clear();
                        }

                        if (loop)
                        {
                            if (currentTestType != null)
                            {
                                translatedConditionName = this.TranslateConditionName(conditionName, (TestParametersType)currentTestType);

                                //переводим имя условия в понятное пользователю и заодно фильтруем список имён условий - не показываем пользователю условия, которые ему не интересны
                                if (translatedConditionName != null)
                                {
                                    Condition condition = new Condition()
                                    {
                                        Name = translatedConditionName,
                                        Value = conditionValue,
                                        UnitMeasure = Dictionaries.ConditionUnitMeasure(conditionName)
                                    };
                                    listOfConditions.Add(condition);
                                }
                            }

                            previousTestType = currentTestType;
                            previousProfTestTypeID = currentProfTestTypeID;
                            previousTestEnable = currentTestEnable;
                        }
                    }

                    //список условий нам больше не понадобится
                    listOfConditions = null;
                }

                finally
                {
                    reader.Close();
                }
            }

            finally
            {
                //если данная реализация открыла соединение к БД, то она же его должна закрыть
                //если соединение к БД было открыто вызывающей реализацией - не закрываем его в данной реализации
                if (connectionOpened)
                    connection.Close();
            }
        }

        /*
        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private Expander FindExpander(int profTestTypeID, string testTypeName)
        {
            //ищет компонент Expander в this по принятым profTestTypeID и testTypeName
            //если такого нет - возвращает null
            //свойство Tag во всех компонентах Expander визуализирующих тесты хранит значение идентификатора profTestTypeID, а свойство Header хранит наименование теста
            foreach (Expander expander in FindVisualChildren<Expander>(MainStackPanel))
            {
                if ((Convert.ToInt32(expander.Tag) == profTestTypeID) && (expander.Header.ToString() == testTypeName))
                    return expander;
            }

            return null;
        }
        
        private DataGrid FindDataGrid(Expander expander)
        {
            //ищет компонент DataGrid в принятом expander. если он в нём есть - то только в единственном числе
            //если такого нет - возвращает null
            StackPanel sp = expander.Content as StackPanel;

            if (sp != null)
                return sp.Children[0] as DataGrid;

            return null;
        }
        
        private void LoadedEventHandler(object sender, RoutedEventArgs e)
        {
            //красим DataGridColumnHeader
            DataGrid dg = sender as DataGrid;

            if (dg != null)
            {
                Style style = new Style(typeof(System.Windows.Controls.Primitives.DataGridColumnHeader));
                style.Setters.Add(new Setter { Property = BackgroundProperty, Value = Application.Current.Resources["CustomBlue1"] });

                foreach (DataGridColumn column in dg.Columns)
                {
                    column.HeaderStyle = style;
                }
            }
        }
       */

        private string ParameterName(string parameterName, string value, out string formattedValue)
        {
            int iValue;
            bool isDouble;
            double dValue;

            formattedValue = value;

            if (Routines.IsInteger(value, out iValue, out isDouble, out dValue))
            {
                formattedValue = iValue.ToString();
            }
            else
            {
                //принятое value не является целым числом
                if (isDouble)
                {
                    string formatValue = Dictionaries.ParameterFormat(parameterName);
                    formattedValue = dValue.ToString(formatValue, System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            return Dictionaries.ParameterName(parameterName);
        }

        private DataGridTextColumn CreateColumn(DataGridSqlResult dg, string header, string bindPath)
        {
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = header;
            column.Binding = new Binding(bindPath);
            dg.Columns.Add(column);

            return column;
        }

        private void BuildTest(TestParametersType testType, int testTypeID, List<Condition> listOfConditions)
        {
            //строит визуальное представление теста testType который принадлежит профилю с обозначением tbProfileName.Text по описанию из принятого listOfConditions
            Expander expander = new Expander();
            expander.IsExpanded = true;
            expander.Header = testType;
            expander.FontSize = 16;
            expander.FontWeight = FontWeights.Bold;
            expander.Margin = new Thickness(10, 30, 0, 0);
            this.MainStackPanel.Children.Add(expander);

            StackPanel stackPanel = new StackPanel();
            stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            stackPanel.Orientation = Orientation.Vertical;
            expander.Content = stackPanel;

            Label lb;
            DataGridSqlResult dg;

            //если список принятых условий listOfConditions не пуст - визуализируем его содержимое
            if (listOfConditions.Count != 0)
            {
                lb = new Label();
                lb.Margin = new Thickness(9, 0, 0, 0);
                lb.Content = Properties.Resources.ConditionsOfTest;
                lb.FontSize = 12;
                lb.FontWeight = FontWeights.Normal;
                stackPanel.Children.Add(lb);

                dg = new DataGridSqlResult();
                dg.HeadersVisibility = DataGridHeadersVisibility.Column;
                dg.ColumnHeaderHeight = 30;
                dg.Margin = new Thickness(9, 0, 0, 0);
                dg.HorizontalAlignment = HorizontalAlignment.Left;

                DataTable dt = new DataTable();
                dg.Tag = dt;
                dg.FontWeight = FontWeights.Normal;

                stackPanel.Children.Add(dg);

                //размещаем список условий из принятого listOfConditions в dg
                DataRow dataRowConditionUnitMeasure = dt.NewRow();
                DataRow dataRowConditionValues = dt.NewRow();

                try
                {
                    dataRowConditionUnitMeasure.BeginEdit();

                    try
                    {
                        dataRowConditionValues.BeginEdit();

                        try
                        {
                            foreach (Condition condition in listOfConditions)
                            {
                                if (condition.Name != null)
                                {
                                    this.PutCondition(dg, dataRowConditionUnitMeasure, dataRowConditionValues, condition.Name, condition.Value, condition.UnitMeasure);
                                    this.CreateColumn(dg, condition.Name, condition.Name);
                                }
                            }
                        }
                        finally
                        {
                            dataRowConditionValues.EndEdit();
                        }
                    }
                    finally
                    {
                        dataRowConditionUnitMeasure.EndEdit();
                    }
                }
                finally
                {
                    dt.Rows.Add(dataRowConditionUnitMeasure);
                    dt.Rows.Add(dataRowConditionValues);
                }

                //устанавливаем созданному DataGrid источник данных
                dg.ItemsSource = dt.DefaultView;
            }

            //создаём интерфейс ручного ввода значений измеренных параметров
            lb = new Label();
            lb.Margin = new Thickness(9, 10, 0, 0);
            lb.Content = string.Concat(Properties.Resources.DeviceParameters, " \"", tbDeviceCode.Text, tbDeviceCodeDelimeter.Text, tbDeviceCodePartOfGroupName.Text, "\"");
            lb.FontSize = 12;
            lb.FontWeight = FontWeights.Normal;
            stackPanel.Children.Add(lb);

            //создаём и запоминаем в списке this.FListOfDataGridWithInformationForSave ссылку на созданный DataGridSqlResult чтобы в последсвии их перебрать и сохранить введённую в них информацию в базу данных
            dg = new DataGridSqlResult();
            this.FListOfDataGridWithInformationForSave.Add(dg);
            dg.Name = string.Concat("dg", testTypeID.ToString());
            dg.IsReadOnly = false;
            dg.HeadersVisibility = DataGridHeadersVisibility.Column;
            dg.ColumnHeaderHeight = 30;
            dg.Margin = new Thickness(9, 0, 0, 0);
            dg.HorizontalAlignment = HorizontalAlignment.Left;
            dg.CanUserAddRows = true;
            dg.FontWeight = FontWeights.Normal;
            DbRoutines.CollectionOfMeasuredParameter itemSource = new DbRoutines.CollectionOfMeasuredParameter(testTypeID);

            if (this.FDevID != null)
                itemSource.Load((int)this.FDevID); //, this.ParameterName

            dg.ItemsSource = itemSource;
            dg.InitializingNewItem += DataGridInitializingNewItem;

            //для возможности удаления текущей записи
            dg.KeyDown += DataGrid_KeyDown;

            //столбец наименования параметра
            DataGridComboBoxColumn cmbColumn = new DataGridComboBoxColumn();
            Style cmbStyle = new Style(typeof(ComboBox));
            cmbStyle.Setters.Add(new EventSetter(ComboBox.SelectionChangedEvent, new SelectionChangedEventHandler(SelectionParameterChanged)));
            cmbStyle.Setters.Add(new Setter(ComboBox.TagProperty, dg));
            cmbColumn.EditingElementStyle = cmbStyle;

            //формируем список параметров именно для данного теста, параметры для других тестов выкидываем
            List<string> wishList = Routines.MeasuredParametersByTestType(testType);
            List<DbRoutines.Parameter> source = this.CollectionOfParameters.ToList();

            //если wishList=null - значит нет ограничения на отображение параметров данного теста - надо показывать их все
            if (wishList != null)
            {
                var onlyParamNames = this.CollectionOfParameters.Select(x => x.ParamName).Intersect(wishList.Select(x => x));
                source = this.CollectionOfParameters.Where(x => onlyParamNames.Contains(x.ParamName)).ToList();
            }

            foreach (DbRoutines.Parameter param in source)
                param.ParamName = Dictionaries.ParameterName(param.ParamName);

            cmbColumn.ItemsSource = source;

            //ObservableCollection<DbRoutines.Parameter> source = this.CollectionOfParameters.Select().Intersect(wishList);
            //cmbColumn.ItemsSource = source;
            //cmbColumn.ItemsSource = this.CollectionOfParameters;

            cmbColumn.DisplayMemberPath = "ParamName";
            cmbColumn.SelectedValueBinding = new Binding("ParamID");
            cmbColumn.SelectedValuePath = "ParamID";
            cmbColumn.Header = Properties.Resources.Name;
            dg.Columns.Add(cmbColumn);

            //столбец единицы измерения параметра устанавливается автоматически при выборе параметра
            DataGridTextColumn column = new DataGridTextColumn();
            column.IsReadOnly = true;
            column.Header = Properties.Resources.Um;
            column.Binding = new Binding("ParamUm");
            dg.Columns.Add(column);

            //столбец значения параметра вводится пользователем
            //включаем выравнивание данных этого столбца по правому краю
            column = new DataGridTextColumn();
            Style tbStyle = new Style();
            tbStyle.Setters.Add(new Setter(TextBox.TextAlignmentProperty, TextAlignment.Right));
            column.CellStyle = tbStyle;
            column.Header = Properties.Resources.Value;
            column.Binding = new Binding("ParamValue");
            //уставнавливаем формат - два знака после запятой
            column.Binding.StringFormat = "N2";
            dg.Columns.Add(column);

            stackPanel.Children.Add(dg);
        }

        private void SelectionParameterChanged(object sender, SelectionChangedEventArgs e)
        {
            //при выборе параметра из выпадающего списка будем:
            //   - запрещать выбор параметров, которые уже есть в тесте - нет смысла в повторном использовании параметра в одном и том же тесте
            //   - автоматически писать в выбранной строке DataGrid единицу измерения выбранного параметра
            ComboBox cmb = sender as ComboBox;

            if ((cmb != null) && (cmb.SelectedItem != null))
            {
                DbRoutines.Parameter param = cmb.SelectedItem as DbRoutines.Parameter;

                if (param != null)
                {
                    DataGridSqlResult dg = cmb.Tag as DataGridSqlResult;

                    if (dg != null)
                    {
                        ObservableCollection<DbRoutines.MeasuredParameter> itemsSource = dg.ItemsSource as ObservableCollection<DbRoutines.MeasuredParameter>;

                        if (itemsSource != null)
                        {
                            DbRoutines.MeasuredParameter item = dg.CurrentItem as DbRoutines.MeasuredParameter;

                            if ((itemsSource.Where(i => i.ParamID == param.ParamID)).Count() != 0)
                            {
                                //запрещаем выбирать параметр, т.к. его значение уже определено
                                cmb.SelectedIndex = -1;
                                item.ParamUm = null;
                                item.ParamValue = null;

                                return;
                            }

                            //пишем единицу измерения                            
                            item.ParamUm = param.ParamUm;
                        }
                    }
                }
            }

        }

        private void PrepareConditionsAndParameters()
        {
            this.PrepareStatusesByAssemblyProtocol();

            if (this.FProfileID != null)
            {
                //пользователь закончил ввод обозначения профиля - вычисляем тип изделия
                this.FDeviceTypeRu = DbRoutines.DeviceTypeRuByProfileName(tbProfileName.Text);

                //загружаем список параметров в this.CollectionOfParameters из базы данных как есть
                //список имён параметров при этом никак не усекаем - читаем все параметры, которые есть в базе данных
                DbRoutines.LoadParameters(this.CollectionOfParameters); //Dictionaries.ParameterName

                //строим визуальное представление Conditions по идентификатору профиля
                this.LoadConditionsAndParametersByProfileID();
            }
        }

        public bool? ShowModal()
        {
            //демонстрация формы для создания изделий (вручную)
            this.PrepareConditionsAndParameters();

            bool? result = this.ShowDialog();

            return result;
        }

        private class Condition : object
        {
            private string FName;
            public string Name
            {
                get { return FName; }
                set { FName = value; }
            }

            private string FValue;
            public string Value
            {
                get { return FValue; }
                set { FValue = value; }
            }

            private string FUnitMeasure;
            public string UnitMeasure
            {
                get { return FUnitMeasure; }
                set { FUnitMeasure = value; }
            }
        }

        private void CustomTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //меняем значение свойства Text на NotSetted если оно стало равным пустой строке
            //делаем шрифт красным если значение свойства Text равно NotSetted и чёрным если оно не равно NotSetted
            TextBox tb = sender as TextBox;

            if (tb != null)
            {
                if (tb.Text.Trim() == string.Empty)
                    tb.Text = Properties.Resources.NotSetted;

                tb.Foreground = (tb.Text == Properties.Resources.NotSetted) ? Brushes.Red : Brushes.Black;
            }
        }

        private void DataGridInitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            //реализация обработки события инициализации новой записи в DataGrid
            //данное событие наступает сразу при втыкании курсора в поле доступное для ввода (единица измерения не может быть введена пользователем - она доступна ему только для чтения)
            //при этом создаётся новая запись в dg.ItemsSource - фиксируем владельца только что созданной записи
            DbRoutines.MeasuredParameter newItem = e.NewItem as DbRoutines.MeasuredParameter;

            if (newItem != null)
            {
                DataGridSqlResult dg = sender as DataGridSqlResult;

                if (dg != null)
                {
                    DbRoutines.CollectionOfMeasuredParameter itemSource = dg.ItemsSource as DbRoutines.CollectionOfMeasuredParameter;

                    if (itemSource != null)
                        newItem.Owner = itemSource;
                }
            }
        }

        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            //при одновременном нажатии любого Alt и Delete на клавиатуре будем метить текущую запись в списке измеренных параметров как удалённую запись
            DataGridSqlResult dg = sender as DataGridSqlResult;

            if ((e.SystemKey == Key.Delete) && (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)))
            {
                if (dg != null)
                {
                    IList<DataGridCellInfo> selCells = dg.SelectedCells;

                    if (selCells != null)
                    {
                        DbRoutines.MeasuredParameter parameter = selCells[0].Item as DbRoutines.MeasuredParameter;

                        //помечаем параметр как удалённый (ParamID=0 и DevParamID не null)
                        parameter.ParamID = 0;
                        parameter.ParamName = Properties.Resources.Deleted;
                        parameter.ParamValue = null;
                        parameter.ParamUm = Properties.Resources.Deleted;
                    }
                }
            }
        }

        private string SelectGroupName(string groupName, out int? selectedGroupID)
        {
            //выбор ПЗ из списка всех ПЗ, зарегистрированных в справочнике SyteLine со статусом 'C' и префиксом '8-'
            //если принятый groupName не null - устанавливает курсор на строку с groupName
            SelectJob selectJob = new SelectJob();
            selectJob.Owner = this;

            string selectedGroupName = groupName;

            if (selectJob.ShowModal("4", ref selectedGroupName, out selectedGroupID, out string selectedItem))
                return selectedGroupName;

            //пользователь не выбрал ПЗ
            selectedGroupID = null;
            return null;
        }

        private void btSelectGroupName_Click(object sender, RoutedEventArgs e)
        {
            int? selectGroupID;
            string selectGroupName = this.SelectGroupName(tbGroupName.Text, out selectGroupID);

            if (selectGroupName != null)
            {
                this.FGroupID = selectGroupID;
                tbGroupName.Text = selectGroupName;
            }
        }

        private void btSelectProfile_Click(object sender, RoutedEventArgs e)
        {
            //выбор профиля из списка всех профилей
            SelectProfile selectProfile = new SelectProfile();
            selectProfile.Owner = this;

            int? selectedProfileID = this.FProfileID;

            if (selectProfile.ShowModal(Properties.Resources.SelectProfile, ref selectedProfileID, out string selectedProfile, out string selectedProfileGUID))
            {
                this.FProfileID = selectedProfileID;
                tbProfileName.Text = selectedProfile;
                this.FProfileGUID = selectedProfileGUID;

                //перестраиваем список условий и параметров
                this.PrepareConditionsAndParameters();
            }
        }

        private string DeviceCode()
        {
            return string.Concat(tbDeviceCode.Text, tbDeviceCodeDelimeter.Text, tbDeviceCodePartOfGroupName.Text);
        }

        private void btSelectDeviceCodePartOfGroupName_Click(object sender, RoutedEventArgs e)
        {
            //выбираем обозначение ПЗ из списка
            //в большинстве случаев код изделия должен содержать ПЗ по которому он обрабатывается. поэтому облегчим участь пользователя
            string groupName = ((tbDeviceCodePartOfGroupName.Text == null) || (tbDeviceCodePartOfGroupName.Text == string.Empty) || (tbDeviceCodePartOfGroupName.Text == Properties.Resources.NotSetted)) ? tbGroupName.Text : tbDeviceCodePartOfGroupName.Text;

            int? selectedGroupID;
            string selectedGroupName = this.SelectGroupName(groupName, out selectedGroupID);

            if (selectedGroupName != null)
            {
                int delimeterIndex = selectedGroupName.LastIndexOf("-");

                //выбрасываем суффикс если он есть
                tbDeviceCodePartOfGroupName.Text = (delimeterIndex == -1) ? selectedGroupName : selectedGroupName.Substring(0, delimeterIndex);

                //мы сформировали код изделия. вычисление его идентификатора выполняется в реализации сохранения данных, кторые ввёл пользователь в данную форму
                //string deviceCode = this.DeviceCode();
                //this.FDevID = DbRoutines.DevIDByDeviceCode(deviceCode);
            }

            if ((tbDeviceCodePartOfGroupName.Text == null) || (tbDeviceCodePartOfGroupName.Text == string.Empty))
                tbDeviceCodePartOfGroupName.Text = Properties.Resources.NotSetted;

            tbDeviceCodePartOfGroupName.Foreground = (tbDeviceCodePartOfGroupName.Text == Properties.Resources.NotSetted) ? Brushes.Red : Brushes.Black;
        }

        private string CurrentDeviceCode()
        {
            //возвращает обозначение создаваемого средствами данной формы изделия
            return string.Concat(tbDeviceCode.Text, tbDeviceCodeDelimeter.Text, tbDeviceCodePartOfGroupName.Text);
        }

        private void tbDeviceCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //разрешаем вводить только цифры
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void tbProfileName_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    //перестраиваем список условий и параметров
                    this.PrepareConditionsAndParameters();
                    e.Handled = true;
                    break;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    e.Handled = true;
                    this.DialogResult = false;
                    break;

                case Key.F5:
                    e.Handled = true;
                    this.PrepareConditionsAndParameters();
                    break;
            }
        }

        //источник данных для интерфейса ввода пользователем информации об измеренном параметре
        private ObservableCollection<DbRoutines.Parameter> FCollectionOfParameters = null;
        public ObservableCollection<DbRoutines.Parameter> CollectionOfParameters
        {
            get
            {
                if (this.FCollectionOfParameters == null)
                    this.FCollectionOfParameters = new ObservableCollection<DbRoutines.Parameter>();

                return this.FCollectionOfParameters;
            }
        }

        private void SaveMeasuredParametersToDB()
        {
            //пробегает по списку this.FListOfDataGridWithInformationForSave и выполняет сохранение описания измеряемых параметров в базу данных
            if (this.FListOfDataGridWithInformationForSave != null)
            {
                foreach (DataGridSqlResult dt in this.FListOfDataGridWithInformationForSave)
                {
                    //если что-то не было переписано в dt.ItemsSource - то сделаем это
                    dt.CommitEdit();

                    DbRoutines.CollectionOfMeasuredParameter itemSource = dt.ItemsSource as DbRoutines.CollectionOfMeasuredParameter;

                    if (itemSource != null)
                    {
                        foreach (DbRoutines.MeasuredParameter param in itemSource)
                        {
                            param.SaveToDB((int)this.FDevID, itemSource.TestTypeID);
                        }
                    }
                }
            }
        }

        private void btSaveParameters_Click(object sender, RoutedEventArgs e)
        {
            /*
            string groupName = tbGroupName.Text.Trim();

            if ((this.FGroupID == null) && ((groupName != string.Empty) || (groupName != Properties.Resources.NotSetted)))
            {
                //ПЗ выбран из списка ПЗ, существующих в SL, но в базе данных КИП СПП такое ПЗ отсутствует - создаём его
                this.FGroupID = DbRoutines.InsertToGroups(groupName);
            }
            else
            {
                if ((groupName == string.Empty) || (groupName == Properties.Resources.NotSetted))
                {
                    //ПЗ не выбрано пользователем, либо его обозначение не корректно - ругаемся и прекращаем исполнение данной реализации
                    MessageBox.Show(string.Concat(Properties.Resources.GroupName, ". ", Properties.Resources.ValueIsNotGood), string.Concat(Properties.Resources.CheckValue, " ", Properties.Resources.GroupName), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }

            if (this.FProfileGUID == null)
            {
                //профиль не выбран пользователем, либо его обозначение не корректно - ругаемся и прекращаем исполнение данной реализации
                MessageBox.Show(string.Concat(Properties.Resources.Profile, ". ", Properties.Resources.ValueIsNotGood), string.Concat(Properties.Resources.CheckValue, " ", Properties.Resources.Profile), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            //не ограничиваем пользователя в написании кода изделия - у него всегда будет возможность его исправить после создания с не правильным кодом
            string deviceCode = this.DeviceCode().Trim();

            //проверяем, что пользователь написал код изделия не длиннее, чем можно сохранить в базе данных
            if (deviceCode.Length > 64)
            {
                MessageBox.Show(string.Concat(Properties.Resources.Code, ". ", Properties.Resources.AllowedNumberOfCharacters, "64"), string.Concat(Properties.Resources.CheckValue, " ", Properties.Resources.Code), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            //получаем значение для поля USR
            long userID = ((MainWindow)this.Owner).FUserID;
            DbRoutines.FullUserNameByUserID(userID, out string fullUserName);

            bool? sapID = null;
            if (cmb_StatusByAssemblyProtocol.SelectedItem is DbRoutines.StatusByAssemblyProtocol statusByAssemblyProtocol)
                sapID = statusByAssemblyProtocol.SapID;

            //проверяем наличие изделия с обозначением this.DeviceCode() в базе дынных           
            this.FDevID = DbRoutines.DevIDByDeviceCode(deviceCode);

            if (this.FDevID == null)
            {
                //изделие с сформированным кодом отсутствует в базе данных - создаём его
                this.FDevID = DbRoutines.CreateDevID(deviceCode, (int)this.FGroupID, this.FProfileGUID, fullUserName);
            }
            else
            {
                //изделие с сформированным кодом существует в базе данных
                DbRoutines.UpdateDevices((int)this.FDevID, (int)this.FGroupID, this.FProfileGUID, deviceCode, fullUserName, sapID);
            }

            //сохраняем изменённые и созданные параметры изделия в базу данных
            this.SaveMeasuredParametersToDB();

            //перечитываем данные из базы данных чтобы показать пользователю результат сохранения в базу данных
            this.PrepareConditionsAndParameters();
            */
        }

        private void cmb_StatusByAssemblyProtocol_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Delete) || (e.Key == Key.Back))
            {
                ComboBox cmb = sender as ComboBox;

                if (cmb != null)
                    cmb.SelectedItem = null;
            }
        }

        private void PrepareStatusesByAssemblyProtocol()
        {
            //на момент вызова CommonResources.DataSourceOfStatusByAssemblyProtocol точно должен быть создан и загружен данными
            cmb_StatusByAssemblyProtocol.ItemsSource = CommonResources.DataSourceOfStatusByAssemblyProtocol;
            cmb_StatusByAssemblyProtocol.DisplayMemberPath = "Descr";
        }
    }


}
